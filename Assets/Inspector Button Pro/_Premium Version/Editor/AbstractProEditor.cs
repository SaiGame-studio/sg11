using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
//using DefaultNamespace;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using com.cyborgAssets.inspectorButtonPro;
using com.cyborgAssets.internalIBP;
using com.cyborgAssets.internalIBPEditor;

namespace com.cyborgAssets.internalIBPProPrem
{
    public abstract class AbstractProEditor : UnityEditor.Editor
    {
        private const string serializedMethods = "serializedMethods";

        private SerializedProperty serializedMethodsProp;
        private readonly List<string> unsupportedArgs = new List<string>();

        protected abstract bool AllowSceneObjects { get; }

        private MethodInfo[] GetMethods(Type type)
        {
            var allMethods = type.GetMethods(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

            return Array.FindAll(
                allMethods,
                x => x
                    .GetCustomAttributes(typeof(IButtonAttribute), true)
                    .Any());
        }

        private Type GetTargetType()
        {
            var target = serializedObject.targetObject;
            var type = target.GetType();

            return type;
        }

        private void OnEnable()
        {
            //if (serializedObject == null)
            //this solves a unity bug that shouldn't happen I believe
            //as Unity should auto assigns the target of the editor
            if (target == null)
                return;


            serializedObject.Update();
            serializedMethodsProp = serializedObject.FindProperty(serializedMethods);

            var type = GetTargetType();
            var methodInfos = GetMethods(type);

            //delete unused serialized values ( I think )
            var k = 0;
            while (k < serializedMethodsProp.arraySize)
            {
                var serializedMethodProp = serializedMethodsProp.GetArrayElementAtIndex(k);
                var signature = serializedMethodProp
                    .FindPropertyRelative(SerializedMethod.SignatureProp)
                    .stringValue;

                if (!Array.Exists(methodInfos, x => x.ToString() == signature))
                    serializedMethodsProp.DeleteArrayElementAtIndex(k);
                else
                    k++;
            }

            //loop all method infos
            int methodIndex = -1;
            foreach (var methodInfo in methodInfos)
            {
                methodIndex++;

                var signature = methodInfo.ToString();
                var shouldBeAdded = true;
                //check the method
                //and if it's not serialized (existing signatures don't match )
                //then they should be added
                for (var i = 0; i < serializedMethodsProp.arraySize; i++)
                {
                    var serializedMethodProp = serializedMethodsProp.GetArrayElementAtIndex(i);
                    var existingSignature = serializedMethodProp
                        .FindPropertyRelative(SerializedMethod.SignatureProp)
                        .stringValue;

                    //if no existing (serialized) signature match this signature
                    if (!existingSignature.Equals(signature))
                        continue;

                    //if any existing ( serialized ) signature matches, then
                    //this method params shouldn't be added
                    shouldBeAdded = false;
                    break;
                }

                if (shouldBeAdded)
                {
                    //var newSize = ++serializedMethodsProp.arraySize;
                    //var serializedMethodProp = serializedMethodsProp.GetArrayElementAtIndex(newSize - 1);

                    //create empty element in the serialized methods prop
                    serializedMethodsProp.InsertArrayElementAtIndex(methodIndex);
                    //get the empty element
                    var serializedMethodProp = serializedMethodsProp.GetArrayElementAtIndex(methodIndex);

                    serializedMethodProp.FindPropertyRelative(SerializedMethod.NameProp).stringValue = methodInfo.Name;
                    serializedMethodProp.FindPropertyRelative(SerializedMethod.SignatureProp).stringValue = signature;

                    //serializedMethodProp.FindPropertyRelative(SerializedMethod.ParametersProp).DeleteCommand();
                    var serializedParametersProp =
                        serializedMethodProp.FindPropertyRelative(SerializedMethod.ParametersProp);
                    //serializedParametersProp.Reset();
                    var parameterInfos = methodInfo.GetParameters();

                    //bug fix: this clears the parameters array when we are first adding the serialized method
                    //ensuring it doens't start with ghost values that it gets 
                    //from the method below ( we still don't know why it got the 
                    //parameter values from the method below )
                    serializedParametersProp.ClearArray();
                    serializedParametersProp.arraySize = parameterInfos.Length;
                    serializedObject.ApplyModifiedProperties();
                    var j = 0;

                    //serialize each method parameters
                    foreach (var parameterInfo in parameterInfos)
                    {
                        var serializedParameterProp = serializedParametersProp.GetArrayElementAtIndex(j);
                        serializedParameterProp
                            .FindPropertyRelative(SerializedParameter.NameProp)
                            .stringValue = parameterInfo.Name;

                        var unsupported = false;
                        try
                        {
                            serializedParameterProp.SetSerializedParameterType(parameterInfo.ParameterType);
                        }
                        catch (ArgumentException)
                        {
                            unsupported = true;
                        }

                        serializedParameterProp
                            .FindPropertyRelative(SerializedParameter.UnsupportedProp)
                            .boolValue = unsupported;

                        j++;
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawSerializedParameter(SerializedProperty serializedParameter)
        {
            var subProperty = serializedParameter.GetSerializedParameterValueProperty();
            var nameProperty = serializedParameter.FindPropertyRelative(SerializedParameter.NameProp);
            var parameterType = (SerializedParameterType)serializedParameter
                .FindPropertyRelative(SerializedParameter.TypeProp)
                .enumValueIndex;

            var isObjectRef = parameterType == SerializedParameterType.ObjectReference;
            var isEnum = parameterType == SerializedParameterType.Enum;

            if (isObjectRef || isEnum)
            {
                var objectTypeName = serializedParameter
                    .FindPropertyRelative(SerializedParameter.RealTypeProp)
                    .stringValue;
                var objectType = Type.GetType(objectTypeName);

                EditorGUI.showMixedValue = serializedObject.targetObjects.Length > 1;
                EditorGUI.BeginChangeCheck();

                Object newObject = null;
                int newEnum = 0;

                if (isObjectRef)
                {
                    newObject = EditorGUILayout.ObjectField(
                        nameProperty.stringValue,
                        subProperty.objectReferenceValue,
                        objectType,
                        AllowSceneObjects);
                }
                else
                {
                    var names = Enum.GetNames(objectType);
                    newEnum = EditorGUILayout.Popup(nameProperty.stringValue, subProperty.intValue, names);
                }

                if (EditorGUI.EndChangeCheck())
                    if (isObjectRef)
                        subProperty.objectReferenceValue = newObject;
                    else
                        subProperty.intValue = newEnum;
                EditorGUI.showMixedValue = false;
            }
            else if (parameterType == SerializedParameterType.Quaternion)
            {
                subProperty.quaternionValue = EditorGUILayout
                    .Vector4Field(
                        nameProperty.stringValue,
                        subProperty.quaternionValue.ToVector4())
                    .ToQuaternion();
            }
            else
            {
                EditorGUILayout.PropertyField(
                    subProperty,
                    new GUIContent(nameProperty.stringValue));
            }
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            serializedObject.Update();

            var type = GetTargetType();
            var methodInfos = GetMethods(type);

            for (int i = 0; i < serializedMethodsProp.arraySize; i++)
            {
                EditorGUILayout.BeginVertical("Box");

                var serializedMethod = serializedMethodsProp.GetArrayElementAtIndex(i);
                var serializedMethodName = serializedMethod
                    .FindPropertyRelative(SerializedMethod.NameProp)
                    .stringValue;
                var serializedMethodSignature = serializedMethod
                    .FindPropertyRelative(SerializedMethod.SignatureProp)
                    .stringValue;
                var serializedParameters = serializedMethod.FindPropertyRelative(SerializedMethod.ParametersProp);

                for (int j = 0; j < serializedParameters.arraySize; j++)
                {
                    var serializedParameter = serializedParameters.GetArrayElementAtIndex(j);
                    if (serializedParameter.FindPropertyRelative(SerializedParameter.UnsupportedProp).boolValue)
                        unsupportedArgs.Add(
                            serializedParameter.FindPropertyRelative(SerializedParameter.NameProp).stringValue);
                }

                string error = null;
                if (unsupportedArgs.Count > 0)
                    error = "These parameters can't be serialized: " + string.Join(", ", unsupportedArgs.ToArray());

                unsupportedArgs.Clear();

                if (string.IsNullOrEmpty(error))
                {
                    for (int j = 0; j < serializedParameters.arraySize; j++)
                    {
                        DrawSerializedParameter(serializedParameters.GetArrayElementAtIndex(j));
                    }
                }
                else
                {
                    EditorGUILayout.HelpBox(error, MessageType.Error);
                }

                var methodInfo = Array.Find(methodInfos, x => x.ToString() == serializedMethodSignature);
                var attribute = methodInfo.GetCustomAttributes(typeof(IButtonAttribute), true)[0] as IButtonAttribute;
                var checkResult = attribute.PerformCheck(serializedObject.targetObject);

                var rect = EditorGUILayout.GetControlRect();
                var currentColor = GUI.color;
                GUI.color = checkResult ? currentColor : Color.red;
                var buttonPressed = GUI.Button(rect, serializedMethodName);
                GUI.color = currentColor;

                if (string.IsNullOrEmpty(error) && !checkResult)
                    error = attribute.Error;

                if (buttonPressed)
                {
                    //AbstractEditor.AssemblyUpdate();

                    if (string.IsNullOrEmpty(error))
                    {
                        foreach (var targetObject in serializedObject.targetObjects)
                        {
                            var targetSerializedMethodsProp = new SerializedObject(targetObject)
                                .FindProperty(serializedMethods);

                            for (var k = 0; k < targetSerializedMethodsProp.arraySize; k++)
                            {
                                var targetSerializedMethodProp =
                                    targetSerializedMethodsProp.GetArrayElementAtIndex(k);
                                var targetSerializedMethodSignature = targetSerializedMethodProp
                                    .FindPropertyRelative(SerializedMethod.SignatureProp)
                                    .stringValue;
                                if (targetSerializedMethodSignature != serializedMethodSignature)
                                    continue;

                                var targetSerializedParameters =
                                    targetSerializedMethodProp.FindPropertyRelative(
                                        SerializedMethod.ParametersProp);
                                var paramValues = new object[targetSerializedParameters.arraySize];
                                for (var j = 0; j < targetSerializedParameters.arraySize; j++)
                                {
                                    paramValues[j] = targetSerializedParameters
                                        .GetArrayElementAtIndex(j)
                                        .ExtractSerializedParameterValue();
                                }

                                object methodInvokationResult = methodInfo.Invoke(targetObject, paramValues);

                                if (methodInfo.ReturnType != typeof(void))
                                {
                                    //Debug.Log(DateTime.Now.ToShortTimeString() + "** " + methodInvokationResult);
                                    //Debug.Log("**" + methodInvokationResult+"**");
                                    //string returnResult = "null";
                                    //if (methodInvokationResult != null)
                                    //    returnResult = methodInvokationResult.ToString();
                                    //Debug.Log(returnResult + " -- " + methodInfo.Name);
                                    AbstractEditor.PrintReturnResult(" -- ", methodInvokationResult, methodInfo.Name);
                                }

                                break;
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError(error);
                    }
                }

                EditorGUILayout.EndVertical();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
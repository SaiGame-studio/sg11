using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using com.cyborgAssets.inspectorButtonPro;

namespace com.cyborgAssets.internalIBPEditor
{
    public abstract class AbstractEditor : UnityEditor.Editor
    {
        struct ParameterValue
        {
            public readonly ParameterInfo ParameterInfo;
            public readonly object Value;

            public ParameterValue(ParameterInfo parameterInfo, object value)
            {
                ParameterInfo = parameterInfo;
                Value = value;
            }
        }

        private static Dictionary<int, Dictionary<MethodInfo, ParameterValue[]>> cache =
            new Dictionary<int, Dictionary<MethodInfo, ParameterValue[]>>();

        protected abstract bool AllowSceneObjects { get; }

        private MethodInfo[] GetMethods(Type type)
        {
            return type.GetMethods(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
        }

        private Type GetTargetType()
        {
            var target = serializedObject.targetObject;
            var type = GetTargetType(target);
            return type;
        }

        private Type GetTargetType(UnityEngine.Object targetObject)
        {
            //var target = serializedObject.targetObject;
            var target = targetObject;
            var type = target.GetType();

            return type;
        }

        private Dictionary<MethodInfo, ParameterValue[]> methodsParams =
            new Dictionary<MethodInfo, ParameterValue[]>();

        private void OnEnable()
        {
            if (target == null)
                return;
            if (serializedObject == null)
                return;
            if (serializedObject.targetObject == null)
                return;
            var type = GetTargetType();
            var objectID = serializedObject.targetObject.GetInstanceID();

            if (cache.ContainsKey(objectID))
            {
                methodsParams = cache[objectID];
                return;
            }

            methodsParams = new Dictionary<MethodInfo, ParameterValue[]>();
            foreach (var methodInfo in GetMethods(type))
            {
                var attributes = methodInfo.GetCustomAttributes(typeof(IButtonAttribute), true);
                if (!attributes.Any())
                    continue;

                var parameterInfos = methodInfo.GetParameters();
                var paramValues = new ParameterValue[parameterInfos.Length];
                for (int i = 0; i < parameterInfos.Length; i++)
                {
                    paramValues[i] = new ParameterValue(
                        parameterInfos[i],
                        parameterInfos[i].ParameterType.GetDefaultValue());
                }

                methodsParams.Add(methodInfo, paramValues);
            }

            cache.Add(objectID, methodsParams);
        }

        //static bool b = false;
        //static int c;






        private void TryAddObjectToCacheMine(UnityEngine.Object targetObjectMine)
        {
            var type = GetTargetType(targetObjectMine);
            var objectID = targetObjectMine.GetInstanceID();



            if (cache.ContainsKey(objectID))
            {
                methodsParams = cache[objectID];
                return;
            }

            methodsParams = new Dictionary<MethodInfo, ParameterValue[]>();
            foreach (var methodInfo in GetMethods(type))
            {
                var attributes = methodInfo.GetCustomAttributes(typeof(IButtonAttribute), true);
                if (!attributes.Any())
                    continue;

                var parameterInfos = methodInfo.GetParameters();
                var paramValues = new ParameterValue[parameterInfos.Length];
                for (int i = 0; i < parameterInfos.Length; i++)
                {
                    paramValues[i] = new ParameterValue(
                        parameterInfos[i],
                        parameterInfos[i].ParameterType.GetDefaultValue());
                }

                methodsParams.Add(methodInfo, paramValues);
            }

            cache.Add(objectID, methodsParams);
        }

        //[UnityEditor.Callbacks.DidReloadScripts]
        //private static void OnScriptsReloaded()
        //{
        //    var a1 = TypeCache.GetMethodsWithAttribute(typeof(ProButtonAttribute));
        //    var a2 = TypeCache.GetMethodsWithAttribute(typeof(ProPlayButtonAttribute));

        //    int count = a1.Count + a2.Count;
        //    int finalTarget = 33;
        //    int baseNumber = 6;
        //    //if (count >= 73)
        //    //{
        //    //    target = 99;
        //    //    baseNumber = 73;
        //    //}
        //    c = count - baseNumber;


        //    if (count > finalTarget)
        //    {
        //        b = true;
        //    }
        //    else
        //    {
        //        b = false;
        //    }
        //    AssemblyUpdate();
        //}


        private bool IsTypeSerializable(Type type)
        {
            return type == typeof(int) ||
                   type == typeof(bool) ||
                   type == typeof(float) ||
                   type == typeof(string) ||
                   type == typeof(Color) ||
                   type == typeof(Object) ||
                   type.IsSubclassOf(typeof(Object)) ||
                   type.IsEnum ||
                   type == typeof(Vector2) ||
                   type == typeof(Vector3) ||
                   type == typeof(Vector4) ||
                   type == typeof(Rect) ||
                   type == typeof(AnimationCurve) ||
                   type == typeof(Bounds) ||
                   type == typeof(Quaternion) ||
                   type == typeof(Vector2Int) ||
                   type == typeof(Vector3Int) ||
                   type == typeof(RectInt) ||
                   type == typeof(BoundsInt);
            ;
        }

        //private static void AssemblyLog10()
        //{
        //    Inspector_Update();
        //}

        private bool DrawParameter(ParameterValue parameterValue, out object value)
        {
            var type = parameterValue.ParameterInfo.ParameterType;
            if (type == typeof(int))
            {
                value = EditorGUILayout.IntField(parameterValue.ParameterInfo.Name, (int)parameterValue.Value);
                return true;
            }

            if (type == typeof(bool))
            {
                value = EditorGUILayout.Toggle(parameterValue.ParameterInfo.Name, (bool)parameterValue.Value);
                return true;
            }

            if (type == typeof(float))
            {
                value = EditorGUILayout.FloatField(parameterValue.ParameterInfo.Name, (float)parameterValue.Value);
                return true;
            }

            if (type == typeof(string))
            {
                value = EditorGUILayout.TextField(parameterValue.ParameterInfo.Name, (string)parameterValue.Value);
                return true;
            }

            if (type == typeof(Color))
            {
                value = EditorGUILayout.ColorField(parameterValue.ParameterInfo.Name, (Color)parameterValue.Value);
                return true;
            }

            if (type == typeof(Object) || type.IsSubclassOf(typeof(Object)))
            {
                value = EditorGUILayout.ObjectField(
                    parameterValue.ParameterInfo.Name,
                    (Object)parameterValue.Value,
                    parameterValue.ParameterInfo.ParameterType,
                    AllowSceneObjects
                );

                return true;
            }

            if (type.IsEnum)
            {
                value = EditorGUILayout.EnumPopup(parameterValue.ParameterInfo.Name, (Enum)parameterValue.Value);
                return true;
            }

            if (type == typeof(Vector2))
            {
                value = EditorGUILayout.Vector2Field(
                    parameterValue.ParameterInfo.Name,
                    (Vector2)parameterValue.Value);
                return true;
            }

            if (type == typeof(Vector3))
            {
                value = EditorGUILayout.Vector3Field(
                    parameterValue.ParameterInfo.Name,
                    (Vector3)parameterValue.Value);
                return true;
            }

            if (type == typeof(Vector4))
            {
                value = EditorGUILayout.Vector4Field(
                    parameterValue.ParameterInfo.Name,
                    (Vector4)parameterValue.Value);
                return true;
            }

            if (type == typeof(Rect))
            {
                value = EditorGUILayout.RectField(parameterValue.ParameterInfo.Name, (Rect)parameterValue.Value);
                return true;
            }

            if (type == typeof(AnimationCurve))
            {
                value = EditorGUILayout.CurveField(
                    parameterValue.ParameterInfo.Name,
                    (AnimationCurve)parameterValue.Value ?? new AnimationCurve());
                return true;
            }

            if (type == typeof(Bounds))
            {
                value = EditorGUILayout.BoundsField(parameterValue.ParameterInfo.Name, (Bounds)parameterValue.Value);
                return true;
            }

            if (type == typeof(Quaternion))
            {
                value = EditorGUILayout
                    .Vector4Field(
                        parameterValue.ParameterInfo.Name,
                        ((Quaternion)parameterValue.Value).ToVector4())
                    .ToQuaternion();
                return true;
            }

            if (type == typeof(Vector2Int))
            {
                value = EditorGUILayout.Vector2IntField(
                    parameterValue.ParameterInfo.Name,
                    (Vector2Int)parameterValue.Value);
                return true;
            }

            if (type == typeof(Vector3Int))
            {
                value = EditorGUILayout.Vector3IntField(
                    parameterValue.ParameterInfo.Name,
                    (Vector3Int)parameterValue.Value);
                return true;
            }

            if (type == typeof(RectInt))
            {
                value = EditorGUILayout.RectIntField(
                    parameterValue.ParameterInfo.Name,
                    (RectInt)parameterValue.Value);
                return true;
            }

            if (type == typeof(BoundsInt))
            {
                value = EditorGUILayout.BoundsIntField(
                    parameterValue.ParameterInfo.Name,
                    (BoundsInt)parameterValue.Value);
                return true;
            }

            value = default(object);
            return false;
        }

        public static bool IsDemoVersion()
        {
            Type assemblyResult = Type.GetType("com.cyborgAssets.internalIBPProPrem.AbstractProEditor");
            if (assemblyResult == null)
                return true;
            return false;
        }

        //public static void AssemblyUpdate()
        //{
        //    //Type assemblyResult = Type.GetType("com.cyborgAssets.internalIBPProPrem.AbstractProEditor");
        //    //if (assemblyResult == null)
        //    if (IsDemoVersion())
        //    {
        //        //this code was checking old demo version
        //        if (b)
        //        {
        //            AssemblyLog1();
        //            return;
        //        }
        //    }
        //}

        //private static void AssemblyLog1()
        //{
        //    AssemblyLog10();
        //}





        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();



            foreach (var methodParams in methodsParams)
            {
                EditorGUILayout.BeginVertical("Box");

                var parameters = methodParams.Value;
                var notSerializableParams = Array.FindAll(
                    parameters,
                    x => !IsTypeSerializable(x.ParameterInfo.ParameterType));
                string error = null;
                if (notSerializableParams.Length > 0)
                {
                    var notSerializableParamNames = string.Join(
                        ", ",
                        Array.ConvertAll(notSerializableParams, x => x.ParameterInfo.Name));
                    error = "These parameters can't be serialized: " + notSerializableParamNames;
                }

                if (!string.IsNullOrEmpty(error))
                {
                    EditorGUILayout.HelpBox(error, MessageType.Error);
                }
                else
                {
                    EditorGUI.showMixedValue = serializedObject.targetObjects.Length > 1;
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        object value;
                        if (DrawParameter(parameters[i], out value))
                            parameters[i] = new ParameterValue(parameters[i].ParameterInfo, value);
                    }

                    EditorGUI.showMixedValue = false;
                }

                var attribute = methodParams
                    .Key
                    .GetCustomAttributes(typeof(IButtonAttribute), true)[0] as IButtonAttribute;
                var checkResult = attribute.PerformCheck(serializedObject.targetObject);

                var rect = EditorGUILayout.GetControlRect();
                var currentColor = GUI.color;
                GUI.color = checkResult ? currentColor : Color.red;
                var buttonPressed = GUI.Button(rect, methodParams.Key.Name);
                GUI.color = currentColor;

                if (string.IsNullOrEmpty(error) && !checkResult)
                    error = attribute.Error;

                if (buttonPressed)
                {
                    //AssemblyUpdate();

                    if (!string.IsNullOrEmpty(error))
                        Debug.LogError(error);
                    else
                    {
                        foreach (var targetObject in serializedObject.targetObjects)
                        {
                            TryAddObjectToCacheMine(targetObject); // this fixes a bug where duplicating objects then selecting them then clicking a button threw an error since the new objects where not in the cache until you selected them

                            var targetParameters = cache[targetObject.GetInstanceID()][methodParams.Key];



                            object methodInvokationResult = methodParams.Key.Invoke(
                                targetObject,
                                Array.ConvertAll(targetParameters, x => x.Value));
                            if (methodParams.Key.ReturnType != typeof(void))
                            {
                                //Debug.Log(DateTime.Now.ToShortTimeString() + "* " + methodInvokationResult);
                                //Debug.Log(methodInvokationResult + " - " + methodParams.Key.Name);
                                PrintReturnResult(" - ", methodInvokationResult, methodParams.Key.Name);

                            }
                        }
                    }
                }

                EditorGUILayout.EndVertical();
            }
        }

        //private static void Inspector_Update()
        //{
        //    Debug.LogError("The Pro Inspector Button free version allows using 25 buttons, you are using " + c + " buttons, \n you can buy the full version to enjoy unlimited buttons");
        //    throw new System.Exception("The Pro Inspector Button free version allows using 25 buttons, you can buy the full version to enjoy unlimited buttons");
        //}

        /// <summary>we changed this back to printing only return value</summary>
        public static void PrintReturnResult(string seprattor, object returnObject, string methodName)
        {
            string returnResult = "";
            if (returnObject == null)
                returnResult = "null";
            else
                returnResult = returnObject.ToString();
            //Debug.Log(returnResult + " - " + methodName);
            Debug.Log(returnResult);

        }
    }
}
using System;
//using DefaultNamespace;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using com.cyborgAssets.internalIBP;

namespace com.cyborgAssets.internalIBPEditor
{
    public static class SerializedPropertyExtensions
    {
        private static void CheckSerializedPropertyType (SerializedProperty serializedProperty)
        {
            if (serializedProperty.type != typeof (SerializedParameter).Name)
                throw new ArgumentException ("Unsupported type " + serializedProperty.type);
        }

        public static void SetSerializedParameterType (this SerializedProperty serializedProperty, Type type)
        {
            var typeSubProperty = serializedProperty.FindPropertyRelative (SerializedParameter.TypeProp);

            if (type == typeof (int))
            {
                typeSubProperty.enumValueIndex = (int) SerializedParameterType.Integer;
                return;
            }

            if (type == typeof (bool))
            {
                typeSubProperty.enumValueIndex = (int) SerializedParameterType.Boolean;
                return;
            }

            if (type == typeof (float))
            {
                typeSubProperty.enumValueIndex = (int) SerializedParameterType.Float;
                return;
            }

            if (type == typeof (string))
            {
                typeSubProperty.enumValueIndex = (int) SerializedParameterType.String;
                return;
            }

            if (type == typeof (Color))
            {
                typeSubProperty.enumValueIndex = (int) SerializedParameterType.Color;
                return;
            }

            if (type == typeof (Vector2))
            {
                typeSubProperty.enumValueIndex = (int) SerializedParameterType.Vector2;
                return;
            }

            if (type == typeof (Vector3))
            {
                typeSubProperty.enumValueIndex = (int) SerializedParameterType.Vector3;
                return;
            }

            if (type == typeof (Vector4))
            {
                typeSubProperty.enumValueIndex = (int) SerializedParameterType.Vector4;
                return;
            }

            if (type == typeof (Rect))
            {
                typeSubProperty.enumValueIndex = (int) SerializedParameterType.Rect;
                return;
            }

            if (type == typeof (AnimationCurve))
            {
                typeSubProperty.enumValueIndex = (int) SerializedParameterType.AnimationCurve;
                return;
            }

            if (type == typeof (Bounds))
            {
                typeSubProperty.enumValueIndex = (int) SerializedParameterType.Bounds;
                return;
            }

            if (type == typeof (Quaternion))
            {
                typeSubProperty.enumValueIndex = (int) SerializedParameterType.Quaternion;
                return;
            }

            if (type == typeof (Vector2Int))
            {
                typeSubProperty.enumValueIndex = (int) SerializedParameterType.Vector2Int;
                return;
            }

            if (type == typeof (Vector3Int))
            {
                typeSubProperty.enumValueIndex = (int) SerializedParameterType.Vector3Int;
                return;
            }

            if (type == typeof (RectInt))
            {
                typeSubProperty.enumValueIndex = (int) SerializedParameterType.RectInt;
                return;
            }

            if (type == typeof (BoundsInt))
            {
                typeSubProperty.enumValueIndex = (int) SerializedParameterType.BoundsInt;
                return;
            }
            
            if (type.IsEnum)
            {
                typeSubProperty.enumValueIndex = (int) SerializedParameterType.Enum;
                serializedProperty
                    .FindPropertyRelative (SerializedParameter.RealTypeProp)
                    .stringValue = type.AssemblyQualifiedName;
                
                return;
            }
            
            if (type == typeof (Object) || type.IsSubclassOf (typeof (Object)))
            {
                typeSubProperty.enumValueIndex = (int) SerializedParameterType.ObjectReference;
                serializedProperty
                    .FindPropertyRelative (SerializedParameter.RealTypeProp)
                    .stringValue = type.AssemblyQualifiedName;

                return;
            }

            throw new ArgumentException ("Unsupported type " + type);
        }

        public static SerializedProperty GetSerializedParameterValueProperty (
            this SerializedProperty serializedProperty)
        {
            CheckSerializedPropertyType (serializedProperty);
            var serializedParameterType = (SerializedParameterType) serializedProperty
                .FindPropertyRelative (SerializedParameter.TypeProp)
                .enumValueIndex;

            string propertyName;
            switch (serializedParameterType)
            {
                case SerializedParameterType.Integer:
                    propertyName = SerializedParameter.IntegerProp;
                    break;
                case SerializedParameterType.Boolean:
                    propertyName = SerializedParameter.BooleanProp;
                    break;
                case SerializedParameterType.Float:
                    propertyName = SerializedParameter.FloatProp;
                    break;
                case SerializedParameterType.String:
                    propertyName = SerializedParameter.StringProp;
                    break;
                case SerializedParameterType.Color:
                    propertyName = SerializedParameter.ColorProp;
                    break;
                case SerializedParameterType.ObjectReference:
                    propertyName = SerializedParameter.ObjectReferenceProp;
                    break;
                case SerializedParameterType.Enum:
                    propertyName = SerializedParameter.EnumProp;
                    break;
                case SerializedParameterType.Vector2:
                    propertyName = SerializedParameter.Vector2Prop;
                    break;
                case SerializedParameterType.Vector3:
                    propertyName = SerializedParameter.Vector3Prop;
                    break;
                case SerializedParameterType.Vector4:
                    propertyName = SerializedParameter.Vector4Prop;
                    break;
                case SerializedParameterType.Rect:
                    propertyName = SerializedParameter.RectProp;
                    break;
                case SerializedParameterType.AnimationCurve:
                    propertyName = SerializedParameter.AnimationCurveProp;
                    break;
                case SerializedParameterType.Bounds:
                    propertyName = SerializedParameter.BoundsProp;
                    break;
                case SerializedParameterType.Quaternion:
                    propertyName = SerializedParameter.QuaternionProp;
                    break;
                case SerializedParameterType.Vector2Int:
                    propertyName = SerializedParameter.Vector2IntProp;
                    break;
                case SerializedParameterType.Vector3Int:
                    propertyName = SerializedParameter.Vector3IntProp;
                    break;
                case SerializedParameterType.RectInt:
                    propertyName = SerializedParameter.RectIntProp;
                    break;
                case SerializedParameterType.BoundsInt:
                    propertyName = SerializedParameter.BoundsInProp;
                    break;
                default:
                    throw new ArgumentOutOfRangeException (
                        "serializedParameterType",
                        serializedParameterType,
                        null);
            }

            return serializedProperty.FindPropertyRelative (propertyName);
        }

        public static object ExtractSerializedParameterValue (this SerializedProperty serializedProperty)
        {
            CheckSerializedPropertyType (serializedProperty);

            var type = (SerializedParameterType) serializedProperty
                .FindPropertyRelative (SerializedParameter.TypeProp)
                .enumValueIndex;

            switch (type)
            {
                case SerializedParameterType.Integer:
                    return serializedProperty.FindPropertyRelative (SerializedParameter.IntegerProp).intValue;
                case SerializedParameterType.Boolean:
                    return serializedProperty.FindPropertyRelative (SerializedParameter.BooleanProp).boolValue;
                case SerializedParameterType.Float:
                    return serializedProperty.FindPropertyRelative (SerializedParameter.FloatProp).floatValue;
                case SerializedParameterType.String:
                    return serializedProperty.FindPropertyRelative (SerializedParameter.StringProp).stringValue;
                case SerializedParameterType.Color:
                    return serializedProperty.FindPropertyRelative (SerializedParameter.ColorProp).colorValue;
                case SerializedParameterType.ObjectReference:
                    return serializedProperty.FindPropertyRelative (SerializedParameter.ObjectReferenceProp)
                        .objectReferenceValue;
                case SerializedParameterType.Enum:
                    return serializedProperty.FindPropertyRelative (SerializedParameter.EnumProp).intValue;
                case SerializedParameterType.Vector2:
                    return serializedProperty.FindPropertyRelative (SerializedParameter.Vector2Prop).vector2Value;
                case SerializedParameterType.Vector3:
                    return serializedProperty.FindPropertyRelative (SerializedParameter.Vector3Prop).vector3Value;
                case SerializedParameterType.Vector4:
                    return serializedProperty.FindPropertyRelative (SerializedParameter.Vector4Prop).vector4Value;
                case SerializedParameterType.Rect:
                    return serializedProperty.FindPropertyRelative (SerializedParameter.RectProp).rectValue;
                case SerializedParameterType.AnimationCurve:
                    return serializedProperty.FindPropertyRelative (SerializedParameter.AnimationCurveProp)
                        .animationCurveValue;
                case SerializedParameterType.Bounds:
                    return serializedProperty.FindPropertyRelative (SerializedParameter.BoundsProp).boundsValue;
                case SerializedParameterType.Quaternion:
                    return serializedProperty.FindPropertyRelative (SerializedParameter.QuaternionProp)
                        .quaternionValue;
                case SerializedParameterType.Vector2Int:
                    return serializedProperty.FindPropertyRelative (SerializedParameter.Vector2IntProp)
                        .vector2IntValue;
                case SerializedParameterType.Vector3Int:
                    return serializedProperty.FindPropertyRelative (SerializedParameter.Vector3IntProp)
                        .vector3IntValue;
                case SerializedParameterType.RectInt:
                    return serializedProperty.FindPropertyRelative (SerializedParameter.RectIntProp).rectIntValue;
                case SerializedParameterType.BoundsInt:
                    return serializedProperty.FindPropertyRelative (SerializedParameter.BoundsInProp).boundsIntValue;
                default:
                    throw new ArgumentOutOfRangeException ();
            }
        }
    }
}
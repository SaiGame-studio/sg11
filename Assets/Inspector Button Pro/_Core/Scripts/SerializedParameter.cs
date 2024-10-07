using System;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace com.cyborgAssets.internalIBP

{
    public enum SerializedParameterType : byte
    {
        Integer,
        Boolean,
        Float,
        String,
        Color,
        ObjectReference,
        Enum,
        Vector2,
        Vector3,
        Vector4,
        Rect,
        AnimationCurve,
        Bounds,
        Quaternion,
        Vector2Int,
        Vector3Int,
        RectInt,
        BoundsInt
    }

    [Serializable]
    public struct SerializedParameter
    {
        public const string NameProp = "name";
        public const string TypeProp = "type";

        public const string IntegerProp = "integerValue";
        public const string BooleanProp = "booleanValue";
        public const string FloatProp = "floatValue";
        public const string StringProp = "stringValue";
        public const string ColorProp = "colorValue";
        public const string ObjectReferenceProp = "objectReferenceValue";
        public const string EnumProp = "enumValue";
        public const string Vector2Prop = "vector2Value";
        public const string Vector3Prop = "vector3Value";
        public const string Vector4Prop = "vector4Value";
        public const string RectProp = "rectValue";
        public const string AnimationCurveProp = "animationCurveValue";
        public const string BoundsProp = "boundsValue";
        public const string QuaternionProp = "quaternionValue";
        public const string Vector2IntProp = "vector2IntValue";
        public const string Vector3IntProp = "vector3IntValue";
        public const string RectIntProp = "rectIntValue";
        public const string BoundsInProp = "boundsInValue";
        public const string UnsupportedProp = "unsupported";
        public const string RealTypeProp = "realType";

        [SerializeField]
        private string name;

        [SerializeField]
        private SerializedParameterType type;

        [SerializeField]
        private bool unsupported;

        [SerializeField]
        private int integerValue;

        [SerializeField]
        private bool booleanValue;

        [SerializeField]
        private float floatValue;

        [SerializeField]
        private string stringValue;

        [SerializeField]
        private Color colorValue;

        [SerializeField]
        private Object objectReferenceValue;

        [SerializeField]
        private string realType;

        [SerializeField]
        private int enumValue;

        [SerializeField]
        private Vector2 vector2Value;

        [SerializeField]
        private Vector3 vector3Value;

        [SerializeField]
        private Vector4 vector4Value;

        [SerializeField]
        private Rect rectValue;

        [SerializeField]
        private AnimationCurve animationCurveValue;

        [SerializeField]
        private Bounds boundsValue;

        [SerializeField]
        private Quaternion quaternionValue;

        [SerializeField]
        private Vector2Int vector2IntValue;

        [SerializeField]
        private Vector3Int vector3IntValue;

        [SerializeField]
        private RectInt rectIntValue;

        [SerializeField]
        private BoundsInt boundsInValue;
    }
}
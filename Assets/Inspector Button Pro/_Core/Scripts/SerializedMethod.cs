using System;
using UnityEngine;

namespace com.cyborgAssets.internalIBP
//namespace DefaultNamespace
{
    [Serializable]
    public struct SerializedMethod
    {
        public const string NameProp = "name";
        public const string SignatureProp = "signature";
        public const string ParametersProp = "parameters";
        public const string UnsupportedArgs = "unsupportedArgs";

        [SerializeField]
        private string name;

        [SerializeField]
        private string signature;

        [SerializeField]
        private SerializedParameter[] parameters;

        [SerializeField]
        private string[] unsupportedArgs;
    }
}
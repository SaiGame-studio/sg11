//using DefaultNamespace;
using com.cyborgAssets.internalIBP;
using UnityEngine;

namespace com.cyborgAssets.inspectorButtonPro
{

    /// <summary>
    /// previous called ProScriptableObject
    /// </summary>
    public class ButtonScriptableObject : ScriptableObject
    {
        #if UNITY_EDITOR
        [SerializeField, HideInInspector]
        private SerializedMethod[] serializedMethods;
        #endif
    }
}
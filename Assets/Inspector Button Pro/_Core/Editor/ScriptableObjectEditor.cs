using UnityEditor;
using UnityEngine;

namespace com.cyborgAssets.internalIBPEditor
{
    //[CustomEditor (typeof (ScriptableObject), true, isFallback = true), CanEditMultipleObjects]
    [CustomEditor (typeof (ScriptableObject), true), CanEditMultipleObjects]
    public class ScriptableObjectEditor : AbstractEditor
    {
        protected override bool AllowSceneObjects
        {
            get { return false; }
        }
    }
}
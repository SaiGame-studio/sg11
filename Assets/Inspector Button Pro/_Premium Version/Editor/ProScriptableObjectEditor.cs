using com.cyborgAssets.inspectorButtonPro;
using com.cyborgAssets.internalIBPProPrem;
using UnityEditor;

namespace com.cyborgAssets.internalIBPEditor
{
    //[CustomEditor (typeof (ButtonScriptableObject), true, isFallback = true), CanEditMultipleObjects]
    [CustomEditor (typeof (ButtonScriptableObject), true), CanEditMultipleObjects]
    public class ProScriptableObjectEditor : AbstractProEditor
    { 
        protected override bool AllowSceneObjects
        {
            get { return false; }
        }
    }
}
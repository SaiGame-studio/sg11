using com.cyborgAssets.inspectorButtonPro;
using com.cyborgAssets.internalIBP;
using com.cyborgAssets.internalIBPProPrem;
using UnityEditor;

namespace com.cyborgAssets.internalIBPEditor
{
    //[CustomEditor (typeof (ButtonMonoBehavior), true, isFallback = true), CanEditMultipleObjects]
    [CustomEditor(typeof(ButtonMonoBehavior), true), CanEditMultipleObjects]
    public class ProMonoBehaviourEditor : AbstractProEditor
    {
        /// <summary>this is used by Unity, to avoid dragging scene object references into prefab fields</summary>
        protected override bool AllowSceneObjects
        {
            get { return !serializedObject.targetObject.IsPrefab (); }
        }
    }
}
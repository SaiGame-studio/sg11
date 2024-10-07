//using DefaultNamespace;
using com.cyborgAssets.internalIBP;
using UnityEngine;

/// <summary>
/// previously called ProMonoBehavior
/// </summary>
/// 

namespace com.cyborgAssets.inspectorButtonPro
{
    public class ButtonMonoBehavior : MonoBehaviour
    {
        //if it's Unity 2021.3 then only keep the serialized values in the editor
        //( to avoid any increase to the build size )
        //otherwise, keep the values in the game, because older Unity versions
        //support for this feature is not guaranteed
        //this didn't work for some reason, values were still stripped 
        //from build in Unity 2020
        //so we abandon this setup for now
        //and reverted the code to how it initially was

        //#if UNITY_2021_3_OR_NEWER
        //#if UNITY_EDITOR
        //[SerializeField, HideInInspector]
        //private SerializedMethod[] serializedMethods;
        //#endif

        //#else

#if UNITY_EDITOR
        [SerializeField, HideInInspector]
        private SerializedMethod[] serializedMethods;
#endif
    }
}
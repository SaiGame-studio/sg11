using UnityEngine;

namespace com.cyborgAssets.internalIBP
{
    public static class ObjectExtensions
    {
        public static bool IsPrefab(this Object obj)
        {
            return false;

            //#if UNITY_2018_1_OR_NEWER && UNITY_EDITOR
            //var stage = UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage ();
            //return UnityEditor.PrefabUtility.IsPartOfPrefabAsset (obj) || stage != null;
            //#elif UNITY_EDITOR
            //return UnityEditor.PrefabUtility.GetPrefabType (obj) == UnityEditor.PrefabType.Prefab;
            //#else
            //return false;
            //#endif
        }
    }
}
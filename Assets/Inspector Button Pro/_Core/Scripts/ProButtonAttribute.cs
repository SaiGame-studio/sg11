using com.cyborgAssets.internalIBP;
using System;
using Object = UnityEngine.Object;

namespace com.cyborgAssets.inspectorButtonPro
{

    public class ProButtonAttribute : Attribute, IButtonAttribute
    {
        public string Error
        {
            get { return "Selected object is a prefab, use [Prefab] tag to execute method on a prefab"; }
        }

        public bool PerformCheck(Object obj)
        {
            return !obj.IsPrefab();
        }
    }
}
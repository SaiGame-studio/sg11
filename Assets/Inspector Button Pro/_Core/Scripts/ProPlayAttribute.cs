using com.cyborgAssets.internalIBP;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace com.cyborgAssets.inspectorButtonPro
{

    public class ProPlayButtonAttribute : Attribute, IButtonAttribute
    {
        public string Error
        {
            get
            {
                return
                    "Unity Editor is not running, press the unity play button - or use the [ProButton] attribute to execute methods in Edit mode";
                //"Selected object is a prefab or Play Mode is not active, use [Prefab] tag to execute method on a prefab, use [Edit] tag to execute method in Edit Mode";
            }
        }

        public bool PerformCheck(Object obj)
        {
            return !obj.IsPrefab() && Application.isPlaying;
        }
    }
}
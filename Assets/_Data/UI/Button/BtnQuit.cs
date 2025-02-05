using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnQuit : BaseButton
{
    protected override void OnClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

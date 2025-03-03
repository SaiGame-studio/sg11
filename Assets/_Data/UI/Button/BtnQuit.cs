using UnityEngine;

public class BtnQuit : BaseButton
{
    protected override void Start()
    {
        base.Start();
        if(!PlatformDetector.Instance.isWindow) gameObject.SetActive(false); 
    }

    protected override void OnClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

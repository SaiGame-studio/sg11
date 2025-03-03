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
        Application.Quit();
    }
}

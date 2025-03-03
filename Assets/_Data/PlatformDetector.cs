using UnityEngine;

public class PlatformDetector : SaiSingleton<PlatformDetector>
{
    public bool isWindow { get; private set; }
    public bool isWeb { get; private set; }
    public bool isAndroid { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        DetectPlatform();
    }

    private void DetectPlatform()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
                isWindow = true;
                isWeb = false;
                isAndroid = false;
                break;

            case RuntimePlatform.WebGLPlayer:
                isWindow = false;
                isWeb = true;
                isAndroid = false;
                break;

            case RuntimePlatform.Android:
                isWindow = false;
                isWeb = false;
                isAndroid = true;
                break;

            default:
                isWindow = false;
                isWeb = false;
                isAndroid = false;
                Debug.LogWarning("Unknow platform");
                break;
        }
    }
}

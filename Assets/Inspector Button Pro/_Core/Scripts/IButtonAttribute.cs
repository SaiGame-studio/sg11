using UnityEngine;

namespace com.cyborgAssets.inspectorButtonPro
{

    public interface IButtonAttribute
    {
        string Error { get; }
        bool PerformCheck(Object obj);
    }
}

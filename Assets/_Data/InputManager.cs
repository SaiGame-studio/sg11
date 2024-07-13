using UnityEngine;

public class InputManager : SaiMonoBehaviour
{
    protected void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            GridManagerCtrl.Instance.blockHandler.Unchoose();
            BlockDebug.Instance.ClearDebug();
        }
    }
}

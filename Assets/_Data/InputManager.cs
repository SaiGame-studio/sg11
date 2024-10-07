using UnityEngine;

public class InputManager : SaiSingleton<InputManager>
{
    public bool isDebug = false;

    protected void Update()
    {
        this.ToogleDebugMode();
        this.ClearChooseBlock();
        this.DeleteChooseBlock();
    }

    protected virtual void ToogleDebugMode()
    {
        if (Input.GetKeyUp(KeyCode.F3)) this.isDebug = !this.isDebug;
    }

    protected virtual void DeleteChooseBlock()
    {
        if (Input.GetKeyUp(KeyCode.Delete)) GridManagerCtrl.Instance.blockDebug.DeleteFirstBlock();
    }

    protected virtual void ClearChooseBlock()
    {
        if (Input.GetMouseButtonUp(1))
        {
            GridManagerCtrl.Instance.blockHandler.Unchoose();
            BlockDebug.Instance.ClearDebug();
        }
    }
}

using UnityEngine;

public class InputManager : SaiSingleton<InputManager>
{
    public bool isDebug = false;
    public KonamiCodeChecker konamiCodeChecker {  get; private set; }

    protected override void Start()
    {
        base.Start();
        konamiCodeChecker = new KonamiCodeChecker();
    }

    protected void Update()
    {
        this.ToogleDebugMode();
        this.CheckingKonamiInput();
        this.ClearChooseBlock();
        this.DeleteChooseBlock();
    }

    protected virtual void ToogleDebugMode()
    {
        if (Input.GetKeyUp(KeyCode.F3))
        {
            if (isDebug)
            {
                ActivateDebugMode(false);
                return;
            }
            else if (konamiCodeChecker.IsCheckingKonamiCode())
            {
                konamiCodeChecker.SetCheckingKonamiCode(false);
                konamiCodeChecker.Reset();
            }
            else
            {
                konamiCodeChecker.SetCheckingKonamiCode(true);
            }

        }
    }

    private void CheckingKonamiInput()
    {
        if (!konamiCodeChecker.IsCheckingKonamiCode()) return;

        konamiCodeChecker.CheckInput();

        if (!konamiCodeChecker.IsCodeEntered) return;

        ActivateDebugMode(true);
        konamiCodeChecker.SetCheckingKonamiCode(false);
        konamiCodeChecker.Reset();
    }

    private void ActivateDebugMode(bool active)
    {
        isDebug = active;
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

    public void ActiveDebugMode(bool active)
    {
        this.isDebug = active;
    }
}

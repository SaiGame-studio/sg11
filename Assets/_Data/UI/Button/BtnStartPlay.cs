using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnStartPlay : BaseButton
{
    [SerializeField] protected GameMode gameMode;

    protected override void OnClick()
    {
        GameManager.Instance.SetGameMode(gameMode);
        GameManager.Instance.ResetGameOverState();
        GameManager.Instance.StartNewGame();
    }
}

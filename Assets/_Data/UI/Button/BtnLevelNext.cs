using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BtnLevelNext : BaseButton
{
    private void FixedUpdate()
    {
        this.LevelStatus();
    }

    protected override void OnClick()
    {
        GameManager.Instance.NextLevel();
    }

    protected virtual void LevelStatus()
    {
        this.button.interactable = GridManagerCtrl.Instance.gridSystem.blocksRemain == 0;
    }
}

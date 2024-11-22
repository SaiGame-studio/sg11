using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnShuffle : BaseButton
{
    protected override void OnClick()
    {
        if (GameManager.Instance.RemainShuffle > 0)
        {
            GridManagerCtrl.Instance.blockAuto.ShuffleBlocks();
            SoundManager.Instance.PlaySound(SoundManager.Sound.win);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnShuffle : BaseButton
{
    protected override void OnClick()
    {
        GridManagerCtrl.Instance.blockAuto.ShuffleBlocks();
    }
}

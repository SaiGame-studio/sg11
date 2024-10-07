using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnMoveBlocks : BaseButton
{
    protected override void OnClick()
    {
        GridManagerCtrl.Instance.gameLevel.GetCurrentLevelObj().MoveBlocks();
    }
}

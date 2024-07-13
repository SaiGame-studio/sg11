using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnHint : BaseButton
{
    protected override void OnClick()
    {
        BlockDebug.Instance.ClearDebug();
        GridManagerCtrl.Instance.blockAuto.ShowHint();
    }
}

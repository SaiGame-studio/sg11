using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnHint : BaseButton
{
    protected override void OnClick()
    {
        GridManagerCtrl.Instance.blockAuto.ShowHint();
    }
}

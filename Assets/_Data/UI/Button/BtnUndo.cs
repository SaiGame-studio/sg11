using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnUndo : BaseButton
{
    protected override void OnClick()
    {
        BlockDebug.Instance.NodeUndo();
    }
}

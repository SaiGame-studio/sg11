using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnDeleteBlock : BaseButton
{
    protected override void OnClick()
    {
        GridManagerCtrl.Instance.blockDebug.DeleteFirstBlock();
    }
}

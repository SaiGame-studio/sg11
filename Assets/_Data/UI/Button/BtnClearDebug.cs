using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnClearDebug : BaseButton
{
    protected override void OnClick()
    {
        BlockDebug.Instance.ClearDebug();
    }
}

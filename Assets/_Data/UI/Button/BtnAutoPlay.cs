using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnAutoPlay : BaseButton
{
    protected override void OnClick()
    {
        BlockDebug.Instance.AutoPlay();
    }
}

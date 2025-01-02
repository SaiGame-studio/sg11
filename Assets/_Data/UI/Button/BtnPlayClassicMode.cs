using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnPlayClassicMode : BaseButton
{
    protected override void OnClick()
    {
        GameManager.Instance.StartNewGame();
    }
}

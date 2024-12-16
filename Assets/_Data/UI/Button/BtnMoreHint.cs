using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnMoreHint : BaseButton
{
    [SerializeField] private int hintNum = 5;
    protected override void OnClick()
    {
        GameManager.Instance.AddMoreHint(hintNum);
    }
}

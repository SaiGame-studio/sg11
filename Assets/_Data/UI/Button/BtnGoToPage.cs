using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnGoToPage : BaseButton
{
    [SerializeField] protected int pageIndex;
    protected override void OnClick()
    {
        UIManager.Instance.GoToPage(pageIndex);
    }
}

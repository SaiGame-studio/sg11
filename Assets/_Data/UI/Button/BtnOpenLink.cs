using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnOpenLink : BaseButton
{
    [SerializeField] private string link;

    protected override void OnClick()
    {
        Application.OpenURL(link);
    }
}

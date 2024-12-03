using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnLink : BaseButton
{
    [SerializeField] private string link;

    protected override void OnClick()
    {
        Application.OpenURL(link);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnLogin : BaseButton
{
    protected UILoginPanel uiLoginPannel;

    protected override void OnClick()
    {
        if (uiLoginPannel == null) return;

        uiLoginPannel.LoginPhoton();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUILoginPanel();
    }

    protected virtual void LoadUILoginPanel()
    {
        if (this.uiLoginPannel != null) return;
        this.uiLoginPannel = GetComponentInParent<UILoginPanel>();
        Debug.LogWarning(transform.name + " LoadUILoginPanel", gameObject);
    }
}

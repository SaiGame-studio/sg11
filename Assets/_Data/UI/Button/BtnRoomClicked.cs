using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnRoomClicked : BaseButton
{
    protected UIRoomProfile uiRoomProfile;

    protected override void OnClick()
    {
        if (uiRoomProfile == null) return;
        uiRoomProfile.OnClickRoom();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIRoomProfile();
    }

    private void LoadUIRoomProfile()
    {
        if (this.uiRoomProfile != null) return;
        this.uiRoomProfile = GetComponent<UIRoomProfile>();
        Debug.LogWarning(transform.name + ": LoadUIRoomProfile", gameObject);
    }
}

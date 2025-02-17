using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnJoinRoom : BaseButton
{
    protected UIRoomProfile uiRoomProfile;

    protected override void OnClick()
    {
        uiRoomProfile.JoinRoom();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIRoomProfile();
    }

    private void LoadUIRoomProfile()
    {
        if (this.uiRoomProfile != null) return;
        this.uiRoomProfile = GetComponentInParent<UIRoomProfile>();
        Debug.LogWarning(transform.name + " LoadUIRoomProfile", gameObject);
    }
}

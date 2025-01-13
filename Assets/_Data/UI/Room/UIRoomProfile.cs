using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRoomProfile : SaiMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI roomName;
    [SerializeField] protected RoomProfile roomProfile;

    public virtual void SetRoomProfile(RoomProfile roomProfile)
    {
        this.roomProfile = roomProfile;
        this.roomName.text = this.roomProfile.name;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRoomName(); ;
    }

    private void LoadRoomName()
    {
        if (this.roomName != null) return;
        this.roomName = GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + " LoadRoomName", gameObject);
    }
}

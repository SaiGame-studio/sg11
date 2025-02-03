using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRoomProfile : SaiMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI roomName;
    [SerializeField] protected RoomProfile roomProfile;
    protected PhotonRoom photonRoom;

    public virtual void SetRoomProfile(RoomProfile roomProfile)
    {
        this.roomProfile = roomProfile;
        this.roomName.text = this.roomProfile.name;
    }

    public virtual void SetPhotonRoom(PhotonRoom photonRoom)
    {
        this.photonRoom = photonRoom;
    }

    public virtual void OnClickRoom()
    {
        if(photonRoom != null)
        {
            photonRoom.SetRoomInput(roomName.text);
        }
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

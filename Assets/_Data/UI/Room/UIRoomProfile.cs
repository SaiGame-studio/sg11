using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class UIRoomProfile : SaiMonoBehaviour
{
    [SerializeField] protected RoomProfile roomProfile;
    [SerializeField] protected TextMeshProUGUI roomName;
    [SerializeField] protected Button btnJoin;

    protected virtual void FixedUpdate()
    {
        UpdateJoinButtonStatus();
    }

    public virtual void JoinRoom()
    {
        PhotonManagerCtrl.Instance.photonLobby.JoinRoom(roomProfile.name);
    }

    protected virtual void UpdateJoinButtonStatus()
    {
        btnJoin.interactable = !(roomProfile.playerCount == roomProfile.maxPlayers);
    }

    public virtual void SetRoomProfile(RoomProfile _roomProfile)
    {
        this.roomProfile = _roomProfile;
        this.roomName.text = this.roomProfile.name;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRoomName();
        this.LoadJoinBtn();
    }

    private void LoadRoomName()
    {
        if (this.roomName != null) return;
        this.roomName = GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + " LoadRoomName", gameObject);
    }

    protected void LoadJoinBtn()
    {
        if (this.btnJoin != null) return;
        this.btnJoin = GetComponentInChildren<Button>();
        Debug.LogWarning(transform.name + " LoadJoinBtn", gameObject);
    }
}

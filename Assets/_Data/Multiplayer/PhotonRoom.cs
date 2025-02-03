using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhotonRoom : SaiMonoBehaviourPunCallbacks
{
    public TMP_InputField input;
    public Transform roomContent;
    public UIRoomProfile roomPrefab;
    public List<RoomInfo> updatedRooms;
    public List<RoomProfile> rooms = new List<RoomProfile>();

    public virtual void Create()
    {
        string name = input.text;
        PhotonNetwork.CreateRoom(name);
    }

    public virtual void Join()
    {
        string name = input.text;
        Debug.Log(transform.name + ": Join Room " + name);
        PhotonNetwork.JoinRoom(name);
    }

    public override void OnCreatedRoom()
    {
        //Todo
    }

    public override void OnJoinedRoom()
    {
        //Todo
    }

    public override void OnLeftRoom()
    {
        //Todo
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed: " + message);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        this.updatedRooms = roomList;

        foreach (RoomInfo roomInfo in roomList)
        {
            if (roomInfo.RemovedFromList) this.RoomRemove(roomInfo);
            else this.RoomAdd(roomInfo);
        }

        this.UpdateRoomProfileUI();
    }

    protected virtual void RoomAdd(RoomInfo roomInfo)
    {
        RoomProfile roomProfile = new RoomProfile
        {
            name = roomInfo.Name
        };
        this.rooms.Add(roomProfile);

    }

    protected virtual void UpdateRoomProfileUI()
    {
        foreach (Transform child in this.roomContent)
        {
            Destroy(child.gameObject);
        }

        foreach (RoomProfile roomProfile in this.rooms)
        {
            UIRoomProfile uiRoomProfile = Instantiate(this.roomPrefab);
            uiRoomProfile.SetRoomProfile(roomProfile);
            uiRoomProfile.SetPhotonRoom(this);
            uiRoomProfile.transform.SetParent(this.roomContent, false);
        }
    }

    protected virtual void RoomRemove(RoomInfo roomInfo)
    {
        RoomProfile roomProfile = this.RoomByName(roomInfo.Name);
        if (roomProfile == null) return;
        this.rooms.Remove(roomProfile);
    }

    protected virtual RoomProfile RoomByName(string name)
    {
        foreach (RoomProfile roomProfile in this.rooms)
        {
            if (roomProfile.name == name) return roomProfile;
        }
        return null;
    }

    public virtual void SetRoomInput(string roomName)
    {
        this.input.text = roomName;
    }
}

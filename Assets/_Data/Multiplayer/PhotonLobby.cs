using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhotonLobby : SaiMonoBehaviourPunCallbacks
{
    [Header("Lobby Setup")]
    [SerializeField] protected TMP_InputField roomNameInput;
    [SerializeField] protected Transform roomListContent;
    [SerializeField] protected UIRoomProfile roomProfilePrefab;

    protected List<RoomProfile> roomProfiles = new List<RoomProfile>();
    protected Dictionary<string, UIRoomProfile> uiRoomProfiles = new Dictionary<string, UIRoomProfile>();

    public virtual bool CanCreateRoom()
    {
        if(roomNameInput.text.Length == 0)
        {
            return false;
        }
        return true;
    }

    public virtual void CreateRoom()
    {
        if (!CanCreateRoom()) return;

        string roomName = roomNameInput.text;
        PhotonNetwork.CreateRoom(roomName, new RoomOptions
        {
            MaxPlayers = 4,
            BroadcastPropsChangeToAll = true
        });
        roomNameInput.text = "";
    }

    public virtual void JoinRoom(string _roomName)
    {
        if (!IsRoomNameValid(_roomName)) return;

        PhotonNetwork.JoinRoom(_roomName);
    }

    protected virtual bool IsRoomNameValid(string roomName)
    {
        RoomProfile roomProfile = roomProfiles.Find(profile => profile.name == roomName);
        if (roomProfile == null)
        {
            Debug.LogWarning("Room does not exist!");
            return false;
        }

        if (roomProfile.playerCount >= roomProfile.maxPlayers)
        {
            Debug.LogWarning("Room is full! Cannot join.");
            return false;
        }
        return true;
    }

    public virtual void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateRoomProfiles(roomList);
        RefreshRoomListUI();
    }

    protected virtual void UpdateRoomProfiles(List<RoomInfo> roomList)
    {
        foreach (var roomInfo in roomList)
        {
            if (roomInfo.RemovedFromList)
            {
                RemoveRoomProfile(roomInfo.Name);
            }
            else
            {
                AddRoomProfile(roomInfo);
            }
        }
    }

    private void AddRoomProfile(RoomInfo roomInfo)
    {
        var existingProfile = roomProfiles.Find(profile => profile.name == roomInfo.Name);

        if (existingProfile != null)
        {
            existingProfile.playerCount = roomInfo.PlayerCount;
            existingProfile.maxPlayers = roomInfo.MaxPlayers;
            return;
        }

        RoomProfile newProfile = new RoomProfile
        {
            name = roomInfo.Name,
            playerCount = roomInfo.PlayerCount,
            maxPlayers = roomInfo.MaxPlayers
        };
        roomProfiles.Add(newProfile);
    }

    private void RemoveRoomProfile(string roomName)
    {
        roomProfiles.RemoveAll(profile => profile.name == roomName);
    }

    private void RefreshRoomListUI()
    {
        foreach (Transform child in roomListContent)
        {
            Destroy(child.gameObject);
        }

        foreach (var profile in roomProfiles)
        {
            UIRoomProfile uiProfile = Instantiate(roomProfilePrefab, roomListContent);
            uiProfile.SetRoomProfile(profile);
        }
    }
}

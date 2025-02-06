using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhotonRoom : SaiMonoBehaviourPunCallbacks
{
    [Header("Lobby setup")]
    public TMP_InputField input;
    public Transform roomContent;
    public UIRoomProfile roomPrefab;
    public List<RoomInfo> updatedRooms;
    public int roomPanelIndex;
    public List<RoomProfile> rooms = new List<RoomProfile>();

    [Space]
    [Header("Room setup")]
    public Transform roomPanel;
    public List<UIPlayerItem> playerItems = new List<UIPlayerItem>();
    public UIPlayerItem playerItemPrefab;
    public Transform playerItemParent;

    [Space]
    public Button startButton;
    public Button readyButton;
    public Button cancelButton;

    #region Room and Lobby

    public virtual void Create()
    {
        string name = input.text;
        PhotonNetwork.CreateRoom(name, new RoomOptions() { MaxPlayers = 4, BroadcastPropsChangeToAll = true});
        input.text = "";
    }

    public virtual void Join()
    {
        string name = input.text;
        Debug.Log(transform.name + ": Join Room " + name);
        PhotonNetwork.JoinRoom(name);
        input.text = "";
    }

    public virtual void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnCreatedRoom()
    {
        //Todo
    }

    public override void OnJoinedRoom()
    {
        UIManager.Instance.GoToPage(roomPanelIndex);
        UpdatePlayerList();

        if (PhotonNetwork.IsMasterClient)
        {
            startButton.gameObject.SetActive(true);
            startButton.onClick.AddListener(StartGame);
            startButton.interactable = false;
        }
        else
        {
            startButton.gameObject.SetActive(false);
        }
    }

    private void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("GameScene"); // Load màn chơi chính
        }
    }

    public override void OnLeftRoom()
    {
        UIManager.Instance.GoToPage(roomPanelIndex - 1);
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
        RoomProfile roomProfile;

        roomProfile = this.RoomByName(roomInfo.Name);
        if (roomProfile != null) return;

        roomProfile = new RoomProfile
        {
            name = roomInfo.Name
        };
        this.rooms.Add(roomProfile);
    }

    protected virtual void UpdateRoomProfileUI()
    {
        this.ClearRoomProfileUI();

        foreach (RoomProfile roomProfile in this.rooms)
        {
            UIRoomProfile uiRoomProfile = Instantiate(this.roomPrefab);
            uiRoomProfile.SetRoomProfile(roomProfile);
            uiRoomProfile.SetPhotonRoom(this);
            uiRoomProfile.transform.SetParent(this.roomContent, false);
        }
    }

    protected virtual void ClearRoomProfileUI()
    {
        foreach (Transform child in this.roomContent)
        {
            Destroy(child.gameObject);
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
    #endregion

    #region Inside Room
    protected virtual void UpdatePlayerList()
    {
        foreach (UIPlayerItem playerItem in this.playerItems)
        {
            Destroy(playerItem.gameObject);
        }
        this.playerItems.Clear();

        if(PhotonNetwork.CurrentRoom == null) return;

        readyButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);

        List<Player> sortedPlayers = new List<Player>(PhotonNetwork.CurrentRoom.Players.Values);
        sortedPlayers.Sort((a, b) => a.ActorNumber.CompareTo(b.ActorNumber));

        foreach (Player player in sortedPlayers)
        {
            UIPlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            

            newPlayerItem.SetPlayerInfo(player);

            if (player.IsMasterClient)
            {
                newPlayerItem.masterPlayerImage.gameObject.SetActive(true);
                newPlayerItem.readyIndicator.gameObject.SetActive(false);
                newPlayerItem.stateImage.gameObject.SetActive(false);
            }
            else
            {
                newPlayerItem.masterPlayerImage.gameObject.SetActive(false);
            }

            if (player == PhotonNetwork.LocalPlayer)
            {
                newPlayerItem.ApplyLocalChange(readyButton, cancelButton);
            }
            newPlayerItem.UpdateReadyState();
            playerItems.Add(newPlayerItem);
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        UpdatePlayerList();
        CheckAllPlayersReady();
    }

    private void CheckAllPlayersReady()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            if (player.Value.IsMasterClient) continue;

            if (!player.Value.CustomProperties.TryGetValue("isReady", out object readyState) || !(bool)readyState)
            {
                startButton.interactable = false;
                return;
            }
        }
        startButton.interactable = true;
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        UpdatePlayerList();
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.gameObject.SetActive(true);
            startButton.onClick.AddListener(StartGame);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
        CheckAllPlayersReady();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
        CheckAllPlayersReady();
    }

    #endregion
}

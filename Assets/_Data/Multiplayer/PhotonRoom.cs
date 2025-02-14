using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhotonRoom : SaiMonoBehaviourPunCallbacks
{
    [Header("Room Setup")]
    [SerializeField] private UIPlayerItem playerItemPrefab;
    private List<UIPlayerItem> playerItems = new List<UIPlayerItem>();
    [Header("Setup UI")]
    [SerializeField] private Transform playerListContent;
    [SerializeField] private TextMeshProUGUI roomName;
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button readyButton;
    [SerializeField] private Button cancelButton;

    [Space]
    [SerializeField] private int lobbyPageIndex;
    [SerializeField] private int roomPageIndex;

    public override void OnJoinedRoom()
    {
        roomName.text = "Room: " + PhotonNetwork.CurrentRoom.Name;
        UIManager.Instance.GoToPage(roomPageIndex);
        RefreshPlayerList();

        if (PhotonNetwork.IsMasterClient)
        {
            ConfigureStartGameButton();
        }
        else
        {
            startGameButton.gameObject.SetActive(false);
        }
    }

    protected virtual void ConfigureStartGameButton()
    {
        startGameButton.gameObject.SetActive(true);
        startGameButton.onClick.RemoveAllListeners();
        startGameButton.onClick.AddListener(StartGame);
        startGameButton.interactable = false;
    }

    protected virtual void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("GameMultiplayScene");
        }
    }

    public override void OnLeftRoom()
    {
        UIManager.Instance.GoToPage(lobbyPageIndex);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RefreshPlayerList();
        CheckPlayersReadyState();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RefreshPlayerList();
        CheckPlayersReadyState();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        RefreshPlayerList();
        CheckPlayersReadyState();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        RefreshPlayerList();
        if (PhotonNetwork.IsMasterClient)
        {
            ConfigureStartGameButton();
        }
    }

    #region Private Helpers

    protected virtual void RefreshPlayerList()
    {
        ClearPlayerItems();

        if (PhotonNetwork.CurrentRoom == null) return;

        EnableButtons(false);

        List<Player> sortedPlayers = SortPlayersByActorNumber();

        foreach (var player in sortedPlayers)
        {
            UIPlayerItem playerItem = Instantiate(playerItemPrefab, playerListContent);
            playerItem.SetPlayerInfo(player);

            if (player.IsMasterClient)
            {
                playerItem.SetUpForMasterClient();
            }

            if (player == PhotonNetwork.LocalPlayer)
            {
                playerItem.ApplyLocalPlayerSetup(readyButton, cancelButton);
            }

            playerItem.UpdateReadyState();
            playerItems.Add(playerItem);
        }
    }

    protected static List<Player> SortPlayersByActorNumber()
    {
        List<Player> sortedPlayers = new List<Player>(PhotonNetwork.CurrentRoom.Players.Values);
        sortedPlayers.Sort((a, b) => a.ActorNumber.CompareTo(b.ActorNumber));
        return sortedPlayers;
    }

    protected virtual void EnableButtons(bool active)
    {
        readyButton.gameObject.SetActive(active);
        cancelButton.gameObject.SetActive(active);
    }

    protected virtual void ClearPlayerItems()
    {
        foreach (var item in playerItems)
        {
            Destroy(item.gameObject);
        }
        playerItems.Clear();
    }

    protected virtual void CheckPlayersReadyState()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        foreach (var player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            if (player.IsMasterClient) continue;

            if (!player.CustomProperties.TryGetValue("isReady", out object readyState) || !(bool)readyState)
            {
                startGameButton.interactable = false;
                return;
            }
        }
        startGameButton.interactable = true;
    }

    #endregion
}

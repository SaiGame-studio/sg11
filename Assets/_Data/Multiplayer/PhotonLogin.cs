using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonLogin : SaiMonoBehaviourPunCallbacks
{
    [SerializeField] protected int pageLobbyIndex;
    [SerializeField] protected UILoginPanel loginPanel;

    public virtual void Connect(string username)
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LocalPlayer.NickName = username;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        UIManager.Instance.GoToPage(pageLobbyIndex);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        UIManager.Instance.GoToPage(pageLobbyIndex - 1);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUILoginPanel();
    }

    private void LoadUILoginPanel()
    {
        if (this.loginPanel != null) return;
        this.loginPanel = FindObjectOfType<UILoginPanel>(true);
        Debug.LogWarning(transform.name + " LoadUILoginPanel", gameObject);
    }
}

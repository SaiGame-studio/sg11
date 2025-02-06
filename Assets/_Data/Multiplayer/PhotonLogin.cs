using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonLogin : SaiMonoBehaviourPunCallbacks
{
    [SerializeField] protected int pageLobbyIndex;
    [SerializeField] protected UILoginPanel loginPanel;

    public virtual void Login()
    {
        string name = loginPanel.inputUsername.text;

        if (!IsValidName(name)) return;

        loginPanel.warningText.gameObject.SetActive(false);
        loginPanel.inputUsername.text = "";

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LocalPlayer.NickName = name;
        PhotonNetwork.ConnectUsingSettings();
    }

    protected virtual bool IsValidName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            loginPanel.warningText.text = "Name cannot be empty!";
            loginPanel.warningText.gameObject.SetActive(true);
            return false;
        }
        return true;
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

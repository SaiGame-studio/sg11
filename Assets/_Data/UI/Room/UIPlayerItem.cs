using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerItem : SaiMonoBehaviourPunCallbacks
{
    [SerializeField] protected PlayerProfile playerProfile;
    private bool isReady = false;
    public Player player;

    [Header("UI Components")]
    [SerializeField] protected TextMeshProUGUI playerName;
    [SerializeField] protected Image background;
    [SerializeField] protected Color highlightColor;
    [SerializeField] protected Button readyButton;
    [SerializeField] protected Button cancelButton;
    [SerializeField] public Image readyIndicator;
    [SerializeField] public Image stateImage;
    [SerializeField] protected Sprite[] stateSprites;
    [SerializeField] public Image masterPlayerImage;

    public virtual void SetPlayerInfo(Player _player)
    {
        this.player = _player;
        playerProfile = new PlayerProfile();
        playerProfile.name = _player.NickName;
        this.playerName.text = this.playerProfile.name;
    }

    public void UpdateReadyState()
    {
        //if(player.IsMasterClient) return;

        if (player.CustomProperties.TryGetValue("isReady", out object readyState))
        {
            isReady = (bool)readyState;
        }
        UpdateReadyIndicator();
    }

    public virtual void ApplyLocalChange(Button _readyBtn, Button _cancelBtn)
    {
        this.background.color = this.highlightColor;
        this.readyButton = _readyBtn;
        this.cancelButton = _cancelBtn;
        if (player.IsMasterClient)
        {
            masterPlayerImage.gameObject.SetActive(true);
            readyIndicator.gameObject.SetActive(false);
            stateImage.gameObject.SetActive(false);
        }
        else
        {
            masterPlayerImage.gameObject.SetActive(false);
            readyButton.gameObject.SetActive(true);
            readyButton.onClick.RemoveAllListeners();
            readyButton.onClick.AddListener(ToggleReady);

            cancelButton.gameObject.SetActive(false);
            cancelButton.onClick.RemoveAllListeners();
            cancelButton.onClick.AddListener(ToggleReady);
        }
        this.UpdateReadyState();
    }

    private void ToggleReady()
    {
        isReady = !isReady;

        ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable
        {
            { "isReady", isReady }
        };

        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        UpdateReadyIndicator();
    }

    private void UpdateReadyIndicator()
    {
        if (player.IsMasterClient) return;

        stateImage.sprite = isReady ? stateSprites[1] : stateSprites[0];

        if (!player.IsLocal) return;

        readyButton.gameObject.SetActive(!isReady);
        cancelButton.gameObject.SetActive(isReady);
    }

    #region Components Loading
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerName();
        this.LoadBackground();
    }

    private void LoadPlayerName()
    {
        if (this.playerName != null) return;
        this.playerName = GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + " LoadPlayerName", gameObject);
    }

    private void LoadBackground()
    {
        if (this.background != null) return;
        this.background = GetComponent<Image>();
        Debug.LogWarning(transform.name + " LoadBackground", gameObject);
    }
    #endregion
}

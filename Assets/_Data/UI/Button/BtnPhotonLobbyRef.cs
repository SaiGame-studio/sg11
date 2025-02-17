using UnityEngine;

public class BtnPhotonLobbyRef : BaseButton
{
    [SerializeField] protected PhotonLobby photonLobby;

    protected override void OnClick()
    {
        // For override
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPhotonLobbyManager();
    }

    private void LoadPhotonLobbyManager()
    {
        if (this.photonLobby != null) return;
        this.photonLobby = FindObjectOfType<PhotonLobby>();
        Debug.LogWarning(transform.name + " LoadPhotonRoom", gameObject);
    }
}

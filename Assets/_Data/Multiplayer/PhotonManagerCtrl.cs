using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonManagerCtrl : SaiMonoBehaviour
{
    [Header("Photon Manager Ctrl")]
    private static PhotonManagerCtrl instance;
    public static PhotonManagerCtrl Instance => instance;

    [Header("Photon Class")]
    public PhotonLogin photonLogin;
    public PhotonRoom photonRoom;
    public PhotonLobby photonLobby;

    protected override void Awake()
    {
        base.Awake();
        if (PhotonManagerCtrl.instance != null) Debug.LogError("Only 1 PhotonManagerCtrl allow to exist");
        PhotonManagerCtrl.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPhotonLogin();
        this.LoadPhotonRoom();
        this.LoadPhotonLobby();
    }

    private void LoadPhotonLogin()
    {
        if (this.photonLogin != null) return;
        this.photonLogin = GetComponentInChildren<PhotonLogin>(true);
        Debug.LogWarning(transform.name + " LoadPhotonLogin", gameObject);
    }

    private void LoadPhotonRoom()
    {
        if (this.photonRoom != null) return;
        this.photonRoom = GetComponentInChildren<PhotonRoom>(true);
        Debug.LogWarning(transform.name + " LoadPhotonRoom", gameObject);
    }

    private void LoadPhotonLobby()
    {
        if (this.photonLobby != null) return;
        this.photonLobby = GetComponentInChildren<PhotonLobby>(true);
        Debug.LogWarning(transform.name + " LoadPhotonLobby", gameObject);
    }
}

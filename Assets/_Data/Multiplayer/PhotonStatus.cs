using UnityEngine;
using Photon.Pun;
using TMPro;

public class PhotonStatus : SaiMonoBehaviour
{
    public string photonStatus;
    public TextMeshProUGUI textStatus;

    // Update is called once per frame
    void Update()
    {
        this.photonStatus = PhotonNetwork.NetworkClientState.ToString();
        this.textStatus.text = this.photonStatus;
    }
}

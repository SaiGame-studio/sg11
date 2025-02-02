using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnLogoutPhoton : BaseButton
{
    protected override void OnClick()
    {
        PhotonNetwork.Disconnect();
    }
}

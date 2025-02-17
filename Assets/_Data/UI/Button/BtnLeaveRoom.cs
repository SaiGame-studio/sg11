using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnLeaveRoom : BtnPhotonLobbyRef
{
    protected override void OnClick()
    {
        photonLobby.LeaveRoom();
    }
}

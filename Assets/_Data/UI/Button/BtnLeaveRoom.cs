using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnLeaveRoom : BtnPhotonRoom
{
    protected override void OnClick()
    {
        photonRoom.Leave();
    }
}

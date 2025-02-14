using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnJoinRoom : BtnPhotonRoom
{
    protected override void OnClick()
    {
        photonRoom.Join();
    }
}

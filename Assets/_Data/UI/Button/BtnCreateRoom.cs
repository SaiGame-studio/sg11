using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCreateRoom : BtnPhotonRoom
{
    protected override void OnClick()
    {
        photonRoom.Create();
    }
}

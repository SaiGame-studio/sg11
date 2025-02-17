using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCreateRoom : BtnPhotonLobbyRef
{
    protected virtual void FixedUpdate()
    {
        UpdateButtonStatus();
    }

    protected virtual void UpdateButtonStatus()
    {
        this.button.interactable = this.photonLobby.CanCreateRoom();
    }

    protected override void OnClick()
    {
        photonLobby.CreateRoom();
    }
}

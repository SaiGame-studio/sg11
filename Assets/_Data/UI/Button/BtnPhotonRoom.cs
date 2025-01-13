using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnPhotonRoom : BaseButton
{
    [SerializeField] protected PhotonRoom photonRoom;

    protected override void OnClick()
    {
        // For override
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPhotonRoom();
    }

    private void LoadPhotonRoom()
    {
        if (this.photonRoom != null) return;
        this.photonRoom = FindObjectOfType<PhotonRoom>();
        Debug.LogWarning(transform.name + " LoadPhotonRoom", gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnLogin : BaseButton
{
    [SerializeField] protected PhotonLogin photonLogin;

    protected override void OnClick()
    {
        if (photonLogin == null) return;
        photonLogin.Login();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPhotonLogin();
    }

    private void LoadPhotonLogin()
    {
        if (this.photonLogin != null) return;
        this.photonLogin = FindObjectOfType<PhotonLogin>(true);
        Debug.LogWarning(transform.name + " LoadPhotonLogin", gameObject);
    }
}

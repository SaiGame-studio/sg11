using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhotonLogin : SaiMonoBehaviour
{
    [SerializeField] protected TMP_InputField inputUsername;
    [SerializeField] protected TMP_Text warningText;
    [SerializeField] protected Button btnLogin;

    public virtual void Login()
    {
        string name = inputUsername.text;

        if (string.IsNullOrEmpty(name))
        {
            warningText.gameObject.SetActive(true);
            return;
        }
        warningText.gameObject.SetActive(false);

        Debug.Log(transform.name + ": Login " + name);

        PhotonNetwork.LocalPlayer.NickName = name;
        PhotonNetwork.ConnectUsingSettings();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInputUsername();
        this.LoadWarningText();
        this.LoadBtnLogin();
    }

    private void LoadWarningText()
    {
        if (this.warningText != null) return;
        this.warningText = transform.Find("WarningText")?.GetComponent<TMP_Text>();
        Debug.LogWarning(transform.name + " LoadWarningText", gameObject);
    }

    private void LoadInputUsername()
    {
        if (this.inputUsername != null) return;
        this.inputUsername = transform.GetComponentInChildren<TMP_InputField>();
        Debug.LogWarning(transform.name + " LoadInputUsername", gameObject);
    }

    private void LoadBtnLogin()
    {
        if (this.btnLogin != null) return;
        this.btnLogin = transform.GetComponentInChildren<Button>();
        Debug.LogWarning(transform.name + " LoadBtnLogin", gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILoginPanel : SaiMonoBehaviour
{
    public TMP_InputField inputUsername;

    /// <summary>
    /// Login button that triggers the login process
    /// </summary>
    public Button loginBtn;

    protected override void Start()
    {
        base.Start();
        loginBtn.onClick.AddListener(OnLoginButtonClicked);
    }

    protected virtual void FixedUpdate()
    {
        this.CheckInputUsername();
    }

    protected void OnLoginButtonClicked()
    {
        string username = inputUsername.text.Trim();
        if (username.Length > 0)
        {
            PhotonManagerCtrl.Instance.photonLogin.Connect(username);
        }
    }

    protected virtual void CheckInputUsername()
    {
        string username = inputUsername.text.Trim();
        if (username.Length > 0)
        {
            this.UpdateButtonState(true);
        }
        else
        {
            this.UpdateButtonState(false);
        }
    }

    protected virtual void UpdateButtonState(bool interactable)
    {
        if (this.loginBtn.interactable != interactable)
        {
            this.loginBtn.interactable = interactable;
        }
    }

    #region Components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInputUsername();
        this.LoadLoginBtn();
    }

    private void LoadLoginBtn()
    {
        if (this.loginBtn != null) return;
        this.loginBtn = transform.GetComponentInChildren<Button>();
        Debug.LogWarning(transform.name + " LoadLoginBtn", gameObject);
    }

    private void LoadInputUsername()
    {
        if (this.inputUsername != null) return;
        this.inputUsername = transform.GetComponentInChildren<TMP_InputField>();
        Debug.LogWarning(transform.name + " LoadInputUsername", gameObject);
    }
    #endregion
}

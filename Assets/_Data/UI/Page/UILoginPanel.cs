using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILoginPanel : SaiMonoBehaviour
{
    public TMP_InputField inputUsername;
    public TMP_Text warningText;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInputUsername();
        this.LoadWarningText();
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
}

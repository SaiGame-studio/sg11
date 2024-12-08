using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEnterKonamiCode : SaiMonoBehaviour
{
    [SerializeField] protected GameObject container;
    [SerializeField] protected TextMeshProUGUI konamiCode;

    protected void FixedUpdate()
    {
        bool isChecking = InputManager.Instance.konamiCodeChecker.IsCheckingKonamiCode();
        this.ActivateUI(isChecking);

        if (isChecking)
        {
            this.konamiCode.text = InputManager.Instance.konamiCodeChecker.GetInputSequence();
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadContainer();
        this.LoadKonamiCode();
    }

    protected virtual void LoadContainer()
    {
        if (this.container != null) return;
        this.container = transform.Find("Container").gameObject;
        Debug.Log(transform.name + " LoadContainer", gameObject);
    }

    protected virtual void LoadKonamiCode()
    {
        if (this.konamiCode != null) return;
        this.konamiCode = transform.Find("KonamiCode").GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.name + " LoadKonamiCode", gameObject);
    }

    private void ActivateUI(bool active)
    {
        this.container.SetActive(active);
        this.konamiCode.gameObject.SetActive(active);
    }
}

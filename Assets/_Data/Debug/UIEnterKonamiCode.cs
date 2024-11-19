using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEnterKonamiCode : SaiMonoBehaviour
{
    [SerializeField] protected GameObject background;
    [SerializeField] protected TextMeshProUGUI title;
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
        this.LoadBackground();
        this.LoadTitle();
        this.LoadKonamiCode();
    }

    protected virtual void LoadBackground()
    {
        if (this.background != null) return;
        this.background = transform.Find("Background").gameObject;
        Debug.Log(transform.name + " LoadBackground", gameObject);
    }

    protected virtual void LoadTitle()
    {
        if (this.title != null) return;
        this.title = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.name + " LoadTitle", gameObject);
    }

    protected virtual void LoadKonamiCode()
    {
        if (this.konamiCode != null) return;
        this.konamiCode = transform.Find("KonamiCode").GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.name + " LoadKonamiCode", gameObject);
    }

    private void ActivateUI(bool active)
    {
        this.background.SetActive(active);
        this.title.gameObject.SetActive(active);
        this.konamiCode.gameObject.SetActive(active);
    }
}

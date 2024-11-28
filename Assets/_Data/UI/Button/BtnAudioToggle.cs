using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnAudioToggle : BaseButton
{
    [SerializeField] private Sprite soundOnIcon;
    [SerializeField] private Sprite soundOffIcon;

    [SerializeField] private Image btnIcon;

    protected override void OnClick()
    {
        //For override
    }

    protected void ToggleSprite(bool isMuted)
    {
        if (isMuted)
        {
            btnIcon.sprite = soundOffIcon;
        }
        else
        {
            btnIcon.sprite = soundOnIcon;
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadButtonIcon();
    }

    private void LoadButtonIcon()
    {
        if (this.btnIcon != null) return;
        btnIcon = transform.Find("Icon").GetComponent<Image>();
        Debug.Log(transform.name + " LoadButtonIcon", gameObject);
    }
}

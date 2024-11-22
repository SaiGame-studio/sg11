using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSound : BtnAudioToggle
{
    protected override void OnClick()
    {
        SoundManager.Instance.ToggleMute();
        ToggleSprite(SoundManager.Instance.IsMuted());
    }

    protected override void Start()
    {
        base.Start();
        ToggleSprite(SoundManager.Instance.IsMuted());
    }
}

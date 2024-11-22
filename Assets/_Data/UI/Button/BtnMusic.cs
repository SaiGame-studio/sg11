using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnMusic : BtnAudioToggle
{
    protected override void OnClick()
    {
        MusicManager.Instance.ToggleMute();
        ToggleSprite(MusicManager.Instance.IsMuted());
    }

    protected override void Start()
    {
        base.Start();
        ToggleSprite(MusicManager.Instance.IsMuted());
    }
}

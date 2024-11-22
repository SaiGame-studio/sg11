using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : SaiSingleton<MusicManager>
{
    private AudioSource audioSource;
    private float volume = .2f;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();

        float muteState = PlayerPrefs.GetFloat("musicMute", 0.0f);
        audioSource.volume = volume;
        audioSource.mute = muteState == 1.0f;
    }

    public void ToggleMute()
    {
        if (audioSource != null)
        {
            audioSource.mute = !audioSource.mute;

            PlayerPrefs.SetFloat("musicMute", audioSource.mute ? 1.0f : 0.0f);
            PlayerPrefs.Save();
        }
    }

    public bool IsMuted()
    {
        return audioSource.mute;
    }
}

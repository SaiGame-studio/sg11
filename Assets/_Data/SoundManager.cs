using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SaiSingleton<SoundManager>
{
    public enum Sound
    {
        click,
        linked,
        no_move,
        oho,
        win,
        finish
    }

    private AudioSource audioSource;
    private Dictionary<Sound, AudioClip> soundDict = new Dictionary<Sound, AudioClip>();
    private float volume = .5f;

    protected override void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();

        float muteState = PlayerPrefs.GetFloat("soundMute", 0.0f);
        audioSource.mute = muteState == 1.0f;
    }

    protected override void Start()
    {
        foreach (Sound sound in System.Enum.GetValues(typeof(Sound)))
        {
            soundDict[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
    }

    public void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot(soundDict[sound], volume);
    }

    public void ToggleMute()
    {
        if (audioSource != null)
        {
            audioSource.mute = !audioSource.mute;

            PlayerPrefs.SetFloat("soundMute", audioSource.mute ? 1.0f : 0.0f);
            PlayerPrefs.Save();
        }
    }

    public bool IsMuted()
    {
        return audioSource.mute;
    }
}


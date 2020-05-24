using System;
using UnityEngine;

/// <summary>
/// AudioManager singleton. Represents a list with all the sounds being called by the key.
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    [Header("All the sounds")]
    public Sound[] sounds;

    [Header("Music toggle")]
    public bool isEnabled = true;

    private Sound _backgroundMusic;

    private void Start()
    {
        isEnabled = PlayerPrefsX.GetBool("music", true);
        foreach (var sound in sounds)
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = sound.volume;
            audioSource.loop = sound.loop;
            audioSource.clip = sound.clip;
            audioSource.pitch = sound.pitch;
            audioSource.playOnAwake = false;
            sound.audioSource = audioSource;
        }
        _backgroundMusic = GetSound("BG");
        if (!isEnabled)
        {
            return;
        }

        Play("BG");
    }

    public void Play(string name)
    {
        var sound = GetSound(name);
        sound.audioSource.Play();
    }

    public void Stop(string name)
    {
        var sound = GetSound(name);
        if (sound.audioSource.isPlaying)
        {
            sound.audioSource.Stop();
        }
    }

    public void ToggleMusic()
    {
        isEnabled = !isEnabled;
        PlayerPrefsX.SetBool("music", isEnabled);

        if (!isEnabled && _backgroundMusic.audioSource.isPlaying)
        {
            Stop("BG");
        }
        else
        {
            Play("BG");
        }
    }

    public Sound GetSound(string name)
    {
        Sound sound = Array.Find(sounds, item => item.name == name);
        if (sound == null)
        {
            Debug.LogError("No Sound Found");
            return null;
        }
        else
        {
            return sound;
        }
    }
}
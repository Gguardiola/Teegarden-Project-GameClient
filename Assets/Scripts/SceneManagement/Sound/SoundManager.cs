using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] musicSounds;
    public Sound[] sfxSounds;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        musicSource.volume = SettingsContext.Instance.GetMusicVolume();
        sfxSource.volume = SettingsContext.Instance.GetSFXVolume();
    }

    public void PlayMusic(string soundName)
    {
        Sound sound = System.Array.Find(musicSounds, s => s.soundName == soundName);
        if (sound != null)
        {
            musicSource.clip = sound.soundClip;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music sound not found: " + soundName);
        }
    }
    
    public void StopMusic(string soundName)
    {
        Sound sound = System.Array.Find(musicSounds, s => s.soundName == soundName);
        if (sound != null && musicSource.isPlaying && musicSource.clip == sound.soundClip)
        {
            musicSource.Stop();
        }
    }

    public void PlaySFX(string soundName)
    {
        Sound sound = System.Array.Find(sfxSounds, s => s.soundName == soundName);
        if (sound != null)
        {
            sfxSource.PlayOneShot(sound.soundClip);
        }
        else
        {
            Debug.LogWarning("SFX sound not found: " + soundName);
        }
    }
        
}
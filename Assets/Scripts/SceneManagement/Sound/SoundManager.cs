using System;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] musicSounds;
    public Sound[] sfxSounds;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource[] sceneSources;

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
        ManageSceneSources(SettingsContext.Instance.GetSFXVolume());
    }
    
    public void SubscribeToSceneSources(AudioSource source)
    {
        sceneSources = sceneSources.Append(source).ToArray();
        ManageSceneSources(SettingsContext.Instance.GetSFXVolume());
    }
    
    public void ManageSceneSources(float volume)
    {
        if (sceneSources != null && sceneSources.Length > 0)
        {
            foreach (var source in sceneSources)
            {
                source.volume = SettingsContext.Instance.GetSFXVolume();
            }            
        }
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
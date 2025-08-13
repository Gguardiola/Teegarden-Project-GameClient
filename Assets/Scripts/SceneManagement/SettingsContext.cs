using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsContext : MonoBehaviour
{
    public static SettingsContext Instance;
    public bool isMenuLikeGameplay = false;
    private SettingsConfig settingsConfig;
    private Slider musicSlider;
    private Slider effectsSlider;
    private Slider mouseSensibilitySlider;
    
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
    
    void Start()
    {
        settingsConfig = Resources.Load<SettingsConfig>("Settings/SettingsConfig");

        LoadPlayerSettings();
        if(mouseSensibilitySlider != null) UpdateMouseSensibilitySettings();
    }

    public void TriggerSliders()
    {
        FindSliders();
        UpdateSliders();
    }
    
    private void FindSliders()
    {
        musicSlider = GameObject.Find("UI_MusicSlider").GetComponent<Slider>();
        effectsSlider = GameObject.Find("UI_SFXSlider").GetComponent<Slider>();
        mouseSensibilitySlider = GameObject.Find("UI_SensibilitySlider").GetComponent<Slider>();

        if (musicSlider == null || effectsSlider == null || mouseSensibilitySlider == null)
        {
            Debug.LogError("One or more sliders not found in the scene.");
        }
    }

    private void UpdateSliders()
    {
        musicSlider.value = settingsConfig.musicVolume;
        effectsSlider.value = settingsConfig.sfxVolume;
        mouseSensibilitySlider.value = settingsConfig.xSensitivity;
    }
    
    private void LoadPlayerSettings()
    {      
        string settingsPath = Application.persistentDataPath + "/settings.json";
        if (System.IO.File.Exists(settingsPath))
        {
            string json = System.IO.File.ReadAllText(settingsPath);
            SettingsConfigWrapper settingsLoad = JsonUtility.FromJson<SettingsConfigWrapper>(json);
            settingsConfig.musicVolume = settingsLoad.musicVolume;
            settingsConfig.sfxVolume = settingsLoad.sfxVolume;
            settingsConfig.xSensitivity = settingsLoad.xSensitivity;
        }
        else
        {
            SavePlayerSettings();
        }
    }

    private void SavePlayerSettings()
    {
        string settingsPath = Application.persistentDataPath + "/settings.json";
        SettingsConfigWrapper settingsWrapper = new SettingsConfigWrapper
        {
            musicVolume = settingsConfig.musicVolume,
            sfxVolume = settingsConfig.sfxVolume,
            xSensitivity = settingsConfig.xSensitivity
        };
        string json = JsonUtility.ToJson(settingsWrapper, true);
        System.IO.File.WriteAllText(settingsPath, json);
    }

    public void UpdateMusicSettings()
    {
        settingsConfig.musicVolume = musicSlider.value;
        SoundManager.Instance.musicSource.volume = musicSlider.value;
        SavePlayerSettings();
    }
    
    public void UpdateSFXSettings()
    {
        settingsConfig.sfxVolume = effectsSlider.value;
        SoundManager.Instance.sfxSource.volume = effectsSlider.value;
        SavePlayerSettings();
    }
    
    public void UpdateMouseSensibilitySettings()
    {
        settingsConfig.xSensitivity = mouseSensibilitySlider.value;
        if (!isMenuLikeGameplay)
        {
            GameObject.Find("Player").GetComponent<PlayerLook>().xSensitivity = mouseSensibilitySlider.value;
        }
        SavePlayerSettings();
    }
    
    public float GetMusicVolume()
    {
        return settingsConfig.musicVolume;
    }
    
    public float GetSFXVolume()
    {
        return settingsConfig.sfxVolume;
    }

    public float GetMouseSensibility()
    {
        return settingsConfig.xSensitivity;
    }
}

using UnityEngine;
[CreateAssetMenu(fileName = "SettingsConfig", menuName = "Configs/Settings")]
public class SettingsConfig : ScriptableObject
{
    [Header("Sound Settings")]
    public float musicVolume = 1.0f;
    public float sfxVolume = 1.0f;
    [Header("Gameplay Settings")]
    public float xSensitivity = 30f;

}
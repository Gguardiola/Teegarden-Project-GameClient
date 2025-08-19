using System;
using UnityEngine;

public class LevelContext : MonoBehaviour
{
    public static LevelContext Instance;
    public string currentLevelName;

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
        SetLevelToShip();
    }

    public void SetLevelToShip()
    {
        currentLevelName = "The Dreyfus";
        SoundManager.Instance.PlayMusic("Ship");
    }
    
    public void SetLevelToStation()
    {
        currentLevelName = "The Station";
        SoundManager.Instance.StopMusic("Ship");
        SoundManager.Instance.PlayMusic("Station");
    }
}

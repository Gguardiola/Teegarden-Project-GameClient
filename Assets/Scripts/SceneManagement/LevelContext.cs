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
}

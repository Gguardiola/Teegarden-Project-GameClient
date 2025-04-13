using UnityEngine;
[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Player")]
public class PlayerConfig : ScriptableObject
{
    public static PlayerConfig Instance { get; private set; }

    [Header("Stats")]
    public float maxHealth = 100f;
    public float speed = 5.0f;
    public float jumpHeight = 1.5f;
    //categoria interaccion, movimiento, UI
#if UNITY_EDITOR
    [UnityEditor.MenuItem("Tools/Select PlayerConfig")]
    public static void SelectConfig()
    {
        var path = "Assets/Resources/PlayerConfig.asset";
        Instance = UnityEditor.AssetDatabase.LoadAssetAtPath<PlayerConfig>(path);
    }
#endif

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void LoadInstance()
    {
        Instance = Resources.Load<PlayerConfig>("PlayerConfig");
        if (Instance == null)
        {
            Debug.LogError("Asset not found!");
        }
    }
}
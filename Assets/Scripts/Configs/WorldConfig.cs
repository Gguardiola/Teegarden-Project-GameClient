using UnityEngine;
[CreateAssetMenu(fileName = "WorldConfig", menuName = "Configs/World")]
public class WorldConfig : ScriptableObject
{
    public static WorldConfig Instance { get; private set; }

    [Header("Parameters")]
    public float gravity = -9.81f;
#if UNITY_EDITOR
    [UnityEditor.MenuItem("Tools/Select WorldConfig")]
    public static void SelectConfig()
    {
        var path = "Assets/Resources/WorldConfig.asset";
        Instance = UnityEditor.AssetDatabase.LoadAssetAtPath<WorldConfig>(path);
    }
#endif

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void LoadInstance()
    {
        Instance = Resources.Load<WorldConfig>("WorldConfig");
        if (Instance == null)
        {
            Debug.LogError("Asset not found!");
        }
    }
}
using TMPro;
using UnityEngine;

public class GameVersion : MonoBehaviour
{
    private TextMeshProUGUI versionText;
    void Start()
    {
        versionText = GetComponent<TextMeshProUGUI>();
        UpdateGameVersion();
    }

    private void UpdateGameVersion()
    {
        string version = Application.version;
        if (versionText != null)
        {
            versionText.text = $"v{version}";
        }
    }
}

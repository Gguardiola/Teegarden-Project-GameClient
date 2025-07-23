using TMPro;
using UnityEngine;

public class LastUpdatesLoader : MonoBehaviour
{  
    private TextMeshProUGUI lastUpdatesText;
    void Start()
    {
        lastUpdatesText = GetComponent<TextMeshProUGUI>();
        
        if (lastUpdatesText != null)
        {
            lastUpdatesText.text += LoadLastUpdateFromResources();
        }
        
    }
    
    private string LoadLastUpdateFromResources()
    {
        TextAsset lastUpdateAsset = Resources.Load<TextAsset>("LastUpdates/lastupdates");
        if (lastUpdateAsset != null)
        {
            return lastUpdateAsset.text;
        }
        else
        {
            return "No updates available.";
        }
    }
    
    
}

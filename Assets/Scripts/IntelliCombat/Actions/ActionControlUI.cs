
using IntelliCombat.Actions;
using UnityEngine;

public class ActionControlUI : MonoBehaviour
{
    public GameObject actionButtons;
    public GameObject inActionButtons;
    public void Start()
    {
        inActionButtons.SetActive(false);
            
    }
        
    public void HideActionButtons(bool isActionInProgress)
    {
        if (isActionInProgress)
        {
            actionButtons.SetActive(false);
            inActionButtons.SetActive(true);
        }
    }

    public void ShowActionButtons(bool isActionInProgress)
    {
        if (!isActionInProgress)
        {
            actionButtons.SetActive(true);
            inActionButtons.SetActive(false);
        }
    }

}
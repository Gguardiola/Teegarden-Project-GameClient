using IntelliCombat.Actions;
using UnityEngine;

public class PlayerSkipTurnAction : SkipTurnAction
{
    public GameObject confirmLabel;
    public ActionControlUI actionControlUI;

    public override void CancelAction()
    {
        isActionInProgress = false;
        HideSkipTurnLabels();
        actionControlUI.ShowActionButtons(isActionInProgress);
    }

    public override void PerformAction()
    {
        if (isActionInProgress) return;
        isActionInProgress = true;
        ShowSkipTurnLabels();
        actionControlUI.HideActionButtons(isActionInProgress);
    }
    
    public override bool EndTurnAction()
    {
        isActionInProgress = false;
        HideSkipTurnLabels(); 
        return true;
    }
    
    private void HideSkipTurnLabels()
    {
        confirmLabel.SetActive(false);
    }

    private void ShowSkipTurnLabels()
    {
        confirmLabel.SetActive(true);
    }
}
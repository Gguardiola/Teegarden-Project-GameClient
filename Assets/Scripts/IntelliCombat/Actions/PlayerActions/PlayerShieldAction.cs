using IntelliCombat.Actions;
using UnityEngine;

public class PlayerShieldAction : ShieldAction
{

    public GameObject confirmLabel;
    public ActionControlUI actionControlUI;
    public override void CancelAction()
    {
        isActionInProgress = false;
        HideShieldLabels();
        actionControlUI.ShowActionButtons(isActionInProgress);
    }

    public override bool EndTurnAction()
    {
        isActionInProgress = false;
        HideShieldLabels();
        return true;
    }

    public override void PerformAction()
    {
        if (isActionInProgress) return;
        isActionInProgress = true;
        ShowShieldLabels();
        actionControlUI.HideActionButtons(isActionInProgress);
    }
    
    private void HideShieldLabels()
    {
        confirmLabel.SetActive(false);
    }
    
    private void ShowShieldLabels()
    {
        confirmLabel.SetActive(true);
    }
}
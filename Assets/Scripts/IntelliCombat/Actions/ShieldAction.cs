using IntelliCombat.Actions;
using UnityEngine;

public class ShieldAction : Action
{
    [SerializeField]
    private float energyCost = 20f;

    public float GetEnergyCost()
    {
        return energyCost;
    }
    public override void CancelAction()
    {
        isActionInProgress = false;
    }

    public override bool EndTurnAction()
    {
        isActionInProgress = false;
        return true;
    }

    public override void PerformAction()
    {
        if (isActionInProgress) return;
        isActionInProgress = true;
    }
    
    public override bool ResolveAction(TurnResolver turnResolver)
    {
        return turnResolver.ResolveTurn(this);
    }
}
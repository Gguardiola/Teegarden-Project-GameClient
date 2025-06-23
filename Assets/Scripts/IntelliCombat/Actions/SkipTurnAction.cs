
using IntelliCombat.Actions;
using UnityEngine;

public class SkipTurnAction : Action
{
    [SerializeField]
    private float regenerationAmount = 20f;
    
    public float GetRegenerationAmount()
    {
        return regenerationAmount;
    }
    public override void CancelAction()
    {
        isActionInProgress = false;
    }

    public override void PerformAction()
    {
        if (isActionInProgress) return;
        isActionInProgress = true;
    }
    
    public override bool EndTurnAction()
    {
        isActionInProgress = false;
        return true;
    }
    
    public override bool ResolveAction(TurnResolver turnResolver)
    {
        return turnResolver.ResolveTurn(this);
    }
}

using System.Collections.Generic;
using IntelliCombat.Actions;
using UnityEngine;

public class AttackAction : Action
{
    public List<AbilityData> playerAbilities = new List<AbilityData>();
    [HideInInspector]
    public AbilityData selectedAbility;
    [HideInInspector]
    public bool isAbilitySelected;
    
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
        if (isAbilitySelected)
        {
            isActionInProgress = false;
            selectedAbility = null;
            return true;
        }

        return false;
    }
    
    public override bool ResolveAction(TurnResolver turnResolver)
    {
        return turnResolver.ResolveTurn(this);
    }

    private void SelectAbility(AbilityData ability)
    {
        selectedAbility = ability;
    }

}
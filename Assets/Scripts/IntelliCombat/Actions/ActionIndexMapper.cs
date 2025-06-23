using System.Collections.Generic;
using IntelliCombat.Actions;
using UnityEngine;

public class ActionIndexMapper
{
    private readonly List<AbilityData> abilities;
    private readonly AttackAction attackAction;
    private readonly ShieldAction shieldAction;
    private readonly SkipTurnAction skipTurnAction;

    public ActionIndexMapper(List<AbilityData> abilities, AttackAction attackAction, ShieldAction shieldAction, SkipTurnAction skipTurnAction)
    {
        this.abilities = abilities;
        this.attackAction = attackAction;
        this.shieldAction = shieldAction;
        this.skipTurnAction = skipTurnAction;
    }

    public Action GetActionFromIndex(int index)
    {
        Debug.Log("Action index received: " + index);
        if (index == 0)
        {
            return shieldAction;
        }
        else if (index == 1)
        {
            return skipTurnAction;
        }

        int adjustedIndex = index - 2;
        if (adjustedIndex >= 0 && adjustedIndex < abilities.Count)
        {
            attackAction.selectedAbility = abilities[adjustedIndex];
            attackAction.isAbilitySelected = true;
            return attackAction;
        }

        Debug.LogWarning("Invalid model action index. Defaulting to skip.");
        return skipTurnAction;
    }
}
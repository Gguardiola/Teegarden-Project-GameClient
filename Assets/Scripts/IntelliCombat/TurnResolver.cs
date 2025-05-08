
using IntelliCombat.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnResolver
{
    private CombatManager combatManager;
    
    public TurnResolver(CombatManager combatManager)
    {
        this.combatManager = combatManager;
    }
    
    public bool ResolveTurn(Action action)
    {
        Debug.Log("Resolving Combat Action");
        return true;
    }

    public bool ResolveTurn(AttackAction action)
    {
        AbilityData selectedAbility = action.selectedAbility;
        if (selectedAbility == null) return false;
        if (combatManager.isPlayerTurn)
        {
            if (!IsAffordable(action, combatManager.playerAvatar)) return false;
            if (selectedAbility.isHealAbility)
            {
                combatManager.playerAvatar.Heal(selectedAbility.healAmount);
                combatManager.playerAvatar.UseEnergy(selectedAbility.cost);
                combatManager.SetLastActionMessage($"You healed yourself with {selectedAbility.abilityName} for {selectedAbility.healAmount} health.");
            }
            else
            {
                if (combatManager.enemyAvatar.isShielded && !selectedAbility.isShieldIgnored)
                {
                    combatManager.playerAvatar.UseEnergy(selectedAbility.cost);
                    combatManager.SetLastActionMessage($"The enemy is shielded!! The ability {selectedAbility.abilityName} did not affect the enemy.");
                }
                else
                {
                    combatManager.enemyAvatar.TakeDamage(selectedAbility.damage);
                    combatManager.playerAvatar.UseEnergy(selectedAbility.cost);
                    combatManager.SetLastActionMessage($"You attacked the enemy with {selectedAbility.abilityName} for {selectedAbility.damage} damage.");                    
                }
            }
        }
        else
        {
            if (!IsAffordable(action, combatManager.enemyAvatar)) return false;
            if (selectedAbility.isHealAbility)
            {
                combatManager.enemyAvatar.Heal(selectedAbility.healAmount);
                combatManager.enemyAvatar.UseEnergy(selectedAbility.cost);
                combatManager.SetLastActionMessage($"The enemy healed itself with {selectedAbility.abilityName} for {selectedAbility.healAmount} health.");
            }
            else
            {
                if (combatManager.playerAvatar.isShielded && !selectedAbility.isShieldIgnored)
                {
                    combatManager.enemyAvatar.UseEnergy(selectedAbility.cost);
                    combatManager.SetLastActionMessage($"You are shielded!! The ability {selectedAbility.abilityName} did not affect you.");
                }
                else
                {
                    combatManager.playerAvatar.TakeDamage(selectedAbility.damage);
                    combatManager.enemyAvatar.UseEnergy(selectedAbility.cost);
                    combatManager.SetLastActionMessage($"The enemy attacked you with {selectedAbility.abilityName} for {selectedAbility.damage} damage.");                 
                }
            }
        }
        
        combatManager.SetEnemyStats();
        combatManager.SetPlayerStats();
        return true;


    }
    public bool ResolveTurn(ShieldAction action)
    {
        if (combatManager.isPlayerTurn)
        {
            combatManager.playerAvatar.isShielded = true;
            combatManager.SetLastActionMessage($"You shielded yourself.");
            combatManager.playerAvatar.UseEnergy(10);
        }
        else
        {
            Debug.Log("Enemy is shielding itself");
            combatManager.enemyAvatar.isShielded = true;
            combatManager.SetLastActionMessage($"The enemy shielded itself.");
            combatManager.enemyAvatar.UseEnergy(10);

        }
        
        combatManager.SetEnemyStats();
        combatManager.SetPlayerStats();
        return true;
    }
    public bool ResolveTurn(SkipTurnAction action)
    {
        if (combatManager.isPlayerTurn)
        {
            combatManager.SetLastActionMessage("You skipped your turn. Recovering energy...");
            combatManager.playerAvatar.RegenerateEnergy(10);
        }
        else
        {
            combatManager.SetLastActionMessage("The enemy skipped its turn. Recovers energy...");
            combatManager.enemyAvatar.RegenerateEnergy(10);
        }
        combatManager.SetEnemyStats();
        combatManager.SetPlayerStats();
        return true;
    }

    private bool IsAffordable(AttackAction action, Avatar currentAvatar)
    {
        AbilityData selectedAbility = action.selectedAbility;
        if (currentAvatar.GetEnergy() < selectedAbility.cost)
        {
            combatManager.SetTemporalLastActionMessage("NOT ENOUGH ENERGY TO PERFORM THIS ACTION!!!.");
            return false;
        }

        return true;
    }
    
}

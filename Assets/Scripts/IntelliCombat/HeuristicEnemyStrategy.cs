using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using IntelliCombat.Actions;

public class HeuristicEnemyStrategy : MonoBehaviour, IEnemyStrategy
{
    public CombatManager combatManager;
    private ShieldAction shieldAction;
    private AttackAction attackAction;
    private SkipTurnAction skipTurnAction;

    private void Awake()
    {
        shieldAction = GetComponent<ShieldAction>();
        attackAction = GetComponent<AttackAction>();
        skipTurnAction = GetComponent<SkipTurnAction>();
        combatManager.SetEnemyInitialAbilities(attackAction.abilities);
    }

    public void Decide(System.Action<Action> onDecisionMade)
    {
        StartCoroutine(HeuristicDecisionCoroutine(onDecisionMade));
    }

    private IEnumerator HeuristicDecisionCoroutine(System.Action<Action> callback)
    {
        yield return new WaitForSeconds(2f);

        float enemyHP = combatManager.enemyAvatar.GetHealth();
        float enemyMaxHP = combatManager.enemyAvatar.GetMaxHealth();
        float enemyEnergy = combatManager.enemyAvatar.GetEnergy();
        float playerHP = combatManager.playerAvatar.GetHealth();

        Action decision = skipTurnAction;
        List<AbilityData> usableAbilities = attackAction.abilities.FindAll(a => a.cost <= enemyEnergy);
        List<AbilityData> healingAbilities = usableAbilities.FindAll(a => a.isHealAbility);
        List<AbilityData> attackAbilities = usableAbilities.FindAll(a => !a.isHealAbility && a.damage > 0);
        float rand = Random.value;

        if (usableAbilities.Count == 0)
        {
            decision = skipTurnAction;
        }
        else if (enemyHP <= enemyMaxHP * 0.4f && healingAbilities.Count > 0)
        {
            decision = rand < 0.7f ? attackAction : skipTurnAction;
            attackAction.selectedAbility = healingAbilities[Random.Range(0, healingAbilities.Count)];
            attackAction.isAbilitySelected = true;
        }
        else if (playerHP <= 15 && attackAbilities.Count > 0)
        {
            decision = rand < 0.8f ? attackAction : skipTurnAction;
            attackAction.selectedAbility = attackAbilities.OrderByDescending(a => a.damage).First();
            attackAction.isAbilitySelected = true;
        }
        else if (attackAbilities.Count > 0)
        {
            if (rand < 0.6f)
            {
                attackAction.selectedAbility = attackAbilities[Random.Range(0, attackAbilities.Count)];
                attackAction.isAbilitySelected = true;
                decision = attackAction;
            }
            else if (healingAbilities.Count > 0 && rand < 0.8f)
            {
                attackAction.selectedAbility = healingAbilities[Random.Range(0, healingAbilities.Count)];
                attackAction.isAbilitySelected = true;
                decision = attackAction;
            }
            else
            {
                decision = skipTurnAction;
            }
        }

        callback?.Invoke(decision);
    }
}
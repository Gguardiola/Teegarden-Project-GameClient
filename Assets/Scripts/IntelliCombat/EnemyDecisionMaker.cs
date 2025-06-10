using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IntelliCombat.Actions;
using UnityEngine;

public class EnemyDecisionMaker : MonoBehaviour
{
    private ShieldAction shieldAction;
    private AttackAction attackAction;
    private SkipTurnAction skipTurnAction;
    [SerializeField]
    public CombatManager combatManager;
    private void Awake()
    { 
        shieldAction = GetComponent<ShieldAction>();
        attackAction = GetComponent<AttackAction>();
        skipTurnAction = GetComponent<SkipTurnAction>();
    }
    public void Decide(System.Action<Action> onDecisionMade)
    {
        StartCoroutine(WaitForDecision(onDecisionMade));
    }

    private IEnumerator WaitForDecision(System.Action<Action> callback)
    {
        yield return new WaitForSeconds(2f);
        float enemyHP = combatManager.enemyAvatar.GetHealth();
        float enemyMaxHP = combatManager.enemyAvatar.GetMaxHealth();
        float enemyEnergy = combatManager.enemyAvatar.GetEnergy();
        float playerHP = combatManager.playerAvatar.GetHealth();

        Action decision = skipTurnAction;
        
        List<AbilityData> usableAbilities = attackAction.abilities
            .FindAll(a => a.cost <= enemyEnergy);

        List<AbilityData> healingAbilities = usableAbilities
            .FindAll(a => a.isHealAbility);

        List<AbilityData> attackAbilities = usableAbilities
            .FindAll(a => !a.isHealAbility && a.damage > 0);

        float rand = Random.value;

        if (usableAbilities.Count == 0)
        {
            decision = skipTurnAction;
        }
        else if (enemyHP <= enemyMaxHP * 0.4f && healingAbilities.Count > 0)
        {
            if (rand < 0.7f)
            {
                attackAction.selectedAbility = healingAbilities[Random.Range(0, healingAbilities.Count)];
                attackAction.isAbilitySelected = true;
                decision = attackAction;
            }
            else if (attackAbilities.Count > 0)
            {
                attackAction.selectedAbility = attackAbilities[Random.Range(0, attackAbilities.Count)];
                attackAction.isAbilitySelected = true;
                decision = attackAction;
            }
            else
            {
                decision = skipTurnAction;
            }
        }
        else if (playerHP <= 15 && attackAbilities.Count > 0)
        {
            if (rand < 0.8f)
            {
                AbilityData maxDamage = attackAbilities.OrderByDescending(a => a.damage).First();
                attackAction.selectedAbility = maxDamage;
                attackAction.isAbilitySelected = true;
                decision = attackAction;
            }
            else
            {
                decision = skipTurnAction;
            }
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
   
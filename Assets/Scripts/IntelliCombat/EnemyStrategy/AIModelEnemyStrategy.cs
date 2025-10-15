using UnityEngine;
using IntelliCombat.Actions;
using System.Collections;
using System.Linq;

public class AIModelEnemyStrategy : MonoBehaviour, IEnemyStrategy
{
    public CombatManager combatManager;
    private ShieldAction shieldAction;
    private AttackAction attackAction;
    private SkipTurnAction skipTurnAction;
    private ActionIndexMapper actionMapper;

    private void Awake()
    {
        shieldAction = GetComponent<ShieldAction>();
        attackAction = GetComponent<AttackAction>();
        skipTurnAction = GetComponent<SkipTurnAction>();
        actionMapper = new ActionIndexMapper(attackAction.abilities, attackAction, shieldAction, skipTurnAction);
    }

    public void Decide(System.Action<Action> onDecisionMade)
    {
        StartCoroutine(ModelDecisionCoroutine(onDecisionMade));
    }
    private IEnumerator ModelDecisionCoroutine(System.Action<Action> callback)
    {
        yield return new WaitForSeconds(1f);

        float[] stateVector = StateEncoder.Encode(combatManager);
        Debug.Log("STATE: [" + string.Join(",", stateVector) + "]");
        float[] normalized = new float[stateVector.Length];
        for (int i = 0; i < stateVector.Length; i++)
        {
            normalized[i] = stateVector[i] / 100f;
        }

        Debug.Log("Normalized: [" + string.Join(",", normalized) + "]");

        float[] qValues = AIModelLoader.GetIntellicombatModel().PredictRawQValues(normalized);
        
        int[] orderedIndices = Enumerable.Range(0, qValues.Length)
            .OrderByDescending(i => qValues[i])
            .ToArray();

        Action selectedAction = null;

        foreach (int index in orderedIndices)
        {
            Action candidate = actionMapper.GetActionFromIndex(index);
            if (CanBeExecutedBy(candidate, combatManager.enemyAvatar))
            {
                selectedAction = candidate;
                break;
            }
        }
        
        if (selectedAction == null)
        {
            Debug.LogWarning("Invalid actions, using default SkipTurnAction.");
            selectedAction = skipTurnAction;
        }

        callback?.Invoke(selectedAction);
    }

    private bool CanBeExecutedBy(Action currentAction, Avatar enemyAvatar)
    {
        float cost = 999;
        if (currentAction is AttackAction)
        {
            AbilityData selectedAbility = ((AttackAction)currentAction).selectedAbility; 
            cost = selectedAbility.cost;
        } else if (currentAction is ShieldAction)
        {
            cost = ((ShieldAction)currentAction).GetEnergyCost();
        }
        
        if (enemyAvatar.GetEnergy() < cost)
        {
            return false;
        }
        
        return true;
    }
}
using System.Collections;
using IntelliCombat.Actions;
using UnityEngine;

public class EnemyDecisionMaker : MonoBehaviour
{
    private ShieldAction shieldAction;
    private void Awake()
    { 
        shieldAction = GetComponent<ShieldAction>();
    }
    public void Decide(System.Action<Action> onDecisionMade)
    {
        StartCoroutine(WaitForDecision(onDecisionMade));
    }

    private IEnumerator WaitForDecision(System.Action<Action> callback)
    {
        yield return new WaitForSeconds(5f);
        Action decision = shieldAction;
        callback?.Invoke(decision);
    }
    
}
   
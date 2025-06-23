using IntelliCombat.Actions;
using UnityEngine;

public class EnemyDecisionMaker : MonoBehaviour
{
    private IEnemyStrategy enemyStrategy;
    private HeuristicEnemyStrategy heuristic;
    private AIModelEnemyStrategy model;
    private void Awake()
    {
        heuristic = GetComponent<HeuristicEnemyStrategy>();
        model = GetComponent<AIModelEnemyStrategy>();

        if (AIModelLoader.GetIntellicombatModel().IsReady)
        {
            enemyStrategy = model;
        }
        else
        {
            enemyStrategy = heuristic;
        }
    }

    public void Decide(System.Action<Action> onDecisionMade)
    {
        enemyStrategy.Decide(onDecisionMade);
    }
}
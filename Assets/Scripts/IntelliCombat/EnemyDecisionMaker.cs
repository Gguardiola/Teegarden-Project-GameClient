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
            Debug.Log("AI Model is ready, using AIModelEnemyStrategy.");
            enemyStrategy = model;
        }
        else
        {
            Debug.Log("AI Model is not ready, using HeuristicEnemyStrategy.");
            enemyStrategy = heuristic;
        }
    }

    public void Decide(System.Action<Action> onDecisionMade)
    {
        enemyStrategy.Decide(onDecisionMade);
    }
}
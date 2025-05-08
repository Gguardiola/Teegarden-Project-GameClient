using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimer;
    private float searchTime = 8f;
    public override void Enter()
    {
        enemyAI.Agent.SetDestination(enemyAI.LastKnownPlayerPosition);
    }

    public override void Perform()
    {
        CheckHealth();
        if (enemyAI.CanSeePlayer())
        {
            enemyStateMachine.ChangeState(enemyAI.attackState);  
        }

        if (enemyAI.Agent.remainingDistance <= enemyAI.Agent.stoppingDistance)
        {
            searchTimer += Time.deltaTime;
            if (searchTimer > searchTime)
            {
                enemyStateMachine.ChangeState(enemyAI.patrolState);
            }
        }
    }

    public override void Exit()
    {
    }
}

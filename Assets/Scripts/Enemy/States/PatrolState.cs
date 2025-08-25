using System.Collections;
using UnityEngine;

public class PatrolState : BaseState
{
    public int waypointIndex;
    public float waitTimer;

    public override void Enter()
    {
    }

    public override void Perform()
    {
        CheckHealth();
        PlayStateSFX();
        PatrolCycle();
        if (enemyAI.CanSeePlayer())
        {
            enemyStateMachine.ChangeState(enemyAI.attackState);
        }
    }

    public override void Exit()
    {
    }

    public void PatrolCycle()
    {
        if (enemyAI.agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 3)
            {
                if (waypointIndex < enemyAI.path.waypoints.Count - 1)
                {
                    waypointIndex++;
                }
                else
                {
                    waypointIndex = 0;
                }
                enemyAI.agent.SetDestination(enemyAI.path.waypoints[waypointIndex].position);
                waitTimer = 0;
                
            }
        }
    }
}

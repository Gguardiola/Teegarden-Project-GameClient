using UnityEngine;

public class PatrolState : BaseState
{
    public int waypointIndex;
    public float waitTimer;

    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void Perform()
    {
        PatrolCycle();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void PatrolCycle()
    {
        if (enemyAI.Agent.remainingDistance < 0.2f)
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
                enemyAI.Agent.SetDestination(enemyAI.path.waypoints[waypointIndex].position);
                waitTimer = 0;
                
            }
        }
    }
}

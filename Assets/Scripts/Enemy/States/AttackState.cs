using UnityEngine;

public class AttackState : BaseState
{
    public int waypointIndex;
    public float moveTimer;
    
    private float losePlayerTimer;
    private float shootTimer;
    public float loseTime = 3f;
    public float shootSpeed = 20f;
    public float shootAccuracy = 3f;

    public override void Enter()
    {
    }

    public override void Perform()
    {
        CheckHealth();
        if (enemyAI.CanSeePlayer())
        {
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shootTimer += Time.deltaTime;
            enemyAI.transform.LookAt(enemyAI.player.transform);
            if (shootTimer > enemyAI.bulletSpawnRate)
            {
                Shoot();
            }
            if (moveTimer > Random.Range(3, 7))
            {
                enemyAI.agent.SetDestination(enemyAI.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
            enemyAI.lastKnownPlayerPosition = enemyAI.player.transform.position;
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > loseTime)
            {
                enemyStateMachine.ChangeState(enemyAI.searchState);
            }
        }
    }

    public override void Exit()
    {
    }
    
    public void Shoot()
    {
        Transform bulletSpawner = enemyAI.bulletSpawner;
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, bulletSpawner.position, enemyAI.transform.rotation);
        Vector3 shootDirection = (enemyAI.player.transform.position - bulletSpawner.position).normalized;
        bullet.GetComponent<Rigidbody>().linearVelocity = Quaternion.AngleAxis(Random.Range(-shootAccuracy, shootAccuracy), Vector3.up) * shootDirection * shootSpeed;
        shootTimer = 0;
    }
    
}

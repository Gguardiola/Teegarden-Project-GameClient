using UnityEngine;

public class AttackState : BaseState
{
    public int waypointIndex;
    public float moveTimer;
    
    private float losePlayerTimer;
    private float shootTimer;
    public float loseTime = 5f;
    public float shootSpeed = 70f;
    public float shootAccuracy = 2f;

    public override void Enter()
    {
    }

    public override void Perform()
    {
        CheckHealth();
        PlayStateSFX();
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
            if (moveTimer > Random.Range(2, 4))
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
        enemyAI.WeaponSFX();
        Transform bulletSpawner = enemyAI.bulletSpawner;
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, bulletSpawner.position, enemyAI.transform.rotation);
        Vector3 shootDirection = (enemyAI.player.transform.position - bulletSpawner.position).normalized;
        bullet.GetComponent<Rigidbody>().linearVelocity = Quaternion.AngleAxis(Random.Range(-shootAccuracy, shootAccuracy), Vector3.up) * shootDirection * shootSpeed;
        shootTimer = 0;
    }
    
}

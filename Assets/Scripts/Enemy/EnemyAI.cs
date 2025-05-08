using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    private EnemyStateMachine enemyStateMachine;
    public PatrolState patrolState;
    public AttackState attackState;
    public SearchState searchState;
    private GameObject player;
    private NavMeshAgent agent;
    private Vector3 lastKnownPlayerPosition;
    public float health = 100f;
    public GameObject Player
    {
        get => player;
    }
    public NavMeshAgent Agent
    {
        get => agent;
    }

    public Vector3 LastKnownPlayerPosition
    {
        get => lastKnownPlayerPosition;
        set => lastKnownPlayerPosition = value;
    }
    public NavigationPath path;
    [SerializeField] 
    public string currentState;
    [Header("Sight values")]
    public float sightRange = 20f;
    public float fieldOfView = 85f;
    public float eyeHeight = 0.6f;

    [Header("Attack values")] public Transform bulletSpawner;

    [Range(0.1f, 10f)] public float bulletSpawnRate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        patrolState = new PatrolState();
        attackState = new AttackState();
        searchState = new SearchState();
        
        enemyStateMachine = GetComponent<EnemyStateMachine>();
        agent = GetComponent<NavMeshAgent>();
        
        enemyStateMachine.Initialize(patrolState);
        
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer();
        CheckHealth();
        currentState = enemyStateMachine.activeState.ToString();
    }

    public bool CanSeePlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < sightRange)
            {
                Vector3 targetDirection = player.transform.position - transform.position - Vector3.up * eyeHeight;
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if (Physics.Raycast(ray, out hitInfo, sightRange))
                    {
                        if (hitInfo.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightRange, Color.red);
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }
    
    public void CheckHealth()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("ENEMY HEALTH: " + health);
        CheckHealth();
    }
}

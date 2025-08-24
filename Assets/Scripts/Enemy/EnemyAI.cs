using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    private EnemyStateMachine enemyStateMachine;
    public PatrolState patrolState;
    public AttackState attackState;
    public SearchState searchState;
    private GameObject _player;
    private NavMeshAgent _agent;
    private Vector3 _lastKnownPlayerPosition;
    public float health = 100f;
    public GameObject player
    {
        get => _player;
    }
    public NavMeshAgent agent
    {
        get => _agent;
    }

    public Vector3 lastKnownPlayerPosition
    {
        get => _lastKnownPlayerPosition;
        set => _lastKnownPlayerPosition = value;
    }
    public NavigationPath path;
    [SerializeField] 
    public string currentState;
    [Header("Sight values")]
    public float sightRange = 35f;
    public float fieldOfView = 85f;
    public float eyeHeight = 0.6f;

    [Header("Attack values")] public Transform bulletSpawner;
    public ParticleSystem DamageParticleSystem;

    [Range(0.1f, 10f)] public float bulletSpawnRate;

    void Start()
    {
        patrolState = new PatrolState();
        attackState = new AttackState();
        searchState = new SearchState();
        
        enemyStateMachine = GetComponent<EnemyStateMachine>();
        _agent = GetComponent<NavMeshAgent>();
        
        enemyStateMachine.Initialize(patrolState);
        
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        CanSeePlayer();
        CheckHealth();
        currentState = enemyStateMachine.activeState.ToString();
    }

    public bool CanSeePlayer()
    {
        if (_player != null)
        {
            if (Vector3.Distance(transform.position, _player.transform.position) < sightRange)
            {
                Vector3 targetDirection = _player.transform.position - transform.position - Vector3.up * eyeHeight;
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if (Physics.Raycast(ray, out hitInfo, sightRange))
                    {
                        if (hitInfo.transform.gameObject == _player)
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
        if (DamageParticleSystem != null)
        {
            DamageParticleSystem.Play();
        }
        CheckHealth();
    }
}

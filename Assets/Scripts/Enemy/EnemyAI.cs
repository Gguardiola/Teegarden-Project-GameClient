using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    private EnemyStateMachine enemyStateMachine;

    private NavMeshAgent agent;
    public NavMeshAgent Agent
    {
        get => agent;
    }

    [SerializeField] public string currentState;

    public NavigationPath path;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyStateMachine = GetComponent<EnemyStateMachine>();
        agent = GetComponent<NavMeshAgent>();
        
        enemyStateMachine.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

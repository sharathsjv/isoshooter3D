using UnityEngine;
using UnityEngine.AI;

public class EnemyCharacterBrain : MonoBehaviour
{
    [SerializeField]
    Animator EnemyStates;
    public GameObject NavMeshTarget;
    [SerializeField]
    NavMeshAgent navMeshAgent;
    [SerializeField]
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyStates = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(NavMeshTarget.transform.position);
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyStates.SetFloat("IDLE_MoveSpeed", navMeshAgent.velocity.magnitude);

        navMeshAgent.SetDestination(NavMeshTarget.transform.position);
    }
}

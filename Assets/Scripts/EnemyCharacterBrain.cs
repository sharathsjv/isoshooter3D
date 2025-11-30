using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class EnemyCharacterBrain : MonoBehaviour
{
    [SerializeField]
    Animator EnemyLocomotionAnimator;
    public GameObject NavMeshTarget,RotationTarget;
    [SerializeField]
    public NavMeshAgent navMeshAgent;
    [SerializeField]
    Vector3 RotationDirection;
    [SerializeField]
    Quaternion lookDirection;
    [SerializeField]
    float turnSpeed;
    [SerializeField]
    public float currentTime, totalAlertStateWaitTime;
    [SerializeField]
    public GameObject[] WaitNodes;
    public int currentNodePointer;
    [SerializeField]
    public bool Alerted;
    [SerializeField]
    Rig AimingRigLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        EnemyLocomotionAnimator.SetFloat("IDLE_MoveSpeed", navMeshAgent.velocity.magnitude);
        
        if (Alerted)
        {
            EnemyLocomotionAnimator.SetFloat("LookDirection",Vector3.SignedAngle(RotationDirection,navMeshAgent.velocity,transform.up));
            LookAtTarget();    
        }
        
    }

    public void ResetStopWatch()
    {
        currentTime = 0;
        
        
    }

    public void LookAtTarget()
    {
        RotationDirection = RotationTarget.transform.position - transform.position;
        lookDirection = Quaternion.LookRotation(RotationDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation,lookDirection, turnSpeed*Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Alerted = true;
            EnemyLocomotionAnimator.SetTrigger("Alerted");
            AimingRigLayer.weight = 1;
        }
    }


}

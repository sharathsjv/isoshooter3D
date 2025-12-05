using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using Hairibar.EngineExtensions;
using Hairibar.Ragdoll.Animation;
using Hairibar.Ragdoll;

public class EnemyCharacterBrain : MonoBehaviour
{
    [SerializeField]
    GameObject FollowTargetForRagdoll;
    [SerializeField]
    Animator EnemyLocomotionAnimator,EnemyStateMachine;
    [SerializeField]
    RagdollAnimator ragdollAnimator;
    [SerializeField]
    RagdollPowerProfile ragdollPowerProfile;
    [SerializeField]
    RagdollSettings currentRagdollSettings;
    [SerializeField]
    Rig AimingRigLayer, LegsRigLayer;

    [SerializeField]
    AnimatorController EnemyLocomotionAnimatorController,NullAnimatorController;
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
    

    //RagDoll Stuff
    [SerializeField]
    List<Rigidbody> rigidbodies;
    [SerializeField]
    GameObject armature;

    //crude checking whether switching works
    [SerializeField]
    bool switchToRagDoll, ragdollSwitched;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ragdollAnimator = GetComponentInChildren<RagdollAnimator>();
        EnemyStateMachine = GetComponent<Animator>();
        currentRagdollSettings = GetComponentInChildren<RagdollSettings>();
    }
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

        if (switchToRagDoll && !ragdollSwitched)
        {
            EnableRagdoll(true);
        }
        if (!switchToRagDoll && ragdollSwitched)
        {
            EnableRagdoll(false);
            
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

    public void EnableRagdoll(bool onoroff)
    {
        ragdollSwitched = onoroff;
        ragdollAnimator.MasterAlpha = 0f;
        ragdollAnimator.MasterDampingRatio = 0f;
        ragdollAnimator.forceTargetPose = false;
        ragdollAnimator.RagdollSettings.PowerProfile = ragdollPowerProfile;
        FollowTargetForRagdoll.SetActive(true);
        EnemyLocomotionAnimator.SetTrigger("ProceduralSwitch");
        LegsRigLayer.weight = 1;
    }


}

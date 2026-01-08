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
using Unity.VisualScripting;
using RootMotion;
using RootMotion.Dynamics;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

public class EnemyCharacterBrain : MonoBehaviour
{
    [SerializeField]
    GameObject FollowTargetForRagdoll;
    [SerializeField]
    public Animator EnemyLocomotionAnimator,EnemyStateMachine;
    [SerializeField]
    RagdollAnimator ragdollAnimator;
    [SerializeField]
    RagdollPowerProfile ragdollPowerProfile;
    [SerializeField]
    RagdollSettings currentRagdollSettings;
    [SerializeField]
    public Rig AimingRigLayer, LegsRigLayer, ArmsRigLayer;
    [SerializeField]
    PuppetMaster puppetMaster;



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
    [SerializeField]
    GameObject LegObject, RightObject, LegMidObject, RightMidObject;

    public float animationspeed;

    BoxCollider detectorBoxCollider;
    public bool isCoverWeight;
    

    //RagDoll Stuff
    [SerializeField]
    List<Rigidbody> rigidbodies;
    [SerializeField]
    public Rigidbody hipsrigidbody;
    [SerializeField]
    GameObject armature;
    [SerializeField]
    AnimationCurve RootMotionSpeedOverTime;
    [SerializeField]
    float PinWeightOverTime;
    [SerializeField]
    float BulletForceMagnitude = 10;
    [SerializeField]
    public Vector3 RagDollRotationDirection;
    [SerializeField]
    public Vector3 BulletDirection;
    

    //crude checking whether switching works
    [SerializeField]
    bool switchToRagDoll, ragdollSwitched, bulletRagdoll;

    [SerializeField]
    public CoverNode currentCoverNode;
    [SerializeField]
    public float healthPoints;
    [SerializeField]
    public CoverManager coverManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ragdollAnimator = GetComponentInChildren<RagdollAnimator>();
        EnemyStateMachine = GetComponent<Animator>();
        currentRagdollSettings = GetComponentInChildren<RagdollSettings>();
        foreach(var a in GetComponentsInChildren<Rigidbody>())
        {
            rigidbodies.Add(a);
        }
        coverManager = GameObject.FindAnyObjectByType<CoverManager>();
    }
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        PinWeightOverTime = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyLocomotionAnimator.SetFloat("IDLE_MoveSpeed", navMeshAgent.velocity.magnitude);
        
        if (Alerted)
        {
            EnemyLocomotionAnimator.SetFloat("LookDirection",Vector3.SignedAngle(RotationDirection,navMeshAgent.velocity,transform.up));
            // EnemyStateMachine.SetTrigger("CoverStateTrigger");
            if (RotationTarget!=null)
                LookAtTarget();    
        }

        if (switchToRagDoll && !ragdollSwitched)
        {
            EnableRagdoll(true);
        }
        // if (!switchToRagDoll && ragdollSwitched)
        // {
        //     EnableRagdoll(false);
            
        // }
        if (ragdollSwitched && puppetMaster.pinWeight!=0)
        {
            
            puppetMaster.pinWeight -= 10f*Time.deltaTime;
            
        }

        if (isCoverWeight&&Alerted)
            SetAimingPoseWeight(0.01f);
        else if (!isCoverWeight&&Alerted)
            SetAimingPoseWeight(1);
        
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
            EnemyStateMachine.SetTrigger("CoverStateTrigger");
            AimingRigLayer.weight = 1;
            detectorBoxCollider = GetComponent<BoxCollider>();
            detectorBoxCollider.enabled = false;
        }

        
    }

    // public void RagDollAfterBulletHit(Vector3 InputBulletDirection)
    // {
    //     bulletRagdoll = true;
    //     EnableRagdoll(true);
    //     BulletDirection = InputBulletDirection;
    //     RagDollRotationDirection = BulletDirection - transform.position;
    //     EnemyLocomotionAnimator.SetFloat("LookDirection",Vector3.SignedAngle(RagDollRotationDirection,transform.forward,transform.up));
    //     EnemyLocomotionAnimator.speed += RootMotionSpeedOverTime.Evaluate(0+Time.deltaTime);
    // }

    public void EnableRagdoll(bool onoroff)
    {
        puppetMaster.pinWeight -= RootMotionSpeedOverTime.Evaluate(PinWeightOverTime);
        //FollowTargetForRagdoll.SetActive(true);
        EnemyLocomotionAnimator.SetTrigger("ProceduralSwitch");
        //LegsRigLayer.weight = 1;
        ArmsRigLayer.weight = 0;
        ragdollSwitched = true;
        DeathAnimation();
       
        // if (hipsrigidbody == null)
        // {
        //     foreach(var a in rigidbodies)
        //     {
        //         if (a.tag == "Hips")
        //         {
        //             hipsrigidbody = a;
        //         }
        //     }
        //     // hipsrigidbody.AddForce(100*-transform.forward,ForceMode.Force);
        //     AddForToRigidBodies();
        // }
        // else
        // {
        //     // hipsrigidbody.AddForce(100*-transform.forward,ForceMode.Force);
        //     AddForToRigidBodies();
        // }
        
    }

    IEnumerator DeathAnimation()
    {
        yield return new WaitForSeconds(1f);
        EnemyLocomotionAnimator.SetTrigger("Dead");
        RagdollSetToDead();
    }

    IEnumerator RagdollSetToDead()
    {
        yield return new WaitForSeconds(4f);
        puppetMaster.state = PuppetMaster.State.Dead;
    }

    public void AddForToRigidBodies ()
    {
        foreach(var a in rigidbodies)
        {
            a.AddForce(10*-transform.forward,ForceMode.Force);
        }
    }

    public void SetAimingPoseWeight(float weight)
    {
        Debug.Log("Setting");
        AimingRigLayer.weight = weight;
    }


}

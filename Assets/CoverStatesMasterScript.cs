using Hairibar.EngineExtensions.Pooling;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class CoverStatesMasterScript : StateMachineBehaviour
{
    public enum CoverStates
    {
        FindAndMoveToCover,
        InCoverCrouchState,
        InCoverShootingState, 
        Dead,
        MoveTowardsPlayerDirect,
        MoveTowardsPlayerCoverToCover,

    }

    [SerializeField]
    Vector3 SelfToCover, SelfToPlayer;
    [SerializeField]
    float AngleBetweenSTCAndSTP;
    [SerializeField]
    CoverStates coverStates;
    [SerializeField]
    EnemyCharacterBrain enemyCharacterBrain;
    [SerializeField]
    bool closestCoverFound;
    [SerializeField]
    float tempDistance=5000, coverToPlayerDistance;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(enemyCharacterBrain==null)
        {
            enemyCharacterBrain = animator.GetComponent<EnemyCharacterBrain>();
        }

        if (coverStates == CoverStates.FindAndMoveToCover)
        {
            tempDistance = 5000;
            foreach(var a in enemyCharacterBrain.coverManager.CoverNodes)
            {
                if (a.CoverHiddenFromPlayer)
                {
                    SelfToCover = a.transform.position - animator.transform.position;
                    SelfToPlayer = enemyCharacterBrain.RotationTarget.transform.position - animator.transform.position;
                    AngleBetweenSTCAndSTP = Vector3.SignedAngle(SelfToCover,SelfToPlayer, animator.transform.up);

                    if (AngleBetweenSTCAndSTP>-90 &&AngleBetweenSTCAndSTP<90)
                    {
                        
                        if (SelfToCover.magnitude<tempDistance)
                        {
                            
                            tempDistance = SelfToCover.magnitude;
                            enemyCharacterBrain.currentCoverNode = a;
                            
                        }
                        
                    }
                }
            }
            // enemyCharacterBrain.currentCoverNode = TemporaryClosestNode;

        
            enemyCharacterBrain.navMeshAgent.SetDestination(enemyCharacterBrain.currentCoverNode.transform.position);
            enemyCharacterBrain.EnemyLocomotionAnimator.SetTrigger("Alerted");
            Debug.Log(NavMesh.GetAreaFromName("CoverEdges"));
        }

        if (coverStates == CoverStates.MoveTowardsPlayerCoverToCover)
        {
            tempDistance = 0;
            foreach(var a in enemyCharacterBrain.coverManager.CoverNodes)
            {
                if (a !=enemyCharacterBrain.currentCoverNode)
                {
                    coverToPlayerDistance = Vector3.Distance(a.transform.position, enemyCharacterBrain.RotationTarget.transform.position);
                    if (coverToPlayerDistance>tempDistance)
                    {
                        enemyCharacterBrain.currentCoverNode = a;
                        tempDistance = coverToPlayerDistance;
                        
                    }
                }
            }

            enemyCharacterBrain.navMeshAgent.SetDestination(enemyCharacterBrain.currentCoverNode.transform.position);
            enemyCharacterBrain.EnemyLocomotionAnimator.SetTrigger("Alerted");
        }

        if (coverStates == CoverStates.InCoverCrouchState)
        {
            enemyCharacterBrain.SetAimingPoseWeight(0.01f);
            enemyCharacterBrain.EnemyLocomotionAnimator.SetTrigger("Crouch");
            
        }
       
        if (coverStates == CoverStates.Dead)
        {
            enemyCharacterBrain.navMeshAgent.isStopped = true;
            enemyCharacterBrain.RotationTarget = null;
        }

        if (coverStates == CoverStates.InCoverShootingState)
        {
            enemyCharacterBrain.isCoverWeight = false;
            enemyCharacterBrain.ResetStopWatch();
            enemyCharacterBrain.EnemyLocomotionAnimator.SetTrigger("Alerted");
        }

        if (coverStates == CoverStates.MoveTowardsPlayerDirect)
        {
            
        }

        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (coverStates == CoverStates.FindAndMoveToCover)
        {
            if (enemyCharacterBrain.navMeshAgent.remainingDistance<0.2)
            {
                animator.SetTrigger("InCoverTrigger");
            }
        }

        if (coverStates == CoverStates.InCoverCrouchState)
        {
            enemyCharacterBrain.isCoverWeight = true;
            enemyCharacterBrain.EnemyLocomotionAnimator.SetTrigger("Crouch");
            if (!enemyCharacterBrain.currentCoverNode.CoverHiddenFromPlayer)
            {
                animator.SetTrigger("SearchForCoverTrigger");
            }
            enemyCharacterBrain.SetAimingPoseWeight(0.01f);
            
        }

        if (coverStates == CoverStates.InCoverShootingState)
        {
            enemyCharacterBrain.currentTime+=Time.deltaTime;
            if (enemyCharacterBrain.recoilHandler.isAtMeanPosition)
            {
                enemyCharacterBrain.weaponScript.Fire();
            }

            if (enemyCharacterBrain.currentTime>10f)
            {
                animator.SetTrigger("InCoverTrigger");
            }

            if (!enemyCharacterBrain.currentCoverNode.CoverHiddenFromPlayer)
            {
                animator.SetTrigger("SearchForCoverTrigger");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if (coverStates == CoverStates.FindAndMoveToCover)
        {
            if (enemyCharacterBrain.navMeshAgent.remainingDistance<0.2)
            {
                animator.ResetTrigger("InCoverTrigger");
            }
        }

        if (coverStates==CoverStates.InCoverCrouchState)
        {
            enemyCharacterBrain.isCoverWeight = false;
            animator.ResetTrigger("SearchForCoverTrigger");
            enemyCharacterBrain.EnemyLocomotionAnimator.ResetTrigger("Crouch");
            
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

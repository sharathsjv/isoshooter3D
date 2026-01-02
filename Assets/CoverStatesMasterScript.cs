using UnityEngine;
using UnityEngine.AI;

public class CoverStatesMasterScript : StateMachineBehaviour
{
    public enum CoverStates
    {
        FindAndMoveToCover,
        InCoverState,

        
    }
    [SerializeField]
    CoverStates coverStates;
    EnemyCharacterBrain enemyCharacterBrain;
    [SerializeField]
    bool closestCoverFound;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(enemyCharacterBrain==null)
        {
            enemyCharacterBrain = animator.GetComponent<EnemyCharacterBrain>();
        }

        if (coverStates == CoverStates.FindAndMoveToCover)
        {
            foreach(var a in enemyCharacterBrain.coverManager.CoverNodes)
            {
                if (a.CoverHiddenFromPlayer)
                {
                    enemyCharacterBrain.currentCoverNode = a;
                    break;
                }
            }
        
            enemyCharacterBrain.navMeshAgent.SetDestination(enemyCharacterBrain.currentCoverNode.transform.position);
            Debug.Log(NavMesh.GetAreaFromName("CoverEdges"));
        }

        if (coverStates == CoverStates.InCoverState)
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

        if (coverStates==CoverStates.InCoverState)
        {
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

        if (coverStates==CoverStates.InCoverState)
        {
            if (!enemyCharacterBrain.currentCoverNode.CoverHiddenFromPlayer)
            {
                animator.ResetTrigger("SearchForCoverTrigger");
            }
            
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

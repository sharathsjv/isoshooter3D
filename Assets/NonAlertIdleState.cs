using UnityEngine;

public class NonAlertIdleState : StateMachineBehaviour
{
    EnemyCharacterBrain EnemyCharacterBrain;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (EnemyCharacterBrain==null)
        {
            EnemyCharacterBrain = animator.GetComponent<EnemyCharacterBrain>();
        }
        else
        {
            EnemyCharacterBrain.ResetStopWatch();
        }
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnemyCharacterBrain.currentTime += Time.deltaTime;
        if (EnemyCharacterBrain.currentTime>EnemyCharacterBrain.totalAlertStateWaitTime)
        {
            animator.SetTrigger("NonAlertIdleForwardTrigger");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(EnemyCharacterBrain.currentNodePointer==EnemyCharacterBrain.WaitNodes.Length)
        {
            EnemyCharacterBrain.currentNodePointer=0;
        
        }
        else if (EnemyCharacterBrain.currentNodePointer!=EnemyCharacterBrain.WaitNodes.Length)
        {
            EnemyCharacterBrain.currentNodePointer++;
        }
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

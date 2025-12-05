using UnityEngine;

public class LegStateAnimation : StateMachineBehaviour
{
    public enum legType
    {
        LeftLegIKTransform,
        RightLegIKTransform,
    }
    [SerializeField]
    AnimationCurve LegYAxis;
    FollowTargetForLegs followTargetForLegs;
    [SerializeField]
    public legType LegType;
    [SerializeField]
    GameObject TargetLeg;
    [SerializeField]
    Vector3 newposition;
    [SerializeField]
    float distance;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (followTargetForLegs == null)
        {
            followTargetForLegs = animator.GetComponent<FollowTargetForLegs>();
        }
        if (LegType == legType.LeftLegIKTransform)
        {
            TargetLeg = followTargetForLegs.LeftLegIKTransform;
        }
        else
        {
            TargetLeg = followTargetForLegs.RightLegIKTransform;
        }
        newposition = TargetLeg.transform.position- Vector3.forward*followTargetForLegs.animationStoppingDistance;
        followTargetForLegs.currentTime=0;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        distance = Vector3.Distance(newposition,TargetLeg.transform.position);
        followTargetForLegs.currentTime+=Time.deltaTime;

        // if (Vector3.Distance(followTargetForLegs.hipTransform.transform.position,TargetLeg.transform.position) > followTargetForLegs.animationTriggerDistance)
        // {
        //     Vector3.Lerp(TargetLeg.transform.position, followTargetForLegs.hipTransform.transform.position, followTargetForLegs.legSpeed * 10 *Time.deltaTime);
        // }
        // if (Vector3.Distance(followTargetForLegs.hipTransform.transform.position,TargetLeg.transform.position) < followTargetForLegs.animationStoppingDistance)
        // {
        //     animator.SetTrigger("LegSwitch");
        // }

        TargetLeg.transform.position = Vector3.Lerp(TargetLeg.transform.position, newposition, followTargetForLegs.legSpeed*Time.deltaTime);
        if (followTargetForLegs.currentTime>followTargetForLegs.waitTime)
        {
            animator.SetTrigger("LegSwitch");
        }
        
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
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

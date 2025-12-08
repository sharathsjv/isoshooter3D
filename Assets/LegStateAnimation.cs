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
    GameObject TargetLeg, MidTargetLeg;
    [SerializeField]
    GameObject TargetTransform, MidTargetTransform;
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
            MidTargetLeg = followTargetForLegs.RightLegIKTransform;
            TargetTransform = followTargetForLegs.LeftLegHipTransform;
            MidTargetTransform = followTargetForLegs.RightLegMidTransform;

            MidTargetTransform.transform.position += new Vector3(followTargetForLegs.enemyCharacterBrain.hipsrigidbody.transform.position.x,0,followTargetForLegs.enemyCharacterBrain.hipsrigidbody.transform.position.z);
            TargetTransform.transform.position += new Vector3(followTargetForLegs.enemyCharacterBrain.hipsrigidbody.transform.position.x,0,followTargetForLegs.enemyCharacterBrain.hipsrigidbody.transform.position.z);
        }
        else
        {
            TargetLeg = followTargetForLegs.RightLegIKTransform;
            MidTargetLeg = followTargetForLegs.LeftLegIKTransform;
            TargetTransform = followTargetForLegs.RightLegHipTransform;
            MidTargetTransform = followTargetForLegs.LeftLegMidTransform;

            MidTargetTransform.transform.position += new Vector3(followTargetForLegs.enemyCharacterBrain.hipsrigidbody.transform.position.x,0,followTargetForLegs.enemyCharacterBrain.hipsrigidbody.transform.position.z);
            TargetTransform.transform.position += new Vector3(followTargetForLegs.enemyCharacterBrain.hipsrigidbody.transform.position.x,0,followTargetForLegs.enemyCharacterBrain.hipsrigidbody.transform.position.z);        
        }
        followTargetForLegs.currentTime=0;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        
        followTargetForLegs.currentTime+=Time.deltaTime;

        // if (Vector3.Distance(followTargetForLegs.hipTransform.transform.position,TargetLeg.transform.position) > followTargetForLegs.animationTriggerDistance)
        // {
        //     TargetLeg.transform.position = Vector3.Lerp(TargetLeg.transform.position, followTargetForLegs.hipTransform.transform.position, followTargetForLegs.legSpeed * 10 *Time.deltaTime);
        // }
        // if (Vector3.Distance(followTargetForLegs.hipTransform.transform.position,TargetLeg.transform.position) < followTargetForLegs.animationStoppingDistance)
        // {
        //     animator.SetTrigger("LegSwitch");
        // }

        if (followTargetForLegs.currentTime < followTargetForLegs.waitTime)
        {
            TargetLeg.transform.position = Vector3.Lerp(TargetLeg.transform.position, TargetTransform.transform.position, followTargetForLegs.legSpeed*Time.deltaTime);
            MidTargetLeg.transform.position = Vector3.Lerp(MidTargetLeg.transform.position, MidTargetTransform.transform.position, followTargetForLegs.legSpeed*Time.deltaTime);
        }

        
        if (followTargetForLegs.currentTime>followTargetForLegs.waitTime)
        {
            animator.SetTrigger("LegSwitch");
        }
        
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       followTargetForLegs.enemyCharacterBrain.hipsrigidbody.AddForce(100*-followTargetForLegs.enemyCharacterBrain.hipsrigidbody.transform.forward,ForceMode.Force);
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

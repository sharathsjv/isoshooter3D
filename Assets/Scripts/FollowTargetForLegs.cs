using UnityEngine;

public class FollowTargetForLegs : MonoBehaviour
{
    [SerializeField]
    public GameObject RightLegIKTransform, LeftLegIKTransform;
    public GameObject RagdollHead,RagdollHips;
    public float CurrentBalanceAngle,HipsToHeadFlatDistance, TargetHipsToHeadFlatDistance;
    [SerializeField]
    Vector3 HipsToHeadVector, LeftLegOffset, RightLegOffset, HipsToHeadFlattened;
    [SerializeField]
    public float animationTriggerDistance, animationStoppingDistance, legSpeed, currentTime, waitTime;
    [SerializeField]
    public EnemyCharacterBrain enemyCharacterBrain;
    [SerializeField]
    bool leftLeg, rightleg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LeftLegOffset = LeftLegIKTransform.transform.position;
        RightLegOffset = RightLegIKTransform.transform.position;
        leftLeg = true;
    }

    // Update is called once per frame
    void Update()
    {
        HipsToHeadVector = RagdollHead.transform.position - RagdollHips.transform.position;
        CurrentBalanceAngle = Vector3.SignedAngle(transform.up,HipsToHeadVector, RagdollHead.transform.forward);
        HipsToHeadFlattened = new Vector3(RagdollHead.transform.position.x,0,RagdollHead.transform.position.z) - new Vector3(RagdollHips.transform.position.x,0,RagdollHips.transform.position.z);
        HipsToHeadFlatDistance = HipsToHeadFlattened.magnitude;

        if (HipsToHeadFlatDistance > TargetHipsToHeadFlatDistance)
        {
            if (leftLeg)
            {
                LeftLegIKTransform.transform.position = Vector3.Lerp(LeftLegIKTransform.transform.position,HipsToHeadFlattened,0.5f*Time.deltaTime);
                if ((LeftLegIKTransform.transform.position-HipsToHeadFlattened).magnitude<0.001)
                {
                    leftLeg = false;
                    rightleg = true;
                }    
            }

            if (rightleg)
            {
                RightLegIKTransform.transform.position = Vector3.Lerp(RightLegIKTransform.transform.position,HipsToHeadFlattened,0.5f*Time.deltaTime);
                if ((RightLegIKTransform.transform.position-HipsToHeadFlattened).magnitude<0.001)
                {
                    rightleg = false;
                    leftLeg = true;
                } 
            }
        }

        LeftLegIKTransform.transform.position = Vector3.Lerp(LeftLegIKTransform.transform.position, LeftLegOffset,0.3f);
        RightLegIKTransform.transform.position = Vector3.Lerp(RightLegIKTransform.transform.position,RightLegOffset,0.3f);

    }
}

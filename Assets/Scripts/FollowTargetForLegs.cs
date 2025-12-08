using UnityEngine;

public class FollowTargetForLegs : MonoBehaviour
{
    [SerializeField]
    public GameObject RightLegIKTransform, LeftLegIKTransform;
    public GameObject RightLegMidTransform, LeftLegMidTransform, RightLegHipTransform, LeftLegHipTransform;
    [SerializeField]
    public float animationTriggerDistance, animationStoppingDistance, legSpeed, currentTime, waitTime;
    [SerializeField]
    public EnemyCharacterBrain enemyCharacterBrain;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

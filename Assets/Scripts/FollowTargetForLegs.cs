using UnityEngine;

public class FollowTargetForLegs : MonoBehaviour
{
    [SerializeField]
    public GameObject RightLegIKTransform, LeftLegIKTransform, hipTransform;
    [SerializeField]
    public float animationTriggerDistance, animationStoppingDistance, legSpeed, currentTime, waitTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

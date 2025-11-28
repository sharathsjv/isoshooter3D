using Unity.Cinemachine;
using UnityEngine;

public class LookingMode : MonoBehaviour
{
    [SerializeField]
    CinemachineCamera cinemachineCamera;
    [SerializeField]
    Transform StartPosition, PreviousPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        transform.position = StartPosition.position;
    }
    void Start()
    {
        PreviousPosition = cinemachineCamera.Target.TrackingTarget;
        cinemachineCamera.Target.TrackingTarget = this.transform;
        
        
    }

    void OnEnable()
    {
        transform.position = StartPosition.localPosition;
        PreviousPosition = cinemachineCamera.Target.TrackingTarget;
        cinemachineCamera.Target.TrackingTarget = this.transform;
    }

    void OnDisable()
    {
        cinemachineCamera.Target.TrackingTarget = StartPosition;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

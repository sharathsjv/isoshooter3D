using System.Numerics;
using UnityEngine;
using UnityEngine.Events;

public class SitOnTransform : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnStartFunctions,OnCompleteFunction;
    [SerializeField]
    GameObject TargetGameObject;
    [SerializeField]
    Transform SitTransform;
    [SerializeField]
    UnityEngine.Vector3 PositionOffset;
    UnityEngine.Quaternion RotationOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnStartFunctions.Invoke();
        TargetGameObject.transform.position = SitTransform.position + PositionOffset;
        TargetGameObject.transform.rotation = SitTransform.rotation;
        OnCompleteFunction.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

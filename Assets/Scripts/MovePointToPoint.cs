using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MovePointToPoint : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnCompleteFunction, OnStartFunctions;
    [SerializeField]
    NavMeshAgent NPCAgent;
    [SerializeField]
    Transform Target;

    public enum TargetType
    {
        customdefined,
        player,
    }

    [SerializeField]
    TargetType targetType;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        OnStartFunctions.Invoke();

        if (targetType == TargetType.customdefined)
        {
            if (Target == null)
            {
                Debug.Log("Please set custom transform");
            }
        }
        else if (targetType == TargetType.player)
        {
            Target = FindAnyObjectByType<PlayerController>().transform;
        }

        if (NPCAgent== null)
        {
            Debug.Log("Please set NPC agent");
        }
        else
        {
            if (NPCAgent.enabled == false)
                NPCAgent.enabled = true;
            
            NPCAgent.SetDestination(Target.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (NPCAgent.remainingDistance<0.02)
        {
            OnCompleteFunction.Invoke();
        }
    }
    
}

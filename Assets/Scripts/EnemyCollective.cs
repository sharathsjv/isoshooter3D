using System.Collections.Generic;
using UnityEngine;

public class EnemyCollective : MonoBehaviour
{
    [SerializeField]
    List <EnemyCharacterBrain> AllEnemies;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var a in GetComponentsInChildren<EnemyCharacterBrain>())
        {
            AllEnemies.Add(a);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AgroAllNPCs()
    {
        foreach(var a in AllEnemies)
        {
            a.ArmsRigLayer.weight=1;
            a.AlertedState();
            a.isSitting = false;
            a.navMeshAgent.enabled = true;
            a.EnemyStateMachine.enabled = true;
            a.EnemyLocomotionAnimator.SetBool("Sitting",false);
        }
    }
}

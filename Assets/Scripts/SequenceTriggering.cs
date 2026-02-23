using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SequenceTriggering : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;
    [SerializeField]
    List<GameObject>ObjectsToActivateOnColliderEnter;
    [SerializeField]
    List<GameObject>ObjectsToActivateOnInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        foreach (var a in ObjectsToActivateOnColliderEnter)
        {
            a.SetActive(true);
        }

        // if (playerController.interactInput)
        // {
        //     foreach(var a in ObjectsToActivateOnInput)
        //     {
        //         a.SetActive(true);
        //     }
        // }
        
    }

    void OnTriggerStay(Collider collision)
    {
        if (playerController.interactInput)
        {
            foreach(var a in ObjectsToActivateOnInput)
            {
                a.SetActive(true);
            }
        }
    }

}

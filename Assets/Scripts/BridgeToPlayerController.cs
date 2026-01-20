using UnityEngine;

public class BridgeToPlayerController : MonoBehaviour
{
    PlayerController playerController;

    void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableDiving()
    {
        playerController.isDiving = false;
        
        
    }

    public void EnableDiving()
    {

        playerController.isDiving = true;
        playerController.isUp = false;
    }

    public void EnableUp()
    {
        playerController.isUp = true;
        playerController.animator.SetTrigger("NotStrafe");
    }

    public void EnableMovement()
    {
        playerController.movementAction.Enable();
    }
}

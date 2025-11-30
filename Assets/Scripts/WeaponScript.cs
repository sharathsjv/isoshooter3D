using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponScript : MonoBehaviour
{
    public PlayerController playerController;
    public RecoilHandler recoilHandler;
    public GameObject[] bullets;
    int currentBullet;

    public void FireInput(InputAction.CallbackContext context)
    {
        if (context.started&&playerController.aimInput)
            Fire();
    }

    void Start()
    {
        bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach(var a in bullets)
        {
            a.SetActive(false);
        }
        playerController = GetComponent<PlayerController>();
    }

    public void Fire()
    {
        bullets[currentBullet].SetActive(true);
        recoilHandler.RecoilFire();
        if (currentBullet!=bullets.Length-1)
        {
            currentBullet++;
        }
        else
        {
            currentBullet=0;
        }
        
    }
}

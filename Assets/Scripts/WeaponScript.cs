using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponScript : MonoBehaviour
{

    public enum ControllerType
    {
        Player, 
        EnemyCharacterBrain,
    }
    [SerializeField]
    ControllerType controllerType;
    public PlayerController playerController;
    public EnemyCharacterBrain enemyCharacterBrain;
    public string BulletTag;
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
        bullets = GameObject.FindGameObjectsWithTag(BulletTag);
        foreach(var a in bullets)
        {
            a.SetActive(false);
        }
        if (controllerType == ControllerType.Player)
        playerController = GetComponent<PlayerController>();
        if (controllerType == ControllerType.EnemyCharacterBrain)
        {
            enemyCharacterBrain = GetComponent<EnemyCharacterBrain>();
            recoilHandler = GetComponentInChildren<RecoilHandler>();
        }
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

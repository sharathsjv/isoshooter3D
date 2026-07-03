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
    [SerializeField]
    int currentBullet;
    [SerializeField]
    public GameObject bulletSpawn;

    public void FireInput(InputAction.CallbackContext context)
    {
        if (context.started&&playerController.aimInput)
            Fire();
    }

    void Awake()
    {
        foreach(Transform child in transform)
        {
            if (child.gameObject.tag  == "GunSpawnLocation")
            {
                bulletSpawn = child.gameObject;
            }
        }
    }

    void Start()
    {
        int i = 0;
        foreach(var a in BulletPool.instance.AllTheBullets)
        {
            if (a.GetComponent<BulletScript>().SpawnPosition==bulletSpawn)
            {   
                bullets[i]=a;
                i++;
            }
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

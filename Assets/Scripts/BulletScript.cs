using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed, multiplier;
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    GameObject SpawnPosition;
    [SerializeField]
    float currentTime, TotalTime;
    [SerializeField]
    GameObject BulletForceCube;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPosition = GameObject.FindGameObjectWithTag("GunSpawnLocation");
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward*multiplier*bulletSpeed*Time.deltaTime,ForceMode.Force);
    }
    void OnEnable()
    {
        if (SpawnPosition==null||rb == null)
        {
            SpawnPosition = GameObject.FindGameObjectWithTag("GunSpawnLocation");
            rb = GetComponent<Rigidbody>();
        }
        currentTime =0;
        transform.position = SpawnPosition.transform.position;
        transform.rotation = SpawnPosition.transform.rotation;
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(transform.forward*bulletSpeed*Time.deltaTime);
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime>TotalTime)
        {
            transform.gameObject.SetActive(false);
        }
        
    }

    // void (Collider other)
    // {
    //     if (other.tag=="Enemy")
    //     {
    //         other.GetComponentInParent<EnemyCharacterBrain>().EnableRagdoll(true);
    //     }
    // }

    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.tag == "Enemy")
    //     {
    //         other.GetComponentInChildren<EnemyCharacterBrain>().EnableRagdoll(true);
    //     }
    // }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Enemy")
         {
           
           collision.transform.GetComponent<Rigidbody>().AddForce(500f*rb.linearVelocity.normalized*Time.deltaTime, ForceMode.Impulse);

         }

        
    }




}

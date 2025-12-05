using System.Numerics;
using UnityEngine;

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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Enemy")
        {
            other.GetComponent<EnemyCharacterBrain>().EnableRagdoll(true);
        }
    }


}

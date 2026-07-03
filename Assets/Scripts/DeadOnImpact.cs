using UnityEngine;

public class DeadOnImpact : MonoBehaviour
{
    [SerializeField]
    EnemyCharacterBrain theBrain;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Enemy")
         {
           
           theBrain = collision.gameObject.GetComponentInParent<EnemyCharacterBrain>();
           theBrain.puppetMaster.Kill();
           Debug.Log(theBrain.name);

         }

        
    }

}

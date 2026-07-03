using UnityEngine;
using RootMotion.Dynamics;

public class StoveTrigger : MonoBehaviour
{

     [SerializeField] private BehaviourFall behaviourFall;
     public SlowMotionCameraSwitcher slowMotionCameraSwitcher;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter(Collider collision)
    {
        // Optional: Filter by specific object tag like a bullet or hammer
        if (collision.gameObject.CompareTag("Enemy"))
        {
            behaviourFall.Activate();
            this.gameObject.SetActive(false);
            
            slowMotionCameraSwitcher.TriggerSlowMoCamera();
        }
    }
}

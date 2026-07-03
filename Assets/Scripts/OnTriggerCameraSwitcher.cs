using UnityEngine;

public class OnTriggerCameraSwitcher : MonoBehaviour
{
        [Header("Prefabs & Physics")]
 
    public SlowMotionCameraSwitcher slowMotionCameraSwitcher;

    private void OnTriggerEnter(Collider collision)
    {
        // Optional: Filter by specific object tag like a bullet or hammer
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            slowMotionCameraSwitcher.TriggerSlowMoCamera();
        }
    }
}

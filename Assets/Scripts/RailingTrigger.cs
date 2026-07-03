using UnityEngine;
using RootMotion.Dynamics;

public class RailingTrigger : MonoBehaviour
{
    [SerializeField] private BehaviourFall behaviourFall;
    public SlowMotionCameraSwitcher slowMotionCameraSwitcher;
    private void OnTriggerEnter(Collider collision)
    {
        // Optional: Filter by specific object tag like a bullet or hammer
        if (collision.gameObject.CompareTag("Enemy"))
        {
            behaviourFall = collision.gameObject.GetComponent<EnemyCharacterBrain>().RailingbehaviourFall;
            behaviourFall.Activate();
            
            
            slowMotionCameraSwitcher.TriggerSlowMoCamera();
        }
    }
}

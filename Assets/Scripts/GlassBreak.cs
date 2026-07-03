using UnityEngine;
using RootMotion.Dynamics;

public class GlassBreak : MonoBehaviour
{
    [Header("Prefabs & Physics")]
    public GameObject fracturedGlassPrefab; // Assign your shard prefab here
    public float explosionForce = 300f;
    public float explosionRadius = 2f;
    public SlowMotionCameraSwitcher slowMotionCameraSwitcher;
    [SerializeField] private BehaviourFall behaviourFall;

    private void OnTriggerEnter(Collider collision)
    {
        // Optional: Filter by specific object tag like a bullet or hammer
        if (collision.gameObject.CompareTag("Enemy"))
        {
            behaviourFall.Activate();
            fracturedGlassPrefab.SetActive(true);
            this.gameObject.SetActive(false);
            slowMotionCameraSwitcher.TriggerSlowMoCamera();
        }
    }
}

using UnityEngine;

public class RecoilHandler : MonoBehaviour
{
    // Public variables for Inspector adjustments
    public float recoilX;
    public float recoilY;
    public float recoilZ;
    public float snappiness;

    // Private variables for internal calculations
    [SerializeField]
    private Vector3 currentTransform;
    
    void Start()
    {
        
    }

    void Update()
    {
              
        // Apply the rotation to the transform
        transform.localPosition = Vector3.Lerp(transform.localPosition, currentTransform,snappiness*Time.deltaTime);
    }

    // Public method to be called when the weapon fires
    public void RecoilFire()
    {
        // Add recoil to the target rotation
        transform.position += new Vector3(Random.Range(-recoilX,recoilX),Random.Range(recoilY,2*recoilY),0);
    }
}

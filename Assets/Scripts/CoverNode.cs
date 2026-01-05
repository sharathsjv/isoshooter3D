using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class CoverNode : MonoBehaviour
{

    [SerializeField]
    RaycastHit RayToPlayer;
    [SerializeField]
    PlayerController player;
    public bool CoverHiddenFromPlayer;
    [SerializeField]
    LayerMask PlayerMask, CoverMask;
    [SerializeField]
    float Range = 50f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position,(player.transform.position - transform.position).normalized, Range,CoverMask))
        {
            Debug.DrawLine(transform.position, (player.transform.position - transform.position)*Range, Color.white);
            CoverHiddenFromPlayer = true;
        }
        else if (Physics.Raycast(transform.position,(player.transform.position - transform.position).normalized, Range,PlayerMask))
        {
            Debug.DrawLine(transform.position, (player.transform.position - transform.position)*Range, Color.red);
            CoverHiddenFromPlayer = false;
        }

        // CoverHiddenFromPlayer = !Physics.Raycast(transform.position,(player.transform.position - transform.position).normalized, Range,layerMask);
        // Debug.DrawLine(transform.position, (player.transform.position - transform.position)*Range);

        
    }
}

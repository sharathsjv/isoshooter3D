using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class CoverNode : MonoBehaviour
{

    [SerializeField]
    RaycastHit RayToPlayer;
    [SerializeField]
    PlayerController player;
    public bool CoverHiddenFromPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position,(player.transform.position - transform.position).normalized,out RayToPlayer, 4))
        {
            Debug.DrawLine(transform.position, RayToPlayer.point);
            CoverHiddenFromPlayer = true;
        }
        else
        {
            CoverHiddenFromPlayer = false;
        }

        
    }
}

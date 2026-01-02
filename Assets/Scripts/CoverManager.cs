using System.Collections.Generic;
using UnityEngine;

public class CoverManager : MonoBehaviour
{
    [SerializeField]
    public List<CoverNode> CoverNodes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var a in GetComponentsInChildren<CoverNode>())
        {
            CoverNodes.Add(a);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

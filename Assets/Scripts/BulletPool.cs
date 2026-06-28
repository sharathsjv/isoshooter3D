using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;

    public GameObject[] AllTheBullets;
    void Awake()
    {
        instance = this;

        for(int i = 0; i<GetComponentsInChildren<BulletScript>().Length-1;i++)
        {
            AllTheBullets[i] = GetComponentsInChildren<BulletScript>()[i].gameObject;
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // for(int i = 0; i<GetComponentsInChildren<BulletScript>().Length-1;i++)
        // {
        //     AllTheBullets[i] = GetComponentsInChildren<BulletScript>()[i].gameObject;
        // }
    }
}

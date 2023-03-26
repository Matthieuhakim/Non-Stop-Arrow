using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats: MonoBehaviour
{

    public float agility = 1f;
    public float strength = 1f;
    public float health = 100f;


    public static PlayerStats instance;


    void Awake()
    {
        instance = GetComponent<PlayerStats>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

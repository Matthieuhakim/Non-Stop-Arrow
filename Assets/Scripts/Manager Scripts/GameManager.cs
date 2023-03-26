using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;


    void Awake()
    {
        instance = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

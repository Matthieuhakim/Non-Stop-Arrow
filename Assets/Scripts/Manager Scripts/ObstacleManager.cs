using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public List<TNT> activeTNTs;

    public static ObstacleManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = GetComponent<ObstacleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

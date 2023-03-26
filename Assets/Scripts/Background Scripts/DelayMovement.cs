using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayMovement : MonoBehaviour
{

    public float delay = 1f;

    private Rigidbody2D player;



    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }


    void Update()
    { 
        transform.position = new Vector3(transform.position.x + (player.velocity.x + delay) * Time.deltaTime, transform.position.y, transform.position.z);
    }
}

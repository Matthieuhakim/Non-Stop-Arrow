using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    public float speed = 1f;

    private Rigidbody2D rgdbody;


    // Start is called before the first frame update
    void Awake()
    {
        rgdbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rgdbody.velocity = new Vector2(speed, rgdbody.velocity.y);
    }
    
}

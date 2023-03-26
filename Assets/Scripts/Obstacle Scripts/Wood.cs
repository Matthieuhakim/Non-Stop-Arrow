using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{

    public Gate gate;


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.collider.tag == GameObjectTags.PLAYER)
        {
            gate.Explode();
        }

    }
}

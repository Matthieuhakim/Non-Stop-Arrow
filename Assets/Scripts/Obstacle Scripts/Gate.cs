using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private Transform player;

    private bool closed = false;
    public bool didHit = false;

    public float distanceToClose = 20f;
    

    public float damage = 20f;

    public Animator gateAnimator;
    public GameObject explosionParticlePrefab;


    

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    // Update is called once per frame
    void Update()
    {
        GoDownAtClose();

    }



    private void GoDownAtClose()
    {

        if (!closed && !didHit)
        {
            if (transform.position.x - player.position.x <= distanceToClose)
            {

                CloseDoor();
            }
        }
    }

    

    private void CloseDoor()
    {
        gateAnimator.enabled = true;
        closed = true;
    }


    public void Explode()
    {
        PlayerStats.instance.health -= damage;
        ExplosionEffect.Explode(gameObject, explosionParticlePrefab);
        Destroy(gameObject);
    }

}

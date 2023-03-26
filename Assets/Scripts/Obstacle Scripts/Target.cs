using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    private Animator gateAnimator;
    private Gate gate;

    // Start is called before the first frame update
    void Awake()
    {
        gateAnimator = GetComponent<Animator>();
        gate = GetComponentInParent<Gate>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag != BackgroundTags.CEILING_TAG && collision.transform.tag != BackgroundTags.FLOOR_TAG && collision.transform.tag != GameObjectTags.TNT)
        {
            if (collision.transform.tag == GameObjectTags.AMMO)
            {

                gateAnimator.SetBool("didHit", true);
                gate.didHit = true;
            }
        }
    }
}

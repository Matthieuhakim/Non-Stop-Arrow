using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;


    private ArrowStats arrowStats;
    private ArrowHolder arrowHolder;
    public AmmoType ammoType;


    public bool hasBeenShot = false;
    public bool readyToRecycle = false;
    public float maxDistanceToBow;
    public int positionInRecycle;

    private Vector3 previousPos;

    private float playerSpeed;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        arrowStats = GetComponent<ArrowStats>();
        arrowHolder = GetComponentInParent<ArrowHolder>();
        playerSpeed = GetComponentInParent<PlayerMovement>().speed;


        rb.bodyType = RigidbodyType2D.Kinematic;
        boxCollider.enabled = false;
    }


    // Update is called once per frame
    private void Update()
    {
        if(ammoType == AmmoType.Single)
        {
            RecycleWhenTooFar();
        }

    }


    private void FixedUpdate()
    {
        LookAtDirection();
    }



    public virtual void Shoot(Vector2 V0)
    {
        playerSpeed = GetComponentInParent<PlayerMovement>().speed;
        gameObject.transform.SetParent(null);

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity += V0;
        rb.velocity += new Vector2(playerSpeed, 0f);

        boxCollider.enabled = true;
        hasBeenShot = true;
    }




    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != GameObjectTags.PLAYER && collision.gameObject.tag != GameObjectTags.AMMO && collision.gameObject.tag != GameObjectTags.WEAPON && collision.gameObject.tag != GameObjectTags.UI && hasBeenShot)
        {
            if (collision.gameObject.tag == BackgroundTags.FLOOR_TAG || collision.gameObject.tag == BackgroundTags.CEILING_TAG)
            {
                rb.bodyType = RigidbodyType2D.Static;
                boxCollider.enabled = false;
                hasBeenShot = false;
            }
            else //if collision is with an enemy or a wall or smthing that doesnt explode and moves 
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
                boxCollider.enabled = false;
                hasBeenShot = false;
                gameObject.transform.SetParent(collision.transform);
            }
        }
    }




    private void RecycleWhenTooFar()
    {
        if (Vector3.Distance(transform.position, arrowHolder.transform.position) > maxDistanceToBow && !readyToRecycle)
        {
            if (arrowHolder.numOfArrowToBeRecycled < arrowHolder.maxArrowsToBeRecycled) // if there is space
            {
                arrowHolder.soonRecycledArrows[arrowHolder.numOfArrowToBeRecycled] = gameObject.GetComponent<Arrow>();
                //Put in recycle array


                positionInRecycle = arrowHolder.numOfArrowToBeRecycled;
                arrowHolder.numOfArrowToBeRecycled += 1;

                readyToRecycle = true;

                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);   //Destroy if Array is Full
            }
        }
    }


    private void LookAtDirection()
    {
        if (hasBeenShot && rb.bodyType != RigidbodyType2D.Static)
        {
            transform.rotation = LookAt2D(transform.position - previousPos);
        }

        previousPos = transform.position;
    }



    static Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }
}





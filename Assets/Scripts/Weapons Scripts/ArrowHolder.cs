using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHolder : MonoBehaviour
{

    public GameObject arrowPrefab;
    public GameObject tripleArrowPrefab;
    public GameObject explosiveArrowPrefab;

    public int numOfArrowToBeRecycled;
    public int maxArrowsToBeRecycled = 10;
    public Arrow[] soonRecycledArrows;

    private PlayerAimAndShoot playerAimAndShoot;

    private void Awake()
    {
        playerAimAndShoot = GetComponentInParent<PlayerAimAndShoot>();
    }

    // Start is called before the first frame update
    void Start()
    {
        soonRecycledArrows = new Arrow[maxArrowsToBeRecycled];
        SpawnArrow();
        
    }



    // Update is called once per frame
    void Update()
    {
        CorrectArrowType();
    }

    public Arrow FindAmmo()
    {
        Arrow arrow = GetComponentInChildren<Arrow>();
        return arrow;
    }


    public void SpawnArrow()
    {
        if (FindAmmo() == null)
        {
            switch (AmmoManager.instance.ammoType)
            {
                case AmmoType.Single:

                    SpawnSingleArrow();
                    break;

                case AmmoType.Triple:

                    SpawnTripleArrow();
                    break;


                case AmmoType.Explosive:
                    SpawnExplosiveArrow();
                    break;

                default:

                    Debug.Log("The arrow type: " + AmmoManager.instance.ammoType + ", is not available");
                    break;
            }
        }
    }


    private void CorrectArrowType()
    {
        if (FindAmmo() != null && !playerAimAndShoot.isAiming)
        {
            if (FindAmmo().ammoType != AmmoManager.instance.ammoType)
            {
                Destroy(FindAmmo().gameObject);
                SpawnArrow();
            }
        }
    }





    private void SpawnSingleArrow() {
        if(AmmoManager.instance.remainingArrows >= arrowPrefab.GetComponent<ArrowStats>().cost)
        {
            if (numOfArrowToBeRecycled > 0)
            {
                if (soonRecycledArrows[numOfArrowToBeRecycled - 1] != null)//see if it isn't empty
                {
                    ResetRecycledArrow(soonRecycledArrows[numOfArrowToBeRecycled - 1]); //recycle last
                }
            }
            else
            {
                GameObject newSingleArrow = Instantiate(arrowPrefab, gameObject.transform);
                //create new if there are no recycled arrows available
            }
        }

    }

    private void SpawnTripleArrow()
    {
        if (AmmoManager.instance.remainingArrows >= tripleArrowPrefab.GetComponent<ArrowStats>().cost)
        {
            GameObject newTripleArrows = Instantiate(tripleArrowPrefab, gameObject.transform);
        }
    }


    private void SpawnExplosiveArrow()
    {
        if (AmmoManager.instance.remainingArrows >= explosiveArrowPrefab.GetComponent<ArrowStats>().cost)
        {
            GameObject newExplosiveArrow = Instantiate(explosiveArrowPrefab, gameObject.transform);
        }
    }

    private void ResetRecycledArrow(Arrow recycleArrow)
    {
        recycleArrow.gameObject.SetActive(true);

        recycleArrow.transform.SetParent(transform);
        recycleArrow.transform.localPosition = Vector3.zero;
        recycleArrow.transform.localRotation = new Quaternion();

        recycleArrow.rb.bodyType = RigidbodyType2D.Kinematic;
        recycleArrow.boxCollider.enabled = false;

        soonRecycledArrows[recycleArrow.positionInRecycle] = null;
        recycleArrow.positionInRecycle = 0;

        recycleArrow.readyToRecycle = false;

        numOfArrowToBeRecycled -= 1;
    }


}

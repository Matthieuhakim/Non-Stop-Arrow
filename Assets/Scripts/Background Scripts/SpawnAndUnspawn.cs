using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnAndUnspawn : MonoBehaviour
{

    public float maxDistanceBeforeUnspawn;
    public float offsetBetweenEachObject;

    private Transform player;

    private GameObject[] otherSimilarObjects;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForUnspawn();
    }


    void CheckForUnspawn()
    {
        if(transform.position.x - player.position.x < -maxDistanceBeforeUnspawn)
        {
            MoveObjectForward();
        }
    }



    void MoveObjectForward()
    {
        otherSimilarObjects = GameObject.FindGameObjectsWithTag(gameObject.tag);

        float biggestPosition = transform.position.x;

        foreach(GameObject obj in otherSimilarObjects)
        {
            if(obj.transform.position.x > biggestPosition)
            {
                biggestPosition = obj.transform.position.x;
            }
        }
        float newPosition = biggestPosition + offsetBetweenEachObject;

        transform.position = new Vector3(newPosition, transform.position.y);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{

    public GameObject collectTokenParticlePrefab;
    private CircleCollider2D circleCollider;

    // Start is called before the first frame update
    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }



    // Update is called once per frame
    void Update()
    {
        
    }


    public virtual void ExecuteSpecialty()
    {

    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == GameObjectTags.PLAYER || collision.tag == GameObjectTags.AMMO)
        {
            Instantiate(collectTokenParticlePrefab, transform.position, Quaternion.identity);
            ExecuteSpecialty();
            Destroy(gameObject);
        }
    }


}

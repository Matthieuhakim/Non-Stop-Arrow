using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{

    public float damage = 10f;
    public float areaOfDamage = 3f;
    public string[] tagsAffectedByExplosion;


    public GameObject explosionParticlePrefab;


    // Start is called before the first frame update
    void Start()
    {
        ObstacleManager.instance.activeTNTs.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag != BackgroundTags.CEILING_TAG && collision.transform.tag != BackgroundTags.FLOOR_TAG && collision.transform.tag != GameObjectTags.TNT)
        {
            if (collision.transform.tag == GameObjectTags.PLAYER)
            {
                PlayerStats.instance.health -= damage;
            }

            Explode();
        }
    }

    public void Explode()
    {
        ObstacleManager.instance.activeTNTs.Remove(this);

        ExplosionEffect.Explode(gameObject, explosionParticlePrefab, areaOfDamage, damage, tagsAffectedByExplosion);
        Destroy(gameObject);
    }


    public void ExplodeAfterDelay()
    {
        StartCoroutine("WaitBeforeExploding");
    }

    private IEnumerator WaitBeforeExploding()
    {
        yield return new WaitForSeconds(0.1f);
        Explode();
    }
}

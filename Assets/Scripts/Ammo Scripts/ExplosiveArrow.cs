using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveArrow : Arrow
{
    private GameObject[] enemies;

    private ArrowStats explosiveArrowStats;
    private SpriteRenderer spriteRenderer;



    public GameObject explosionParticleEffectPrefab;

    public float delayAfterContact = 0.5f;

    public Color flickerColor;
    private Color normalColor;
    public float flickerTimer = 0.1f;

    public string[] tagsAffectedByExplosion;

    // Start is called before the first frame update
    void Start()
    {
        explosiveArrowStats = GetComponent<ArrowStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != GameObjectTags.PLAYER && collision.gameObject.tag != GameObjectTags.AMMO && collision.gameObject.tag != GameObjectTags.WEAPON && hasBeenShot)
        {
            if (collision.gameObject.tag == BackgroundTags.FLOOR_TAG || collision.gameObject.tag == BackgroundTags.CEILING_TAG)
            {
                rb.bodyType = RigidbodyType2D.Static;
                boxCollider.enabled = false;
                hasBeenShot = false;
            }
            else
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
                boxCollider.enabled = false;
                hasBeenShot = false;
                gameObject.transform.SetParent(collision.transform);
            }

            if (collision.gameObject.tag == GameObjectTags.TNT)
            {
                Explode();
            }
            else
            {
                StartCoroutine("Flicker");
                StartCoroutine("WaitBeforeExplosion");
            }
        }
    }



    private void Explode()
    {

        ExplosionEffect.Explode(gameObject, explosionParticleEffectPrefab, explosiveArrowStats.areaOfDamage, explosiveArrowStats.damage, tagsAffectedByExplosion);
        Destroy(gameObject);
    }



    private void SwitchColor()
    {
        Color color = spriteRenderer.color;

        if(color == normalColor)
        {
            color = flickerColor;
        }
        else if(color == flickerColor)
        {
            color = normalColor;
        }

        spriteRenderer.color = color;
    }



    private IEnumerator Flicker()
    {
        while(gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(flickerTimer);

            SwitchColor();

            yield return new WaitForSeconds(flickerTimer / 1.5f);

            SwitchColor();
        }
    }




    private IEnumerator WaitBeforeExplosion()
    {
        yield return new WaitForSeconds(delayAfterContact);
        Explode();
    }

}


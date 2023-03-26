using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{

    public static void Explode(GameObject gameObj, GameObject particle, float areaOfDamage, float damage, string[] tagsAffected)
    {
        gameObj.SetActive(false);

        Instantiate(particle, gameObj.transform.position, Quaternion.identity);

        foreach(string tag in tagsAffected)
        {
            switch (tag)
            {
                case GameObjectTags.TNT:
                    ExplodeNearbyTNT(gameObj, areaOfDamage);
                    break;

                case GameObjectTags.PLAYER:
                    HurtPlayerIfClose(gameObj, areaOfDamage, damage);
                    break;

                default:
                    Debug.Log(tag + ": tag unknown");
                    break;
            }
        }
    }


    public static void Explode(GameObject gameObj, GameObject particle)
    {
        gameObj.SetActive(false);

        Instantiate(particle, gameObj.transform.position, Quaternion.identity);
    }



    private static void ExplodeNearbyTNT(GameObject gameObj,float radius)
    {
        List<TNT> explodeTNT = new List<TNT>();

        foreach (TNT tnt in ObstacleManager.instance.activeTNTs)
        {
            float distance = Vector3.Distance(tnt.transform.position, gameObj.transform.position);
            if (distance <= radius)
            {
                explodeTNT.Add(tnt);
            }
        }

        for(int i = 0; i < explodeTNT.Count; i++)
        {
            explodeTNT[i].ExplodeAfterDelay();
        }
    }


    private static void HurtPlayerIfClose(GameObject gameObj, float radius, float damage)
    {
        GameObject playerGameObject = PlayerStats.instance.gameObject;

        float distance = Vector3.Distance(playerGameObject.transform.position, gameObj.transform.position);
        if (distance <= radius)
        {
            PlayerStats.instance.health -= damage;
        }
    }
}

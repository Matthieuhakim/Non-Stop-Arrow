using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleArrow : Arrow
{

    public Arrow arrowUp, arrowMid, arrowDown;

    private ArrowStats tripleArrowStat;

    public float spread = 1f;
    public float damageReductionForExtremities = 1f;

    private void Start()
    {
        tripleArrowStat = GetComponent<ArrowStats>();

        PassInfoToChildren();
    }


    private void PassInfoToChildren()
    {
        ArrowStats arrowUpStats, arrowMidStats, arrowDownStats;

        arrowUpStats = arrowUp.GetComponent<ArrowStats>();
        arrowMidStats = arrowMid.GetComponent<ArrowStats>();
        arrowDownStats = arrowDown.GetComponent<ArrowStats>();


        ArrowStats[] stats = new ArrowStats[3];
        stats[0] = arrowUpStats;
        stats[1] = arrowMidStats;
        stats[2] = arrowDownStats;


        foreach (ArrowStats stat in stats)
        {
            stat.damage = tripleArrowStat.damage;
            stat.speed = tripleArrowStat.speed;
        }

        arrowUpStats = stats[0];
        arrowMidStats = stats[1];
        arrowDownStats = stats[2];

        arrowUpStats.damage /= damageReductionForExtremities;
        arrowDownStats.damage /= damageReductionForExtremities;
    }



    public override void Shoot(Vector2 V0)
    {
        arrowUp.Shoot(V0 + Vector2.up * spread);
        arrowMid.Shoot(V0);
        arrowDown.Shoot(V0 + Vector2.down * spread);

        Destroy(gameObject);
    }
}

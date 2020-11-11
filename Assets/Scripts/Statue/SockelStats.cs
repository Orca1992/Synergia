using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SockelStats : MonoBehaviour
{
    public ComboStats[] combination;

    public ComboStats GetCombination(GodType mainGod)
    {
        foreach(ComboStats stats in combination)
        {
            if(stats.towerGod == mainGod)
            {
                return stats;
            }
        }

        return null;

    }

    
}

[Serializable]
public class ComboStats
{
    public GodType towerGod;
    public int cost;
    public GameObject bulletPrefab;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtemisArrowPoseidon : Bullet
{
    [SerializeField]
    [Range(0f, 1f)]
    private float stunChance;
    [SerializeField]
    private Vector2 MinMaxStun;

    protected override void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();


        if (e)
        {
            if (Random.Range(0f, 1f) <= stunChance)
            {
                e.StopMove(Random.Range(MinMaxStun.x, MinMaxStun.y));
            }
            e.TakeDamage(damage);
        }

    }
}

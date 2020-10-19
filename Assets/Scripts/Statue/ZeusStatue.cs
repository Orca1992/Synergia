using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusStatue : Statue
{

    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

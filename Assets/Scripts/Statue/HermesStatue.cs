using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermesStatue : Statue
{
    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

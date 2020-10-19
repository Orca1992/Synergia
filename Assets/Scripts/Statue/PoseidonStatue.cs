using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseidonStatue : Statue
{
    

    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    
}

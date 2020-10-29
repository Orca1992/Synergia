using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 1f;
    public int damage = 1;

    //ein wenig mit Partikel gearbeitet :)
    public GameObject impactEffect;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    public void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        //movement

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        //wenn  kein ziel getroffen haben
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    public void HitTarget()
    {
        //Partikel Effekt
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position,transform.rotation);
        Destroy(effectIns, 2f);

        Damage(target);

        Destroy(gameObject);
    }

    protected virtual void Damage (Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if(e != null)
        {
            e.TakeDamage(damage);
            
        }
       
    }
}

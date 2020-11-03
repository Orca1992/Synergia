using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : MonoBehaviour
{

    // Enemy der Statuen angreift
    public float speed = 10f;
    public float range = 3f;
    private float stoppingDistance = 3f;

    public string statueTag = "Statue";

    private Transform endtarget;
    private Transform target;
    private int wavepointIndex = 0;

    void Start()
    {
        //erster punkt des arrays
        endtarget = Waypoints.points[0];
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    //sucht immer eine neue Statue
    void UpdateTarget()
    {
        GameObject statue = GameObject.FindGameObjectWithTag(statueTag);

        float shortedDistance = Mathf.Infinity;
        GameObject nearestStatue = null;

        float distanceToStatue = Vector3.Distance(transform.position, statue.transform.position);
        if (distanceToStatue < shortedDistance)
        {
            shortedDistance = distanceToStatue;
            nearestStatue = statue;
        }

        if (nearestStatue != null && shortedDistance <= range)
        {
            target = nearestStatue.transform;
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        //wenn er kein Statue hat
        if(target == null)
        {
            //Enemy bewegt sich in die richtung(dir) des ziels
            Vector3 dir = endtarget.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, endtarget.position) <= 0.2f)
            {
                GetNextWaypoint();
            }

        }
        else
        {
            //target = null;

            //er bleibt vor der Statue stehen 
            if (Vector3.Distance(transform.position, target.position) > stoppingDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            
        }

      
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        wavepointIndex++;
        endtarget = Waypoints.points[wavepointIndex];
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

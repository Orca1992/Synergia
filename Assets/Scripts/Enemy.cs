using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public float speed = 10f;

    private Transform target;
    private int wavepointIndex = 0;

    void Start()
    {
        //erster punkt des arrays
        target = Waypoints.points[0];

    }
    
    void Update()
    {
        //Enemy bewegt sich in die richtung(dir)
         Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if(wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Die();
        }
    }

    //besser eine eigen Methode zu schreiben um animation oder partikel einzubauen
    void Die()
    {
        Destroy(gameObject);
    }
}

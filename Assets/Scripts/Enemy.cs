using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public float speed = 10f;

    private Transform target;
    private int wavepointIndex = 0;
    private bool canMove;

    void Start()
    {
        //erster punkt des arrays
        target = Waypoints.points[0];
        canMove = true;

    }
    
    void Update()
    {
      if (canMove)
        {
            //Enemy bewegt sich in die richtung(dir)
            Vector3 dir = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(dir);
            transform.rotation = rotation;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);


            if (Vector3.Distance(transform.position, target.position) <= 0.2f)
            {
                GetNextWaypoint();
            }

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
        WaveSpawner.EnemiesAlive--;
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

    //besser eine eigene Methode zu schreiben um animation oder partikel einzubauen
    void Die()
    {
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }

    //ZeusPoseidonBullet
    public void StopMove(float duration)
    {
        canMove = false;
        StartCoroutine(MoveDuration(duration));

    }

    private IEnumerator MoveDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        canMove = true;
    }
}

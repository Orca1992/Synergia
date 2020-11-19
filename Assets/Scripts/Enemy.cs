using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public Action<GameObject> onDeath;
    public float health = 100;
    public float startspeed = 10f;
    [HideInInspector]
    public float speed;
    public int worth = 10;

    private Transform target;
    private int wavepointIndex = 0;
    private bool canMove;

    void Start()
    {
        //erster punkt des arrays
        target = Waypoints.points[0];
        canMove = true;

        speed = startspeed;

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

            speed = startspeed;
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

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Die();
            PlayerStats.Money += worth;

        }
    }

    //besser eine eigene Methode zu schreiben um animation oder partikel einzubauen
    void Die()
    {
        WaveSpawner.EnemiesAlive--;
        onDeath?.Invoke(gameObject);
        Destroy(gameObject);
    }

    //ZeusPoseidonBullet
    public void StopMove(float duration)
    {
        canMove = false;
        StartCoroutine(MoveDuration(duration));

    }
    //PoseidonBeam
    public void Slow(float pct)
    {
        speed = startspeed * (1f - pct);
    }

    private IEnumerator MoveDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        canMove = true;
    }
}

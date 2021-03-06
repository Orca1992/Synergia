﻿using System.Collections;
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
    public float distanceToGoal { get; private set; }

    private Transform[] path;
    public GameObject die_effect;

    private Transform target;
    private int wavepointIndex = 0;
    private bool canMove;

    Animator animator;
    private AudioSource source;

    public Material Highlight;
    private Material Current;

    public Renderer renderer;

    public void Init(Transform[] path)
    {

        this.path = path;
        //erster punkt des arrays
        target = path[0];
        canMove = true;

        speed = startspeed;
        for(int i = 0; i < path.Length-1; i++)
        {
            //
            distanceToGoal += Vector3.Distance(path[i].position, path[i + 1].position);
        }

    }

    void Start()
    {
        Current = renderer.material;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
      if (canMove)
        {
            animator.SetBool("isWalking", true);

            //Enemy bewegt sich in die richtung(dir)
            Vector3 dir = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(dir);
            
            transform.rotation = rotation;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            distanceToGoal -= speed * Time.deltaTime;


            if (Vector3.Distance(transform.position, target.position) <= 0.2f)
            {
                GetNextWaypoint();
            }

            speed = startspeed;
        }
    }

    void GetNextWaypoint()
    {
        if(wavepointIndex >= path.Length - 1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = path[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }

    public void TakeDamage(float amount)
    {
        renderer.material = Highlight;
        Invoke("ResetHighlight", 0.1f);
        health -= amount;
        if(health <= 0)
        {
            
            Die();
            PlayerStats.Money += worth;

        }
    }

    void ResetHighlight()
    {
        renderer.material = Current;
    }

    //besser eine eigene Methode zu schreiben um animation oder partikel einzubauen
    void Die()
    {
        GameObject dieIns = (GameObject)Instantiate(die_effect, transform.position, transform.rotation);

        animator.SetBool("isWalking", false);
        WaveSpawner.EnemiesAlive--;
        onDeath?.Invoke(gameObject);
        //Destroy(gameObject);
        Destroy(gameObject);
    }

    //ZeusPoseidonBullet
    public void StopMove(float duration)
    {
        animator.SetBool("isWalking", false);
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

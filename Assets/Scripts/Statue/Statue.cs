using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    public Transform target;
    public GodType statueType { get; private set; }
    public GodType sockelType { get; private set; }


    [Header("Attributes")]
    public TowerStats activeStats;
    public StatueConfig config;

    public float range;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public GameObject bulletPrefab;
    public Transform firePoint;

    void Start()
    {

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        config = GetComponent<StatueConfig>();
        statueType = GodType.None;
        sockelType = GodType.None;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            //speichert die Distanz der Gegner in Unity units
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }

    void Update()
    {
        if(target == null)
        {
            return;
        }


        //shooting Rate

        if(fireCountdown <= 0f && statueType != GodType.None)
        {

            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime; 
    }

    void Shoot()
    {

        GameObject bulletGO = (GameObject)Instantiate(activeStats.bulletPrefab, firePoint.position, firePoint.rotation);
        //die referenz für das Projektil, danach wird das Ziel übegeben von der statue
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        
        if (bullet != null)
        {
            bullet.damage = activeStats.damage;
            bullet.Seek(target);
        }
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    //setter für die godtype statue und sockel
    public void ChangeStatue(GodType type)
    {
        statueType = type;
        config.SetStatue(type);

        if (type == GodType.Sell)
        {
            activeStats = null;
            statueType = GodType.None;
        }
        else
        {
            
            activeStats = config.GetStats();
        }
        
    }

    public void ChangeSockel(GodType type)
    {
        sockelType = type;
        config.SetSockel(type);

        if (type == GodType.Sell)
        {
            sockelType = GodType.None;
        }
        
    }


}

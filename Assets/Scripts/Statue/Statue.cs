using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    public Transform target;
    public GodType statueType { get; private set; }
    public GodType sockelType { get; private set; }

    public GameObject rangePrefab;
    private EnemyDetection detection;


    [Header("Attributes")]
    public TowerStats activeStats;
    public StatueConfig config;

    public float range;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    private GameObject activeBullet;


    private Transform activeFirepoint;

    public Transform firePointPoseidon;
    public Transform firePointZeus;
    public Transform firePointArtemis;

    void Start()
    {
        var rangeGO = Instantiate(rangePrefab, transform.position, Quaternion.identity);
        detection = rangeGO.GetComponent<EnemyDetection>();
        detection.OnTargetChanged += OnTargetChanged;
        //für die Towerrange
        detection.Init(15f);
        //InvokeRepeating("UpdateTarget", 0f, 0.5f);
        config = GetComponent<StatueConfig>();
        statueType = GodType.None;
        sockelType = GodType.None;

    }

    private void OnTargetChanged(GameObject newTarget)
    {
        target = newTarget.transform;
        Debug.Log(newTarget);
    }


    void UpdateTarget()
    {
        if(statueType == GodType.None)
        {
            return;
        }
//        target = detection.GetEnemy();


        //GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        //float shortestDistance = Mathf.Infinity;
        //GameObject nearestEnemy = null;

        //foreach (GameObject enemy in enemies)
        //{
        //    //speichert die Distanz der Gegner in Unity units
        //    float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
        //    if (distanceToEnemy < shortestDistance)
        //    {
        //        shortestDistance = distanceToEnemy;
        //        nearestEnemy = enemy;
        //    }
        //}

        //if (nearestEnemy != null && shortestDistance <= range)
        //{
        //    target = nearestEnemy.transform;
        //}
        //else
        //{
        //    target = null;
        //}

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
            SetFirepoint(statueType);
            if(target != null)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }
        
        fireCountdown -= Time.deltaTime; 
    }

    void Shoot()
    {

        GameObject bulletGO = (GameObject)Instantiate(activeBullet, activeFirepoint.position, activeFirepoint.rotation);
        //die referenz für das Projektil, danach wird das Ziel übegeben von der statue
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        
        if (bullet != null)
        {
            bullet.damage = activeStats.damage;
            //problem mit null
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
        config.SetSockel(type);

        if (type == GodType.Sell)
        {
            detection.setStatus(false);
            activeStats = null;
            activeBullet = null;
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
            detection.setStatus(false);
            sockelType = GodType.None;
            activeBullet = null;
        }
        else
        {
            activeBullet = config.GetCurProjectile(statueType);
        }
        
    }

    public void SetFirepoint(GodType statuetyp)
    {
        switch (statuetyp)
        {
            case GodType.Zeus:
                activeFirepoint = firePointZeus;
                break;
            case GodType.Poseidon:
                activeFirepoint = firePointPoseidon;
                break;
            case GodType.Artemis:
                activeFirepoint = firePointArtemis;
                break;
            case GodType.Hermes:
                activeFirepoint = null;
                break;
        }
    }

    //public void SetBullet(GodType statuetyp, GodType sockeltyp)
    //{
    //    switch (statuetyp)
    //    {
    //        case GodType.Zeus:
    //             switch(sockeltyp)
    //             {
    //                case GodType.Zeus:
    //                    activeBullet = activeStats.baseBullet;
    //                    break;
    //                case GodType.Poseidon:
    //                    activeBullet = activeStats.upgradeBullet1;
    //                    break;
    //                case GodType.Artemis:
    //                    activeBullet = activeStats.upgradeBullet2;
    //                    break;
    //                default:
    //                    activeBullet = null;
    //                    break;
    //            }
    //         case GodType.Poseidon:
    //            switch(sockeltyp)
    //            {
    //                case GodType.Poseidon:
    //                    activeBullet = activeStats.baseBullet;
    //                    break;
    //                case GodType.Zeus:
    //                    activeBullet = activeStats.upgradeBullet1;
    //                    break;
    //                case GodType.Artemis:
    //                    activeBullet = activeStats.upgradeBullet2;
    //                    break;
    //                default:
    //                    activeBullet = null;
    //                    break;
    //            }
    //        case GodType.Artemis:
    //            switch(sockeltyp)
    //            {
    //            case GodType.Artemis:
    //                activeBullet = activeStats.baseBullet;
    //                break;
    //            case GodType.Zeus:
    //                activeBullet = activeStats.upgradeBullet1;
    //                break;
    //            case GodType.Poseidon:
    //                activeBullet = activeStats.upgradeBullet2;
    //                break;
    //            default:
    //                activeBullet = null;
    //                    break;
    //            }
    //        case GodType.Hermes:
    //            activeBullet = null;
    //            break;

    //    }
    //}


}

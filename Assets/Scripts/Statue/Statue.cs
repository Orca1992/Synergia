using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    [Header("General")]
    public Transform target;
    private Transform activeFirepoint;
    public ComboStats towerStats { get; private set; }
    //
    private ComboStats upgradeStats;

    public GodType statueType { get; private set; }
    public GodType sockelType { get; private set; }

    [Header("Range")]
    //public string enemyTag = "Enemy";
    public GameObject rangePrefab;
    private EnemyDetection detection;

    
    [Header("Shooting")]
    public StatueConfig config;

    private float fireCountdown = 0f;

    [Header("Firepoints")]
    public Transform firePointPoseidon;
    public Transform firePointZeus;
    public Transform firePointArtemis;

    [Header("WaterBeam")]
    public bool useBeam = false;

    public LineRenderer line;
    public ParticleSystem impactBeam;


    void Start()
    {
        upgradeStats = new ComboStats();
        var rangeGO = Instantiate(rangePrefab, transform.position, Quaternion.identity);
        detection = rangeGO.GetComponent<EnemyDetection>();
        detection.OnTargetChanged += OnTargetChanged;
        detection.ownStatue = this;
        //für die Towerrange
        detection.Init(0f, true);
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
        if (statueType == GodType.Hermes)
            return;

        

        //shooting Rate
        if(useBeam)
        {
            if (target == null)
            {
                line.enabled = false;
                impactBeam.Stop();
            }
            else
            {
                Beam();
            }
        }
        else
        {
           
            if (fireCountdown <= 0f && statueType != GodType.None)
            {
                SetFirepoint(statueType);
                if (target != null)
                {
                    Shoot();
                    fireCountdown = 1f * (towerStats.fireRate + upgradeStats.fireRate);
                }
            }
            fireCountdown -= Time.deltaTime;
        }  
    }


    void Beam()
    {
        target.GetComponent<Enemy>().TakeDamage((towerStats.damage + upgradeStats.damage) * Time.deltaTime);
        //
        target.GetComponent<Enemy>().Slow(towerStats.fireRate + upgradeStats.fireRate);

        //graphic
        if (!line.enabled)
        {
            line.enabled = true;
            impactBeam.Play();

        }
        line.SetPosition(0, firePointPoseidon.position);
        line.SetPosition(1, target.position);

        //Vector3 dir = firePointPoseidon.position = target.position;

        //impactBeam.transform.position = target.position + dir.normalized * .5f;

        //impactBeam.transform.rotation = Quaternion.LookRotation(dir);

        impactBeam.transform.position = target.position;

    }

    void Shoot()
    {

        GameObject bulletGO = (GameObject)Instantiate(towerStats.bulletPrefab, activeFirepoint.position, activeFirepoint.rotation);
        //die referenz für das Projektil, danach wird das Ziel übegeben von der statue
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        
        if (bullet != null)
        {
            bullet.damage = towerStats.damage + upgradeStats.damage;
            //problem mit null
            bullet.Seek(target);
            
        }
    }

    //setter für die godtype statue und sockel
    public void ChangeStatue(GodType type)
    {
        statueType = type;
        config.SetStatue(type);
        config.SetSockel(type);
        if(type == GodType.Poseidon)
        {
            useBeam = true;
        }
        if (type == GodType.Sell)
        {
            SellTower();
        }
        else
        {
            towerStats = config.GetStats(statueType);
        }

    }

    public void ChangeSockel(GodType type)
    {
        sockelType = type;
        config.SetSockel(type);

        if (type == GodType.Sell)
        {
            SellTower();
        }
        else
        {
           towerStats = config.GetStats(statueType);
           detection.Init(towerStats.range + upgradeStats.buffRange, statueType == GodType.Hermes? true : false);
           
        }      
    }

    private void SellTower()
    {
        if(statueType == GodType.Hermes)
        {
            detection.OnTowerSell();
        }
        detection.SetStatus(false);
        statueType = GodType.None;
        sockelType = GodType.None;
    }

    public void onBuff(ComboStats buff)
    {
        upgradeStats = buff;
        detection.Init(towerStats.range + upgradeStats.buffRange, statueType == GodType.Hermes ? true : false);
    }

    public void clearBuff()
    {
        upgradeStats.buffRange = 0f;
        upgradeStats.damage = 0f;
        upgradeStats.fireRate = 0f;

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

}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDetection : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> statues = new List<GameObject>();
    private GameObject curTarget;
    private bool isActive;

    private float timer;
    private float intervalTick = 0.5f;

    public Statue ownStatue;
    public UnityAction<GameObject> OnTargetChanged;
    public Transform rangeIndicator;

    public void Update()
    {
        if(isActive)
        {
            timer += Time.deltaTime;
            if(timer >= intervalTick)
            {
                UpdateTarget();
                timer = 0;
            }
        }
    }

    private void UpdateTarget()
    {
        GameObject tempTarget = null;
        if(enemies.Count > 0)
        {
            tempTarget = enemies[0];
        }
        else
        {
            //kein Gegner in Reichweite
            curTarget = null;
            OnTargetChanged?.Invoke(curTarget);
        }
        foreach(GameObject enemy in enemies)
        {
            if(enemy.GetComponent<Enemy>().distanceToGoal < tempTarget.GetComponent<Enemy>().distanceToGoal)
            {
                tempTarget = enemy;
            }
        }
        curTarget = tempTarget;
        OnTargetChanged?.Invoke(curTarget);
    }

    public void SetRange(float range)
    {
        rangeIndicator.localScale = new Vector3(range * 2f, 0.1f, range * 2f);
    }

    public void Init(float range, bool isBuffTower)
    {
        isActive = true;
        if(isBuffTower)
        {
            TowersInRange(range);
            GetComponent<SphereCollider>().radius = range;
        }
        else
        {
            GetComponent<SphereCollider>().radius = range;
            foreach(var enemy in enemies)
            {
                //die distanz zwischen gegner und turm  größer ist als die max reichweite
                if (Vector3.Distance(enemy.transform.position, transform.position) >= range)
                {
                    enemies.Remove(enemy);
                }
            }
            if(!enemies.Contains(curTarget))
            {
                if (enemies.Count > 0)
                {
                    curTarget = enemies[enemies.Count - 1];   
                    OnTargetChanged?.Invoke(curTarget);
                }
                else
                {
                    curTarget = null;
                    OnTargetChanged?.Invoke(curTarget);
                }
            }
            
        }
    }

    private void OnEnemyDeath(GameObject enemy)
    {
        if(enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
            UpdateTarget();
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isActive)
        {
            return;
        }
        //Debug.Log(other.name);
        if(other.CompareTag("Enemy"))
        {
            if(curTarget == null)
            {
                curTarget = other.gameObject;
                OnTargetChanged?.Invoke(curTarget);
            }
            enemies.Add(other.gameObject);
            other.gameObject.GetComponent<Enemy>().onDeath += OnEnemyDeath;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!isActive)
        {
            return;
        }
        if (other.CompareTag("Enemy"))
        {
            
            if (enemies.Contains(other.gameObject))
            {
                other.gameObject.GetComponent<Enemy>().onDeath -= OnEnemyDeath;
                enemies.Remove(other.gameObject);
                if(curTarget == other.gameObject)
                {
                    UpdateTarget();
                }
            }

        }
    }

    private void TowersInRange(float range)
    {
        statues.Clear();
        Collider[] targets = Physics.OverlapSphere(transform.position, range);

        foreach (var objects in targets)
        {
            if (objects.CompareTag("Node"))
            {
                statues.Add(objects.GetComponent<Node>().statueTransform.gameObject);
            }
        }
        foreach (var statue in statues)
        {
            if (statue.GetComponent<Statue>().statueType != GodType.Hermes)
            {
                Debug.Log(statue.GetComponent<Statue>().statueType);
                statue.GetComponent<Statue>().onBuff(ownStatue.towerStats);
            } 
        }
    }

    public void OnTowerSell()
    {
        foreach (var statue in statues)
        {
            statue.GetComponent<Statue>().clearBuff();
        }
    }
    public void SetStatus(bool status)
    {
        isActive = status;
        if(!status)
        {
            enemies.Clear();
            statues.Clear();
        }

    }

    

}

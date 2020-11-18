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

    public Statue ownStatue;
    public UnityAction<GameObject> OnTargetChanged;


    public void Init(float range, bool isBuffTower)
    {
        isActive = true;
        if(isBuffTower)
        {
            TowersInRange(range);
            GetComponent<SphereCollider>().radius = 0;
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
                enemies.Remove(other.gameObject);
                if(curTarget == other.gameObject)
                {
                    if(enemies.Count > 0)
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
    }

    private void TowersInRange(float range)
    {
        statues.Clear();
        Collider[] targets = Physics.OverlapSphere(transform.position, range);
        foreach (var objects in targets)
        {
            if (objects.CompareTag("Statue"))
            {
                statues.Add(objects.GetComponent<Node>().statueTransform.gameObject);
            }
        }
        foreach (var statue in statues)
        {
            statue.GetComponent<Statue>().onBuff(ownStatue.towerStats);
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

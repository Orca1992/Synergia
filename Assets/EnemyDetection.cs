using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDetection : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();
    private GameObject curTarget;
    public UnityAction<GameObject> OnTargetChanged;
    private bool isActive;

    public void Init(float range)
    {
        isActive = true;
        GetComponent<SphereCollider>().radius = range;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isActive)
        {
            return;
        }
        Debug.Log(other.name);
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

    public void setStatus(bool status)
    {
        isActive = status;
        if(!status)
        {
            enemies.Clear(); 
        }

    }

}

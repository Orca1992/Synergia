﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WaveCont
{
    public List<Wave> enemies = new List<Wave>();
    //die zeit wielange eine Wave braucht, bis die nächste wave spawnbar ist
    public float waveduration = 30f;
}

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public WaveCont[] waves;

    public Transform spawnpoint1;
    public Transform spawnpoint2;

    public float timeBetweenWaves = 5f;

    //die zeit wann die nächste wave spawned
    public float countdown = 20f;

    //für die UI 
    public Text waveCountdownText;

    private int waveIndex = -1;
    private float timer;
    private bool waveactive;
    float waveduration;

    public int GetMaxWaves { get { return waves.Length; } }
    public int GetCurrentWave { get { return waveIndex+1; } }

    void Start()
    {
        Invoke("StartGame", countdown);
    }

    void Update()
    {
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }

        //boolean waveactive true, timer+ reale zeit
        //timer größer die zeit der wellenzeit
        if (waveactive)
        {
            timer += Time.deltaTime;
            if (timer > waveduration)
            {
                SpawnNextWave();
            }
        }
        else
        {
            countdown -= Time.deltaTime;

            //Text für die UI
            waveCountdownText.text = countdown.ToString("F1");
        }

        //wenn der index so groß ist wie die wirklich länge  und keine alle enemies besiegt sind
        //WIN condition
        if (waveIndex == waves.Length && EnemiesAlive == 0)
        {

            waveactive = false;
            Debug.Log("Level gewonnen!");
            this.enabled = false;
        }


        
    }

    
    void SpawnNextWave()
    {
        waveIndex++;
        timer = 0;
        if(waveIndex >= waves.Length)
        {
            //alle Wellen verbraucht, spiel gewonnen weil überlebt
            return;
        }
        waveduration = waves[waveIndex].waveduration;
        //Debug.LogFormat("maxWaves: {0} von {1}", waveIndex+1, waves.Length );
        //EnemiesAlive = 0;
        for (int i = 0; i < waves[waveIndex].enemies.Count; i++)
        {
            Wave wave = waves[waveIndex].enemies[i];
            EnemiesAlive += wave.amountEnemy;
            StartCoroutine(SpawnEnemy(wave));
        }

        
    }

    IEnumerator SpawnEnemy(Wave wavedata)
    {
        int spawned = wavedata.amountEnemy;
        while (spawned > 0)
        {
            //multiple enemy spawn
            for (int i = 0; i < wavedata.clusterSpawn; i++)
            {
                if (spawned > 0)
                {
                    Instantiate(wavedata.enemy, spawnpoint1.position, spawnpoint1.rotation);
                    //Debug.Log("spawned");
                    spawned--;
                }
            }
            yield return new WaitForSeconds(wavedata.rate);
        }
        //Debug.Log("spawn finish!");
    }

    private void StartGame()
    {
        SpawnNextWave();
        waveactive = true;
        waveCountdownText.gameObject.SetActive(false);
    }
}

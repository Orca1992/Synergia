using System.Collections;
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
    private float countdown = 10f;

    //für die UI 
    public Text waveCountdownText;
    public static int waveMax = 0;
    public static int waveCur;

    public int waveIndex = 0;
    private float timer;
    private bool waveactive;
    float waveduration;

    void Start()
    {
        Invoke("SpawnNextWave", countdown);
        waveactive = true;
        waveMax = waves.Length;
        waveCur = 1;
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
                waveIndex++;
            }
        }

        //wenn der index so groß ist wie die wirklich länge  und keine alle enemies besiegt sind
        //WIN condition
        if (waveIndex == waves.Length && EnemiesAlive == 0)
        {

            waveactive = false;
            Debug.Log("Level gewonnen!");
            this.enabled = false;
        }


        countdown -= Time.deltaTime;

        //Text für die UI
        waveCountdownText.text = countdown.ToString("F1"); 
    }

    
    void SpawnNextWave()
    {
        timer = 0;
        waveduration = waves[waveIndex].waveduration;
        
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
                    EnemiesAlive++;
                    spawned--;
                }
            }
            yield return new WaitForSeconds(wavedata.rate);
        }
    }
}

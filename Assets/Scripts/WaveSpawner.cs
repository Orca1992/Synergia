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

    public Text waveCountdownText;

    private int waveIndex = 0;
    private float timer;
    private bool waveactive;
    float waveduration;

    void Start()
    {
        Invoke("SpawnNextWave", countdown);
        waveactive = true;
    }

    void Update()
    {
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }

        //startverzögerung
        if (waveactive)
        {
            timer += Time.deltaTime;
            if (timer > waveduration)
            {
                SpawnNextWave();
            }
        }

        if (waveIndex == waves.Length && EnemiesAlive == 0)
        {

            waveactive = false;
            Debug.Log("Level gewonnen!");
            this.enabled = false;
        }


        countdown -= Time.deltaTime;

        //Text für die UI
        waveCountdownText.text = countdown.ToString("F2"); 
    }

    //erlaubt erst nach einer gewissen Zeit den Code weiterauszführen
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

        //Win-Condition, wenn alle Waves durch sind 
        waveIndex++;
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

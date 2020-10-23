using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public Transform spawnpoint1;
    public Transform spawnpoint2;

    public float timeBetweenWaves = 5f;
    //die zeit wann die nächste wave spawned
    private float countdown = 2f;

    public Text waveCountdownText;

    private int waveIndex = 0;

    void Update()
    {
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }

        if (EnemiesAlive > 0)
        {
            return;
        }
       if(countdown <= 0f)
        {
            // so spricht man eine methode an mit IEnumerator
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        //Text für die UI
        waveCountdownText.text = Mathf.Round(countdown).ToString(); 
    }

    //erlaubt erst nach einer gewissen Zeit den Code weiterauszführen
    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];

        for(int i = 0; i < wave.amountEnemy; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        //Win-Condition, wenn alle Waves durch sind 
        if (waveIndex == waves.Length)
        {
            Debug.Log("Level gewonnen!");
            this.enabled = false;
        }
        
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnpoint1.position, spawnpoint1.rotation);
        EnemiesAlive++;
        //in dem Fall auch hier nochmal, weil 2 gespawnt werden
        Instantiate(enemy, spawnpoint2.position, spawnpoint2.rotation);
        EnemiesAlive++;

    }
}

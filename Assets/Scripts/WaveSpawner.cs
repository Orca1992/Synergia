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
    
    public float countdown = 20f; //der Countdown, Anfang des Levels
    public static int EnemiesAlive = 0;
    private int waveIndex = -1;
    private float timer;
    private bool waveactive;
    float waveduration; // der Zeitabstand der nächsten Welle


    public WaveCont[] waves;

    public Transform spawnpoint1;
    public Transform spawnpoint2;

    [Header("WaveUI")]
    public Text waveCountdownText;

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
        if (waveactive)
        {
            timer += Time.deltaTime;
            //timer größer die zeit der wellenzeit
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

        //WIN-CONDITION
        //wenn der index so groß ist wie die wirklich länge und es keine enemies leben
        if (waveIndex == waves.Length && EnemiesAlive == 0)
        {

            waveactive = false;
            //- WIN UI einbauen
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
            //alle Wellen verbraucht, spiel gewonnen alle Wellen überlebt
            return;
        }

        waveduration = waves[waveIndex].waveduration;
        Debug.LogFormat("maxWaves: {0} von {1}", waveIndex+1, waves.Length );

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
        Debug.Log(wavedata.rate);
        int spawned = wavedata.amountEnemy;
        while (spawned > 0)
        {
            //multiple enemy spawn
            
            if (spawned > 0)
            {
                Instantiate(wavedata.enemy, spawnpoint1.position, spawnpoint1.rotation);
                //Debug.Log("spawned");
                spawned--;
            }
                yield return new WaitForSeconds(wavedata.rate);
            

            //for (int i = 0; i < wavedata.clusterSpawn; i++)
            //{
            //    if (spawned > 0)
            //    {
            //        Instantiate(wavedata.enemy, spawnpoint1.position, spawnpoint1.rotation);
            //        //Debug.Log("spawned");
            //        spawned--;
            //    }
            //}
            //yield return new WaitForSeconds(wavedata.rate);
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

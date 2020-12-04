using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WaveCont
{
    public List<Wave> enemies = new List<Wave>();
}

public class WaveSpawner : MonoBehaviour
{
    
    public float countdown = 20f; //der Countdown, Anfang des Levels
    public static int EnemiesAlive = 0;
    private int waveIndex = -1;
    private bool waveactive;
    private float timer;
    private float waveTime;
    private bool gameFinished;
    private bool uiActive;

    public GameObject winUI;

    public WaveCont[] waves;

    [Header("WaveUI")]
    public Text waveCountdownText;

    public int GetMaxWaves { get { return waves.Length; } }
    public int GetCurrentWave { get { return waveIndex+1; } }

    void Start()
    {
        Invoke("StartGame", countdown);
        winUI.SetActive(false);
    }

    void Update()
    {
        //Debug.Log(timer);
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
            if (timer > waveTime)
            {
                SpawnNextWave();
                timer = 0f;
            }

        }
        else
        {
            countdown -= Time.deltaTime;

            //Text für die UI
            waveCountdownText.text = countdown.ToString("F1");
        }
        if(gameFinished && EnemiesAlive == 0)
        {
            if(!uiActive)
            {
                Debug.Log("Gewonnen");
                GameFinish();
                uiActive = true;
            }

        }
        
    }

    
    void SpawnNextWave()
    {
        waveIndex++;
        //timer = 0;

        if(waveIndex >= waves.Length)
        {
            //alle Wellen verbraucht, spiel gewonnen alle Wellen überlebt

            waveactive = false;
            gameFinished = true;
            return;
        }

        Debug.LogFormat("maxWaves: {0} von {1}", waveIndex+1, waves.Length );

        
        StartCoroutine(DelaySubWave());

    }

    IEnumerator DelaySubWave()
    {
        waveTime = GetTotalWaveTime();
        int cachedIndex = waveIndex;

        //Debug.Log(waveIndex);
        for (int i = 0; i < waves[cachedIndex].enemies.Count; i++)
        {

            Wave wave = waves[cachedIndex].enemies[i];
            EnemiesAlive += wave.amountEnemy;
            StartCoroutine(SpawnEnemy(wave));
            float timeBetweenWaves =
                waves[cachedIndex].enemies[i].rate *
                waves[cachedIndex].enemies[i].amountEnemy +
                waves[cachedIndex].enemies[i].extraWavetime;
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    IEnumerator SpawnEnemy(Wave wavedata)
    {
        //Debug.Log(wavedata.rate);
        int spawned = wavedata.amountEnemy;
        while (spawned > 0)
        {
            //multiple enemy spawn
            
            if (spawned > 0)
            {
                
                var GO = Instantiate(wavedata.enemy, wavedata.waypoints.points[0].position , wavedata.waypoints.points[0].rotation);
                GO.GetComponent<Enemy>().Init(wavedata.waypoints.points);
                //Debug.Log("spawned");
                spawned--;
            }
                yield return new WaitForSeconds(wavedata.rate);
        }

    }

    private float GetTotalWaveTime()
    {
        float time = 0;

        for (int i = 0; i < waves[waveIndex].enemies.Count; i++)
        {

            time +=
                waves[waveIndex].enemies[i].rate *
                waves[waveIndex].enemies[i].amountEnemy +
                waves[waveIndex].enemies[i].extraWavetime;
            
        }
        return time;

    }

    public void StartEarlyWave()
    {
        //Button skip
        waveTime = 0;
    }

    private void GameFinish()
    {
        winUI.SetActive(true);
        Debug.Log("Ende");

    }

    private void StartGame()
    {
        SpawnNextWave();
        waveactive = true;
        waveCountdownText.gameObject.SetActive(false);
    }
}

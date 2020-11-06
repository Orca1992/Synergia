using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{ 
    public Text waveText;
    public WaveSpawner spawner;
    void Update()
    {
        //Ausgabe: zB 1. /4 Welle
        waveText.text = spawner.GetCurrentWave + " / " + spawner.GetMaxWaves + " Welle";
    }
}

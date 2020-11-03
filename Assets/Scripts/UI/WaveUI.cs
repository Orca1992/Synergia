using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{ 
    public Text waveText;

    void Update()
    {
        //Ausgabe: zB 1. /4 Welle
        waveText.text = WaveSpawner.waveCur + " / " + WaveSpawner.waveMax + " Welle";
    }
}

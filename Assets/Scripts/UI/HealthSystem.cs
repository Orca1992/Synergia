using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider.value = PlayerStats.startLives;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = PlayerStats.Lives;
    }
}

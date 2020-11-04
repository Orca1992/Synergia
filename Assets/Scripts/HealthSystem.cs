using System;
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

    private void Update()
    {
        slider.value = PlayerStats.Lives;
    }


    public void SetMaxHealth(int health)
    {
        slider.maxValue = PlayerStats.startLives;
        slider.value = health;
    }


    public void SetHealth(int health)
    {
        slider.value = health;
    }
}

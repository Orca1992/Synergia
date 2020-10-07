using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameEnded = false;

    void Update()
    {
        if(PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
        Debug.Log("Game Over!");
    }
}

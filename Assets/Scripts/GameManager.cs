using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public static bool GameIsOver;

    void Start()
    {
        GameIsOver = false;
    }
    
    void Update()
    {
        if (GameIsOver)
            return;

        if(Input.GetKeyDown("e"))
        {
            EndGame();
        }
        if(PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }
}

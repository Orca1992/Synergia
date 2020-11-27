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

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Level1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void Level2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
    public void Level3()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
    }
    public void Level4()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(4);
    }
    public void Level5()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(5);
    }

    public void LevelSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(6);
    }

    public void HowToPlay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(7);
    }

    void EndGame()
    {
        Time.timeScale = 0f;
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }

    
}

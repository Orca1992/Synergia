using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        //Pausemenu ist true wenn er einmal space drückt oder p,
        //beim zweiten mal drücken wird die boolean methode wieder false
        ui.SetActive(!ui.activeSelf);

        // wenn true dann stoppt die TIME; 
        if(ui.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        Toggle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}

using UnityEngine;

public class HelpSettings: MonoBehaviour
{
    public GameObject ui;

   

    public void Toggle()
    {
       
        ui.SetActive(!ui.activeSelf);

        
        if (ui.activeSelf)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }
}
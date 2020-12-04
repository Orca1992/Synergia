using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fastForward : MonoBehaviour
{

    public GameObject play;
    public GameObject fastPlay;

    
         public void SwitchSpeed()
         {
            if (Time.timeScale==1f)
            {
            Time.timeScale = 2f;
            fastPlay.SetActive(true);
            play.SetActive(false);
            }

            else
            {
            Time.timeScale = 1f;
            play.SetActive(true);
            fastPlay.SetActive(false);
            }

       
         }














































    /*[SerializeField] Text fastAndNormalText;

    public void FastAndNormal()
    {
        if (Time.timeScale==1f)
        {
            Time.timeScale = 2f;
            fastAndNormalText.text = "";
        }

        else
        {
            Time.timeScale = 1f;
            fastAndNormalText.text = "";
        }
    }*/
}


/*public class fastForward : MonoBehaviour
{
    public Image myImageComponent;
    public Sprite play;
    public Sprite fastPlay;



    public void SetImage()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 2f;
            myImageComponent.sprite = play;
        }

        else
        {
            Time.timeScale = 1f;
            myImageComponent.sprite = fastPlay;
        }
    }
}
*/
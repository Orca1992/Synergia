using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueConfig : MonoBehaviour
{

    public GameObject hermesstatue;


    private GameObject activeStatue;

    public TowerStats GetStats { get { return activeStatue.GetComponent<TowerStats>(); } }

    public void SetStatue(GodType typ)
    {
        activeStatue.SetActive(false);
       switch(typ)
        {

            case GodType.Hermes:
                activeStatue = hermesstatue;
                hermesstatue.SetActive(true);
                break;
        }
          
    }
    public void SetSockel(GodType type)
    {

    }
}

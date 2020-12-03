using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUIStats : MonoBehaviour
{
    public GameObject obj;
    

    

    public void Toggle()
    {

        obj.SetActive(!obj.activeSelf);


    }

}

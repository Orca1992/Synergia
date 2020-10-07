using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //singleton Pattern, one instance in the scene
    public static BuildManager instance;

    //Zeus mit eigener 
    private GameObject ZeusOToBuild;

    void Awake()
    {
        if(instance != null)
        { 
            Debug.LogError("Es kann nur ein Buildmanager in der Szene sein");
        }
        instance = this;

    }

    //name muss noch geändert werden, hier noch als standartstatue
    public GameObject standardStatuePrefab;

     void Start()
    {
        ZeusOToBuild = standardStatuePrefab;
    }


    public GameObject GetStatueToBuild()
    {
        return ZeusOToBuild;
    }



}

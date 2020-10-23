using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //singleton Pattern, one instance in the scene
    public static BuildManager instance;

    //Der Nuller ist wichtig da das die Baseform ist mit BaseSockel
    public GameObject ZeusOToBuild;
    public GameObject PoseidonOToBuild;
    public GameObject ArtemisOToBuild; 
    public GameObject HermesOToBuild;

    private GameObject statueToBuild;

    void Awake()
    {
        if(instance != null)
        { 
            Debug.LogError("Es kann nur ein Buildmanager in der Szene sein");
        }
        instance = this;

    }

    public GameObject GetStatueToBuild()
    {
        return statueToBuild;
    }

    public void SetStatueToBuild(GameObject statue)
    {
        statueToBuild = statue;
    }

    //geht das??
    public void Delete()
    {
        Destroy(statueToBuild);
    }
    



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    BuildManager buildManager;

    public Color hoverColor;
    public Vector3 positionOffset;

    private Renderer rend;
    private Color startColor;


    [HideInInspector]
    public GameObject statue;
    [HideInInspector]
    public StatueBlueprint statueBlueprint;
    [HideInInspector]
    public bool isUpgraded;


    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }


    void OnMouseDown()
    {

        //ist die 
        //wsl auch für upgrades nötig
        if (statue != null)
        {
            buildManager.SelectedNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;


        BuildStatue(buildManager.GetStatueToBuild());
        
    }

    void BuildStatue(StatueBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Nicht genug Geld!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _statue = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        statue = _statue;

        statueBlueprint = blueprint;

        //Buildeffekt hier? staub?
        //GameObject effect = (GameObject)Instantiate(_buildEffect___, GetBuildPosition(), Quaternion.identity);
        //Destroy(effect, 5f);

        Debug.Log("Statue wurde gekauft! Money left: " + PlayerStats.Money);
    }


    //einbauen wie ich 3-4 verschiedene Upgrades erstelle? muss eingebaut werden
    public void UpgradeStatue()
    {
        if (PlayerStats.Money < statueBlueprint.upgradeCost1)
        {
            Debug.Log("Nicht genug Geld um aufzuleveln!");
            return;
        }

        PlayerStats.Money -= statueBlueprint.upgradeCost1;

        //Alte Statue wird gelöscht
        Destroy(statue);


        //Neue upgrade Statue wird platziert
        GameObject _statue = (GameObject)Instantiate(statueBlueprint.upgradedPrefab1, GetBuildPosition(), Quaternion.identity);
        statue = _statue;

        //Buildeffekt hier? staub?
        //GameObject effect = (GameObject)Instantiate(_buildEffect___, GetBuildPosition(), Quaternion.identity);
        //Destroy(effect, 5f);

        isUpgraded = true;


        Debug.Log("Statue wurde gekauft! Money left: " + PlayerStats.Money);
    }

    void OnMouseEnter()
    {
        if (!buildManager.CanBuild)
            return;

        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}

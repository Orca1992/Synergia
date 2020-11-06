using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    BuildManager buildManager;

    public Color hoverColor;
    public Vector3 positionOffset;
    public Transform statueTransform;

    public GameObject _buildEffect___;

    private Renderer rend;
    private Color startColor;

    private bool isBuild;

    private Statue statue;
 
    [HideInInspector]
    public bool isUpgraded;


    private void Start()
    {
        statue = statueTransform.GetComponent<Statue>();
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

        if (!isBuild)
        {
            buildManager.SelectedNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;
       
    }

    


    //einbauen wie ich 3-4 verschiedene Upgrades erstelle? muss eingebaut werden
    public void Upgrade(GodType typ)
    {
        int buildingCost = statue.config.StatueCost(typ);
        //keine Statue gebaut
        if (statue.statueType == GodType.None)
        {
            

            if (PlayerStats.Money < buildingCost)
            {
                Debug.Log("Nicht genug Geld um aufzuleveln!");
                return;
            }

            PlayerStats.Money -= buildingCost;


            GameObject effect = (GameObject)Instantiate(_buildEffect___, GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 5f);

            statue.ChangeStatue(typ);

            Debug.Log("Statue wurde gekauft! Money left: " + PlayerStats.Money);
        }
        else if(typ == GodType.Sell)
        {
            statue.ChangeStatue(typ);
            statue.ChangeSockel(typ);
            PlayerStats.Money += buildingCost / 2;
        }
        //Statue gebaut, aber kein Sockel gebaut
        else if(statue.sockelType == GodType.None)
        {

            //die Sockelkosten 
            buildingCost = statue.config.SockelCost(statue.statueType, typ);

            if (PlayerStats.Money < buildingCost)
            {
                Debug.Log("Nicht genug Geld um aufzuleveln!");
                return;
            }

            PlayerStats.Money -= buildingCost;


            //Buildeffekt hier? staub?
            //GameObject effect = (GameObject)Instantiate(_buildEffect___, GetBuildPosition(), Quaternion.identity);
            //Destroy(effect, 5f);

            statue.ChangeSockel(typ);

            Debug.Log("Statue wurde gekauft! Money left: " + PlayerStats.Money);
        }
        
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

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
        int buildingCost;

        //keine Statue gebaut
        
        if (statue.statueType == GodType.None && statue.statueType != GodType.Sell)
        {
            buildingCost = statue.config.StatueCost(typ);

            if (PlayerStats.Money < buildingCost)
            {
                Debug.Log("Nicht genug Geld um aufzuleveln!");
                return;
            }

            PlayerStats.Money -= buildingCost;


            GameObject effect = (GameObject)Instantiate(_buildEffect___, GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 1f);

            statue.ChangeStatue(typ);
            statue.ChangeSockel(typ);
//            statue.SetBullet(statue.statueType, statue.sockelType);

            Debug.Log("Statue wurde gekauft! Money left: " + PlayerStats.Money);
        }
        else if (typ == GodType.Sell)
        {
            buildingCost = statue.config.StatueCost(statue.statueType);
            statue.ChangeStatue(typ);
            statue.ChangeSockel(typ);
//            statue.SetBullet(statue.statueType, statue.sockelType);
            PlayerStats.Money += (buildingCost / 2);

            //sell-effect einbauen

            Debug.Log("Statue wurde verkauft! Money left: " + PlayerStats.Money);
        }


        //Statue gebaut, aber kein Sockel
        else if(statue.sockelType != typ)
        {
            

            //die Sockelkosten 
            buildingCost = statue.config.SockelCost(statue.statueType, typ);

            if (PlayerStats.Money < buildingCost)
            {
                Debug.Log("Nicht genug Geld um aufzuleveln!");
                return;
            }

            PlayerStats.Money -= buildingCost;


            GameObject effect = (GameObject)Instantiate(_buildEffect___, GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 1f);

            statue.ChangeSockel(typ);
//            statue.SetBullet(statue.statueType, statue.sockelType);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    BuildManager buildManager;

    //public Color hoverColor;
    public Vector3 positionOffset;
    public Transform statueTransform;

    public GameObject _buildEffect___;

    private Renderer rend;
    private Color startColor;

    public bool isBuild { get; private set; }

    public Statue statue { get; private set; }

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

        buildManager.DeselectNode();

        if (statue != null)
        {
            buildManager.SelectedNode(this);
            return;
        }

        //buildManager.SelectedNode(this);

    }


    public void Upgrade(GodType typ)
    {
        int buildingCost;

        //keine Statue gebaut
        if (statue.statueType == GodType.None)
        {
            if(typ == GodType.Sell)
            {
                return;
            }

            buildingCost = statue.config.SockelCost(typ, typ);

            if (PlayerStats.Money < buildingCost)
            {
                //in der ui anzeigen
                Debug.Log("Nicht genug Geld um aufzuleveln!");
                return;
            }

            PlayerStats.Money -= buildingCost;

            if(statue.statueType != GodType.Sell)
            {
                Instantiate(_buildEffect___, GetBuildPosition(), Quaternion.identity);
            }
            
            statue.ChangeStatue(typ);
            statue.ChangeSockel(typ);
            isBuild = true;

            if (statue.statueType == GodType.Poseidon)
            {
                statue.useBeam = true;
                //Debug.Log("ich bin Poseidon, ich schieße einen Wasserstrahl!");
            }

            //statue.SetBullet(statue.statueType, statue.sockelType);

            Debug.Log("Statue wurde gekauft! Money left: " + PlayerStats.Money);
        }
        else if (typ == GodType.Sell)
        {
            buildingCost = statue.config.SockelCost(statue.statueType, statue.sockelType);
            statue.ChangeStatue(GodType.None);
            statue.ChangeSockel(GodType.None);

            PlayerStats.Money += (buildingCost / 2);

            isBuild = false;
            isUpgraded = false;
            statue.useBeam = false;

            //sell-effect einbauen

            Debug.Log("Statue wurde verkauft! Money left: " + PlayerStats.Money);
        }

        //wenn der Sockel nicht der angegebener Typ ist und nicht upgegraded
        else if(statue.sockelType != typ && !isUpgraded)
        {
            Debug.Log("Upgrade!");
            //die Sockelkosten 
            buildingCost = statue.config.SockelCost(statue.statueType, typ);

            if (PlayerStats.Money < buildingCost)
            {
                //in der ui anzeigen
                Debug.Log("Nicht genug Geld um aufzuleveln!");
                return;
            }

            PlayerStats.Money -= buildingCost;


            Instantiate(_buildEffect___, GetBuildPosition(), Quaternion.identity);

            statue.ChangeSockel(typ);
            isBuild = true;
            isUpgraded = true;

            //Debug.Log("Statue wurde gekauft! Money left: " + PlayerStats.Money);
        }     
        
    }

    //void OnMouseEnter()
    //{

    //    rend.material.color = hoverColor;
    //}

    //private void OnMouseExit()
    //{
    //    rend.material.color = startColor;
    //}
}

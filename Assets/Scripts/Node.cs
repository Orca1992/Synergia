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


    [Header("Optional")]
    public GameObject statue;


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
        //wsl auch fürs upgrades nötig
        if (statue != null)
        {
            buildManager.SelectedNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        buildManager.BuildStatueOn(this);
        
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;

    private Renderer rend;

    private GameObject statue;
    private Color startColor;
    

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void OnMouseDown()
    {
        if (statue != null)
        {
            //Debug.Log("Es ist schon eine schöne Statue auf der Platform");
            //zum testen um statue zu entfernen
            Destroy(statue.gameObject);
            return;
        }

        GameObject statueToBuild = BuildManager.instance.GetStatueToBuild();
        statue = (GameObject)Instantiate(statueToBuild, transform.position + positionOffset, transform.rotation);
    }

    void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}

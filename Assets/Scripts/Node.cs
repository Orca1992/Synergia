using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    BuildManager buildManager;

    public Color hoverColor;
    public Vector3 positionOffset;

    private Renderer rend;

    private GameObject statue;
    private Color startColor;
    

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    void OnMouseDown()
    {
        // CameraController.instance.hookedOnNode = transform;

        if (buildManager.GetStatueToBuild() == null)
            return;

        if (statue != null)
        {
            //Debug.Log("Es ist schon eine schöne Statue auf der Platform");
            //zum testen um statue zu entfernen
            Destroy(statue.gameObject);
            return;
        }

        GameObject statueToBuild = buildManager.GetStatueToBuild();
        statue = (GameObject)Instantiate(statueToBuild, transform.position + positionOffset, transform.rotation);
    }

    void OnMouseEnter()
    {
        if (buildManager.GetStatueToBuild() == null)
            return;

        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}

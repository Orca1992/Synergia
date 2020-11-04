using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //singleton Pattern, one instance in the scene
    public static BuildManager instance;

    private Statue statueToBuild;
    private Node selectedNode;

    public NodeUI nodeUI;

    //isr es möglich es zu bauen auf dem node?
    public bool CanBuild { get { return statueToBuild != null; } }

    void Awake()
    {
        if(instance != null)
        { 
            Debug.LogError("Es kann nur ein Buildmanager in der Szene sein");
        }
        instance = this;

    }

    //base statue mit soll ausgewählt werden
    public void SelectedNode(Node node)
    {
        //beim zweiten mal anklicken, veschwindet die nodeUI
        if(selectedNode == node)
        {
            DeselectNode();
            return;

        }

        selectedNode = node;
        

        //UI
        nodeUI.SetTarget(node);
    }

    //upgrade die base, da sie schon erstellt wurde
    public void SelectStatueToBuild(Statue statue)
    {
        statueToBuild = statue;
        DeselectNode();
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();

    }

    public Statue GetStatueToBuild()
    {
        return statueToBuild;
    }

}

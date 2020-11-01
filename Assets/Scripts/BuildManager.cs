using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //singleton Pattern, one instance in the scene
    public static BuildManager instance;

    private StatueBlueprint statueToBuild;
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

    public void SelectedNode(Node node)
    {
        //wenn die das node angeklickt wird was ident ist mit der node die reinkommt, wird esdelected
        if(selectedNode == node)
        {
            DeselectNode();
            return;

        }

        selectedNode = node;
        statueToBuild = null;

        //UI
        nodeUI.SetTarget(node);
    }


    public void SelectStatueToBuild(StatueBlueprint statue)
    {
        statueToBuild = statue;
        DeselectNode();
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();

    }

    public StatueBlueprint GetStatueToBuild()
    {
        return statueToBuild;
    }

}

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
        //
        DeselectNode();
        selectedNode = node;
        //UI
        nodeUI.SetTarget(node);

        if (node.isBuild)
        {
            selectedNode.statue.ShowRangeIndicator(true);
        }

    }

    //upgrade die base, da sie schon erstellt wurde
    public void SelectStatueToBuild(Statue statue)
    {
        statueToBuild = statue;
        DeselectNode();
    }

    public void DeselectNode()
    {
        if(selectedNode == null)
        { 
            return;
        }
        if(selectedNode.isBuild)
        {
            selectedNode.statue.ShowRangeIndicator(false);
        }

        selectedNode = null;
        nodeUI.Hide();

    }

    public Statue GetStatueToBuild()
    {
        return statueToBuild;
    }

}

using UnityEngine;

public class NodeUI : MonoBehaviour
{
    BuildManager buildManager;

    public GameObject ui;
    public Statue statue;
    
    private Node target;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SetTarget (Node t)
    {
        target = t;
        transform.position = target.GetBuildPosition();

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade(GodType type)
    {
        target.Upgrade(type);
        BuildManager.instance.DeselectNode();
    }

    
}

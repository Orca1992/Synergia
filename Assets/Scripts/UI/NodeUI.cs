using UnityEngine;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    private Node target;

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

    public void Upgrade()
    {
        target.UpgradeStatue();
        BuildManager.instance.DeselectNode();
    }
}

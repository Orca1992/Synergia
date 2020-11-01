using UnityEngine;

public class TowerMenu : MonoBehaviour
{
    public StatueBlueprint ZeusBase;
    public StatueBlueprint PoseidonBase;
    public StatueBlueprint ArtemisBase;
    public StatueBlueprint HermesBase;


    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectZeusStatue()
    {
        Debug.Log("Zeus, the Bolt");
        buildManager.SelectStatueToBuild(ZeusBase);
    }
    public void SelectPoseidonStatue()
    {
        Debug.Log("Poseidon, the Ocean");
        buildManager.SelectStatueToBuild(PoseidonBase);
    }
    public void SelectArtemisStatue()
    {
        Debug.Log("Artemis, the Hunter");
        buildManager.SelectStatueToBuild(ArtemisBase);
    }
    public void SelectHermesStatue()
    {
        Debug.Log("Hermes, the Deliver");
        buildManager.SelectStatueToBuild(HermesBase);
    }

    public void PurchaseNone()
    {
        Debug.Log("Delete");
       // buildManager.Delete();

    }
}

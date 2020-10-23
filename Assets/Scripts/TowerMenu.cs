using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMenu : MonoBehaviour
{
    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void PurchaseZeusStatue()
    {
        Debug.Log("Zeus, the Bolt");
        buildManager.SetStatueToBuild(buildManager.ZeusOToBuild);
    }
    public void PurchasePoseidonStatue()
    {
        Debug.Log("Poseidon, the Fish");
        buildManager.SetStatueToBuild(buildManager.PoseidonOToBuild);
    }
    public void PurchaseArtemisStatue()
    {
        Debug.Log("Artemis, the Hunter");
        buildManager.SetStatueToBuild(buildManager.ArtemisOToBuild);
    }
    public void PurchaseHermesStatue()
    {
        Debug.Log("Hermes, the Deliver");
        buildManager.SetStatueToBuild(buildManager.HermesOToBuild);
    }

    public void PurchaseNone()
    {
        Debug.Log("Delete");
        buildManager.Delete();

    }
}

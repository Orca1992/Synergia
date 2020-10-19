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
        Debug.Log("Pikachu");
        buildManager.SetStatueToBuild(buildManager.ZeusOToBuild);
    }
    public void PurchasePoseidonStatue()
    {
        Debug.Log("wet ass GOD");
        buildManager.SetStatueToBuild(buildManager.PoseidonOToBuild);
    }
    public void PurchaseArtemisStatue()
    {
        Debug.Log("QuotenFRAU");
        buildManager.SetStatueToBuild(buildManager.ArtemisOToBuild);
    }
    public void PurchaseHermesStatue()
    {
        Debug.Log("DHL");
        buildManager.SetStatueToBuild(buildManager.HermesOToBuild);
    }
}

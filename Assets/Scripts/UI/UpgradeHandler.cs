using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class UpgradeHandler : MonoBehaviour
{
    public GodType type;
    public NodeUI nodeUI;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { OnButtonClick(); });
	 
    }
    void OnButtonClick()
    {
        Debug.Log(type);
        nodeUI.Upgrade(type);
    }
}

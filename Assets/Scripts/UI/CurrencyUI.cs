using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    public Text currencyText;
    

    void Update()
    {
        currencyText.text = PlayerStats.Money.ToString();
    }
}

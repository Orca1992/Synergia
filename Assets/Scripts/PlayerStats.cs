using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //hier kommt noch currency, welcher der Spieler besitzt
    public static int Money;
    public int startMoney = 400;

    public static int Lives;
    public static int startLives = 20;

    void Start()
    {
        Money = startMoney;
        Lives = startLives;
    }

    
}

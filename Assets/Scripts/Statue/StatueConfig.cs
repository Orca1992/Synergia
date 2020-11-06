using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueConfig : MonoBehaviour
{

    //Im Inspector alle möglichen Statuen und Sockeln
    public GameObject zeusStatue;
    public GameObject poseidonStatue;
    public GameObject artemisStatue;
    public GameObject hermesStatue;

    public GameObject zeusBase;
    public GameObject poseidonBase;
    public GameObject artemisBase;
    public GameObject hermesBase;

    //die auf der Node aktivierte Statue und Sockel
    private GameObject activeStatue;
    private GameObject activeSockel;

    //Von der jeweiligen Statue Stats die auf die activeStatue übertragen wird
    public TowerStats GetStats()
    {

        return activeStatue.GetComponent<TowerStats>();
    }


    // hier wird angegeben wieviel die 
    public int StatueCost (GodType statuetyp)
    {
        int cost = 0;
        setVisability(true);

        switch (statuetyp)
        {
            case GodType.Zeus:
                cost = zeusStatue.GetComponent<TowerStats>().cost;
                break;
            case GodType.Poseidon:
                cost = poseidonStatue.GetComponent<TowerStats>().cost;
                break;
            case GodType.Artemis:
                cost = artemisStatue.GetComponent<TowerStats>().cost;
                break;
            case GodType.Hermes:
                cost = hermesStatue.GetComponent<TowerStats>().cost;
                break;
        }
        setVisability(false);
        return cost;
    }

    public int SockelCost(GodType statuetyp, GodType sockelTyp)
    {
        int cost = 0;

        setVisability(true);
        switch (sockelTyp)
        {
            case GodType.Zeus:
                cost = zeusBase.GetComponent<SockelStats>().GetCombination(statuetyp).cost;
                break;
            case GodType.Poseidon:
                cost = poseidonBase.GetComponent<SockelStats>().GetCombination(statuetyp).cost;
                break;
            case GodType.Artemis:
                cost = artemisBase.GetComponent<SockelStats>().GetCombination(statuetyp).cost;
                break;
            case GodType.Hermes:
                cost = hermesBase.GetComponent<SockelStats>().GetCombination(statuetyp).cost;
                break;
        }
        setVisability(false);
        return cost;
    } 

    private void Start()
    {
        activeStatue = null;
        activeSockel = null;
    }

    public void SetStatue(GodType typ)
    {
       if(activeStatue != null)
        {
            activeStatue.SetActive(false);
        }
       switch(typ)
        {

            case GodType.Zeus:
                activeStatue = zeusStatue;
                zeusStatue.SetActive(true);
                break;
            case GodType.Poseidon:
                activeStatue = poseidonStatue;
                poseidonStatue.SetActive(true);
                break;
            case GodType.Artemis:
                activeStatue = artemisStatue;
                artemisStatue.SetActive(true);
                break;
            case GodType.Hermes:
                activeStatue = hermesStatue;
                hermesStatue.SetActive(true);
                break;
            case GodType.Sell:
                activeStatue = null;
                break;
                
        }
          
    }
    public void SetSockel(GodType typ)
    {
        if (activeSockel != null)
        {
            activeSockel.SetActive(false);
        }
        switch (typ)
        {

            case GodType.Zeus:
                activeSockel = zeusBase;
                zeusBase.SetActive(true);
                break;
            case GodType.Poseidon:
                activeSockel = poseidonBase;
                poseidonBase.SetActive(true);
                break;
            case GodType.Artemis:
                activeSockel = artemisBase;
                artemisBase.SetActive(true);
                break;
            case GodType.Hermes:
                activeSockel = hermesBase;
                hermesBase.SetActive(true);
                break;
            case GodType.Sell:
                activeSockel = null;
                break;
        }
    }

    private void setVisability(bool visable)
    {
        zeusStatue.SetActive(visable);
        poseidonStatue.SetActive(visable);
        artemisStatue.SetActive(visable);
        hermesStatue.SetActive(visable);

        zeusBase.SetActive(visable);
        poseidonBase.SetActive(visable);
        artemisBase.SetActive(visable);
        hermesBase.SetActive(visable);

        if(activeStatue != null)
        {
            activeStatue.SetActive(true);
        }
        if (activeSockel != null)
        {
            activeSockel.SetActive(true);
        }
    }
}

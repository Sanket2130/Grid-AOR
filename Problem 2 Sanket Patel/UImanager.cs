using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public Text ammo;
    public Text health;
    public Text credit;
    
    public GameObject[] hud = new GameObject[4];

    private void Start()
    {
        for (int i = 0; i < hud.Length; i++)
        {
            hud[i].SetActive(false);
        }
    }

    public void setAmmo(string i)
    {
        ammo.text = i;
    }

    public void setHealth(string i)
    {
        health.text = i;
    }

    public void setCredit(string i)
    {
        credit.text = i;
    }

    public void setDisplay(int e)
    {
        for (int i = 0; i < hud.Length; i++)
        {
            hud[i].SetActive(false);
        }

        for (int i = 0; i < hud.Length; i++)
        {
            if(i == e) hud[i].SetActive(true);
        }
    }
}

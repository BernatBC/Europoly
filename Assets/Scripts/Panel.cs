using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>Panel</c> calls the right method after pressing a player panel.
/// </summary>
public class Panel : MonoBehaviour
{
    /// <summary>
    /// GameObject <c>scripts</c> contains all scripts.
    /// </summary>
    private GameObject scripts;

    /// <summary>
    /// Method <c>Start</c> initialize the scripts GameObject.
    /// </summary>
    private void Start()
    {
        scripts = GameObject.Find("GameHandler");
    }

    /// <summary>
    /// Method <c>Show</c> shows information about the pressed player or trades with him.
    /// </summary>
    /// <param name="name"></param>
    public void Show(string name)
    {
        Debug.Log("Show");
        if (name == "panel1") scripts.GetComponent<CellInfo>().ShowPlayerPanel(0, 1, true);
        else if (name == "panel2") scripts.GetComponent<CellInfo>().ShowPlayerPanel(1, 1, true);
        else if (name == "panel3") scripts.GetComponent<CellInfo>().ShowPlayerPanel(2, 1, true);
        else if (name == "panel4") scripts.GetComponent<CellInfo>().ShowPlayerPanel(3, 1, true);
        else if (name == "trade1") scripts.GetComponent<CellInfo>().TradingPartnerSelected(1);
        else if (name == "trade2") scripts.GetComponent<CellInfo>().TradingPartnerSelected(2);
        else if (name == "trade3") scripts.GetComponent<CellInfo>().TradingPartnerSelected(3);
    }
}

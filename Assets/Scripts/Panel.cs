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
    /// CellInfo class.
    /// </summary>
    private CellInfo cellInfo;

    /// <summary>
    /// Method <c>Start</c> initialize the scripts GameObject.
    /// </summary>
    private void Start()
    {
        cellInfo = GetComponent<CellInfo>();
    }

    /// <summary>
    /// Method <c>Show</c> shows information about the pressed player or trades with him.
    /// </summary>
    /// <param name="name"></param>
    public void Show(string name)
    {
        Debug.Log($"Pressed {name}");
        if (name == "panel1") cellInfo.ShowPlayerPanel(0, 1, true);
        else if (name == "panel2") cellInfo.ShowPlayerPanel(1, 1, true);
        else if (name == "panel3") cellInfo.ShowPlayerPanel(2, 1, true);
        else if (name == "panel4") cellInfo.ShowPlayerPanel(3, 1, true);
        else if (name == "trade1") cellInfo.TradingPartnerSelected(1);
        else if (name == "trade2") cellInfo.TradingPartnerSelected(2);
        else if (name == "trade3") cellInfo.TradingPartnerSelected(3);
    }
}

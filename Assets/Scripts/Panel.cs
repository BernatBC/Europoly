using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void Show()
    {
        if (CompareTag("panel1")) scripts.GetComponent<CellInfo>().ShowPlayerPanel(0, 1, true);
        else if (CompareTag("panel2")) scripts.GetComponent<CellInfo>().ShowPlayerPanel(1, 1, true);
        else if (CompareTag("panel3")) scripts.GetComponent<CellInfo>().ShowPlayerPanel(2, 1, true);
        else if (CompareTag("panel4")) scripts.GetComponent<CellInfo>().ShowPlayerPanel(3, 1, true);
        else if (CompareTag("trade1")) scripts.GetComponent<CellInfo>().TradingPartnerSelected(1);
        else if (CompareTag("trade2")) scripts.GetComponent<CellInfo>().TradingPartnerSelected(2);
        else if (CompareTag("trade3")) scripts.GetComponent<CellInfo>().TradingPartnerSelected(3);
    }
}

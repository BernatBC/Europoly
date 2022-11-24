using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    GameObject scripts;

    private void Start()
    {
        scripts = GameObject.Find("GameHandler");
    }
    public void Show()
    {
        if (CompareTag("panel1")) scripts.GetComponent<Cell_info>().ShowPlayerInfo(0, 1, true);
        else if (CompareTag("panel2")) scripts.GetComponent<Cell_info>().ShowPlayerInfo(1, 1, true);
        else if (CompareTag("panel3")) scripts.GetComponent<Cell_info>().ShowPlayerInfo(2, 1, true);
        else if (CompareTag("panel4")) scripts.GetComponent<Cell_info>().ShowPlayerInfo(3, 1, true);
        else if (CompareTag("trade1")) scripts.GetComponent<Cell_info>().SecondTradePanel(1);
        else if (CompareTag("trade2")) scripts.GetComponent<Cell_info>().SecondTradePanel(2);
        else if (CompareTag("trade3")) scripts.GetComponent<Cell_info>().SecondTradePanel(3);
    }
}

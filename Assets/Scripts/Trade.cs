using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class <c>Trade</c> contains various methods related to trading.
/// </summary>
public class Trade : MonoBehaviour
{
    /// <summary>
    /// GameObject <c>firstPanel</c> trading panel number 1.
    /// </summary>
    public GameObject firstPanel;

    /// <summary>
    /// GameObject <c>secondTravel</c> trading panel number 2.
    /// </summary>
    public GameObject secondPanel;

    /// <summary>
    /// CellInfo class.
    /// </summary>
    private CellInfo cellInfo;

    /// <summary>
    /// CashManagement class.
    /// </summary>
    private CashManagement cashManagement;

    /// <summary>
    /// Method <c>Start</c> initializes both trading panel
    /// </summary>
    private void Start()
    {
        GameObject scripts = GameObject.Find("GameHandler");
        cellInfo = scripts.GetComponent<CellInfo>();
        cashManagement = scripts.GetComponent<CashManagement>();

        for (int i = 0; i < 30; ++i) {
            int num = i;
            Button b = firstPanel.transform.Find("targeta" + i.ToString()).gameObject.AddComponent(typeof(Button)) as Button;
            b.onClick.AddListener(delegate { cellInfo.ToggleTradingMiniCard(1, num); });

            b = secondPanel.transform.Find("targeta" + i.ToString()).gameObject.AddComponent(typeof(Button)) as Button;
            b.onClick.AddListener(delegate { cellInfo.ToggleTradingMiniCard(2, num); });
        }
    }

    /// <summary>
    /// Method <c>ReadBox</c> reads the cash value from trading panel number 1.
    /// </summary>
    /// <param name="boxContent">Amount of cash indicated by the input field.</param>
    public void ReadBox(string boxContent)
    {
        int cashOffered;
        if (int.TryParse(boxContent, out cashOffered)) {
            if (cashOffered < 0) {
                cellInfo.UpdateTradingCash(1, 0);
                firstPanel.transform.Find("Cash").gameObject.GetComponent<InputField>().text = "";
                return;
            }
            int max = cashManagement.GetCash(cellInfo.GetTradingPlayer(1));
            if (cashOffered > max)
            {
                cellInfo.UpdateTradingCash(1, max);
                firstPanel.transform.Find("Cash").gameObject.GetComponent<InputField>().text = max.ToString();
                return;
            }
            cellInfo.UpdateTradingCash(1, cashOffered);
        }
        else cellInfo.UpdateTradingCash(1, 0);
    }

    /// <summary>
    /// Method <c>ReadBox2</c> reads the cash value from trading panel number 2
    /// </summary>
    /// <param name="boxContent">Amount of cash indicated by the input field.</param>
    public void ReadBox2(string boxContent)
    {
        int cashOffered;
        if (int.TryParse(boxContent, out cashOffered))
        {
            if (cashOffered < 0)
            {
                cellInfo.UpdateTradingCash(2, 0);
                secondPanel.transform.Find("Cash").gameObject.GetComponent<InputField>().text = "";
                return;
            }
            int max = cashManagement.GetCash(cellInfo.GetTradingPlayer(2));
            if (cashOffered > max)
            {
                cellInfo.UpdateTradingCash(2, max);
                secondPanel.transform.Find("Cash").gameObject.GetComponent<InputField>().text = max.ToString();
                return;
            }
            cellInfo.UpdateTradingCash(2, cashOffered);
        }
        else cellInfo.UpdateTradingCash(2, 0);

    }
}

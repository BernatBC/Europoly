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
    /// GameObject <c>scripts</c> contains all scripts.
    /// </summary>
    private GameObject scripts;

    /// <summary>
    /// GameObject <c>firstPanel</c> trading panel number 1.
    /// </summary>
    public GameObject firstPanel;

    /// <summary>
    /// GameObject <c>secondTravel</c> trading panel number 2.
    /// </summary>
    public GameObject secondPanel;

    /// <summary>
    /// Method <c>Start</c> initializes both trading panel
    /// </summary>
    private void Start()
    {
        scripts = GameObject.Find("GameHandler");
        for (int i = 0; i < 30; ++i) {
            int num = i;
            Button b = firstPanel.transform.Find("targeta" + i.ToString()).gameObject.AddComponent(typeof(Button)) as Button;
            b.onClick.AddListener(delegate { scripts.GetComponent<CellInfo>().ToggleTradingMiniCard(1, num); });

            b = secondPanel.transform.Find("targeta" + i.ToString()).gameObject.AddComponent(typeof(Button)) as Button;
            b.onClick.AddListener(delegate { scripts.GetComponent<CellInfo>().ToggleTradingMiniCard(2, num); ; });
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
                scripts.GetComponent<CellInfo>().UpdateTradingCash(1, 0);
                firstPanel.transform.Find("Cash").gameObject.GetComponent<InputField>().text = "";
                return;
            }
            int max = scripts.GetComponent<CashManagement>().GetCash(scripts.GetComponent<CellInfo>().GetTradingPlayer(1));
            if (cashOffered > max)
            {
                scripts.GetComponent<CellInfo>().UpdateTradingCash(1, max);
                firstPanel.transform.Find("Cash").gameObject.GetComponent<InputField>().text = max + "";
                return;
            }
            scripts.GetComponent<CellInfo>().UpdateTradingCash(1, cashOffered);
        }
        else scripts.GetComponent<CellInfo>().UpdateTradingCash(1, 0);
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
                scripts.GetComponent<CellInfo>().UpdateTradingCash(2, 0);
                secondPanel.transform.Find("Cash").gameObject.GetComponent<InputField>().text = "";
                return;
            }
            int max = scripts.GetComponent<CashManagement>().GetCash(scripts.GetComponent<CellInfo>().GetTradingPlayer(2));
            if (cashOffered > max)
            {
                scripts.GetComponent<CellInfo>().UpdateTradingCash(2, max);
                secondPanel.transform.Find("Cash").gameObject.GetComponent<InputField>().text = max + "";
                return;
            }
            scripts.GetComponent<CellInfo>().UpdateTradingCash(2, cashOffered);
        }
        else scripts.GetComponent<CellInfo>().UpdateTradingCash(2, 0);

    }
}

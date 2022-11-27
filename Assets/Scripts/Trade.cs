using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trade : MonoBehaviour
{
    GameObject scripts;
    public GameObject firstPanel;
    public GameObject secondPanel;

    void Start()
    {
        scripts = GameObject.Find("GameHandler");
        for (int i = 0; i < 30; ++i) {
            int num = i;
            Button b = firstPanel.transform.Find("targeta" + i.ToString()).gameObject.AddComponent(typeof(Button)) as Button;
            b.onClick.AddListener(delegate { scripts.GetComponent<Cell_info>().Toggle(1, num); });

            b = secondPanel.transform.Find("targeta" + i.ToString()).gameObject.AddComponent(typeof(Button)) as Button;
            b.onClick.AddListener(delegate { scripts.GetComponent<Cell_info>().Toggle(2, num); ; });
        }
    }

    public void ReadBox(string s)
    {
        int l;
        if (int.TryParse(s, out l)) {
            if (l < 0) {
                scripts.GetComponent<Cell_info>().UpdateTradingCash(1, 0);
                firstPanel.transform.Find("Cash").gameObject.GetComponent<InputField>().text = "";
                return;
            }
            int max = scripts.GetComponent<Cash_management>().GetCash(scripts.GetComponent<Cell_info>().GetTradingPlayer(1));
            if (l > max)
            {
                scripts.GetComponent<Cell_info>().UpdateTradingCash(1, max);
                firstPanel.transform.Find("Cash").gameObject.GetComponent<InputField>().text = max + "";
                return;
            }
            scripts.GetComponent<Cell_info>().UpdateTradingCash(1, l);
        }
        else scripts.GetComponent<Cell_info>().UpdateTradingCash(1, 0);
    }

    public void ReadBox2(string s)
    {
        int l;
        if (int.TryParse(s, out l))
        {
            if (l < 0)
            {
                scripts.GetComponent<Cell_info>().UpdateTradingCash(2, 0);
                secondPanel.transform.Find("Cash").gameObject.GetComponent<InputField>().text = "";
                return;
            }
            int max = scripts.GetComponent<Cash_management>().GetCash(scripts.GetComponent<Cell_info>().GetTradingPlayer(2));
            if (l > max)
            {
                scripts.GetComponent<Cell_info>().UpdateTradingCash(2, max);
                secondPanel.transform.Find("Cash").gameObject.GetComponent<InputField>().text = max + "";
                return;
            }
            scripts.GetComponent<Cell_info>().UpdateTradingCash(2, l);
        }
        else scripts.GetComponent<Cell_info>().UpdateTradingCash(2, 0);

    }
}

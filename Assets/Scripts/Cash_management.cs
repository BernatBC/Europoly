using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cash_management : MonoBehaviour
{
    public Text cash1_text;
    public Text cash2_text;

    public int initial_cash = 2000;
    int cash1;
    int cash2;

    int tax_cash = 0;

    public void refund_cash(int player) {
        Debug.Log("Player " + player + " " + tax_cash);
        if (player == 0)
        {
            cash1 += tax_cash;
            cash1_text.text = cash1 + "";
        }
        else
        {
            cash2 += tax_cash;
            cash2_text.text = cash2 + "";
        }
        tax_cash = 0;
    }

    public void modify_cash(int player, int amount, bool taxes) {
        Debug.Log("Player " + player + " " + amount);
        if (player == 0)
        {
            cash1 += amount;
            cash1_text.text = cash1 + "";
        }
        else {
            cash2 += amount;
            cash2_text.text = cash2 + "";
        }
        if (taxes) tax_cash -= amount;
    }
    public void modify_cash_per(int player, float amount, bool taxes)
    {
        Debug.Log("Player " + player + " " + Mathf.RoundToInt(cash1 * amount));
        if (player == 0)
        {
            int am = Mathf.RoundToInt(cash1 * amount);
            cash1 += am;
            cash1_text.text = cash1 + "";
            if (taxes) tax_cash -= am;
        }
        else
        {
            int am = Mathf.RoundToInt(cash2 * amount);
            cash2 += am;
            cash2_text.text = cash2 + "";
            if (taxes) tax_cash -= am;
        }
    }

    void Start()
    {
        cash1 = initial_cash;
        cash2 = initial_cash;
        cash1_text.text = cash1 + "";
        cash2_text.text = cash2 + "";
    }
}

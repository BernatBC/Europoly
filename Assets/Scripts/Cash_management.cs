using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cash_management : MonoBehaviour
{
    public Text cash1_text;
    public Text cash2_text;
    public Text cash3_text;
    public Text cash4_text;

    public int initial_cash;
    int cash1;
    int cash2;
    int cash3;
    int cash4;
    int n_players;

    int tax_cash = 0;

    public void refund_cash(int player) {
        //Debug.Log("Player " + player + " " + tax_cash);
        if (player == 0)
        {
            cash1 += tax_cash;
            cash1_text.text = cash1 + "";
        }
        else if (player == 1)
        {
            cash2 += tax_cash;
            cash2_text.text = cash2 + "";
        }
        else if (player == 2)
        {
            cash3 += tax_cash;
            cash3_text.text = cash3 + "";
        }
        else
        {
            cash4 += tax_cash;
            cash4_text.text = cash4 + "";
        }
        tax_cash = 0;
    }

    public bool modify_cash(int player, int amount, bool taxes) {
        //Debug.Log("Player " + player + " " + amount);
        if (player == 0)
        {
            cash1 += amount;
            cash1_text.text = cash1 + "";
        }
        else if (player == 1) {
            cash2 += amount;
            cash2_text.text = cash2 + "";
        }
        else if (player == 2)
        {
            cash3 += amount;
            cash3_text.text = cash3 + "";
        }
        else
        {
            cash4 += amount;
            cash4_text.text = cash4 + "";
        }
        if (taxes) tax_cash -= amount;
        return true;
    }
    public void modify_cash_per(int player, float amount, bool taxes)
    {
        //Debug.Log("Player " + player + " " + Mathf.RoundToInt(cash1 * amount));
        if (player == 0)
        {
            int am = Mathf.RoundToInt(cash1 * amount);
            cash1 += am;
            cash1_text.text = cash1 + "";
            if (taxes) tax_cash -= am;
        }
        else if (player == 1)
        {
            int am = Mathf.RoundToInt(cash2 * amount);
            cash2 += am;
            cash2_text.text = cash2 + "";
            if (taxes) tax_cash -= am;
        }
        else if (player == 2)
        {
            int am = Mathf.RoundToInt(cash3 * amount);
            cash3 += am;
            cash3_text.text = cash3 + "";
            if (taxes) tax_cash -= am;
        }
        else
        {
            int am = Mathf.RoundToInt(cash4 * amount);
            cash4 += am;
            cash4_text.text = cash4 + "";
            if (taxes) tax_cash -= am;
        }
    }

    public void collect_from_everybody(int player, int amount) {
        if (player == 0) {
            modify_cash(0, (n_players - 1) * amount, false);
            modify_cash(1, -amount, false);
            modify_cash(2, -amount, false);
            modify_cash(3, -amount, false);
        }
        else if (player == 1) {
            modify_cash(1, (n_players - 1) * amount, false);
            modify_cash(0, -amount, false);
            modify_cash(2, -amount, false);
            modify_cash(3, -amount, false);
        }
        else if (player == 2)
        {
            modify_cash(2, (n_players - 1) * amount, false);
            modify_cash(0, -amount, false);
            modify_cash(1, -amount, false);
            modify_cash(3, -amount, false);
        }
        else if (player == 3)
        {
            modify_cash(3, (n_players - 1) * amount, false);
            modify_cash(0, -amount, false);
            modify_cash(1, -amount, false);
            modify_cash(2, -amount, false);
        }
    }

    public void pay_everybody(int player, int amount)
    {
        if (player == 0)
        {
            modify_cash(0, -(n_players - 1) * amount, false);
            modify_cash(1, amount, false);
            modify_cash(2, amount, false);
            modify_cash(3, amount, false);
        }
        else if (player == 1)
        {
            modify_cash(1, -(n_players - 1) * amount, false);
            modify_cash(0, amount, false);
            modify_cash(2, amount, false);
            modify_cash(3, amount, false);
        }
        else if (player == 2)
        {
            modify_cash(2, -(n_players - 1) * amount, false);
            modify_cash(0, amount, false);
            modify_cash(1, amount, false);
            modify_cash(3, amount, false);
        }
        else if (player == 3)
        {
            modify_cash(3, -(n_players - 1) * amount, false);
            modify_cash(0, amount, false);
            modify_cash(1, amount, false);
            modify_cash(2, amount, false);
        }
    }

    public int GetCash(int player) {
        if (player == 0) return cash1;
        if (player == 1) return cash2;
        if (player == 2) return cash3;
        if (player == 3) return cash4;
        return cash1;
    }

    void Start()
    {
        initial_cash = DataHolder.initial_cash;
        n_players = DataHolder.n_players;
        cash1 = initial_cash;
        cash2 = initial_cash;
        cash3 = initial_cash;
        cash4 = initial_cash;
        cash1_text.text = cash1 + "";
        cash2_text.text = cash2 + "";
        cash3_text.text = cash3 + "";
        cash4_text.text = cash4 + "";
    }
}

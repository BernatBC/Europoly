using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>CashManagement</c> contains the cash each player has and updates the UI.
/// </summary>
public class CashManagement : MonoBehaviour
{
    /// <summary>
    /// Text[] <c>cashText</c> UI cash text from each player.
    /// </summary>
    public Text[] cashText;

    /// <summary>
    /// int <c>initialCash</c> initial cash.
    /// </summary>
    public int initialCash;

    /// <summary>
    /// int[] <c>cash</c> value of cash from each player.
    /// </summary>
    private int[] cash;

    /// <summary>
    /// int <c>numberOfPlayers</c> number of players playing.
    /// </summary>
    private int numberOfPlayers;

    /// <summary>
    /// int <c>taxCash</c> accumulated money collected from taxes.
    /// </summary>
    private int taxCash = 0;

    /// <summary>
    /// Method <c>RefundCash</c> adds the tax collected money to the indicated player.
    /// </summary>
    /// <param name="player">Player to receive the cash.</param>
    public void RefundCash(int player) {
        //Debug.Log("Player " + player + " " + tax_cash);
        cash[player] += taxCash;
        cashText[0].text = cash[player] + "";
        taxCash = 0;
    }

    /// <summary>
    /// bool function <c>ModifyCash</c> Adds or substracts the indicated amount to the indicated player.
    /// </summary>
    /// <param name="player">Player to modify the cash.</param>
    /// <param name="amount">Amount of cash to add/substract.</param>
    /// <param name="taxes">Indicates if the amount is substracted by paying taxes.</param>
    /// <param name="mustPay">Indicates if the amount to substract must be substracted.</param>
    /// <returns><c>true</c> if cash has been modified, <c>false</c> otherwise.</returns>
    public bool ModifyCash(int player, int amount, bool taxes, bool mustPay) {
        //Debug.Log("Player " + player + " " + amount);
        if (!mustPay && cash[player] + amount < 0 && amount < 0) return false;

        cash[player] += amount;
        cashText[player].text = cash[player] + "";
        if (taxes) taxCash -= amount;
        return true;
    }

    /// <summary>
    /// Method <c>ModifyCashPercent</c> Multiplies the percentage indicated with the cash from the player indicated.
    /// </summary>
    /// <param name="player">Player to modify the cash.</param>
    /// <param name="percentage">Percentage to multiply</param>
    /// <param name="taxes">Indicates if the percentage is applied by paying taxes.</param>
    public void ModifyCashPercent(int player, float percentage, bool taxes)
    {
        //Debug.Log("Player " + player + " " + Mathf.RoundToInt(cash1 * amount));
        int am = Mathf.RoundToInt(cash[player] * percentage);
        cash[player] += am;
        cashText[player].text = cash[player] + "";
        if (taxes) taxCash -= am;
    }

    /// <summary>
    /// Method <c>CollectFromEveryBody</c> Substracts the amount indicated from players (except the indicated one) and gives it to this one
    /// </summary>
    /// <param name="player">Player to receive the cash.</param>
    /// <param name="amount">Amount substracted from each other player.</param>
    public void CollectFromEverybody(int player, int amount) {
        ModifyCash(player, (numberOfPlayers - 1) * amount, false, true);
        for (int i = 0; i < 4; i++) if (i != player) ModifyCash(i, -amount, false, true);
    }

    /// <summary>
    /// Method <c>PayEverybody</c> gives the amount indicated to each other player.
    /// </summary>
    /// <param name="player">Player that gives the cash.</param>
    /// <param name="amount">Amount to be received by each other player.</param>
    public void PayEverybody(int player, int amount)
    {
        ModifyCash(player, -(numberOfPlayers - 1) * amount, false, true);
        for (int i = 0; i < 4; i++) if (i != player) ModifyCash(i, amount, false, true);
    }

    /// <summary>
    /// int function <c>GetCash</c> returns the amount of cash from the indicated player.
    /// </summary>
    /// <param name="player">Player to get the cash from.</param>
    /// <returns>Amount of cash.</returns>
    public int GetCash(int player) {
        return cash[player];
    }

    /// <summary>
    /// Method <c>Start</c> initializes cash values and texts.
    /// </summary>
    private void Start()
    {
        cash = new int[4];
        initialCash = DataHolder.initialCash;
        numberOfPlayers = DataHolder.numberOfPlayers;
        for (int i = 0; i < 4; i++) {
            cash[i] = initialCash;
            cashText[i].text = cash[i] + "";
        }
    }
}

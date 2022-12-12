using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


/// <summary>
/// Class <c>Bot</c> contains the decision-making algorithms for the computer player.
/// </summary>
public class Bot : MonoBehaviour
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
    /// Method <c>RollDice</c> calls RollDice method.
    /// </summary>
    public void RollDice() {
        //Always Roll
        Debug.Log("RollDice");

        StartCoroutine(WaitAndRoll(1));
    }

    /// <summary>
    /// Method <c>BuyPropertyDecision</c> decides wether to buy a property or to pass.
    /// </summary>
    /// <param name="cellName">The cell which you can buy.</param>
    public void BuyPropertyDecision(string cellName) {
        //Always buy if possible
        Debug.Log("Choose buy/pass property " + cellName);

        StartCoroutine(WaitAndPay(1.5f));
    }

    /// <summary>
    /// Method <c>AcceptRejectTrade</c> decides whther is better to accept a trading offer or not.
    /// </summary>
    public void AcceptRejectTrade() {
        //Calculate value and accept if Vafter > Vbefore
        Debug.Log("Choose accept/reject property");
    }

    /// <summary>
    /// Method <c>LeaveJail</c> decides if it's better to roll doubles, pay fine or play the card.
    /// </summary>
    /// <param name="hasFreeCard">Indicates if the bot has a card to leave jail.</param>
    public void LeaveJail(bool hasFreeCard) {
        //if cellsUnowned > K -> Pay (Use card if possible)
        //else roll
        Debug.Log("Choose roll/pay");

        if (scripts.GetComponent<CellInfo>().CountUnownedCells() <= 3) scripts.GetComponent<Movements>().RollDicesDoubles();
        else if (hasFreeCard) scripts.GetComponent<Movements>().UseCard();
        else scripts.GetComponent<Movements>().Pay();
    }

    /// <summary>
    /// Method <c>Travel</c> decides which station to travel.
    /// </summary>
    /// <param name="currentStation">Indicates the station where the player is.</param>
    /// <param name="canTravelStation1">Indicates if the player can travel to Station1</param>
    /// <param name="canTravelStation2">Indicates if the player can travel to Station2</param>
    /// <param name="canTravelStation3">Indicates if the player can travel to Station3</param>
    /// <param name="canTravelStation4">Indicates if the player can travel to Station4</param>
    public void Travel(string currentStation, bool canTravelStation1, bool canTravelStation2, bool canTravelStation3, bool canTravelStation4) {
        //Travel to furthest station
        Debug.Log("Choose station to move/stay");

        string travelingTo = "";
        if (canTravelStation4 || currentStation == "Station4") travelingTo = "Station4";
        else if (canTravelStation3 || currentStation == "Station3") travelingTo = "Station3";
        else if (canTravelStation2 || currentStation == "Station2") travelingTo = "Station2";
        else if (canTravelStation1 || currentStation == "Station1") travelingTo = "Station1";

        if (travelingTo != currentStation) scripts.GetComponent<Movements>().MoveTo(currentStation, travelingTo);   
    }

    /// <summary>
    /// Method <c>BeforeEndTorn</c> decides what to do before ending the torn; mortgaging/unmortgaging, buying/selling houses, trading,...
    /// </summary>
    public void BeforeEndTorn() {
        //Some Strategy
        Debug.Log("End Torn");
        StartCoroutine(WaitAndEnd(1.5f));
    }

    /// <summary>
    /// Method <c>WaitAndRoll</c> waits for the indicated seconds and rolls the dice.
    /// </summary>
    /// <param name="timeInSeconds">Seconds to wait.</param>
    /// <returns></returns>
    private IEnumerator WaitAndRoll(float timeInSeconds)
    {
        yield return new WaitForSecondsRealtime(timeInSeconds);
        scripts.GetComponent<Movements>().RollDice();
    }

    /// <summary>
    /// Method <c>WaitAndEnd</c> waits for the indicated seconds and ends the torn.
    /// </summary>
    /// <param name="timeInSeconds">Seconds to wait.</param>
    /// <returns></returns>
    private IEnumerator WaitAndEnd(float timeInSeconds)
    {
        yield return new WaitForSecondsRealtime(timeInSeconds);
        scripts.GetComponent<Movements>().NextTorn();
    }

    /// <summary>
    /// Method <c>WaitAndPay</c> waits for the indicated seconds and pays the fine to leave jail.
    /// </summary>
    /// <param name="timeInSeconds">Seconds to wait.</param>
    /// <returns></returns>
    private IEnumerator WaitAndPay(float timeInSeconds)
    {
        yield return new WaitForSecondsRealtime(timeInSeconds);
        scripts.GetComponent<CellInfo>().BuyCell();
    }
}

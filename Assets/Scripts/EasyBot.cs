using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>EasyBot</c> contains the decision-making algorithms for the computer player, difficulty easy.
/// </summary>
public class EasyBot : Bot
{
    /// <summary>
    /// Method <c>AcceptRejectTrade</c> decides whther is better to accept a trading offer or not.
    /// </summary>
    public override void AcceptRejectTrade()
    {
        Debug.Log("EasyBot: Choose accept/reject property");
        cellInfo.MakeTrade();
    }

    /// <summary>
    /// Method <c>LeaveJail</c> decides if it's better to roll doubles, pay fine or play the card.
    /// </summary>
    /// <param name="hasFreeCard">Indicates if the bot has a card to leave jail.</param>
    public override void LeaveJail(bool hasFreeCard)
    {
        Debug.Log("EasyBot: Choose roll/pay");

        movements.RollDicesDoubles();
    }

    /// <summary>
    /// Method <c>BeforeEndTorn</c> decides what to do before ending the torn; mortgaging/unmortgaging, buying/selling houses, trading,...
    /// </summary>
    /// <param name="player">Player representing the bot</param>
    /// <param name="propertyInformation">Dictionary of properties.</param>
    /// <param name="railroadInformation">Dictionary of stations.</param>
    /// <param name="water">Water utility.</param>
    /// <param name="electrical">Electrical utility.</param>
    public override void BeforeEndTorn(int player, Dictionary<string, CellInfo.Property> propertyInformation, Dictionary<string, CellInfo.RailRoad> railroadInformation, CellInfo.Utility water, CellInfo.Utility electrical)
    {
        Debug.Log("EasyBot: End Torn");
        StartCoroutine(WaitAndEnd(1.5f));
    }
}

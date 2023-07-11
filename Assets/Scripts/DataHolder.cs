using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>DataHolder</c> contains game configuration information.
/// </summary>
public class DataHolder : MonoBehaviour
{
    /// <summary>
    /// int <c>initialCash</c> initial cash.
    /// </summary>
    public static int initialCash = 2000;

    /// <summary>
    /// int <c>numberOfPlayers</c> number of players.
    /// </summary>
    public static int numberOfPlayers = 3;

    /// <summary>
    /// bool <c>botSelected1</c> indicates wether the player 1 is a computer player or not.
    /// </summary>
    public static bool botSelected1 = false;

    /// <summary>
    /// int <c>botDifficulty1</c> indicates bot difficulty, values: 0 -> Easy, 1 -> Medium, 2 -> Hard
    /// </summary>
    public static int botDifficulty1 = 1;

    /// <summary>
    /// bool <c>botSelected2</c> indicates wether the player 2 is a computer player or not.
    /// </summary>
    public static bool botSelected2 = true;

    /// <summary>
    /// int <c>botDifficulty1</c> indicates bot difficulty, values: 0 -> Easy, 1 -> Medium, 2 -> Hard
    /// </summary>
    public static int botDifficulty2 = 1;

    /// <summary>
    /// bool <c>botSelected3</c> indicates wether the player 3 is a computer player or not.
    /// </summary>
    public static bool botSelected3 = true;

    /// <summary>
    /// int <c>botDifficulty1</c> indicates bot difficulty, values: 0 -> Easy, 1 -> Medium, 2 -> Hard
    /// </summary>
    public static int botDifficulty3 = 1;

    /// <summary>
    /// bool <c>botSelected4</c> indicates wether the player 4 is a computer player or not.
    /// </summary>
    public static bool botSelected4 = false;

    /// <summary>
    /// int <c>botDifficulty1</c> indicates bot difficulty, values: 0 -> Easy, 1 -> Medium, 2 -> Hard
    /// </summary>
    public static int botDifficulty4 = 1;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class <c>Menu</c> contains methods used with the main menu.
/// </summary>
public class Menu : MonoBehaviour
{
    /// <summary>
    /// Text <c>numberOfPlayers</c> shows the number of players selected.
    /// </summary>
    public TMP_Text numberOfPlayers;

    /// <summary>
    /// Slider <c>slider</c> allows to select the number of players.
    /// </summary>
    public Slider slider;

    /// <summary>
    /// GameObject[] <c>players</c> contains the toggle button of each player alongside the Player and Bot text.
    /// </summary>
    public GameObject[] players;

    /// <summary>
    /// string <c>initialCash</c> contains the value of the initial cash input box.
    /// </summary>
    private string initialCash = "";

    /// <summary>
    /// int <c>numberOfPlayersSelected</c> contains the number of players of the slider.
    /// </summary>
    private int numberOfPlayersSelected = 2;

    /// <summary>
    /// bool[] <c>botSelected</c> contains if the player selected is the computer or not.
    /// </summary>
    private bool[] botSelected;

    private int[] botDifficulty;

    /// <summary>
    /// Method <c>Start</c> initializes the slider and botSelected values.
    /// </summary>
    private void Start()
    {
        botSelected = new bool[4];
        botDifficulty = new int[4];
        for (int i = 0; i < 4; ++i)
        {
            botSelected[i] = false;
            botDifficulty[i] = 1;
        }

        slider.onValueChanged.AddListener((value) =>
        {
            numberOfPlayersSelected = Mathf.RoundToInt(value);
            numberOfPlayers.text = numberOfPlayersSelected.ToString();
            SetPlayerPanels();
        });
    }

    /// <summary>
    /// Method <c>PlayButtonPressed</c> sends the information to DataHolder.
    /// </summary>
    public void PlayButtonPressed() {
        int initialCashInteger;
        if (int.TryParse(initialCash, out initialCashInteger))
        {
            if (initialCashInteger < 100) DataHolder.initialCash = 100;
            else DataHolder.initialCash = initialCashInteger;
        }
        else DataHolder.initialCash = 2000;
        DataHolder.numberOfPlayers = numberOfPlayersSelected;
        DataHolder.botSelected1 = botSelected[0];
        DataHolder.botSelected2 = botSelected[1];
        DataHolder.botSelected3 = botSelected[2];
        DataHolder.botSelected4 = botSelected[3];
        DataHolder.botDifficulty1 = botDifficulty[0];
        DataHolder.botDifficulty2 = botDifficulty[1];
        DataHolder.botDifficulty3 = botDifficulty[2];
        DataHolder.botDifficulty4 = botDifficulty[3];
        SceneManager.LoadScene("SampleScene");
    }

    /// <summary>
    /// Methof <c>SetPlayerPanels</c> shows the toggle button of each player up to the number of players selected.
    /// </summary>
    private void SetPlayerPanels() {
        if (numberOfPlayersSelected == 2)
        {
            players[2].SetActive(false);
            players[3].SetActive(false);
        }
        else if (numberOfPlayersSelected == 3)
        {
            players[2].SetActive(true);
            players[3].SetActive(false);
        }
        else {
            players[2].SetActive(true);
            players[3].SetActive(true);
        }
    }

    /// <summary>
    /// Method <c>ToggleBotButton</c> sets the right toggle button after being pressed and assigns the bot selected value of the player.
    /// </summary>
    /// <param name="player">Player to toggle the value.</param>
    public void ToggleBotButton(int player) {
        botSelected[player] = !botSelected[player];
        if (botSelected[player])
        {
            players[player].transform.Find("Selected").gameObject.SetActive(true);
            players[player].transform.Find("Unselected").gameObject.SetActive(false);
        }
        else {
            players[player].transform.Find("Selected").gameObject.SetActive(false);
            players[player].transform.Find("Unselected").gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Method <c>ReadBox</c> sets the initial cash value.
    /// </summary>
    /// <param name="boxContent">Content of the initial cash input field.</param>
    public void ReadBox(string boxContent) {
        initialCash = boxContent;
    }

    /// <summary>
    /// Method <c>SetBotDifficulty1</c> sets the bot difficulty for bot 1.
    /// </summary>
    /// <param name="difficulty">Difficulty selected in the dropdown.</param>
    public void SetBotDifficulty1(int difficulty) {
        botDifficulty[0] = difficulty;
    }

    /// <summary>
    /// Method <c>SetBotDifficulty1</c> sets the bot difficulty for bot 2.
    /// </summary>
    /// <param name="difficulty">Difficulty selected in the dropdown.</param>
    public void SetBotDifficulty2(int difficulty)
    {
        botDifficulty[1] = difficulty;
    }

    /// <summary>
    /// Method <c>SetBotDifficulty1</c> sets the bot difficulty for bot 3.
    /// </summary>
    /// <param name="difficulty">Difficulty selected in the dropdown.</param>
    public void SetBotDifficulty3(int difficulty)
    {
        botDifficulty[2] = difficulty;
    }

    /// <summary>
    /// Method <c>SetBotDifficulty1</c> sets the bot difficulty for bot 4.
    /// </summary>
    /// <param name="difficulty">Difficulty selected in the dropdown.</param>
    public void SetBotDifficulty4(int difficulty)
    {
        botDifficulty[3] = difficulty;
    }
}

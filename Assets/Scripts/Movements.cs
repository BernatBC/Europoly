using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class <c>Movements</c> functions and methods related to player movement.
/// </summary>
public class Movements : MonoBehaviour
{
    /// <summary>
    /// GameObject[] <c>cells</c> cells of the board.
    /// </summary>
    public GameObject[] cells;

    /// <summary>
    /// GameObject[] <c>players</c> players objects.
    /// </summary>
    public GameObject[] players;

    /// <summary>
    /// GameObject[] <c>playerPanels</c> cash and torn indicators from each player.
    /// </summary>
    public GameObject[] playerPanel;

    /// <summary>
    /// Buttonn <c>RollDice</c> roll dice button.
    /// </summary>
    public Button rollDice;

    /// <summary>
    /// Button <c>endTorn</c> end torn button.
    /// </summary>
    public Button endTorn;

    /// <summary>
    /// Button <c>rollDoubles</c> roll doubles button.
    /// </summary>
    public Button rollDoubles;

    /// <summary>
    /// Button <c>pay50</c> pay to get out of jail button.
    /// </summary>
    public Button pay50;

    /// <summary>
    /// Button <c>getOutForFree</c> give card to leave jail button.
    /// </summary>
    public Button getOutForFree;

    /// <summary>
    /// Button <c>travel</c> travel to station button.
    /// </summary>
    public Button travel;

    /// <summary>
    /// TMP_Text <c>dice1</c> dice 1 UI text.
    /// </summary>
    public TMP_Text dice1;

    /// <summary>
    /// TMP_Text <c>dice2</c> dice 2 UI text.
    /// </summary>
    public TMP_Text dice2;

    /// <summary>
    /// AudioSource <c>diceSound</c> sound of a rolling dice.
    /// </summary>
    public AudioSource diceSound;

    /// <summary>
    /// bool <c>moving</c> indicates if the selected player is moving or not.
    /// </summary>
    private bool moving = false;

    /// <summary>
    /// int <c>movementsRemaining</c> indicates the number of remaining movements from the selected player.
    /// </summary>
    private int movementsRemaining = 0;

    /// <summary>
    /// int <c>playerTorn</c> indicates the player torn.
    /// </summary>
    private int playerTorn = 0;

    /// <summary>
    /// int <c>doublesRolled</c> indicates the number of dubles rolled in the current torn.
    /// </summary>
    private int doublesRolled = 0;

    /// <summary>
    /// Vector3 <c>destination</c> contains the destination the selected player has.
    /// </summary>
    private Vector3 destination;

    /// <summary>
    /// bool <c>alreadyTraveled</c> indicates if the selected player has traveled in the current torn or not.
    /// </summary>
    private bool alreadyTraveled = false;

    /// <summary>
    /// bool <c>carryOnMoving</c> indicates if the player will move again after landing on a cell.
    /// </summary>
    private bool carryOnMoving = false;

    /// <summary>
    /// int <c>diceValue1</c> indiactes the value of dice 1.
    /// </summary>
    private int diceValue1;

    /// <summary>
    /// int <c>diceValue2</c> indiactes the value of dice 2.
    /// </summary>
    private int diceValue2;

    /// <summary>
    /// Player[] <c>playerInfo</c> contains information from each player.
    /// </summary>
    private Player[] playerInfo;

    /// <summary>
    /// struct <c>Player</c> player information.
    /// </summary>
    private struct Player {
        /// <summary>
        /// int <c>position</c> number of cell the player is.
        /// </summary>
        public int position;

        /// <summary>
        /// int <c>penalizedTorns</c> number of penalized torns the player has.
        /// </summary>
        public int penalizedTorns;

        /// <summary>
        /// int <c>numberOutOfJailCards</c> number of out of jail cards the player has.
        /// </summary>
        public int numberOutOfJailCards;

        /// <summary>
        /// bool <c>isDead</c> indicates if the player is dead or not.
        /// </summary>
        public bool isDead;

        /// <summary>
        /// bool <c>isBot</c> indicates if the player is the computer or not.
        /// </summary>
        public bool isBot;
    };

    /// <summary>
    /// int <c>numberOfPlayers</c> number of players on game.
    /// </summary>
    private int numberOfPlayers;

    /// <summary>
    /// int <c>numberOfPlayersAlive</c> number of players that hasn't been eliminated.
    /// </summary>
    private int numberOfPlayersAlive;

    /// <summary>
    /// GameObject <c>scripts</c> contains all scripts.
    /// </summary>
    private GameObject scripts;

    /// <summary>
    /// Method <c>Start</c> initializes buttons, panels and datastructures.
    /// </summary>
    private void Start()
    {
        scripts = GameObject.Find("GameHandler");
        rollDice.onClick.AddListener(RollDice);
        endTorn.onClick.AddListener(NextTorn);
        rollDoubles.onClick.AddListener(RollDicesDoubles);
        pay50.onClick.AddListener(Pay);
        getOutForFree.onClick.AddListener(UseCard);
        travel.onClick.AddListener(ShowAvailableTrainStations);
        rollDice.gameObject.SetActive(false);
        endTorn.gameObject.SetActive(false);
        pay50.gameObject.SetActive(false);
        rollDoubles.gameObject.SetActive(false);
        getOutForFree.gameObject.SetActive(false);
        travel.gameObject.SetActive(false);

        numberOfPlayers = DataHolder.numberOfPlayers;
        numberOfPlayersAlive = numberOfPlayers;
        playerInfo = new Player[numberOfPlayers];
        for (int i = 0; i < numberOfPlayers; ++i)
        {
            players[i].transform.position = new Vector3(cells[0].transform.position.x, cells[0].transform.position.y + 1, cells[0].transform.position.z);
            destination = players[i].transform.position;
            playerInfo[i].position = 0;
            playerInfo[i].penalizedTorns = 0;
            playerInfo[i].numberOutOfJailCards = 0;
            playerInfo[i].isDead = false;
            if (i == 0) playerInfo[i].isBot = DataHolder.botSelected1;
            else if (i == 1) playerInfo[i].isBot = DataHolder.botSelected2;
            else if (i == 2) playerInfo[i].isBot = DataHolder.botSelected3;
            else if (i == 3) playerInfo[i].isBot = DataHolder.botSelected4;
        }

        if (numberOfPlayers <= 3)
        {
            playerPanel[3].SetActive(false);
            if (numberOfPlayers == 2)
            {
                playerPanel[2].SetActive(false);
                playerPanel[0].transform.position = new Vector3(playerPanel[0].transform.position.x, playerPanel[0].transform.position.y - 150, playerPanel[0].transform.position.z);
                playerPanel[1].transform.position = new Vector3(playerPanel[1].transform.position.x, playerPanel[1].transform.position.y - 150, playerPanel[1].transform.position.z);
            }
            else
            {
                playerPanel[0].transform.position = new Vector3(playerPanel[0].transform.position.x, playerPanel[0].transform.position.y - 50, playerPanel[0].transform.position.z);
                playerPanel[1].transform.position = new Vector3(playerPanel[1].transform.position.x, playerPanel[1].transform.position.y - 50, playerPanel[1].transform.position.z);
                playerPanel[2].transform.position = new Vector3(playerPanel[2].transform.position.x, playerPanel[2].transform.position.y - 50, playerPanel[2].transform.position.z);
            }
        }
        SetPanelColor(playerPanel[0], new Color32(243, 146, 55, 255));

        MakeRollDice();
    }

    /// <summary>
    /// Method <c>ShowAvailableTrainStations</c> highlights stations the player can travel to.
    /// </summary>
    private void ShowAvailableTrainStations() {
        travel.gameObject.SetActive(false);
        scripts.GetComponent<CellInfo>().TravelSelected();
        if (scripts.GetComponent<CellInfo>().CanTravel("Station", playerTorn)) SetCellColor(5, new Color32(255, 241, 56, 255));
        if (scripts.GetComponent<CellInfo>().CanTravel("Station2", playerTorn)) SetCellColor(15, new Color32(255, 241, 56, 255));
        if (scripts.GetComponent<CellInfo>().CanTravel("Station3", playerTorn)) SetCellColor(25, new Color32(255, 241, 56, 255));
        if (scripts.GetComponent<CellInfo>().CanTravel("Station4", playerTorn)) SetCellColor(35, new Color32(255, 241, 56, 255));
    }

    /// <summary>
    /// Method <c>ShowTravelButton</c> shows the travel button if the player is not the computer and hasn't traveled yet in this torn. If the player is the computer, the computer can decide what to do.
    /// </summary>
    public void ShowTravelButton() {
        if (alreadyTraveled) alreadyTraveled = false;
        else {
            alreadyTraveled = true;
            if (playerInfo[playerTorn].isBot) scripts.GetComponent<Bot>().Travel(cells[playerInfo[playerTorn].position].name, scripts.GetComponent<CellInfo>().CanTravel("Station", playerTorn), scripts.GetComponent<CellInfo>().CanTravel("Station2", playerTorn), scripts.GetComponent<CellInfo>().CanTravel("Station3", playerTorn), scripts.GetComponent<CellInfo>().CanTravel("Station4", playerTorn));
            else travel.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Method <c>UndoStationColor</c> paints the stations back to white.
    /// </summary>
    public void UndoStationColor()
    {
        if (!scripts.GetComponent<CellInfo>().StationMortgaged(1)) SetCellColor(5, new Color32(255, 255, 255, 255));
        if (!scripts.GetComponent<CellInfo>().StationMortgaged(2)) SetCellColor(15, new Color32(255, 255, 255, 255));
        if (!scripts.GetComponent<CellInfo>().StationMortgaged(3)) SetCellColor(25, new Color32(255, 255, 255, 255));
        if (!scripts.GetComponent<CellInfo>().StationMortgaged(4)) SetCellColor(35, new Color32(255, 255, 255, 255));
    }

    /// <summary>
    /// Method <c>SetColor</c> sets the indicated cell to the indicated color.
    /// </summary>
    /// <param name="cell">Number of cell to paint.</param>
    /// <param name="color">Color to paint.</param>
    private void SetCellColor(int cell, Color32 color)
    {
        Transform cellComponents = cells[cell].transform;
        foreach (Transform component in cellComponents)
        {
            if (component.CompareTag("CellBody")) component.GetComponent<Renderer>().material.color = color;
        }
        return;
    }

    /// <summary>
    /// Method <c>SetPanelColor</c> changes the indicated panel to the indicated color.
    /// </summary>
    /// <param name="panel">Panel to paint.</param>
    /// <param name="color">Color to paint.</param>
    private void SetPanelColor(GameObject panel, Color32 color) {
        Transform cellComponents = panel.transform;
        foreach (Transform component in cellComponents)
        {
            if (component.name == "Panel") component.GetComponent<Image>().color = color;
        }
        return;
    }

    /// <summary>
    /// Method <c>EndGame</c> Starts end game for the current player.
    /// </summary>
    private void EndGame() {
        Debug.Log("Endgame for player " + playerTorn);
        if (!scripts.GetComponent<CellInfo>().EndgameForPlayer(playerTorn)) return;
        Debug.Log(playerTorn + " dead");
        playerInfo[playerTorn].isDead = true;
        //If only one player remaining -> endGame screen
    }

    /// <summary>
    /// Method <c>NextTorn</c> Sets the torn to the next player.
    /// </summary>
   public void NextTorn()
    {
        travel.gameObject.SetActive(false);
        endTorn.gameObject.SetActive(false);
        scripts.GetComponent<CellInfo>().DisableCard();
        if (scripts.GetComponent<CashManagement>().GetCash(playerTorn) < 0) EndGame();
        if (playerInfo[playerTorn].isDead) SetPanelColor(playerPanel[playerTorn], new Color32(131, 133, 140, 255));
        else SetPanelColor(playerPanel[playerTorn], new Color32(237, 231, 217, 255));

        ++playerTorn;
        if (playerTorn == numberOfPlayers) playerTorn = 0;
        scripts.GetComponent<CellInfo>().SetPlayer(playerTorn);
        destination = players[playerTorn].transform.position;
        SetPanelColor(playerPanel[playerTorn], new Color32(243, 146, 55, 255));
        alreadyTraveled = false;
        if (playerInfo[playerTorn].isDead) NextTorn();

        if (playerInfo[playerTorn].penalizedTorns == 0) {
            MakeRollDice();
            return;
        }

        if (playerInfo[playerTorn].isBot) scripts.GetComponent<Bot>().LeaveJail(playerInfo[playerTorn].numberOutOfJailCards > 0);
        else {
            rollDoubles.gameObject.SetActive(true);
            pay50.gameObject.SetActive(true);
            if (playerInfo[playerTorn].numberOutOfJailCards > 0) getOutForFree.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Method <c>Pay</c> the player decides to pay to leave jail.
    /// </summary>
    public void Pay() {
        if (!scripts.GetComponent<CashManagement>().ModifyCash(playerTorn, -100, false, false)) return;
        rollDoubles.gameObject.SetActive(false);
        pay50.gameObject.SetActive(false);
        getOutForFree.gameObject.SetActive(false);
        playerInfo[playerTorn].penalizedTorns = 0;

        MakeRollDice();
    }

    /// <summary>
    /// Method <c>UseCard</c> the player decides to use a get out for free card to leave jail.
    /// </summary>
   public void UseCard() {
        rollDoubles.gameObject.SetActive(false);
        pay50.gameObject.SetActive(false);
        getOutForFree.gameObject.SetActive(false);
        playerInfo[playerTorn].penalizedTorns = 0;

        MakeRollDice();
        playerInfo[playerTorn].numberOutOfJailCards--;
    }

    /// <summary>
    /// Method <c>IncrementByOneOutOfJailCards</c> increments by one unit the number of get out for free cards for the indicated player.
    /// </summary>
    /// <param name="player">Player to increment the cards number to.</param>
    public void IncrementByOneOutOfJailCards(int player) {
        playerInfo[player].numberOutOfJailCards++;
    }

    /// <summary>
    /// Method <c>Mortgage</c> paints the indicated cell to red.
    /// </summary>
    /// <param name="cell">Cell name to paint.</param>
    public void Mortgage(string cellName) {
        SetCellColor(StringToPos(cellName), new Color32(55, 55, 55, 255));
    }

    /// <summary>
    /// Method <c>Unmortgage</c> paints the indicated cell to white.
    /// </summary>
    /// <param name="cell">Cell name to paint.</param>
    public void Unmortgage(string cellName)
    {
        SetCellColor(StringToPos(cellName), new Color32(255, 255, 255, 255));
    }

    /// <summary>
    /// int function <c>StringToPos</c> returns the number of cell respresented by the indicated cell name.
    /// </summary>
    /// <param name="cellName">Cell name.</param>
    /// <returns>Number of cell.</returns>
    private int StringToPos(string cellName) {
        for (int i = 0; i < cells.Length; ++i) if (cells[i].name == cellName) return i;
        return -1;
    }

    /// <summary>
    /// Method <c>MoveTo</c> moves the current player form origin to destination.
    /// </summary>
    /// <param name="origin">Cell name where the player is.</param>
    /// <param name="destination">Cell name where the player will go to.</param>
    public void MoveTo(string origin, string destination) {
        carryOnMoving = true;
        int originCellNumber = StringToPos(origin);
        int destinationCellNumber = StringToPos(destination);
        int distance = destinationCellNumber - originCellNumber;
        if (distance < 0) distance += cells.Length;
        MoveNumberOfCells(distance);
    }

    /// <summary>
    /// Method <c>RollDicesDoubles</c> the player has decided to roll doubles when in jail.
    /// </summary>
    public void RollDicesDoubles()
    {
        diceSound.Play();
        rollDoubles.gameObject.SetActive(false);
        pay50.gameObject.SetActive(false);
        getOutForFree.gameObject.SetActive(false);
        diceValue1 = Random.Range(1, 7);
        diceValue2 = Random.Range(1, 7);
        dice1.text = diceValue1 + "";
        dice2.text = diceValue2 + "";
        if (diceValue1 == diceValue2) {
            movementsRemaining = diceValue1 + diceValue2;
            playerInfo[playerTorn].penalizedTorns = 0;
        }
        else --playerInfo[playerTorn].penalizedTorns;
        MakeEndTorn();
    }

    /// <summary>
    /// Method <c>CellPosition</c> returns the cell world position.
    /// </summary>
    /// <param name="cellName">Cell name.</param>
    /// <returns>Cell world position.</returns>
    public Vector3 CellPosition(string cellName) {
        for (int i = 0; i < 40; ++i) {
            if (cells[i].name == cellName) return cells[i].transform.position;
        }
        return new Vector3(0, 0, 0);
    }

    /// <summary>
    /// int function <c>GetPlayerTorn</c> returns the current player torn.
    /// </summary>
    /// <returns>Current player torn.</returns>
    public int GetPlayerTorn() {
        return playerTorn;
    }

    /// <summary>
    /// Method <c>GoToJail</c> the current player goes to jail.
    /// </summary>
    public void GoToJail() {
        Debug.Log("Player" + playerTorn + " go to jail");
        carryOnMoving = true;
        endTorn.gameObject.SetActive(false);
        rollDice.gameObject.SetActive(false);
        destination = new Vector3(cells[10].transform.position.x - 2, cells[10].transform.position.y + 1, cells[10].transform.position.z - 2);
        moving = true;
        movementsRemaining = 0;
        doublesRolled = 0;
        playerInfo[playerTorn].position = 10;
        playerInfo[playerTorn].penalizedTorns = 3;
    }

    /// <summary>
    /// Method <c>MoveNumberOfCells</c> moves the current player the number of cells indicated.
    /// </summary>
    /// <param name="numberOfCells">Number of cells.</param>
    public void MoveNumberOfCells(int numberOfCells) {
        movementsRemaining = numberOfCells;
        endTorn.gameObject.SetActive(false);
        rollDice.gameObject.SetActive(false);
    }

    /// <summary>
    /// Method <c>RollDice</c> rolls both dices.
    /// </summary>
    public void RollDice()
    {
        scripts.GetComponent<CellInfo>().DisableCard();
        travel.gameObject.SetActive(false);
        rollDice.gameObject.SetActive(false);
        diceSound.Play();
        diceValue1 = Random.Range(1, 7);
        diceValue2 = Random.Range(1, 7);
        //diceValue1 = 4;
        //diceValue2 = 3;
        dice1.text = diceValue1 + "";
        dice2.text = diceValue2 + "";
        StartCoroutine(WaitAndMove(1));
    }

    /// <summary>
    /// Method <c>WaitAndMove</c> waits and sets the number of movements remaining to the dice value.
    /// </summary>
    /// <param name="timeInSeconds">Seconds to wait.</param>
    /// <returns></returns>
    private IEnumerator WaitAndMove(float timeInSeconds)
    {
        yield return new WaitForSecondsRealtime(timeInSeconds);

        movementsRemaining = diceValue1 + diceValue2;
        if (diceValue1 == diceValue2)
        {
            ++doublesRolled;
            if (doublesRolled >= 3) GoToJail();
        }
        else doublesRolled = 0;
    }

    /// <summary>
    /// int function <c>GetOutOfJail</c> returns teh number of get out of jail for free cards the indicated player has.
    /// </summary>
    /// <param name="player">Player to get the number of cards from.</param>
    /// <returns>Number of get out of jail for free cards.</returns>
    public int GetOutOfJail(int player) {
        return playerInfo[player].numberOutOfJailCards;
    }

    /// <summary>
    /// int function <c>GetNumberOfPlayers</c> returns the number of players that are in game.
    /// </summary>
    /// <returns>Number of players in game.</returns>
    public int GetNumberOfPlayers() {
        return numberOfPlayers;
    }

    /// <summary>
    /// bool function <c>IsBot</c> returns if the indicated player is the computer or not
    /// </summary>
    /// <param name="player">Player to get the information from.</param>
    /// <returns><c>true</c> if the player is the computer, <c>false</c> otherwise.</returns>
    public bool IsBot(int player) {
        return playerInfo[player].isBot;
    }

    /// <summary>
    /// Method <c>NextCell</c> moves the current player to the next cell.
    /// </summary>
    private void NextCell() {
        if (movementsRemaining > 0)
        {
            moving = true;
            ++playerInfo[playerTorn].position;
            if (playerInfo[playerTorn].position == cells.Length)
            {
                playerInfo[playerTorn].position = 0;
                scripts.GetComponent<CashManagement>().ModifyCash(playerTorn, 200, false, true);
            }

            destination = new Vector3(cells[playerInfo[playerTorn].position].transform.position.x, cells[playerInfo[playerTorn].position].transform.position.y + 1, cells[playerInfo[playerTorn].position].transform.position.z);
            --movementsRemaining;
        }
        else if (movementsRemaining < 0)
        {
            moving = true;
            --playerInfo[playerTorn].position;
            destination = new Vector3(cells[playerInfo[playerTorn].position].transform.position.x, cells[playerInfo[playerTorn].position].transform.position.y + 1, cells[playerInfo[playerTorn].position].transform.position.z);
            ++movementsRemaining;
        }
    }

    /// <summary>
    /// Method <c>MakeRollDice</c> Shows roll dice button if the player is not the computer, sends the bot he can roll the dice signal.
    /// </summary>
    private void MakeRollDice() {
        if (playerInfo[playerTorn].isBot) scripts.GetComponent<Bot>().RollDice();
        else rollDice.gameObject.SetActive(true);
    }

    /// <summary>
    /// Method <c>MakeEndTorn</c> Shows end torn button if the player is not the computer, sends the bot he can end the torn signal.
    /// </summary>
    private void MakeEndTorn() {
        if (playerInfo[playerTorn].isBot) scripts.GetComponent<Bot>().BeforeEndTorn(playerTorn, scripts.GetComponent<CellInfo>().GetPropertyInformation(), scripts.GetComponent<CellInfo>().GetRailroadInformation(), scripts.GetComponent<CellInfo>().GetWater(), scripts.GetComponent<CellInfo>().GetElectrical());
        else endTorn.gameObject.SetActive(true);
    }

    /// <summary>
    /// Method <c>MovePlayer</c> moves the current player to the correct position in the current frame.
    /// </summary>
    private void MovePlayer() {
        players[playerTorn].transform.position = Vector3.MoveTowards(players[playerTorn].transform.position, destination, 12 * Time.deltaTime);
        if (Vector3.Distance(players[playerTorn].transform.position, destination) > 0.001f) return;

        moving = false;
        if (movementsRemaining > 0) return;

        string cell_name = cells[playerInfo[playerTorn].position].name;
        carryOnMoving = false;
        Debug.Log("Player" + playerTorn + " rolled (" + diceValue1 + ", " + diceValue2 + ") Landed on " + cell_name);
        if (cell_name == "GoToJail") GoToJail();
        else if (cell_name != "Start") scripts.GetComponent<CellInfo>().LandedOn(cell_name, playerTorn, diceValue1 + diceValue2);

        if (carryOnMoving)
        {
            carryOnMoving = false;
            return;
        }

        if (doublesRolled > 0) MakeRollDice();
        else MakeEndTorn();
    }

    /// <summary>
    /// Method <c>Update</c> moves the player towards destination.
    /// </summary>
    private void Update()
    {
        if (!moving) NextCell();
        if (Vector3.Distance(players[playerTorn].transform.position, destination) > 0.001f) MovePlayer();
    }
}
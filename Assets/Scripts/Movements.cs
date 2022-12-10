using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movements : MonoBehaviour
{
    public GameObject[] cells;
    public GameObject[] players;
    public GameObject[] panels;
    public Button roll_dice;
    public Button end_torn;
    public Button roll_doubles;
    public Button pay50;
    public Button getOutForFree;
    public Button travel;

    public Text dice1;
    public Text dice2;

    private bool moving = false;
    private int movements_remaining = 0;
    private int player_torn = 0;
    private int doubles_rolled = 0;
    private Vector3 destination;

    bool already_traveled = false;
    bool carryOnMoving = false;

    int d1 = 1;
    int d2 = 1;

    private Player[] Players;

    struct Player {
        public int pos;
        public int penalized_torns;
        public int outofjail;
        public bool dead;
        public bool bot;
    };

    int n_players;
    int players_alive;

    GameObject scripts;

    void Start()
    {
        scripts = GameObject.Find("GameHandler");
        roll_dice.onClick.AddListener(RollDice);
        end_torn.onClick.AddListener(NextTorn);
        roll_doubles.onClick.AddListener(roll_dices_doubles);
        pay50.onClick.AddListener(pay);
        getOutForFree.onClick.AddListener(use_card);
        travel.onClick.AddListener(Travel);
        roll_dice.gameObject.SetActive(false);
        end_torn.gameObject.SetActive(false);
        pay50.gameObject.SetActive(false);
        roll_doubles.gameObject.SetActive(false);
        getOutForFree.gameObject.SetActive(false);
        travel.gameObject.SetActive(false);

        n_players = DataHolder.n_players;
        players_alive = n_players;
        Players = new Player[n_players];
        for (int i = 0; i < n_players; ++i)
        {
            players[i].transform.position = new Vector3(cells[0].transform.position.x, cells[0].transform.position.y + 1, cells[0].transform.position.z);
            destination = players[i].transform.position;
            Players[i].pos = 0;
            Players[i].penalized_torns = 0;
            Players[i].outofjail = 0;
            Players[i].dead = false;
            if (i == 0) Players[i].bot = DataHolder.bot1;
            else if (i == 1) Players[i].bot = DataHolder.bot2;
            else if (i == 2) Players[i].bot = DataHolder.bot3;
            else if (i == 3) Players[i].bot = DataHolder.bot4;
        }

        if (n_players <= 3)
        {
            panels[3].gameObject.SetActive(false);
            if (n_players == 2)
            {
                panels[2].gameObject.SetActive(false);
                panels[0].transform.position = new Vector3(panels[0].transform.position.x, panels[0].transform.position.y - 150, panels[0].transform.position.z);
                panels[1].transform.position = new Vector3(panels[1].transform.position.x, panels[1].transform.position.y - 150, panels[1].transform.position.z);
            }
            else
            {
                panels[0].transform.position = new Vector3(panels[0].transform.position.x, panels[0].transform.position.y - 50, panels[0].transform.position.z);
                panels[1].transform.position = new Vector3(panels[1].transform.position.x, panels[1].transform.position.y - 50, panels[1].transform.position.z);
                panels[2].transform.position = new Vector3(panels[2].transform.position.x, panels[2].transform.position.y - 50, panels[2].transform.position.z);
            }
        }
        ChangeColor(panels[0], new Color32(255, 182, 65, 255));

        MakeRollDice();
    }

    void Travel() {
        travel.gameObject.SetActive(false);
        scripts.GetComponent<Cell_info>().travelSelected();
        if (scripts.GetComponent<Cell_info>().CanTravel("Station", player_torn)) SetColor(5, new Color32(255, 241, 56, 255));
        if (scripts.GetComponent<Cell_info>().CanTravel("Station2", player_torn)) SetColor(15, new Color32(255, 241, 56, 255));
        if (scripts.GetComponent<Cell_info>().CanTravel("Station3", player_torn)) SetColor(25, new Color32(255, 241, 56, 255));
        if (scripts.GetComponent<Cell_info>().CanTravel("Station4", player_torn)) SetColor(35, new Color32(255, 241, 56, 255));
    }

    public void ShowTravelButton() {
        if (already_traveled) already_traveled = false;
        else {
            already_traveled = true;
            if (Players[player_torn].bot) scripts.GetComponent<Bot>().Travel(cells[Players[player_torn].pos].name, scripts.GetComponent<Cell_info>().CanTravel("Station", player_torn), scripts.GetComponent<Cell_info>().CanTravel("Station2", player_torn), scripts.GetComponent<Cell_info>().CanTravel("Station3", player_torn), scripts.GetComponent<Cell_info>().CanTravel("Station4", player_torn));
            else travel.gameObject.SetActive(true);
        }
    }


    public void undoChanges()
    {
        if (!scripts.GetComponent<Cell_info>().StationMortgaged(1)) SetColor(5, new Color32(255, 255, 255, 255));
        if (!scripts.GetComponent<Cell_info>().StationMortgaged(2)) SetColor(15, new Color32(255, 255, 255, 255));
        if (!scripts.GetComponent<Cell_info>().StationMortgaged(3)) SetColor(25, new Color32(255, 255, 255, 255));
        if (!scripts.GetComponent<Cell_info>().StationMortgaged(4)) SetColor(35, new Color32(255, 255, 255, 255));
    }

    void SetColor(int cell, Color32 col)
    {
        Transform t = cells[cell].transform;
        foreach (Transform tr in t)
        {
            if (tr.tag == "CellBody") tr.GetComponent<Renderer>().material.color = col;
        }
        return;
    }

    void ChangeColor(GameObject panel, Color32 col) {
        Transform t = panel.transform;
        foreach (Transform tr in t)
        {
            if (tr.name == "Panel") tr.GetComponent<Image>().color = col;
        }
        return;
    }

    void EndGame() {
        Debug.Log("Endgame for player " + player_torn);
        if (!scripts.GetComponent<Cell_info>().EndgameForPlayer(player_torn)) return;
        Debug.Log(player_torn + " dead");
        Players[player_torn].dead = true;
        //If only one player remaining -> endGame screen
    }

   public void NextTorn()
    {
        travel.gameObject.SetActive(false);
        end_torn.gameObject.SetActive(false);
        scripts.GetComponent<Cell_info>().disableCard();
        if (scripts.GetComponent<Cash_management>().GetCash(player_torn) < 0) EndGame();
        if (Players[player_torn].dead) ChangeColor(panels[player_torn], new Color32(155, 155, 155, 255));
        else ChangeColor(panels[player_torn], new Color32(241, 241, 241, 255));

        ++player_torn;
        if (player_torn == n_players) player_torn = 0;
        scripts.GetComponent<Cell_info>().SetPlayer(player_torn);
        destination = players[player_torn].transform.position;
        ChangeColor(panels[player_torn], new Color32(255, 182, 65, 255));
        already_traveled = false;
        if (Players[player_torn].dead) NextTorn();

        if (Players[player_torn].penalized_torns == 0) {
            MakeRollDice();
            return;
        }

        Debug.Log("Penalized torns player" + player_torn + ": " + Players[player_torn].penalized_torns);

        if (Players[player_torn].bot) scripts.GetComponent<Bot>().LeaveJail(Players[player_torn].outofjail > 0);
        else {
            roll_doubles.gameObject.SetActive(true);
            pay50.gameObject.SetActive(true);
            if (Players[player_torn].outofjail > 0) getOutForFree.gameObject.SetActive(true);
        }
    }

    public void pay() {
        if (!scripts.GetComponent<Cash_management>().modify_cash(player_torn, -100, false, false)) return;
        roll_doubles.gameObject.SetActive(false);
        pay50.gameObject.SetActive(false);
        getOutForFree.gameObject.SetActive(false);
        Players[player_torn].penalized_torns = 0;

        MakeRollDice();
    }

   public void use_card() {
        roll_doubles.gameObject.SetActive(false);
        pay50.gameObject.SetActive(false);
        getOutForFree.gameObject.SetActive(false);
        Players[player_torn].penalized_torns = 0;

        MakeRollDice();
        Players[player_torn].outofjail--;
    }

    public void add_out_of_jail(int player) {
        Players[player].outofjail++;
    }

    public void mortgage(string cell) {
        for (int i = 0; i < cells.Length; ++i) if (cells[i].name == cell) SetColor(i, new Color32(55, 55, 55, 255));
    }

    public void unmortgage(string cell)
    {
        for (int i = 0; i < cells.Length; ++i) if (cells[i].name == cell) SetColor(i, new Color32(255, 255, 255, 255));
    }

    int StringToPos(string cell_name) {
        for (int i = 0; i < cells.Length; ++i) if (cells[i].name == cell_name) return i;
        return -1;
    }

    public void MoveTo(string o, string d) {
        carryOnMoving = true;
        int pos_o = StringToPos(o);
        int pos_d = StringToPos(d);
        int dist = pos_d - pos_o;
        if (dist < 0) dist += cells.Length;
        MoveNCells(dist);
    }

    public void roll_dices_doubles()
    {
        roll_doubles.gameObject.SetActive(false);
        pay50.gameObject.SetActive(false);
        getOutForFree.gameObject.SetActive(false);
        d1 = Random.Range(1, 7);
        d2 = Random.Range(1, 7);
        dice1.text = d1 + "";
        dice2.text = d2 + "";
        if (d1 == d2) {
            movements_remaining = d1 + d2;
            Players[player_torn].penalized_torns = 0;
        }
        else --Players[player_torn].penalized_torns;
        MakeEndTorn();
    }

    public Vector3 cell_pos(string cell_name) {
        for (int i = 0; i < 40; ++i) {
            if (cells[i].name == cell_name) return cells[i].transform.position;
        }
        Vector3 zero = new Vector3(0, 0, 0);
        return zero;
    }

    public int getPlayerTorn() {
        return player_torn;
    }

    public void GoToJail() {
        Debug.Log("Player" + player_torn + " go to jail");
        carryOnMoving = true;
        end_torn.gameObject.SetActive(false);
        roll_dice.gameObject.SetActive(false);
        destination = new Vector3(cells[10].transform.position.x - 2, cells[10].transform.position.y + 1, cells[10].transform.position.z - 2);
        moving = true;
        movements_remaining = 0;
        doubles_rolled = 0;
        Players[player_torn].pos = 10;
        Players[player_torn].penalized_torns = 3;
    }

    public void MoveNCells(int n) {
        movements_remaining = n;
        end_torn.gameObject.SetActive(false);
        roll_dice.gameObject.SetActive(false);
    }

    public void RollDice()
    {
        travel.gameObject.SetActive(false);
        roll_dice.gameObject.SetActive(false);
        d1 = Random.Range(1, 7);
        d2 = Random.Range(1, 7);
        //d1 = 3;
        //d2 = 2;
        dice1.text = d1 + "";
        dice2.text = d2 + "";
        movements_remaining = d1 + d2;
        if (d1 == d2)
        {
            ++doubles_rolled;
            if (doubles_rolled >= 3) GoToJail();
        }
        else doubles_rolled = 0;
    }

    public int GetOutOfJail(int player) {
        return Players[player].outofjail;
    }

    public int GetNPlayers() {
        return n_players;
    }

    public bool IsBot(int player) {
        return Players[player].bot;
    }

    private void NextCell() {
        if (movements_remaining > 0)
        {
            moving = true;
            ++Players[player_torn].pos;
            if (Players[player_torn].pos == cells.Length)
            {
                Players[player_torn].pos = 0;
                scripts.GetComponent<Cash_management>().modify_cash(player_torn, 200, false, true);
            }

            destination = new Vector3(cells[Players[player_torn].pos].transform.position.x, cells[Players[player_torn].pos].transform.position.y + 1, cells[Players[player_torn].pos].transform.position.z);
            --movements_remaining;
        }

        if (movements_remaining < 0)
        {
            moving = true;
            --Players[player_torn].pos;
            destination = new Vector3(cells[Players[player_torn].pos].transform.position.x, cells[Players[player_torn].pos].transform.position.y + 1, cells[Players[player_torn].pos].transform.position.z);
            ++movements_remaining;
        }
    }

    private void MakeRollDice() {
        if (Players[player_torn].bot) scripts.GetComponent<Bot>().RollDice();
        else roll_dice.gameObject.SetActive(true);
    }

    private void MakeEndTorn() {
        if (Players[player_torn].bot) scripts.GetComponent<Bot>().BeforeEndTorn();
        else end_torn.gameObject.SetActive(true);
    }

    private void MovePlayer() {
        players[player_torn].transform.position = Vector3.MoveTowards(players[player_torn].transform.position, destination, 12 * Time.deltaTime);
        if (Vector3.Distance(players[player_torn].transform.position, destination) > 0.001f) return;

        moving = false;
        if (movements_remaining > 0) return;

        string cell_name = cells[Players[player_torn].pos].name;
        carryOnMoving = false;
        Debug.Log("Player" + player_torn + " rolled (" + d1 + ", " + d2 + ") Landed on " + cell_name);
        if (cell_name == "GoToJail") GoToJail();
        else if (cell_name != "Start") scripts.GetComponent<Cell_info>().LandedOn(cell_name, player_torn, d1 + d2);

        if (carryOnMoving)
        {
            carryOnMoving = false;
            return;
        }

        if (doubles_rolled > 0) MakeRollDice();
        else MakeEndTorn();
    }

    void Update()
    {
        if (!moving) NextCell();
        if (Vector3.Distance(players[player_torn].transform.position, destination) > 0.001f) MovePlayer();
    }
}
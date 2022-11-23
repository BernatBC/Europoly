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

    bool s1 = false;
    bool s2 = false;
    bool s3 = false;
    bool s4 = false;

    int d1 = 1;
    int d2 = 1;

    private Player[] Players;

    struct Player {
        public int pos;
        public int penalized_torns;
        public int outofjail;
    };

    int n_players;

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

        end_torn.gameObject.SetActive(false);
        roll_dice.gameObject.SetActive(true);
        pay50.gameObject.SetActive(false);
        roll_doubles.gameObject.SetActive(false);
        getOutForFree.gameObject.SetActive(false);
        travel.gameObject.SetActive(false);

        n_players = DataHolder.n_players;
        Players = new Player[n_players];
        for (int i = 0; i < n_players; ++i)
        {
            players[i].transform.position = new Vector3(cells[0].transform.position.x, cells[0].transform.position.y + 1, cells[0].transform.position.z);
            destination = players[i].transform.position;
            Players[i].pos = 0;
            Players[i].penalized_torns = 0;
            Players[i].outofjail = 0;
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
    }

    void Travel() {
        travel.gameObject.SetActive(false);
        end_torn.gameObject.SetActive(false);
        scripts.GetComponent<Cell_info>().travelSelected();
        if (s1) SetColor(5, new Color32(255, 241, 56, 255));
        if (s2) SetColor(15, new Color32(255, 241, 56, 255));
        if (s3) SetColor(25, new Color32(255, 241, 56, 255));
        if (s4) SetColor(35, new Color32(255, 241, 56, 255));
    }

    public void ShowTravelButton(bool station1, bool station2, bool station3, bool station4) {
        if (already_traveled) already_traveled = false;
        else {
            already_traveled = true;
            travel.gameObject.SetActive(true);
            s1 = station1;
            s2 = station2;
            s3 = station3;
            s4 = station4;
        }
    }


    public void undoChanges()
    {
        if (s1) SetColor(5, new Color32(255, 255, 255, 255));
        if (s2) SetColor(15, new Color32(255, 255, 255, 255));
        if (s3) SetColor(25, new Color32(255, 255, 255, 255));
        if (s4) SetColor(35, new Color32(255, 255, 255, 255));
        s1 = false;
        s2 = false;
        s3 = false;
        s4 = false;
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

    void NextTorn()
    {
        travel.gameObject.SetActive(false);
        end_torn.gameObject.SetActive(false);
        //panels[player_torn].GetComponents<Image>().color = new Color32(241, 241, 241, 255);
        ChangeColor(panels[player_torn], new Color32(241, 241, 241, 255));
        ++player_torn;
        if (player_torn == n_players) player_torn = 0;
        scripts.GetComponent<Cell_info>().SetPlayer(player_torn);
        destination = players[player_torn].transform.position;
        ChangeColor(panels[player_torn], new Color32(255, 182, 65, 255));
        if (Players[player_torn].penalized_torns == 0) roll_dice.gameObject.SetActive(true);
        else {
            roll_doubles.gameObject.SetActive(true);
            pay50.gameObject.SetActive(true);
            if (Players[player_torn].outofjail > 0) getOutForFree.gameObject.SetActive(true);
            --Players[player_torn].penalized_torns;
        }
        
    }

    void pay() { 
        roll_doubles.gameObject.SetActive(false);
        pay50.gameObject.SetActive(false);
        getOutForFree.gameObject.SetActive(false);
        scripts.GetComponent<Cash_management>().modify_cash(player_torn, -100, false);
        Players[player_torn].penalized_torns = 0;
        roll_dice.gameObject.SetActive(true);
    }

    void use_card() {
        roll_doubles.gameObject.SetActive(false);
        pay50.gameObject.SetActive(false);
        getOutForFree.gameObject.SetActive(false);
        Players[player_torn].penalized_torns = 0;
        roll_dice.gameObject.SetActive(true);
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
        int pos_o = StringToPos(o);
        int pos_d = StringToPos(d);
        int dist = pos_d - pos_o;
        if (dist < 0) dist += cells.Length;
        MoveNCells(dist);
    }

    void roll_dices_doubles()
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
        else end_torn.gameObject.SetActive(true);
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

    void RollDice()
    {
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

    void Update()
    {
        if (!moving && movements_remaining > 0)
        {
            moving = true;
            ++Players[player_torn].pos;
            if (Players[player_torn].pos == cells.Length) {
                Players[player_torn].pos = 0;
                scripts.GetComponent<Cash_management>().modify_cash(player_torn, 200, false);
            }

            destination = new Vector3(cells[Players[player_torn].pos].transform.position.x, cells[Players[player_torn].pos].transform.position.y + 1, cells[Players[player_torn].pos].transform.position.z);
            --movements_remaining;
        }

        if (!moving && movements_remaining < 0)
        {
            moving = true;
            --Players[player_torn].pos;
            destination = new Vector3(cells[Players[player_torn].pos].transform.position.x, cells[Players[player_torn].pos].transform.position.y + 1, cells[Players[player_torn].pos].transform.position.z);
            ++movements_remaining;
        }

        if (Vector3.Distance(players[player_torn].transform.position, destination) > 0.001f)
        {
            players[player_torn].transform.position = Vector3.MoveTowards(players[player_torn].transform.position, destination, 12 * Time.deltaTime);
            if (Vector3.Distance(players[player_torn].transform.position, destination) <= 0.001f)
            {
                moving = false;
                if (movements_remaining == 0) {
                    if (doubles_rolled > 0) roll_dice.gameObject.SetActive(true);
                    else end_torn.gameObject.SetActive(true);
                    string cell_name = cells[Players[player_torn].pos].name;
                    if (cell_name == "GoToJail") GoToJail();
                    else if (cell_name != "Start") scripts.GetComponent<Cell_info>().LandedOn(cell_name, player_torn, d1 + d2);
                }
                
            }
        }
    }
}
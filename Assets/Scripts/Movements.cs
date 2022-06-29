using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movements : MonoBehaviour
{
    public GameObject[] cells;
    public GameObject[] players;
    public Button roll_dice;
    public Button end_torn;
    public Button roll_doubles;
    public Button pay50;

    public Text dice1;
    public Text dice2;

    private int[] players_pos;
    private int[] penalized_torns;
    private bool moving = false;
    private int movements_remaining = 0;
    private int player_torn = 0;
    private int doubles_rolled = 0;
    private Vector3 destination;

    int d1 = 1;
    int d2 = 1;

    GameObject scripts;

    void Start()
    {
        scripts = GameObject.Find("GameHandler");
        roll_dice.onClick.AddListener(RollDice);
        end_torn.onClick.AddListener(NextTorn);
        roll_doubles.onClick.AddListener(roll_dices_doubles);
        pay50.onClick.AddListener(pay);
        end_torn.gameObject.SetActive(false);
        roll_dice.gameObject.SetActive(true);
        pay50.gameObject.SetActive(false);
        roll_doubles.gameObject.SetActive(false);

        players_pos = new int[players.Length];
        penalized_torns = new int[players.Length];
        for (int i = 0; i < players.Length; ++i)
        {
            players[i].transform.position = new Vector3(cells[0].transform.position.x, cells[0].transform.position.y + 1, cells[0].transform.position.z);
            destination = players[i].transform.position;
            players_pos[i] = 0;
            penalized_torns[i] = 0;
        }
    }

    void NextTorn()
    {
        end_torn.gameObject.SetActive(false);
        ++player_torn;
        if (player_torn == players.Length) player_torn = 0;
        destination = players[player_torn].transform.position;
        if (penalized_torns[player_torn] == 0) roll_dice.gameObject.SetActive(true);
        else {
            roll_doubles.gameObject.SetActive(true);
            pay50.gameObject.SetActive(true);
            --penalized_torns[player_torn];
        }
        
    }

    void pay() { 
        roll_doubles.gameObject.SetActive(false);
        pay50.gameObject.SetActive(false);
        scripts.GetComponent<Cash_management>().modify_cash(player_torn, -100, false);
        penalized_torns[player_torn] = 0;
        roll_dice.gameObject.SetActive(true);
    }

    void roll_dices_doubles()
    {
        roll_doubles.gameObject.SetActive(false);
        pay50.gameObject.SetActive(false);
        d1 = Random.Range(1, 7);
        d2 = Random.Range(1, 7);
        dice1.text = d1 + "";
        dice2.text = d2 + "";
        if (d1 == d2) {
            movements_remaining = d1 + d2;
            penalized_torns[player_torn] = 0;
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
        Debug.Log("Go to jail");
        end_torn.gameObject.SetActive(false);
        roll_dice.gameObject.SetActive(false);
        destination = new Vector3(cells[10].transform.position.x - 2, cells[10].transform.position.y + 1, cells[10].transform.position.z - 2);
        moving = true;
        movements_remaining = 0;
        doubles_rolled = 0;
        players_pos[player_torn] = 10;
        penalized_torns[player_torn] = 3;
    }

    void RollDice()
    {
        roll_dice.gameObject.SetActive(false);
        d1 = Random.Range(1, 7);
        d2 = Random.Range(1, 7);
        //d1 = 5;
        //d2 = 5;
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

    void Update()
    {
        if (!moving && movements_remaining > 0)
        {
            moving = true;
            ++players_pos[player_torn];
            if (players_pos[player_torn] == cells.Length) {
                players_pos[player_torn] = 0;
                scripts.GetComponent<Cash_management>().modify_cash(player_torn, 200, false);
            }

            destination = new Vector3(cells[players_pos[player_torn]].transform.position.x, cells[players_pos[player_torn]].transform.position.y + 1, cells[players_pos[player_torn]].transform.position.z);
            --movements_remaining;
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
                    string cell_name = cells[players_pos[player_torn]].name;
                    if (cell_name == "GoToJail") GoToJail();
                    else if (cell_name != "Start") scripts.GetComponent<Cell_info>().LandedOn(cell_name, player_torn, d1 + d2);
                }
                
            }
        }
    }
}
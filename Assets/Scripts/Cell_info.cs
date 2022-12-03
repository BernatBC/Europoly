using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cell_info : MonoBehaviour
{
    [Header("Buttons")]
    public Button buy_button;
    public Button pass_button;
    public Button buy_house_button;
    public Button sell_house_button;
    public Button mortgage;
    public Button trade;
    public Button cancel;
    public Button offer;
    public Button accept;
    public Button reject;

    [Header("Cards")]
    public GameObject CardUI;
    public GameObject rrcard;
    public GameObject chestcard;
    public GameObject chancecard;
    public GameObject electric_card;
    public GameObject water_card;
    public GameObject income_tax_card;
    public GameObject luxury_tax_card;

    [Header("Select trading player buttons")]
    public GameObject TradingButton1;
    public GameObject TradingButton2;
    public GameObject TradingButton3;

    [Header("Other")]
    public GameObject[] owner_mark;
    public GameObject house;
    public GameObject hotel;
    public GameObject tradePanel1;
    public GameObject tradePanel2;

    RaycastHit hit;
    Ray ray;
    bool card_shown = false;
    bool rr_card_shown = false;
    bool electric_card_shown = false;
    bool water_card_shown = false;
    bool income_card_shown = false;
    bool luxury_card_shown = false;
    bool cc_card_shown = false;
    bool chance_card_shown = false;
    bool just_buttons = false;
    bool buy_selected = false;
    bool sell_selected = false;
    bool mortgage_selected = false;
    bool travel_selected = false;
    bool trading_selected = false;

    bool tradePanel1_on = false;
    bool tradePanel2_on = false;

    int actual_player = 0;
    string actual_cell = "";

    int multiplier = 1;

    //Trading info
    string[] TradingCells1 = new string[30];
    string[] TradingCells2 = new string[30];
    bool[] CellSelected1 = new bool[30];
    bool[] CellSelected2 = new bool[30];
    int cash1 = 0;
    int cash2 = 0;
    int trading_partner = 0;

    bool freeze_trading = false;

    GameObject scripts;

    struct Cell {
        public string name;
        public int cost, house_cost, houses, owner, rent, rent1, rent2, rent3, rent4, rentH;
        public bool mortgaged;
    };

    struct RailRoad {
        public string name;
        public int owner;
        public bool mortgaged;
    }

    struct Utility
    {
        public int owner;
        public bool mortgaged;
    }

    Utility electrical;
    Utility water;

    Dictionary<string, Cell> info;
    Dictionary<string, RailRoad> rr_info;
    Dictionary<string, GameObject> owner_prefabs;
    Dictionary<string, GameObject[]> houses_prefabs;

    void InitializeCellDictionary() {
        info = new Dictionary<string, Cell>();
        Cell c = new Cell { };
        c.name = "Old Kent Rd";
        c.cost = 60;
        c.house_cost = 50;
        c.houses = 0;
        c.owner = -1;
        c.rent = 2;
        c.rent1 = 10;
        c.rent2 = 30;
        c.rent3 = 90;
        c.rent4 = 160;
        c.rentH = 250;
        c.mortgaged = false;
        info.Add("Brown", c);

        c.name = "Whitechapel Rd";
        c.cost = 60;
        c.house_cost = 50;
        c.rent = 4;
        c.rent1 = 20;
        c.rent2 = 60;
        c.rent3 = 180;
        c.rent4 = 320;
        c.rentH = 450;
        info.Add("Brown2", c);

        c.name = "The Angel, Islington";
        c.cost = 100;
        c.house_cost = 50;
        c.rent = 6;
        c.rent1 = 30;
        c.rent2 = 90;
        c.rent3 = 270;
        c.rent4 = 400;
        c.rentH = 550;
        info.Add("LightBlue", c);

        c.name = "Euston Rd";
        c.cost = 100;
        c.house_cost = 50;
        c.rent = 6;
        c.rent1 = 30;
        c.rent2 = 90;
        c.rent3 = 270;
        c.rent4 = 400;
        c.rentH = 550;
        info.Add("LightBlue2", c);

        c.name = "Pentonville Rd";
        c.cost = 120;
        c.house_cost = 50;
        c.rent = 8;
        c.rent1 = 40;
        c.rent2 = 100;
        c.rent3 = 300;
        c.rent4 = 450;
        c.rentH = 600;
        info.Add("LightBlue3", c);

        c.name = "Pall Mall";
        c.cost = 140;
        c.house_cost = 100;
        c.rent = 10;
        c.rent1 = 50;
        c.rent2 = 150;
        c.rent3 = 450;
        c.rent4 = 625;
        c.rentH = 750;
        info.Add("Purple", c);

        c.name = "Whitehall";
        c.cost = 140;
        c.house_cost = 100;
        c.rent = 10;
        c.rent1 = 50;
        c.rent2 = 150;
        c.rent3 = 450;
        c.rent4 = 625;
        c.rentH = 750;
        info.Add("Purple2", c);

        c.name = "Northumberland Ave";
        c.cost = 160;
        c.house_cost = 100;
        c.rent = 12;
        c.rent1 = 60;
        c.rent2 = 180;
        c.rent3 = 500;
        c.rent4 = 700;
        c.rentH = 900;
        info.Add("Purple3", c);

        c.name = "Bow St";
        c.cost = 180;
        c.house_cost = 100;
        c.rent = 14;
        c.rent1 = 70;
        c.rent2 = 200;
        c.rent3 = 550;
        c.rent4 = 750;
        c.rentH = 950;
        info.Add("Orange", c);

        c.name = "Marlborough St";
        c.cost = 180;
        c.house_cost = 100;
        c.rent = 14;
        c.rent1 = 70;
        c.rent2 = 200;
        c.rent3 = 550;
        c.rent4 = 750;
        c.rentH = 950;
        info.Add("Orange2", c);

        c.name = "Vine St";
        c.cost = 200;
        c.house_cost = 100;
        c.rent = 16;
        c.rent1 = 80;
        c.rent2 = 220;
        c.rent3 = 600;
        c.rent4 = 800;
        c.rentH = 1000;
        info.Add("Orange3", c);

        c.name = "Strand";
        c.cost = 220;
        c.house_cost = 150;
        c.rent = 18;
        c.rent1 = 90;
        c.rent2 = 250;
        c.rent3 = 700;
        c.rent4 = 875;
        c.rentH = 1050;
        info.Add("Red", c);

        c.name = "Fleet St";
        c.cost = 220;
        c.house_cost = 150;
        c.rent = 18;
        c.rent1 = 90;
        c.rent2 = 250;
        c.rent3 = 700;
        c.rent4 = 875;
        c.rentH = 1050;
        info.Add("Red2", c);

        c.name = "Trafalgar Sq";
        c.cost = 240;
        c.house_cost = 150;
        c.rent = 20;
        c.rent1 = 100;
        c.rent2 = 300;
        c.rent3 = 750;
        c.rent4 = 925;
        c.rentH = 1100;
        info.Add("Red3", c);

        c.name = "Leicester St";
        c.cost = 260;
        c.house_cost = 150;
        c.rent = 22;
        c.rent1 = 110;
        c.rent2 = 330;
        c.rent3 = 800;
        c.rent4 = 975;
        c.rentH = 1150;
        info.Add("Yellow", c);

        c.name = "Coventry St";
        c.cost = 260;
        c.house_cost = 150;
        c.rent = 22;
        c.rent1 = 110;
        c.rent2 = 330;
        c.rent3 = 800;
        c.rent4 = 975;
        c.rentH = 1150;
        info.Add("Yellow2", c);

        c.name = "Piccadilly";
        c.cost = 280;
        c.house_cost = 150;
        c.rent = 24;
        c.rent1 = 120;
        c.rent2 = 360;
        c.rent3 = 850;
        c.rent4 = 1025;
        c.rentH = 1200;
        info.Add("Yellow3", c);

        c.name = "Regent St";
        c.cost = 300;
        c.house_cost = 200;
        c.rent = 26;
        c.rent1 = 130;
        c.rent2 = 390;
        c.rent3 = 900;
        c.rent4 = 1100;
        c.rentH = 1275;
        info.Add("Green", c);

        c.name = "Oxford St";
        c.cost = 300;
        c.house_cost = 200;
        c.rent = 26;
        c.rent1 = 130;
        c.rent2 = 390;
        c.rent3 = 900;
        c.rent4 = 1100;
        c.rentH = 1275;
        info.Add("Green2", c);

        c.name = "Bond St";
        c.cost = 320;
        c.house_cost = 200;
        c.rent = 28;
        c.rent1 = 150;
        c.rent2 = 450;
        c.rent3 = 1000;
        c.rent4 = 1200;
        c.rentH = 1400;
        info.Add("Green3", c);

        c.name = "Park Lane";
        c.cost = 350;
        c.house_cost = 200;
        c.rent = 35;
        c.rent1 = 175;
        c.rent2 = 500;
        c.rent3 = 1100;
        c.rent4 = 1300;
        c.rentH = 1500;
        info.Add("DarkBlue", c);

        c.name = "Mayfair";
        c.cost = 400;
        c.house_cost = 200;
        c.rent = 50;
        c.rent1 = 200;
        c.rent2 = 600;
        c.rent3 = 1400;
        c.rent4 = 1700;
        c.rentH = 2000;
        info.Add("DarkBlue2", c);
    }

    void InitializeRRDictionary() {
        rr_info = new Dictionary<string, RailRoad>();
        RailRoad c = new RailRoad { };
        c.name = "Kings Cross";
        c.owner = -1;
        c.mortgaged = false;
        rr_info.Add("Station", c);
        c.name = "Marylebone";
        rr_info.Add("Station2", c);
        c.name = "Fenchurch St";
        rr_info.Add("Station3", c);
        c.name = "Liverpool St";
        rr_info.Add("Station4", c);
    }

    void InitializePrefabDictionary() {
        houses_prefabs = new Dictionary<string, GameObject[]>();
        GameObject[] v = new GameObject[5];
        houses_prefabs.Add("Brown", v);
        houses_prefabs.Add("Brown2", v);
        houses_prefabs.Add("LightBlue", v);
        houses_prefabs.Add("LightBlue2", v);
        houses_prefabs.Add("LightBlue3", v);
        houses_prefabs.Add("Purple", v);
        houses_prefabs.Add("Purple2", v);
        houses_prefabs.Add("Purple3", v);
        houses_prefabs.Add("Orange", v);
        houses_prefabs.Add("Orange2", v);
        houses_prefabs.Add("Orange3", v);
        houses_prefabs.Add("Red", v);
        houses_prefabs.Add("Red2", v);
        houses_prefabs.Add("Red3", v);
        houses_prefabs.Add("Yellow", v);
        houses_prefabs.Add("Yellow2", v);
        houses_prefabs.Add("Yellow3", v);
        houses_prefabs.Add("Green", v);
        houses_prefabs.Add("Green2", v);
        houses_prefabs.Add("Green3", v);
        houses_prefabs.Add("DarkBlue", v);
        houses_prefabs.Add("DarkBlue2", v);
    }

    void Start()
    {
        scripts = GameObject.Find("GameHandler");
        InitializeCellDictionary();
        InitializeRRDictionary();
        InitializePrefabDictionary();
        electrical = new Utility { };
        owner_prefabs = new Dictionary<string, GameObject>();
        electrical.owner = -1;
        electrical.mortgaged = false;

        water = new Utility { };
        water.owner = -1;
        water.mortgaged = false;
        buy_button.onClick.AddListener(buy_property);
        pass_button.onClick.AddListener(pass_clicked);
        buy_house_button.onClick.AddListener(buy_house);
        sell_house_button.onClick.AddListener(sell_house);
        mortgage.onClick.AddListener(mortgage_unmortgage);
        trade.onClick.AddListener(trading);
        cancel.onClick.AddListener(canceling);
        offer.onClick.AddListener(offering);
        reject.onClick.AddListener(canceling);
        accept.onClick.AddListener(makeTrade);
    }

    void buy_house() {
        if (trading_selected) return;
        buy_selected = true;
        sell_selected = false;
        mortgage_selected = false;
    }

    void sell_house() {
        if (trading_selected) return;
        sell_selected = true;
        buy_selected = false;
        mortgage_selected = false;
    }

    void pass_clicked() {
        disableCard();
        just_buttons = false;
        buy_button.gameObject.SetActive(false);
        pass_button.gameObject.SetActive(false);
    }
    void ShowRentCard(string cell_name) {
        Image top = CardUI.transform.Find("Top").GetComponent<Image>();

        if (cell_name == "Brown" || cell_name == "Brown2") top.color = new Color32(166, 84, 31, 255);
        else if (cell_name == "LightBlue" || cell_name == "LightBlue2" || cell_name == "LightBlue3") top.color = new Color32(0, 203, 255, 255);
        else if (cell_name == "Purple" || cell_name == "Purple2" || cell_name == "Purple3") top.color = new Color32(255, 0, 117, 255);
        else if (cell_name == "Orange" || cell_name == "Orange2" || cell_name == "Orange3") top.color = new Color32(255, 78, 0, 255);
        else if (cell_name == "Red" || cell_name == "Red2" || cell_name == "Red3") top.color = new Color32(255, 15, 0, 255);
        else if (cell_name == "Yellow" || cell_name == "Yellow2" || cell_name == "Yellow3") top.color = new Color32(255, 236, 0, 255);
        else if (cell_name == "Green" || cell_name == "Green2" || cell_name == "Green3") top.color = new Color32(0, 142, 7, 255);
        else if (cell_name == "DarkBlue" || cell_name == "DarkBlue2") top.color = new Color32(57, 70, 255, 255);
        else return;

        CardUI.transform.Find("Name").GetComponent<Text>().text = info[cell_name].name;
        CardUI.transform.Find("Cost_value").GetComponent<Text>().text = info[cell_name].cost + "";
        CardUI.transform.Find("Rent_value").GetComponent<Text>().text = info[cell_name].rent + "";
        CardUI.transform.Find("Rent1_value").GetComponent<Text>().text = info[cell_name].rent1 + "";
        CardUI.transform.Find("Rent2_value").GetComponent<Text>().text = info[cell_name].rent2 + "";
        CardUI.transform.Find("Rent3_value").GetComponent<Text>().text = info[cell_name].rent3 + "";
        CardUI.transform.Find("Rent4_value").GetComponent<Text>().text = info[cell_name].rent4 + "";
        CardUI.transform.Find("RentH_value").GetComponent<Text>().text = info[cell_name].rentH + "";
        CardUI.transform.Find("CostHouse_value").GetComponent<Text>().text = info[cell_name].house_cost + "";
        CardUI.transform.Find("CostHotel_value").GetComponent<Text>().text = info[cell_name].house_cost + "";
        CardUI.transform.Find("Mortgage_value").GetComponent<Text>().text = info[cell_name].cost / 2 + "";

        if (info[cell_name].mortgaged) CardUI.transform.Find("Body").GetComponentInChildren<Image>().color = new Color32(120, 120, 120, 255);
        else CardUI.transform.Find("Body").GetComponentInChildren<Image>().color = new Color32(245, 234, 223, 255);
        CardUI.gameObject.SetActive(true);
        card_shown = true;
    }

    private void ShowRRCard(string cell_name) {
        rrcard.transform.Find("Name").GetComponent<Text>().text = rr_info[cell_name].name;
        if (rr_info[cell_name].mortgaged) rrcard.transform.Find("Body").GetComponentInChildren<Image>().color = new Color32(120, 120, 120, 255);
        else rrcard.transform.Find("Body").GetComponentInChildren<Image>().color = new Color32(245, 234, 223, 255);
        rrcard.gameObject.SetActive(true);
        rr_card_shown = true;
    }

    private void ShowUtiliyCard(string cell_name) {
        if (cell_name == "Electric")
        {
            if (electrical.mortgaged) electric_card.transform.Find("Body").GetComponentInChildren<Image>().color = new Color32(120, 120, 120, 255);
            else electric_card.transform.Find("Body").GetComponentInChildren<Image>().color = new Color32(245, 234, 223, 255);
            electric_card.gameObject.SetActive(true);
            electric_card_shown = true;
        }
        else {
            if (water.mortgaged) water_card.transform.Find("Body").GetComponentInChildren<Image>().color = new Color32(120, 120, 120, 255);
            else water_card.transform.Find("Body").GetComponentInChildren<Image>().color = new Color32(245, 234, 223, 255);
            water_card.gameObject.SetActive(true);
            water_card_shown = true;
        }
    }

    private void makeTrade() {
        for (int i = 0; i < CellSelected1.Length; ++i) {
            if (CellSelected1[i]) new_owner(TradingCells1[i], trading_partner);
            if (CellSelected2[i]) new_owner(TradingCells2[i], actual_player);
        }
        scripts.GetComponent<Cash_management>().modify_cash(actual_player, cash2 - cash1, false, false);
        scripts.GetComponent<Cash_management>().modify_cash(trading_partner, cash1 - cash2, false, false);
        if ((actual_cell == "Station" || actual_cell == "Station2" || actual_cell == "Station3" || actual_cell == "Station4") && rr_info[actual_cell].owner == actual_player) possibilityToTravel();
        canceling();
    }

    void new_owner(string cell, int player)
    {
        if (cell == "Station" || cell == "Station2" || cell == "Station3" || cell == "Station4")
        {
            RailRoad r = rr_info[cell];
            rr_info.Remove(cell);
            r.owner = player;
            rr_info.Add(cell, r);
        }
        else if (cell == "Electric") electrical.owner = player;
        else if (cell == "Water") water.owner = player;
        else
        {
            Cell c = info[cell];
            info.Remove(cell);
            c.owner = player;
            info.Add(cell, c);
        }
        remove_owner_marker(cell);
        add_owner_marker(cell, player);
    }

    private void remove_owner_marker(string cell) {
        GameObject a = owner_prefabs[cell];
        Destroy(a);
        owner_prefabs.Remove(cell);
    }

    private void canceling() {
        trading_selected = false;
        freeze_trading = false;
        tradePanel1.transform.Find("Cash").GetComponent<InputField>().readOnly = false;
        tradePanel2.transform.Find("Cash").GetComponent<InputField>().readOnly = false;
        cancel.gameObject.SetActive(false);
        offer.gameObject.SetActive(false);
        tradePanel1.gameObject.SetActive(false);
        tradePanel2.gameObject.SetActive(false);
        accept.gameObject.SetActive(false);
        reject.gameObject.SetActive(false);
    }

    private void offering()
    {
        freeze_trading = true;
        tradePanel1.transform.Find("Cash").GetComponent<InputField>().readOnly = true;
        tradePanel2.transform.Find("Cash").GetComponent<InputField>().readOnly = true;
        accept.gameObject.SetActive(true);
        reject.gameObject.SetActive(true);
        cancel.gameObject.SetActive(false);
        offer.gameObject.SetActive(false);

    }

    public void SecondTradePanel(int tag) {
        TradingButton1.gameObject.SetActive(false);
        TradingButton2.gameObject.SetActive(false);
        TradingButton3.gameObject.SetActive(false);

        if (actual_player == 0) trading_partner = tag;
        else if (actual_player == 1)
        {
            if (tag == 1) trading_partner = 0;
            else trading_partner = tag;
        }
        else if (actual_player == 2)
        {
            if (tag == 3) trading_partner = 3;
            else trading_partner = tag - 1;
        }
        else trading_partner = tag - 1;

        cancel.gameObject.SetActive(true);
        offer.gameObject.SetActive(true);
        trading_selected = true;
        ShowPlayerInfo(actual_player, 1, false);

        ShowPlayerInfo(trading_partner, 2, false);
        tradePanel1.gameObject.SetActive(true);
        tradePanel2.gameObject.SetActive(true);
    }

    public bool EndgameForPlayer(int player) {
        //Sell houses + hotels while cash < 0
        HashSet<string> to_delete = new HashSet<string>();
        //Sell houses
        foreach (string s in info.Keys) {
            Cell c = info[s];
            if (c.owner != player) continue;
            to_delete.Add(s);
            while (c.houses > 0) {
                Cell k = info[s];
                info.Remove(s);
                remove_house(s, k.houses);
                --k.houses;
                info.Add(s, k);
                scripts.GetComponent<Cash_management>().modify_cash(player, k.house_cost / 2, false, true);
                if (scripts.GetComponent<Cash_management>().GetCash(player) >= 0) return false; 
            }
        }
        //Mortgage
        foreach (string s in to_delete) {
            Cell k = info[s];
            if (k.mortgaged) continue;
            info.Remove(s);
            remove_house(s, k.houses);
            k.mortgaged = true;
            info.Add(s, k);
            scripts.GetComponent<Cash_management>().modify_cash(player, k.cost / 2, false, true);
            scripts.GetComponent<Movements>().mortgage(s);
            if (scripts.GetComponent<Cash_management>().GetCash(player) >= 0) return false;
        }
        //Remove propertries, player has lost
        foreach (string s in to_delete) {
            Cell k = info[s];
            info.Remove(s);
            k.owner = -1;
            info.Add(s, k);
            remove_owner_marker(s);
        }
        return true;
    }

    private void EnableTradingButtons(int n_players) {
        if (n_players == 3)
        {
            if (actual_player == 0)
            {
                TradingButton1.transform.Find("Circle").GetComponent<Image>().color = new Color32(0, 137, 255, 255);
                TradingButton2.transform.Find("Circle").GetComponent<Image>().color = new Color32(234, 241, 0, 255);
            }
            else if (actual_player == 1)
            {
                TradingButton1.transform.Find("Circle").GetComponent<Image>().color = new Color32(255, 78, 78, 255);
                TradingButton2.transform.Find("Circle").GetComponent<Image>().color = new Color32(234, 241, 0, 255);
            }
            else {
                TradingButton1.transform.Find("Circle").GetComponent<Image>().color = new Color32(255, 78, 78, 255);
                TradingButton2.transform.Find("Circle").GetComponent<Image>().color = new Color32(0, 137, 255, 255);
            }
        }
        else {
            if (actual_player == 0)
            {
                TradingButton1.transform.Find("Circle").GetComponent<Image>().color = new Color32(0, 137, 255, 255);
                TradingButton2.transform.Find("Circle").GetComponent<Image>().color = new Color32(234, 241, 0, 255);
                TradingButton3.transform.Find("Circle").GetComponent<Image>().color = new Color32(62, 233, 54, 255);
            }
            else if (actual_player == 1)
            {
                TradingButton1.transform.Find("Circle").GetComponent<Image>().color = new Color32(255, 78, 78, 255);
                TradingButton2.transform.Find("Circle").GetComponent<Image>().color = new Color32(234, 241, 0, 255);
                TradingButton3.transform.Find("Circle").GetComponent<Image>().color = new Color32(62, 233, 54, 255);
            }
            else if (actual_player == 2)
            {
                TradingButton1.transform.Find("Circle").GetComponent<Image>().color = new Color32(255, 78, 78, 255);
                TradingButton2.transform.Find("Circle").GetComponent<Image>().color = new Color32(0, 137, 255, 255);
                TradingButton3.transform.Find("Circle").GetComponent<Image>().color = new Color32(62, 233, 54, 255);
            }
            else {
                TradingButton1.transform.Find("Circle").GetComponent<Image>().color = new Color32(255, 78, 78, 255);
                TradingButton2.transform.Find("Circle").GetComponent<Image>().color = new Color32(0, 137, 255, 255);
                TradingButton3.transform.Find("Circle").GetComponent<Image>().color = new Color32(234, 241, 0, 255);
            }

            TradingButton3.gameObject.SetActive(true);
        }
        TradingButton1.gameObject.SetActive(true);
        TradingButton2.gameObject.SetActive(true);
    }

    private void trading() {
        if (freeze_trading) return;
        trading_selected = true;
        int n_players = scripts.GetComponent<Movements>().GetNPlayers();
        if (n_players > 2) {
            EnableTradingButtons(n_players);
            return;
        }
        cancel.gameObject.SetActive(true);
        offer.gameObject.SetActive(true);

        ShowPlayerInfo(actual_player, 1, false);

        if (actual_player == 1) trading_partner = 0;
        else trading_partner = 1;
        ShowPlayerInfo(trading_partner, 2, false);
        tradePanel1.gameObject.SetActive(true);
        tradePanel2.gameObject.SetActive(true);
    }

    private int Repairs(int house, int hotel) {
        int total = 0;
        foreach (Cell c in info.Values)
        {
            if (c.owner == actual_player && c.houses > 0) {
                if (c.houses == 5) total += hotel;
                else total += house*c.houses;
            }
        }
        return total;
    }

    private void ChestCard(int n) {
        if (n == 1) {
            chestcard.transform.Find("Name").GetComponent<Text>().text = "Advance to Go (Collect 200)";
            scripts.GetComponent<Movements>().MoveTo(actual_cell, "Start");
            StartCoroutine(WaitAndDisableCard2(3));
        }
        else if (n == 2) {
            chestcard.transform.Find("Name").GetComponent<Text>().text = "Bank error in your favour. Collect 200";
            scripts.GetComponent<Cash_management>().modify_cash(actual_player, 200, false, true);
            StartCoroutine(WaitAndDisableCard2(3));
        }
        else if (n == 3)
        {
            chestcard.transform.Find("Name").GetComponent<Text>().text = "Doctor’s fee. Pay 50";
            scripts.GetComponent<Cash_management>().modify_cash(actual_player, -50, false, true);
            StartCoroutine(WaitAndDisableCard2(3));
        }
        else if (n == 4)
        {
            chestcard.transform.Find("Name").GetComponent<Text>().text = "From sale of stock you get 50";
            scripts.GetComponent<Cash_management>().modify_cash(actual_player, 50, false, true);
            StartCoroutine(WaitAndDisableCard2(3));
        }
        else if (n == 5)
        {
            chestcard.transform.Find("Name").GetComponent<Text>().text = "Get Out of Jail Free";
            scripts.GetComponent<Movements>().add_out_of_jail(actual_player);
            StartCoroutine(WaitAndDisableCard2(3));
        }
        else if (n == 6)
        {
            chestcard.transform.Find("Name").GetComponent<Text>().text = "Go directly to jail, do not pass Go, do not collect 200";
            scripts.GetComponent<Movements>().GoToJail();
            StartCoroutine(WaitAndDisableCard2(3));
        }
        else if (n == 7)
        {
            chestcard.transform.Find("Name").GetComponent<Text>().text = "Holiday fund matures. Receive 100";
            scripts.GetComponent<Cash_management>().modify_cash(actual_player, 100, false, true);
            StartCoroutine(WaitAndDisableCard2(3));
        }
        else if (n == 8)
        {
            chestcard.transform.Find("Name").GetComponent<Text>().text = "Income tax refund. Collect 20";
            scripts.GetComponent<Cash_management>().modify_cash(actual_player, 20, false, true);
            StartCoroutine(WaitAndDisableCard2(3));
        }
        else if (n == 9)
        {
            chestcard.transform.Find("Name").GetComponent<Text>().text = "It is your birthday. Collect 10 from every player";
            scripts.GetComponent<Cash_management>().collect_from_everybody(actual_player, 10);
            StartCoroutine(WaitAndDisableCard2(3));
        }
        else if (n == 10)
        {
            chestcard.transform.Find("Name").GetComponent<Text>().text = "Life insurance matures. Collect 100";
            scripts.GetComponent<Cash_management>().modify_cash(actual_player, 100, false, true);
            StartCoroutine(WaitAndDisableCard2(3));
        }
        else if (n == 11)
        {
            chestcard.transform.Find("Name").GetComponent<Text>().text = "Pay hospital fees of 100";
            scripts.GetComponent<Cash_management>().modify_cash(actual_player, -100, false, true);
            StartCoroutine(WaitAndDisableCard2(3));
        }
        else if (n == 12)
        {
            chestcard.transform.Find("Name").GetComponent<Text>().text = "Pay school fees of 50";
            scripts.GetComponent<Cash_management>().modify_cash(actual_player, -50, false, true);
            StartCoroutine(WaitAndDisableCard2(3));
        }
        else if (n == 13)
        {
            chestcard.transform.Find("Name").GetComponent<Text>().text = "Receive 25 consultancy fee";
            scripts.GetComponent<Cash_management>().modify_cash(actual_player, 25, false, true);
            StartCoroutine(WaitAndDisableCard2(3));
        }
        else if (n == 14)
        {
            chestcard.transform.Find("Name").GetComponent<Text>().text = "You are assessed for street repairs. 40 per house. 115 per hotel";
            scripts.GetComponent<Cash_management>().modify_cash(actual_player, -Repairs(40, 115), false, true);
            StartCoroutine(WaitAndDisableCard2(3));
        }
        else if (n == 15)
        {
            chestcard.transform.Find("Name").GetComponent<Text>().text = "You have won second prize in a beauty contest. Collect 10";
            scripts.GetComponent<Cash_management>().modify_cash(actual_player, 10, false, true);
            StartCoroutine(WaitAndDisableCard2(3));
        }
        else if (n == 16)
        {
            chestcard.transform.Find("Name").GetComponent<Text>().text = "You inherit 100";
            scripts.GetComponent<Cash_management>().modify_cash(actual_player, 100, false, true);
            StartCoroutine(WaitAndDisableCard2(3));
        }
        chestcard.gameObject.SetActive(true);
        cc_card_shown = true;
    }

    IEnumerator WaitAndDisableCard(float s)
    {
        yield return new WaitForSecondsRealtime(s);
        chancecard.gameObject.SetActive(false);
        chance_card_shown = false;
    }
    IEnumerator WaitAndDisableCard2(float s)
    {
        yield return new WaitForSecondsRealtime(s);
        chestcard.gameObject.SetActive(false);
        cc_card_shown = false;
    }

    private void ChanceCard(int n)
    {
        if (n == 1)
        {
            chancecard.transform.Find("Name").GetComponent<Text>().text = "Advance to Go (Collect 200)";
            scripts.GetComponent<Movements>().MoveTo(actual_cell, "Start");
            StartCoroutine(WaitAndDisableCard(3));
        }
        else if (n == 2)
        {
            chancecard.transform.Find("Name").GetComponent<Text>().text = "Advance to Trafalgar Square. If you pass Go, collect 200";
            scripts.GetComponent<Movements>().MoveTo(actual_cell, "Red3");
            StartCoroutine(WaitAndDisableCard(3));
        }
        else if (n == 3)
        {
            chancecard.transform.Find("Name").GetComponent<Text>().text = "Advance to Mayfair";
            scripts.GetComponent<Movements>().MoveTo(actual_cell, "DarkBlue2");
            StartCoroutine(WaitAndDisableCard(3));
        }
        else if (n == 4)
        {
            chancecard.transform.Find("Name").GetComponent<Text>().text = "Advance to Pall Mall. If you pass Go, collect 200";
            scripts.GetComponent<Movements>().MoveTo(actual_cell, "Purple");
            StartCoroutine(WaitAndDisableCard(3));
        }
        else if (n == 5 || n == 6)
        {
            chancecard.transform.Find("Name").GetComponent<Text>().text = "Advance to the nearest Station. If unowned, you may buy it from the Bank. If owned, pay wonder twice the rental to which they are otherwise entitled";
            if (actual_cell == "Chance") scripts.GetComponent<Movements>().MoveTo(actual_cell, "Station2");
            else if (actual_cell == "Chance2") scripts.GetComponent<Movements>().MoveTo(actual_cell, "Station3");
            else scripts.GetComponent<Movements>().MoveTo(actual_cell, "Station");
            multiplier = 2;
            StartCoroutine(WaitAndDisableCard(3));
        }
        else if (n == 7)
        {
            chancecard.transform.Find("Name").GetComponent<Text>().text = "Advance token to nearest Utility. If unowned, you may buy it from the Bank. If owned, pay owner a total ten times amount thrown.";
            if (actual_cell == "Chance2") scripts.GetComponent<Movements>().MoveTo(actual_cell, "Water");
            else scripts.GetComponent<Movements>().MoveTo(actual_cell, "Electric");
            multiplier = 10;
            StartCoroutine(WaitAndDisableCard(3));
        }
        else if (n == 8)
        {
            chancecard.transform.Find("Name").GetComponent<Text>().text = "Bank pays you dividend of 50";
            scripts.GetComponent<Cash_management>().modify_cash(actual_player, 50, false, true);
            StartCoroutine(WaitAndDisableCard(3));
        }
        else if (n == 9)
        {
            chancecard.transform.Find("Name").GetComponent<Text>().text = "Get Out of Jail Free";
            scripts.GetComponent<Movements>().add_out_of_jail(actual_player);
            StartCoroutine(WaitAndDisableCard(3));
        }
        else if (n == 10)
        {
            chancecard.transform.Find("Name").GetComponent<Text>().text = "Go Back 3 Spaces";
            scripts.GetComponent<Movements>().MoveNCells(-3);
            StartCoroutine(WaitAndDisableCard(3));
        }
        else if (n == 11)
        {
            chancecard.transform.Find("Name").GetComponent<Text>().text = "Go directly to Jail, do not pass Go, do not collect 200";
            scripts.GetComponent<Movements>().GoToJail();
            StartCoroutine(WaitAndDisableCard(3));
        }
        else if (n == 12)
        {
            chancecard.transform.Find("Name").GetComponent<Text>().text = "Make general repairs on all your property. For each house pay 25. For each hotel pay 100";
            scripts.GetComponent<Cash_management>().modify_cash(actual_player, -Repairs(25, 100), false, true);
            StartCoroutine(WaitAndDisableCard(3));
        }
        else if (n == 13)
        {
            chancecard.transform.Find("Name").GetComponent<Text>().text = "Speeding fine 15";
            scripts.GetComponent<Cash_management>().modify_cash(actual_player, -15, false, true);
            StartCoroutine(WaitAndDisableCard(3));
        }
        else if (n == 14)
        {
            chancecard.transform.Find("Name").GetComponent<Text>().text = "Take a trip to Kings Cross Station. If you pass Go, collect 200";
            scripts.GetComponent<Movements>().MoveTo(actual_cell, "Station");
            StartCoroutine(WaitAndDisableCard(3));
        }
        else if (n == 15)
        {
            chancecard.transform.Find("Name").GetComponent<Text>().text = "You have been elected Chairman of the Board. Pay each player 50";
            scripts.GetComponent<Cash_management>().pay_everybody(actual_player, 50);
            StartCoroutine(WaitAndDisableCard(3));
        }
        else if (n == 16)
        {
            chancecard.transform.Find("Name").GetComponent<Text>().text = "Your building loan matures. Collect 150";
            scripts.GetComponent<Cash_management>().modify_cash(actual_player, 150, false, true);
            StartCoroutine(WaitAndDisableCard(3));
        }
        chancecard.gameObject.SetActive(true);
        chance_card_shown = true;
    }
    void ShowTaxCard(string cell_name) {
        if (cell_name == "Tax")
        {
            income_tax_card.gameObject.SetActive(true);
            income_card_shown = true;
        }
        else
        {
            luxury_tax_card.gameObject.SetActive(true);
            luxury_card_shown = true;
        }
    }

    void add_owner_marker(string cell, int player) {
        Vector3 pos = scripts.GetComponent<Movements>().cell_pos(cell);
        if (cell == "Brown" || cell == "Brown2" || cell == "LightBlue" || cell == "LightBlue2" || cell == "LightBlue3" || cell == "Station") pos.z += 1.5f;
        else if (cell == "Purple" || cell == "Purple2" || cell == "Purple3" || cell == "Electric" || cell == "Orange" || cell == "Station2" || cell == "Orange2" || cell == "Orange3") pos.x += 1.5f;
        else if (cell == "Red" || cell == "Red2" || cell == "Red3" || cell == "Station3" || cell == "Yellow" || cell == "Yellow2" || cell == "Yellow3" || cell == "Water") pos.z -= 1.5f;
        else if (cell == "Green" || cell == "Green2" || cell == "Green3" || cell == "DarkBlue" || cell == "DarkBlue2" || cell == "Station4") pos.x -= 1.5f;
        
        GameObject a = Instantiate(owner_mark[player], new Vector3(pos.x, 0.55f, pos.z), owner_mark[player].gameObject.transform.rotation);
        owner_prefabs.Add(cell, a);
    }

    void add_house(string cell_name, int house_pos) {
        Vector3 pos = scripts.GetComponent<Movements>().cell_pos(cell_name);
        if (cell_name == "Brown" || cell_name == "Brown2" || cell_name == "LightBlue" || cell_name == "LightBlue2" || cell_name == "LightBlue3")
        {
            pos.z -= 1.9f;
            if (house_pos == 1) pos.x += 1;
            else if (house_pos == 2) pos.x += 0.33f;
            else if (house_pos == 3) pos.x -= 0.33f;
            else if (house_pos == 4) pos.x -= 1;
            else {
                for (int i = 0; i < 4; ++i) remove_house(cell_name, i + 1);
                houses_prefabs[cell_name][4] = Instantiate(hotel, new Vector3(pos.x, 1.4f, pos.z), Quaternion.identity);
                houses_prefabs[cell_name][4].gameObject.name = cell_name + "_H";
                return;
            }
        }
        else if (cell_name == "Purple" || cell_name == "Purple2" || cell_name == "Purple3" || cell_name == "Orange" || cell_name == "Orange2" || cell_name == "Orange3")
        {
            pos.x -= 1.9f;
            if (house_pos == 1) pos.z -= 1;
            else if (house_pos == 2) pos.z -= 0.33f;
            else if (house_pos == 3) pos.z += 0.33f;
            else if (house_pos == 4) pos.z += 1;
            else
            {
                for (int i = 0; i < 4; ++i) remove_house(cell_name, i + 1);
                houses_prefabs[cell_name][4] = Instantiate(hotel, new Vector3(pos.x, 1.4f, pos.z), Quaternion.identity);
                houses_prefabs[cell_name][4].transform.Rotate(0, 90, 0);
                houses_prefabs[cell_name][4].gameObject.name = cell_name + "_H";
                return;
            }
        }
        else if (cell_name == "Red" || cell_name == "Red2" || cell_name == "Red3" || cell_name == "Yellow" || cell_name == "Yellow2" || cell_name == "Yellow3")
        {
            pos.z += 1.9f;
            if (house_pos == 1) pos.x -= 1;
            else if (house_pos == 2) pos.x -= 0.33f;
            else if (house_pos == 3) pos.x += 0.33f;
            else if (house_pos == 4) pos.x += 1;
            else
            {
                for (int i = 0; i < 4; ++i) remove_house(cell_name, i + 1);
                houses_prefabs[cell_name][4] = Instantiate(hotel, new Vector3(pos.x, 1.4f, pos.z), Quaternion.identity);
                houses_prefabs[cell_name][4].gameObject.name = cell_name + "_H";
                return;
            }
        }
        else if (cell_name == "Green" || cell_name == "Green2" || cell_name == "Green3" || cell_name == "DarkBlue" || cell_name == "DarkBlue2") {
            pos.x += 1.9f;
            if (house_pos == 1) pos.z += 1;
            else if (house_pos == 2) pos.z += 0.33f;
            else if (house_pos == 3) pos.z -= 0.33f;
            else if (house_pos == 4) pos.z -= 1;
            else
            {
                for (int i = 0; i < 4; ++i) remove_house(cell_name, i + 1);
                houses_prefabs[cell_name][4] = Instantiate(hotel, new Vector3(pos.x, 1.4f, pos.z), Quaternion.identity);
                houses_prefabs[cell_name][4].transform.Rotate(0, 90, 0);
                houses_prefabs[cell_name][4].gameObject.name = cell_name + "_H";
                return;
            }
        }
        houses_prefabs[cell_name][house_pos - 1] = Instantiate(house, new Vector3(pos.x, 0.7f, pos.z), Quaternion.identity);
        houses_prefabs[cell_name][house_pos - 1].gameObject.name = cell_name + "_" + house_pos.ToString();
    }

    void remove_house (string cell_name, int house_pos) {
        if (house_pos < 5) {
            GameObject h = GameObject.Find(cell_name + "_" + house_pos.ToString());
            Destroy(h);
            return;
        }
        GameObject h2 = GameObject.Find(cell_name + "_H");
        Destroy(h2);
        for (int i = 1; i <= 4; ++i) add_house(cell_name, i);
    }

    bool disableCard() {
        if (card_shown)
        {
            CardUI.gameObject.SetActive(false);
            card_shown = false;
            return true;
        }
        else if (rr_card_shown)
        {
            rrcard.gameObject.SetActive(false);
            rr_card_shown = false;
            return true;
        }
        else if (electric_card_shown)
        {
            electric_card.gameObject.SetActive(false);
            electric_card_shown = false;
            return true;
        }
        else if (water_card_shown)
        {
            water_card.gameObject.SetActive(false);
            water_card_shown = false;
            return true;
        }
        else if (income_card_shown)
        {
            income_tax_card.gameObject.SetActive(false);
            income_card_shown = false;
            return true;
        }
        else if (luxury_card_shown)
        {
            luxury_tax_card.gameObject.SetActive(false);
            luxury_card_shown = false;
            return true;
        }
        else if (cc_card_shown) {
            chestcard.gameObject.SetActive(false);
            cc_card_shown = false;
            return true;
        }
        else if (chance_card_shown)
        {
            chancecard.gameObject.SetActive(false);
            chance_card_shown = false;
            return true;
        }
        return false;
    }

    void buy_property() {
        if (actual_cell == "Station" || actual_cell == "Station2" || actual_cell == "Station3" || actual_cell == "Station4")
        {
            if (!scripts.GetComponent<Cash_management>().modify_cash(actual_player, -200, false, false)) return;
            RailRoad r = rr_info[actual_cell];
            rr_info.Remove(actual_cell);
            r.owner = actual_player;
            rr_info.Add(actual_cell, r);
            possibilityToTravel();
        }
        else if (actual_cell == "Electric")
        {
            if (!scripts.GetComponent<Cash_management>().modify_cash(actual_player, -150, false, false)) return;
            electrical.owner = actual_player;
        }
        else if (actual_cell == "Water")
        {
            if (!scripts.GetComponent<Cash_management>().modify_cash(actual_player, -150, false, false)) return;
            water.owner = actual_player;
        }
        else {
            Cell c = info[actual_cell];
            if (!scripts.GetComponent<Cash_management>().modify_cash(actual_player, -c.cost, false, false)) return;
            info.Remove(actual_cell);
            c.owner = actual_player;
            info.Add(actual_cell, c);
        }
        disableCard();
        just_buttons = false;
        buy_button.gameObject.SetActive(false);
        pass_button.gameObject.SetActive(false);
        add_owner_marker(actual_cell, actual_player);
    }

    int countStations(int owner) {
        int k = 0;
        if (rr_info["Station"].owner == owner && !rr_info["Station"].mortgaged) ++k;
        if (rr_info["Station2"].owner == owner && !rr_info["Station2"].mortgaged) ++k;
        if (rr_info["Station3"].owner == owner && !rr_info["Station3"].mortgaged) ++k;
        if (rr_info["Station4"].owner == owner && !rr_info["Station4"].mortgaged) ++k;
        return k;
    }

    private void mortgage_unmortgage() {
        if (trading_selected) return;
        mortgage_selected = true;
        buy_selected = false;
        sell_selected = false;
    }

    bool ColorHasHousing(string cell_name) {
        if (cell_name == "Brown" || cell_name == "Brown2") return info["Brown"].houses > 0 || info["Brown2"].houses > 0;
        if (cell_name == "LightBlue" || cell_name == "LightBlue2" || cell_name == "LightBlue3") return info["LightBlue"].houses > 0 || info["LightBlue2"].houses > 0 || info["LightBlue3"].houses > 0;
        if (cell_name == "Purple" || cell_name == "Purple2" || cell_name == "Purple3") return info["Purple"].houses > 0 || info["Purple2"].houses > 0 || info["Purple3"].houses > 0;
        if (cell_name == "Orange" || cell_name == "Orange2" || cell_name == "Orange3") return info["Orange"].houses > 0 || info["Orange2"].houses > 0 || info["Orange3"].houses > 0;
        if (cell_name == "Red" || cell_name == "Red2" || cell_name == "Red3") return info["Red"].houses > 0 || info["Red2"].houses > 0 || info["Red3"].houses > 0;
        if (cell_name == "Yellow" || cell_name == "Yellow2" || cell_name == "Yellow3") return info["Yellow"].houses > 0 || info["Yellow2"].houses > 0 || info["Yellow3"].houses > 0;
        if (cell_name == "Green" || cell_name == "Green2" || cell_name == "Green3") return info["Green"].houses > 0 || info["Green2"].houses > 0 || info["Green3"].houses > 0;
        if (cell_name == "DarkBlue" || cell_name == "DarkBlue2") return info["DarkBlue"].houses > 0 || info["DarkBlue2"].houses > 0;
        return false;
    }

    bool ColorMortgaged(string cell_name)
    {
        if (cell_name == "Brown" || cell_name == "Brown2") return info["Brown"].mortgaged || info["Brown2"].mortgaged;
        if (cell_name == "LightBlue" || cell_name == "LightBlue2" || cell_name == "LightBlue3") return info["LightBlue"].mortgaged || info["LightBlue2"].mortgaged || info["LightBlue3"].mortgaged;
        if (cell_name == "Purple" || cell_name == "Purple2" || cell_name == "Purple3") return info["Purple"].mortgaged || info["Purple2"].mortgaged || info["Purple3"].mortgaged;
        if (cell_name == "Orange" || cell_name == "Orange2" || cell_name == "Orange3") return info["Orange"].mortgaged || info["Orange2"].mortgaged || info["Orange3"].mortgaged;
        if (cell_name == "Red" || cell_name == "Red2" || cell_name == "Red3") return info["Red"].mortgaged || info["Red2"].mortgaged || info["Red3"].mortgaged;
        if (cell_name == "Yellow" || cell_name == "Yellow2" || cell_name == "Yellow3") return info["Yellow"].mortgaged || info["Yellow2"].mortgaged || info["Yellow3"].mortgaged;
        if (cell_name == "Green" || cell_name == "Green2" || cell_name == "Green3") return info["Green"].mortgaged || info["Green2"].mortgaged || info["Green3"].mortgaged;
        if (cell_name == "DarkBlue" || cell_name == "DarkBlue2") return info["DarkBlue"].mortgaged || info["DarkBlue2"].mortgaged;
        return false;
    }

    bool sameColor(string cell_name) {
        if (cell_name == "Brown" || cell_name == "Brown2") return info["Brown"].owner == info["Brown2"].owner;
        if (cell_name == "LightBlue" || cell_name == "LightBlue2" || cell_name == "LightBlue3") return info["LightBlue"].owner == info["LightBlue2"].owner && info["LightBlue"].owner == info["LightBlue3"].owner;
        if (cell_name == "Purple" || cell_name == "Purple2" || cell_name == "Purple3") return info["Purple"].owner == info["Purple2"].owner && info["Purple"].owner == info["Purple3"].owner;
        if (cell_name == "Orange" || cell_name == "Orange2" || cell_name == "Orange3") return info["Orange"].owner == info["Orange2"].owner && info["Orange"].owner == info["Orange3"].owner;
        if (cell_name == "Red" || cell_name == "Red2" || cell_name == "Red3") return info["Red"].owner == info["Red2"].owner && info["Red"].owner == info["Red3"].owner;
        if (cell_name == "Yellow" || cell_name == "Yellow2" || cell_name == "Yellow3") return info["Yellow"].owner == info["Yellow2"].owner && info["Yellow"].owner == info["Yellow3"].owner;
        if (cell_name == "Green" || cell_name == "Green2" || cell_name == "Green3") return info["Green"].owner == info["Green2"].owner && info["Green"].owner == info["Green3"].owner;
        if (cell_name == "DarkBlue" || cell_name == "DarkBlue2") return info["DarkBlue"].owner == info["DarkBlue2"].owner;
        return false;
    }

    public bool StationMortgaged(int station) {
        if (station == 1) return rr_info["Station"].mortgaged;
        if (station == 1) return rr_info["Station2"].mortgaged;
        if (station == 1) return rr_info["Station3"].mortgaged;
        return rr_info["Station4"].mortgaged;
    }

    public void travelSelected() {
        travel_selected = true;
    }

    public bool CanTravel(string cell_name, int player) {
        if (rr_info[cell_name].owner == player && actual_cell != cell_name && !rr_info[cell_name].mortgaged) return true;
        return false;
    }

    void possibilityToTravel() {
        if (countStations(actual_player) >= 2) scripts.GetComponent<Movements>().ShowTravelButton();
    }

    int calculateRent(string cell_name) {
        if (info[cell_name].houses == 1) return info[cell_name].rent1;
        if (info[cell_name].houses == 2) return info[cell_name].rent2;
        if (info[cell_name].houses == 3) return info[cell_name].rent3;
        if (info[cell_name].houses == 4) return info[cell_name].rent4;
        if (info[cell_name].houses == 5) return info[cell_name].rentH;
        if (sameColor(cell_name)) return info[cell_name].rent * 2;
        return info[cell_name].rent;
    }
    public void LandedOn(string cell_name, int player, int dices) {
        actual_cell = cell_name;
        actual_player = player;
        if (cell_name == "Station" || cell_name == "Station2" || cell_name == "Station3" || cell_name == "Station4")
        {
            if (rr_info[cell_name].owner == -1)
            {
                just_buttons = true;
                buy_button.gameObject.SetActive(true);
                pass_button.gameObject.SetActive(true);
            }
            else if (rr_info[cell_name].owner == player) possibilityToTravel();
            else if (!rr_info[cell_name].mortgaged)
            {
                int k = countStations(rr_info[cell_name].owner);
                int p = 0;
                if (k == 1) p = 25 * multiplier;
                else if (k == 2) p = 100 * multiplier;
                else if (k == 3) p = 200 * multiplier;
                else if (k == 4) p = 400 * multiplier;
                scripts.GetComponent<Cash_management>().modify_cash(player, -p, false, true);
                scripts.GetComponent<Cash_management>().modify_cash(rr_info[cell_name].owner, p, false, true);
            }
            ShowRRCard(cell_name);
            multiplier = 1;
        }
        else if (cell_name == "Water")
        {
            ShowUtiliyCard(cell_name);
            if (water.owner == -1)
            {
                just_buttons = true;
                buy_button.gameObject.SetActive(true);
                pass_button.gameObject.SetActive(true);
            }
            else if (player != water.owner && !water.mortgaged)
            {
                if (water.owner == electrical.owner || multiplier != 1)
                {
                    scripts.GetComponent<Cash_management>().modify_cash(player, -10 * dices, false, true);
                    scripts.GetComponent<Cash_management>().modify_cash(water.owner, 10 * dices, false, true);
                }
                else
                {
                    scripts.GetComponent<Cash_management>().modify_cash(player, -4 * dices, false, true);
                    scripts.GetComponent<Cash_management>().modify_cash(water.owner, 4 * dices, false, true);
                }
            }
            multiplier = 1;
        }
        else if (cell_name == "Electric")
        {
            ShowUtiliyCard(cell_name);
            if (electrical.owner == -1)
            {
                just_buttons = true;
                buy_button.gameObject.SetActive(true);
                pass_button.gameObject.SetActive(true);
            }
            else if (player != electrical.owner && !electrical.mortgaged)
            {
                if (water.owner == electrical.owner || multiplier != 1)
                {
                    scripts.GetComponent<Cash_management>().modify_cash(player, -10 * dices, false, true);
                    scripts.GetComponent<Cash_management>().modify_cash(electrical.owner, 10 * dices, false, true);
                }
                else
                {
                    scripts.GetComponent<Cash_management>().modify_cash(player, -4 * dices, false, true);
                    scripts.GetComponent<Cash_management>().modify_cash(electrical.owner, 4 * dices, false, true);
                }
                multiplier = 1;
            }
        }
        else if (cell_name == "Chest" || cell_name == "Chest2" || cell_name == "Chest3") ChestCard(Random.Range(1, 17));
        else if (cell_name == "Chance" || cell_name == "Chance2" || cell_name == "Chance3") ChanceCard(Random.Range(1, 17));
        else if (cell_name == "Tax")
        {
            ShowTaxCard(cell_name);
            scripts.GetComponent<Cash_management>().modify_cash_per(player, -0.1f, true);
        }
        else if (cell_name == "Tax2")
        {
            ShowTaxCard(cell_name);
            scripts.GetComponent<Cash_management>().modify_cash(player, -100, true, true);
        }
        else if (cell_name == "Jail") Debug.Log("Jail Card in progress");
        else if (cell_name == "GoToJail") Debug.Log("GoToJail Card in progress");
        else if (cell_name == "Parking")
        {
            Debug.Log("Parking Card in progress");
            scripts.GetComponent<Cash_management>().refund_cash(player);
        }
        else if (cell_name == "Start") Debug.Log("Start Card in progress");
        else
        {
            ShowRentCard(cell_name);
            if (info[cell_name].owner == -1)
            {
                just_buttons = true;
                buy_button.gameObject.SetActive(true);
                pass_button.gameObject.SetActive(true);
            }
            else if (player != info[cell_name].owner && !info[cell_name].mortgaged)
            {
                int value = calculateRent(cell_name);
                scripts.GetComponent<Cash_management>().modify_cash(player, -value, false, true);
                scripts.GetComponent<Cash_management>().modify_cash(info[cell_name].owner, value, false, true);
            }
        }
    }
    public void ShowCard(string cell_name) {
        if (cell_name == "Station" || cell_name == "Station2" || cell_name == "Station3" || cell_name == "Station4") {
            if (travel_selected && CanTravel(cell_name, actual_player)) {
                scripts.GetComponent<Movements>().MoveTo(actual_cell, cell_name);
                travel_selected = false;
                scripts.GetComponent<Movements>().undoChanges();
            }
            else if (!mortgage_selected) ShowRRCard(cell_name);
            else if (rr_info[cell_name].owner == actual_player && !travel_selected) {
                RailRoad r = rr_info[cell_name];
                rr_info.Remove(cell_name);
                r.mortgaged = !r.mortgaged;
                rr_info.Add(cell_name, r);
                if (rr_info[cell_name].mortgaged)
                {
                    scripts.GetComponent<Cash_management>().modify_cash(actual_player, 100, false, true);
                    scripts.GetComponent<Movements>().mortgage(cell_name);
                }
                else {
                    scripts.GetComponent<Cash_management>().modify_cash(actual_player, -110, false, true);
                    scripts.GetComponent<Movements>().unmortgage(cell_name);
                }
                if ((actual_cell == "Station" || actual_cell == "Station2" || actual_cell == "Station3" || actual_cell == "Station4") && rr_info[actual_cell].owner == actual_player) possibilityToTravel();
            }
        } 
        else if (cell_name == "Water" || cell_name == "Electric") {
            if (!mortgage_selected) ShowUtiliyCard(cell_name);
            else {
                if (cell_name == "Water")
                {
                    water.mortgaged = !water.mortgaged;
                    if (water.mortgaged)
                    {
                        scripts.GetComponent<Cash_management>().modify_cash(actual_player, 75, false, true);
                        scripts.GetComponent<Movements>().mortgage(cell_name);
                    }
                    else
                    {
                        scripts.GetComponent<Cash_management>().modify_cash(actual_player, -83, false, true);
                        scripts.GetComponent<Movements>().unmortgage(cell_name);
                    }
                }
                else
                {
                    electrical.mortgaged = !water.mortgaged;
                    if (electrical.mortgaged)
                    {
                        scripts.GetComponent<Cash_management>().modify_cash(actual_player, 75, false, true);
                        scripts.GetComponent<Movements>().mortgage(cell_name);
                    }
                    else
                    {
                        scripts.GetComponent<Cash_management>().modify_cash(actual_player, -83, false, true);
                        scripts.GetComponent<Movements>().unmortgage(cell_name);
                    }
                }
            }
        }
        else if (cell_name == "Chest" || cell_name == "Chest2" || cell_name == "Chest3") Debug.Log("Chest Card in progress");
        else if (cell_name == "Chance" || cell_name == "Chance2" || cell_name == "Chance3") Debug.Log("Chance Card in progress");
        else if (cell_name == "Tax" || cell_name == "Tax2") ShowTaxCard(cell_name);
        else if (cell_name == "Jail") Debug.Log("Jail Card in progress");
        else if (cell_name == "GoToJail") Debug.Log("GoToJail Card in progress");
        else if (cell_name == "Parking") Debug.Log("Parking Card in progress");
        else if (cell_name == "Start") Debug.Log("Start Card in progress");
        else {
            int player_torn = scripts.GetComponent<Movements>().getPlayerTorn();
            if (buy_selected && player_torn == info[cell_name].owner && sameColor(cell_name) && info[cell_name].houses < 5 && !ColorMortgaged(cell_name)) {
                //Buy house
                Cell c = info[cell_name];
                if (!scripts.GetComponent<Cash_management>().modify_cash(player_torn, -info[cell_name].house_cost, false, false)) return;
                info.Remove(cell_name);
                ++c.houses;
                add_house(cell_name, c.houses);
                info.Add(cell_name, c);
            }
            else if (sell_selected && player_torn == info[cell_name].owner && info[cell_name].houses > 0 && !info[cell_name].mortgaged) {
                //Sell house
                Cell c = info[cell_name];
                info.Remove(cell_name);
                remove_house(cell_name, c.houses);
                --c.houses;
                info.Add(cell_name, c);
                scripts.GetComponent<Cash_management>().modify_cash(player_torn, info[cell_name].house_cost / 2, false, true);
            }
            else if (mortgage_selected && player_torn == info[cell_name].owner && !ColorHasHousing(cell_name)) {
                //Mortgage-Unmortgage
                Cell c = info[cell_name];
                info.Remove(cell_name);
                c.mortgaged = !c.mortgaged;
                info.Add(cell_name, c);
                if (c.mortgaged)
                {
                    scripts.GetComponent<Cash_management>().modify_cash(player_torn, info[cell_name].cost / 2, false, true);
                    scripts.GetComponent<Movements>().mortgage(cell_name);
                }
                else {
                    if (!scripts.GetComponent<Cash_management>().modify_cash(player_torn, (int)(-1.1f * (info[cell_name].cost / 2)), false, false)) {
                        info.Remove(cell_name);
                        c.mortgaged = !c.mortgaged;
                        info.Add(cell_name, c);
                        return;
                    }
                    scripts.GetComponent<Movements>().unmortgage(cell_name);
                }
            }
            else ShowRentCard(cell_name);
        }
        
    }

    public void ShowPlayerInfo(int player, int panel, bool disable_on_click) {
        int outofjail = scripts.GetComponent<Movements>().GetOutOfJail(player);
        GameObject tradePanel;
        if (panel == 1) {
            tradePanel = tradePanel1;
            if (disable_on_click) tradePanel1_on = true;
            for (int k = 0; k < CellSelected1.Length; ++k) CellSelected1[k] = false;
            cash1 = 0;
        } 
        else {
            tradePanel = tradePanel2;
            if (disable_on_click) tradePanel2_on = true;
            for (int k = 0; k < CellSelected2.Length; ++k) CellSelected2[k] = false;
            cash2 = 0;
        }

        if (player == 0) tradePanel.transform.Find("Panel1").gameObject.transform.Find("Circle").GetComponent<Image>().color = new Color32(255, 78, 78, 255);
        else if (player == 1) tradePanel.transform.Find("Panel1").gameObject.transform.Find("Circle").GetComponent<Image>().color = new Color32(0, 137, 255, 255);
        else if (player == 2) tradePanel.transform.Find("Panel1").gameObject.transform.Find("Circle").GetComponent<Image>().color = new Color32(234, 241, 0, 255);
        else tradePanel.transform.Find("Panel1").gameObject.transform.Find("Circle").GetComponent<Image>().color = new Color32(62, 233, 54, 255);

        tradePanel.transform.Find("Cash").GetComponent<InputField>().text = "";

        int i = 0;
        foreach (var c in info) if (c.Value.owner == player) ShowMiniCard(c.Key, i++, tradePanel);
        foreach (var r in rr_info) if (r.Value.owner == player) ShowMiniRail(r.Key, i++, tradePanel);
        if (electrical.owner == player) ShowMiniUtility("Electric", i++, tradePanel);
        if (water.owner == player) ShowMiniUtility("Water", i++, tradePanel);
        for (int j = i; j < 30; ++j) DisableMiniCard(j, tradePanel);

        tradePanel.SetActive(true);
    }

    void DisableMiniCard(int n, GameObject tradePanel) {
        tradePanel.transform.Find("targeta" + n).gameObject.SetActive(false);
    }

    void ShowMiniCard(string cell_name, int n, GameObject tradePanel)
    {
        if (tradePanel == tradePanel1) TradingCells1[n] = cell_name;
        else TradingCells2[n] = cell_name;

        tradePanel.transform.Find("targeta" + n).gameObject.SetActive(true);
        GameObject minicard = tradePanel.transform.Find("targeta" + n).gameObject;
        GameObject franja = minicard.transform.Find("franja").gameObject;
        minicard.transform.Find("inicial").gameObject.SetActive(true);
        minicard.transform.Find("franja").gameObject.SetActive(true);
        minicard.transform.Find("cash").gameObject.SetActive(true);
        if (cell_name == "Brown" || cell_name == "Brown2") franja.GetComponentInChildren<Image>().color = new Color32(166, 84, 31, 255);
        else if (cell_name == "LightBlue" || cell_name == "LightBlue2" || cell_name == "LightBlue3") franja.GetComponentInChildren<Image>().color = new Color32(0, 203, 255, 255);
        else if (cell_name == "Purple" || cell_name == "Purple2" || cell_name == "Purple3") franja.GetComponentInChildren<Image>().color = new Color32(255, 0, 117, 255);
        else if (cell_name == "Orange" || cell_name == "Orange2" || cell_name == "Orange3") franja.GetComponentInChildren<Image>().color = new Color32(255, 78, 0, 255);
        else if (cell_name == "Red" || cell_name == "Red2" || cell_name == "Red3") franja.GetComponentInChildren<Image>().color = new Color32(255, 15, 0, 255);
        else if (cell_name == "Yellow" || cell_name == "Yellow2" || cell_name == "Yellow3") franja.GetComponentInChildren<Image>().color = new Color32(255, 236, 0, 255);
        else if (cell_name == "Green" || cell_name == "Green2" || cell_name == "Green3") franja.GetComponentInChildren<Image>().color = new Color32(0, 142, 7, 255);
        else if (cell_name == "DarkBlue" || cell_name == "DarkBlue2") franja.GetComponentInChildren<Image>().color = new Color32(57, 70, 255, 255);
        else return;

        Cell c = info[cell_name];
        minicard.transform.Find("inicial").gameObject.GetComponentInChildren<Text>().text = c.name[0].ToString();
        minicard.transform.Find("cash").gameObject.GetComponentInChildren<Text>().text = c.cost.ToString();

        minicard.transform.Find("Train").gameObject.SetActive(false);
        minicard.transform.Find("WaterTap").gameObject.SetActive(false);
        minicard.transform.Find("inicial2").gameObject.SetActive(false);
        minicard.transform.Find("cash2").gameObject.SetActive(false);
        minicard.transform.Find("LightBulb").gameObject.SetActive(false);

        if (c.houses == 0 || c.houses == 5) {
            minicard.transform.Find("casa1").gameObject.SetActive(false);
            minicard.transform.Find("casa2").gameObject.SetActive(false);
            minicard.transform.Find("casa3").gameObject.SetActive(false);
            minicard.transform.Find("casa4").gameObject.SetActive(false);
            if (c.houses == 5) {
                minicard.transform.Find("inicial").gameObject.GetComponentInChildren<Text>().text = "H";
            }
        }
        else if (c.houses == 1)
        {
            minicard.transform.Find("casa1").gameObject.SetActive(true);
            minicard.transform.Find("casa2").gameObject.SetActive(false);
            minicard.transform.Find("casa3").gameObject.SetActive(false);
            minicard.transform.Find("casa4").gameObject.SetActive(false);
        }
        else if (c.houses == 2)
        {
            minicard.transform.Find("casa1").gameObject.SetActive(true);
            minicard.transform.Find("casa2").gameObject.SetActive(true);
            minicard.transform.Find("casa3").gameObject.SetActive(false);
            minicard.transform.Find("casa4").gameObject.SetActive(false);
        }
        else if (c.houses == 3)
        {
            minicard.transform.Find("casa1").gameObject.SetActive(true);
            minicard.transform.Find("casa2").gameObject.SetActive(true);
            minicard.transform.Find("casa3").gameObject.SetActive(true);
            minicard.transform.Find("casa4").gameObject.SetActive(false);
        }
        else if (c.houses == 4)
        {
            minicard.transform.Find("casa1").gameObject.SetActive(true);
            minicard.transform.Find("casa2").gameObject.SetActive(true);
            minicard.transform.Find("casa3").gameObject.SetActive(true);
            minicard.transform.Find("casa4").gameObject.SetActive(true);
        }

        if (c.mortgaged) minicard.GetComponentInChildren<Image>().color = new Color32(120, 120, 120, 255);
        else minicard.GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);
    }

    void ShowMiniRail(string cell_name, int n, GameObject tradePanel)
    {
        if (tradePanel == tradePanel1) TradingCells1[n] = cell_name;
        else TradingCells2[n] = cell_name;

        tradePanel.transform.Find("targeta" + n).gameObject.SetActive(true);
        GameObject minicard = tradePanel.transform.Find("targeta" + n).gameObject;
        minicard.transform.Find("inicial").gameObject.SetActive(false);
        minicard.transform.Find("franja").gameObject.SetActive(false);
        minicard.transform.Find("cash").gameObject.SetActive(false);
        minicard.transform.Find("casa1").gameObject.SetActive(false);
        minicard.transform.Find("casa2").gameObject.SetActive(false);
        minicard.transform.Find("casa3").gameObject.SetActive(false);
        minicard.transform.Find("casa4").gameObject.SetActive(false);

        minicard.transform.Find("Train").gameObject.SetActive(true);
        minicard.transform.Find("WaterTap").gameObject.SetActive(false);
        minicard.transform.Find("inicial2").gameObject.SetActive(true);
        minicard.transform.Find("cash2").gameObject.SetActive(true);
        minicard.transform.Find("LightBulb").gameObject.SetActive(false);

        minicard.transform.Find("cash2").gameObject.GetComponentInChildren<Text>().text = "200";
        minicard.transform.Find("inicial2").gameObject.GetComponentInChildren<Text>().text = rr_info[cell_name].name[0].ToString();

        if (rr_info[cell_name].mortgaged) minicard.GetComponentInChildren<Image>().color = new Color32(120, 120, 120, 255);
        else minicard.GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);
    }

    void ShowMiniUtility(string cell_name, int n, GameObject tradePanel)
    {
        if (tradePanel == tradePanel1) TradingCells1[n] = cell_name;
        else TradingCells2[n] = cell_name;

        tradePanel.transform.Find("targeta" + n).gameObject.SetActive(true);
        GameObject minicard = tradePanel.transform.Find("targeta" + n).gameObject;
        minicard.transform.Find("inicial").gameObject.SetActive(false);
        minicard.transform.Find("franja").gameObject.SetActive(false);
        minicard.transform.Find("cash").gameObject.SetActive(false);
        minicard.transform.Find("casa1").gameObject.SetActive(false);
        minicard.transform.Find("casa2").gameObject.SetActive(false);
        minicard.transform.Find("casa3").gameObject.SetActive(false);
        minicard.transform.Find("casa4").gameObject.SetActive(false);

        minicard.transform.Find("Train").gameObject.SetActive(false);
        minicard.transform.Find("inicial2").gameObject.SetActive(false);
        minicard.transform.Find("cash2").gameObject.SetActive(true);
       

        minicard.transform.Find("cash2").gameObject.GetComponentInChildren<Text>().text = "150";

        if (cell_name == "Electric")
        {
            minicard.transform.Find("WaterTap").gameObject.SetActive(false);
            minicard.transform.Find("LightBulb").gameObject.SetActive(true);

            if (electrical.mortgaged) minicard.GetComponentInChildren<Image>().color = new Color32(120, 120, 120, 255);
            else minicard.GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);
        }
        else {
            minicard.transform.Find("WaterTap").gameObject.SetActive(true);
            minicard.transform.Find("LightBulb").gameObject.SetActive(false);

            if (water.mortgaged) minicard.GetComponentInChildren<Image>().color = new Color32(120, 120, 120, 255);
            else minicard.GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    private int CellToMinicard(string cell_name, int panel) {
        if (panel == 1) for (int i = 0; i < TradingCells1.Length; ++i) if (TradingCells1[i] == cell_name) return i;
        if (panel == 2) for (int i = 0; i < TradingCells2.Length; ++i) if (TradingCells2[i] == cell_name) return i;
        return -1;
    }

    private void ToggleColor(int panel, int cell, string cell_name) {
        if (cell_name == "Brown" || cell_name == "Brown2") {
            if (info["Brown"].houses > 0 || info["Brown2"].houses > 0)
            {
                ToggleSelected(panel, CellToMinicard("Brown", panel));
                ToggleSelected(panel, CellToMinicard("Brown2", panel));
            }
            else ToggleSelected(panel, cell);
            return;
        }

        if (cell_name == "LightBlue" || cell_name == "LightBlue2" || cell_name == "LightBlue3") {
            if (info["LightBlue"].houses > 0 || info["LightBlue2"].houses > 0 || info["LightBlue3"].houses > 0) {
                ToggleSelected(panel, CellToMinicard("LightBlue", panel));
                ToggleSelected(panel, CellToMinicard("LightBlue2", panel));
                ToggleSelected(panel, CellToMinicard("LightBlue3", panel));
            }
            else ToggleSelected(panel, cell);
            return;
        }

        if (cell_name == "Purple" || cell_name == "Purple2" || cell_name == "Purple3")
        {
            if (info["Purple"].houses > 0 || info["Purple2"].houses > 0 || info["Purple3"].houses > 0)
            {
                ToggleSelected(panel, CellToMinicard("Purple", panel));
                ToggleSelected(panel, CellToMinicard("Purple2", panel));
                ToggleSelected(panel, CellToMinicard("Purple3", panel));
            }
            else ToggleSelected(panel, cell);
            return;
        }

        if (cell_name == "Orange" || cell_name == "Orange2" || cell_name == "Orange3")
        {
            if (info["Orange"].houses > 0 || info["Orange2"].houses > 0 || info["Orange3"].houses > 0)
            {
                ToggleSelected(panel, CellToMinicard("Orange", panel));
                ToggleSelected(panel, CellToMinicard("Orange2", panel));
                ToggleSelected(panel, CellToMinicard("Orange3", panel));
            }
            else ToggleSelected(panel, cell);
            return;
        }

        if (cell_name == "Red" || cell_name == "Red2" || cell_name == "Red3")
        {
            if (info["Red"].houses > 0 || info["Red2"].houses > 0 || info["Red3"].houses > 0)
            {
                ToggleSelected(panel, CellToMinicard("Red", panel));
                ToggleSelected(panel, CellToMinicard("Red2", panel));
                ToggleSelected(panel, CellToMinicard("Red3", panel));
            }
            else ToggleSelected(panel, cell);
            return;
        }

        if (cell_name == "Yellow" || cell_name == "Yellow2" || cell_name == "Yellow3")
        {
            if (info["Yellow"].houses > 0 || info["Yellow2"].houses > 0 || info["Yellow3"].houses > 0)
            {
                ToggleSelected(panel, CellToMinicard("Yellow", panel));
                ToggleSelected(panel, CellToMinicard("Yellow2", panel));
                ToggleSelected(panel, CellToMinicard("Yellow3", panel));
            }
            else ToggleSelected(panel, cell);
            return;
        }

        if (cell_name == "Green" || cell_name == "Green2" || cell_name == "Green3")
        {
            if (info["Green"].houses > 0 || info["Green2"].houses > 0 || info["Green3"].houses > 0)
            {
                ToggleSelected(panel, CellToMinicard("Green", panel));
                ToggleSelected(panel, CellToMinicard("Green2", panel));
                ToggleSelected(panel, CellToMinicard("Green3", panel));
            }
            else ToggleSelected(panel, cell);
            return;
        }

        if (cell_name == "DarkBlue" || cell_name == "DarkBlue2")
        {
            if (info["DarkBlue"].houses > 0 || info["DarkBlue2"].houses > 0)
            {
                ToggleSelected(panel, CellToMinicard("DarkBlue", panel));
                ToggleSelected(panel, CellToMinicard("DarkBlue2", panel));

            }
            else ToggleSelected(panel, cell);
            return;
        }
    }

    public void Toggle(int panel, int cell) {
        if (freeze_trading) return;

        string cell_name;
        if (panel == 1) cell_name = TradingCells1[cell];
        else cell_name = TradingCells2[cell];

        if (cell_name == "Station" || cell_name == "Station2" || cell_name == "Station3" || cell_name == "Station4" || cell_name == "Electric" || cell_name == "Water")
        {
            ToggleSelected(panel, cell);
            return;
        }
        ToggleColor(panel, cell, cell_name);
    }

    private bool Cell_Mortgaged(string cell_name) {
        if (cell_name == "Electric") return electrical.mortgaged;
        if (cell_name == "Water") return water.mortgaged;
        if (cell_name == "Station" || cell_name == "Station2" || cell_name == "Station3" || cell_name == "Station4") return rr_info[cell_name].mortgaged;
        return info[cell_name].mortgaged; ;
    }

    private void ToggleSelected(int panel, int cell) {
        if (panel == 1)
        {
            CellSelected1[cell] = !CellSelected1[cell];
            if (CellSelected1[cell]) tradePanel1.transform.Find("targeta" + cell).gameObject.GetComponent<Image>().color =  new Color32(255, 182, 65, 255);
            else if (Cell_Mortgaged(TradingCells1[cell])) tradePanel1.transform.Find("targeta" + cell).gameObject.GetComponentInChildren<Image>().color = new Color32(120, 120, 120, 255);
            else tradePanel1.transform.Find("targeta" + cell).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else if (panel == 2) {
            CellSelected2[cell] = !CellSelected2[cell];
            if (CellSelected2[cell]) tradePanel2.transform.Find("targeta" + cell).gameObject.GetComponent<Image>().color = new Color32(255, 182, 65, 255);
            else if (Cell_Mortgaged(TradingCells2[cell])) tradePanel2.transform.Find("targeta" + cell).gameObject.GetComponentInChildren<Image>().color = new Color32(120, 120, 120, 255);
            else tradePanel2.transform.Find("targeta" + cell).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void UpdateTradingCash(int panel, int amount) {
        if (panel == 1) cash1 = amount;
        else cash2 = amount;
    }

    public void SetPlayer(int player) {
        actual_player = player;
    }

    public int GetTradingPlayer(int panel) {
        if (panel == 2) return trading_partner;
        return actual_player;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!just_buttons && !disableCard() && !trading_selected) {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    string cell_name = hit.collider.gameObject.name;
                    if (cell_name != "Background") ShowCard(cell_name);
                }
            }
            buy_selected = false;
            sell_selected = false;
            mortgage_selected = false;
            if (tradePanel1_on) {
                tradePanel1.gameObject.SetActive(false);
                tradePanel1_on = false;
                trading_selected = false;
            }

            if (tradePanel2_on) {
                tradePanel2.gameObject.SetActive(false);
                tradePanel2_on = false;
                trading_selected = false;
            }
        }
    }
}

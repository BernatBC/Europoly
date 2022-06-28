using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Cell_info : MonoBehaviour
{
    public Button buy_button;
    public Button pass_button;
    public Button buy_house_button;
    public Button sell_house_button;

    public GameObject owner_mark1;
    public GameObject owner_mark2;

    public GameObject house;
    public GameObject hotel;

    [Header("Street Card")]
    public GameObject CardUI;
    public Text card_title;
    public Text card_cost;
    public Text card_rent;
    public Text card_rent1;
    public Text card_rent2;
    public Text card_rent3;
    public Text card_rent4;
    public Text card_rentH;
    public Text card_costHs;
    public Text card_costHt;
    public Text card_mortgage;
    public Image top;

    [Header("Railroad Card")]
    public GameObject rrcard;
    public Text rr_title;

    [Header("Utilities")]
    public GameObject electric_card;
    public GameObject water_card;

    [Header("Taxes")]
    public GameObject income_tax_card;
    public GameObject luxury_tax_card;

    RaycastHit hit;
    Ray ray;
    bool card_shown = false;
    bool rr_card_shown = false;
    bool electric_card_shown = false;
    bool water_card_shown = false;
    bool income_card_shown = false;
    bool luxury_card_shown = false;
    bool just_buttons = false;
    bool buy_selected = false;
    bool sell_selected = false;

    int actual_player = -1;
    string actual_cell = "";

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
        c.name = "Short Line";
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
    }

    void buy_house() {
        buy_selected = true;
        sell_selected = false;
    }

    void sell_house() {
        sell_selected = true;
        buy_selected = false;
    }

    void pass_clicked() {
        disableCard();
        just_buttons = false;
        buy_button.gameObject.SetActive(false);
        pass_button.gameObject.SetActive(false);
    }
    void ShowRentCard(string cell_name) {
        if (cell_name == "Brown" || cell_name == "Brown2") top.color = new Color32(166, 84, 31, 255);
        else if (cell_name == "LightBlue" || cell_name == "LightBlue2" || cell_name == "LightBlue3") top.color = new Color32(0, 203, 255, 255);
        else if (cell_name == "Purple" || cell_name == "Purple2" || cell_name == "Purple3") top.color = new Color32(255, 0, 117, 255);
        else if (cell_name == "Orange" || cell_name == "Orange2" || cell_name == "Orange3") top.color = new Color32(255, 78, 0, 255);
        else if (cell_name == "Red" || cell_name == "Red2" || cell_name == "Red3") top.color = new Color32(255, 15, 0, 255);
        else if (cell_name == "Yellow" || cell_name == "Yellow2" || cell_name == "Yellow3") top.color = new Color32(255, 236, 0, 255);
        else if (cell_name == "Green" || cell_name == "Green2" || cell_name == "Green3") top.color = new Color32(0, 142, 7, 255);
        else if (cell_name == "DarkBlue" || cell_name == "DarkBlue2") top.color = new Color32(57, 70, 255, 255);
        else return;
        card_title.text = info[cell_name].name;
        card_cost.text = info[cell_name].cost + "";
        card_rent.text = info[cell_name].rent + "";
        card_rent1.text = info[cell_name].rent1 + "";
        card_rent2.text = info[cell_name].rent2 + "";
        card_rent3.text = info[cell_name].rent3 + "";
        card_rent4.text = info[cell_name].rent4 + "";
        card_rentH.text = info[cell_name].rentH + "";
        card_costHs.text = info[cell_name].house_cost + "";
        card_costHt.text = info[cell_name].house_cost + "";
        card_mortgage.text = info[cell_name].cost / 2 + "";
        CardUI.gameObject.SetActive(true);
        card_shown = true;
    }

    private void ShowRRCard(string cell_name) {
        rr_title.text = rr_info[cell_name].name;
        rrcard.gameObject.SetActive(true);
        rr_card_shown = true;
    }

    private void ShowUtiliyCard(string cell_name) {
        if (cell_name == "Electric")
        {
            electric_card.gameObject.SetActive(true);
            electric_card_shown = true;
        }
        else {
            water_card.gameObject.SetActive(true);
            water_card_shown = true;
        }
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

    void add_owner_marker() {
        Vector3 pos = scripts.GetComponent<Movements>().cell_pos(actual_cell);
        if (actual_cell == "Brown" || actual_cell == "Brown2" || actual_cell == "LightBlue" || actual_cell == "LightBlue2" || actual_cell == "LightBlue3" || actual_cell == "Station") pos.z += 1.5f;
        else if (actual_cell == "Purple" || actual_cell == "Purple2" || actual_cell == "Purple3" || actual_cell == "Electric" || actual_cell == "Orange" || actual_cell == "Station2" || actual_cell == "Orange2" || actual_cell == "Orange3") pos.x += 1.5f;
        else if (actual_cell == "Red" || actual_cell == "Red2" || actual_cell == "Red3" || actual_cell == "Station3" || actual_cell == "Yellow" || actual_cell == "Yellow2" || actual_cell == "Yellow3" || actual_cell == "Water") pos.z -= 1.5f;
        else if (actual_cell == "Green" || actual_cell == "Green2" || actual_cell == "Green3" || actual_cell == "DarkBlue" || actual_cell == "DarkBlue2" || actual_cell == "Station4") pos.x -= 1.5f;
        GameObject a;
        if (actual_player == 0) a = Instantiate(owner_mark1, new Vector3(pos.x, 0.55f, pos.z), owner_mark1.gameObject.transform.rotation);
        else a = Instantiate(owner_mark2, new Vector3(pos.x, 0.55f, pos.z), owner_mark2.gameObject.transform.rotation);
        owner_prefabs.Add(actual_cell, a);
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
                houses_prefabs[cell_name][4].gameObject.name = "_" + cell_name + "H";
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
                houses_prefabs[cell_name][4].gameObject.name = "_" + cell_name + "H";
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
                houses_prefabs[cell_name][4].gameObject.name = "_" + cell_name + "H";
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
                houses_prefabs[cell_name][4].gameObject.name = "_" + cell_name + "H";
                return;
            }
        }
        Debug.Log(cell_name);
        houses_prefabs[cell_name][house_pos - 1] = Instantiate(house, new Vector3(pos.x, 0.7f, pos.z), Quaternion.identity);
        houses_prefabs[cell_name][house_pos - 1].gameObject.name = "_" + cell_name + house_pos.ToString();
    }

    void remove_house (string cell_name, int house_pos) {
        if (house_pos < 5) {
            GameObject h = GameObject.Find(cell_name + house_pos.ToString());
            Destroy(h);
            return;
        }
        GameObject h2 = GameObject.Find("_" + cell_name + "H");
        Destroy(h2);
        for (int i = 0; i < 4; ++i) add_house("_" + cell_name, i + 1);
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
        return false;
    }

    void buy_property() {
        disableCard();
        just_buttons = false;
        buy_button.gameObject.SetActive(false);
        pass_button.gameObject.SetActive(false);
        if (actual_cell == "Station" || actual_cell == "Station2" || actual_cell == "Station3" || actual_cell == "Station4")
        {
            RailRoad r = rr_info[actual_cell];
            rr_info.Remove(actual_cell);
            r.owner = actual_player;
            scripts.GetComponent<Cash_management>().modify_cash(actual_player, -200, false);
            rr_info.Add(actual_cell, r);
        }
        else if (actual_cell == "Electric")
        {
            electrical.owner = actual_player;
            scripts.GetComponent<Cash_management>().modify_cash(actual_player, -150, false);
        }
        else if (actual_cell == "Water")
        {
            water.owner = actual_player;
            scripts.GetComponent<Cash_management>().modify_cash(actual_player, -150, false);
        }
        else {
            Cell c = info[actual_cell];
            info.Remove(actual_cell);
            c.owner = actual_player;
            scripts.GetComponent<Cash_management>().modify_cash(actual_player, -c.cost, false);
            info.Add(actual_cell, c);
        }
        add_owner_marker();
    }

    int countStations(int owner) {
        int k = 0;
        if (rr_info["Station"].owner == owner) ++k;
        if (rr_info["Station2"].owner == owner) ++k;
        if (rr_info["Station3"].owner == owner) ++k;
        if (rr_info["Station4"].owner == owner) ++k;
        return k;
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
            ShowRRCard(cell_name);
            if (rr_info[cell_name].owner == -1) {
                just_buttons = true;
                buy_button.gameObject.SetActive(true);
                pass_button.gameObject.SetActive(true);
            } 
            else if (player != rr_info[cell_name].owner) {
                int k = countStations(rr_info[cell_name].owner);
                int p = 0;
                if (k == 1) p = 25;
                if (k == 2) p = 100;
                if (k == 3) p = 200;
                if (k == 4) p = 400;
                scripts.GetComponent<Cash_management>().modify_cash(player, -p, false);
                scripts.GetComponent<Cash_management>().modify_cash(rr_info[cell_name].owner, p, false);
            }
        }
        else if (cell_name == "Water") {
            ShowUtiliyCard(cell_name);
            if (water.owner == -1)
            {
                just_buttons = true;
                buy_button.gameObject.SetActive(true);
                pass_button.gameObject.SetActive(true);
            }
            else if (player != water.owner)
            {
                if (water.owner == electrical.owner)
                {
                    scripts.GetComponent<Cash_management>().modify_cash(player, -10*dices, false);
                    scripts.GetComponent<Cash_management>().modify_cash(water.owner, 10 * dices, false);
                }
                else {
                    scripts.GetComponent<Cash_management>().modify_cash(player, -4 * dices, false);
                    scripts.GetComponent<Cash_management>().modify_cash(water.owner, 4 * dices, false);
                }
            }
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
            else if(player != electrical.owner)
            {
                if (water.owner == electrical.owner)
                {
                    scripts.GetComponent<Cash_management>().modify_cash(player, -10 * dices, false);
                    scripts.GetComponent<Cash_management>().modify_cash(electrical.owner, 10 * dices, false);
                }
                else
                {
                    scripts.GetComponent<Cash_management>().modify_cash(player, -4 * dices, false);
                    scripts.GetComponent<Cash_management>().modify_cash(electrical.owner, 4 * dices, false);
                }
            }
        }
        else if (cell_name == "Chest" || cell_name == "Chest2" || cell_name == "Chest3") Debug.Log("Chest Card in progress");
        else if (cell_name == "Chance" || cell_name == "Chance2" || cell_name == "Chance3") Debug.Log("Chance Card in progress");
        else if (cell_name == "Tax")
        {
            ShowTaxCard(cell_name);
            scripts.GetComponent<Cash_management>().modify_cash_per(player, -0.1f, true);
        }
        else if (cell_name == "Tax2")
        {
            ShowTaxCard(cell_name);
            scripts.GetComponent<Cash_management>().modify_cash(player, -100, true);
        }
        else if (cell_name == "Jail") Debug.Log("Jail Card in progress");
        else if (cell_name == "GoToJail") Debug.Log("GoToJail Card in progress");
        else if (cell_name == "Parking")
        {
            Debug.Log("Parking Card in progress");
            scripts.GetComponent<Cash_management>().refund_cash(player);
        }
        else if (cell_name == "Start") Debug.Log("Start Card in progress");
        else {
            ShowRentCard(cell_name);
            if (info[cell_name].owner == -1)
            {
                just_buttons = true;
                buy_button.gameObject.SetActive(true);
                pass_button.gameObject.SetActive(true);
            }
            else if (player != info[cell_name].owner)
            {
                int value = calculateRent(cell_name);
                scripts.GetComponent<Cash_management>().modify_cash(player, -value, false);
                scripts.GetComponent<Cash_management>().modify_cash(info[cell_name].owner, value, false);
            }
        }
    }
    public void ShowCard(string cell_name) {
        if (cell_name == "Station" || cell_name == "Station2" || cell_name == "Station3" || cell_name == "Station4") ShowRRCard(cell_name);
        else if (cell_name == "Water" || cell_name == "Electric") ShowUtiliyCard(cell_name);
        else if (cell_name == "Chest" || cell_name == "Chest2" || cell_name == "Chest3") Debug.Log("Chest Card in progress");
        else if (cell_name == "Chance" || cell_name == "Chance2" || cell_name == "Chance3") Debug.Log("Chance Card in progress");
        else if (cell_name == "Tax" || cell_name == "Tax2") ShowTaxCard(cell_name);
        else if (cell_name == "Jail") Debug.Log("Jail Card in progress");
        else if (cell_name == "GoToJail") Debug.Log("GoToJail Card in progress");
        else if (cell_name == "Parking") Debug.Log("Parking Card in progress");
        else if (cell_name == "Start") Debug.Log("Start Card in progress");
        else {
            int player_torn = scripts.GetComponent<Movements>().getPlayerTorn();
            if (buy_selected && player_torn == info[cell_name].owner && sameColor(cell_name) && info[cell_name].houses < 5) {
                //Buy house
                Cell c = info[cell_name];
                info.Remove(cell_name);
                ++c.houses;
                add_house(cell_name, c.houses);
                info.Add(cell_name, c);
                scripts.GetComponent<Cash_management>().modify_cash(player_torn, -info[cell_name].house_cost, false);
            }
            else if (sell_selected && player_torn == info[cell_name].owner && info[cell_name].houses > 0) {
                //Sell house
                Cell c = info[cell_name];
                info.Remove(cell_name);
                remove_house(cell_name, c.houses);
                --c.houses;
                info.Add(cell_name, c);
                scripts.GetComponent<Cash_management>().modify_cash(player_torn, info[cell_name].house_cost/2, false);
            }
            else ShowRentCard(cell_name);
        }
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!just_buttons && !disableCard()) {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    string cell_name = hit.collider.gameObject.name;
                    if (cell_name != "Background") ShowCard(cell_name);
                }
            }
            buy_selected = false;
            sell_selected = false;
        }
    }
}

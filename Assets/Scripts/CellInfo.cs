using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// Class <c>CellInfo</c> contains methods related with buying, selling, cells, hotels, houses, etc.
/// </summary>
public class CellInfo : MonoBehaviour
{
    /// <summary>
    /// Button that allows you to buy a property, railroad or utility.
    /// </summary>
    [Header("Buttons")]
    public Button buyButton;

    /// <summary>
    /// Button that allows you to not buying a property, railroad or utility.
    /// </summary>
    public Button passButton;

    /// <summary>
    /// Button that allows you to buy houses and hotels to current properties.
    /// </summary>
    public Button buyHouseButton;

    /// <summary>
    /// Button that allows you to sell houses and hotels.
    /// </summary>
    public Button sellHouseButton;

    /// <summary>
    /// Button that allows you to mortage and unmortgage properties.
    /// </summary>
    public Button mortgage;

    /// <summary>
    /// Button that allows you to start a trading operation.
    /// </summary>
    public Button trade;

    /// <summary>
    /// Button that allows you to cancel a trading operation.
    /// </summary>
    public Button cancel;

    /// <summary>
    /// Button that allows you to make a trading offer.
    /// </summary>
    public Button offer;

    /// <summary>
    /// Button that allows you to accept a trading offer.
    /// </summary>
    public Button accept;

    /// <summary>
    /// Button that allows you to reject a trading offer.
    /// </summary>
    public Button reject;

    /// <summary>
    /// Propperty card gameObject.
    /// </summary>
    [Header("Cards")]
    public GameObject propertyCard;

    /// <summary>
    /// RailRoad card gameObject.
    /// </summary>
    public GameObject railroadCard;

    /// <summary>
    /// Chest card gameObject.
    /// </summary>
    public GameObject chestCard;

    /// <summary>
    /// Chance card gameObject.
    /// </summary>
    public GameObject chanceCard;

    /// <summary>
    /// Electric utility card gameObject.
    /// </summary>
    public GameObject electricCard;

    /// <summary>
    /// Water utility card gameObject.
    /// </summary>
    public GameObject waterCard;

    /// <summary>
    /// Income tax card gameObject.
    /// </summary>
    public GameObject incomeTaxCard;

    /// <summary>
    /// Luxury card gameObject.
    /// </summary>
    public GameObject luxuryTaxCard;

    /// <summary>
    /// Button to select to trade with the first player (own player not considered).
    /// </summary>
    [Header("Select trading player buttons")]
    public GameObject tradingButton1;

    /// <summary>
    /// Button to select to trade with the second player (own player not considered).
    /// </summary>
    public GameObject tradingButton2;

    /// <summary>
    /// Button to select to trade with the third player (own player not considered).
    /// </summary>
    public GameObject tradingButton3;

    /// <summary>
    /// Owner mark prefab from each player.
    /// </summary>
    [Header("Other")]
    public GameObject[] ownerMark;

    /// <summary>
    /// Prefab of a house.
    /// </summary>
    public GameObject house;

    /// <summary>
    /// Prefab of an hotel.
    /// </summary>
    public GameObject hotel;

    /// <summary>
    /// Left trading panel (current player trading panel).
    /// </summary>
    public GameObject tradePanel1;

    /// <summary>
    /// Right trading panel (partner player trading panel).
    /// </summary>
    public GameObject tradePanel2;

    /// <summary>
    /// Prefab of a trading panel minicard.
    /// </summary>
    public GameObject miniCardPrefab;

    /// <summary>
    /// Raycast to detect which cell the player has selected.
    /// </summary>
    private RaycastHit hit;

    /// <summary>
    /// Raycast to detect which cell the player has selected.
    /// </summary>
    private Ray ray;

    /// <summary>
    /// Boolean that indicates if a property card is shown.
    /// </summary>
    private bool propertyCardShown = false;

    /// <summary>
    /// Boolean that indicates if a railroad card is shown.
    /// </summary>
    private bool railroadCardShown = false;

    /// <summary>
    /// Boolean that indicates if the electric utility card is shown.
    /// </summary>
    private bool electricCardShown = false;

    /// <summary>
    /// Boolean that indicates if the water utility card is shown.
    /// </summary>
    private bool waterCardShown = false;

    /// <summary>
    /// Boolean that indicates if the income tax card is shown.
    /// </summary>
    private bool incomeCardShown = false;

    /// <summary>
    /// Boolean that indicates if the luxury card is shown.
    /// </summary>
    private bool luxuryCardShown = false;

    /// <summary>
    /// Boolean that indicates if a chest card is shown.
    /// </summary>
    private bool chestCardShown = false;

    /// <summary>
    /// Boolean that indicates if a chance card is shown.
    /// </summary>
    private bool chanceCardShown = false;

    /// <summary>
    /// Boolean that indicates if you must press buttons
    /// </summary>
    private bool justButtons = false;

    /// <summary>
    /// Boolean that indicates if buying a house/hotel mode is selected.
    /// </summary>
    private bool buySelected = false;

    /// <summary>
    /// Boolean that indicates if selling a house/hotel mode is selected.
    /// </summary>
    private bool sellSelected = false;

    /// <summary>
    /// Boolean that indicates if mortgaging/unmortgaging mode is selected.
    /// </summary>
    private bool mortgageSelected = false;

    /// <summary>
    /// Boolean that indicates if traveling mode is selected.
    /// </summary>
    private bool travelSelected = false;

    /// <summary>
    /// Boolean that indicates if trading mode is selected.
    /// </summary>
    private bool tradingSelected = false;

    /// <summary>
    /// Boolean tht indicates if the trading panel 1 is on or not.
    /// </summary>
    private bool tradePanel1On = false;

    /// <summary>
    /// Boolean tht indicates if the trading panel 2 is on or not.
    /// </summary>
    private bool tradePanel2On = false;

    /// <summary>
    /// Integer that indicates the current player torn.
    /// </summary>
    private int currentPlayer = 0;

    /// <summary>
    /// Name of the cell <c>currentPlayer</c> is on.
    /// </summary>
    private string actualCell = "";

    /// <summary>
    /// Indicates how many times more than usual the player will pay for rent.
    /// </summary>
    private int multiplier = 1;

    /// <summary>
    /// Name of the cells the current player can trade.
    /// </summary>
    private string[] tradingCells1 = new string[30];

    /// <summary>
    /// Name of the cells the trading partner player can trade.
    /// </summary>
    private string[] tradingCells2 = new string[30];

    /// <summary>
    /// Boolean that indicates if the current player cell is selected to trade.
    /// </summary>
    private bool[] CellSelected1 = new bool[30];

    /// <summary>
    /// Boolean that indicates if the trading partner player cell is selected to trade.
    /// </summary>
    private bool[] CellSelected2 = new bool[30];

    /// <summary>
    /// Amount of cash the current player will pay for trading.
    /// </summary>
    private int tradingCashPlayer1 = 0;

    /// <summary>
    /// Amount of cash the trading partner player will pay for trading.
    /// </summary>
    private int tradingCashPlayer2 = 0;

    /// <summary>
    /// Indicates which player is the trading partner.
    /// </summary>
    private int tradingPartner = 0;

    /// <summary>
    /// Indicates if you can modify trading. <c>True</c> when accepting/rejecting, <c>False</c> otherwise.
    /// </summary>
    private bool freezeTrading = false;

    /// <summary>
    /// Movements class.
    /// </summary>
    private Movements movements;

    /// <summary>
    /// CashManagement class.
    /// </summary>
    private CashManagement cashManagement;

    /// <summary>
    /// Bot class.
    /// </summary>
    private Bot bot;

    /// <summary>
    /// Contains information about each property.
    /// </summary>
    public struct Property {
        /// <summary>
        /// Name of the property.
        /// </summary>
        public string name;

        /// <summary>
        /// Cost of buying the property.
        /// </summary>
        public int cost;

        /// <summary>
        /// Cost of buying a house.
        /// </summary>
        public int houseCost;

        /// <summary>
        /// Number of houses built.
        /// </summary>
        public int houses;

        /// <summary>
        /// Player who owns the property.
        /// </summary>
        public int owner;

        /// <summary>
        /// Amount of rent to pay, 0 houses, 1 house, ... 5 houses (hotel).
        /// </summary>
        public int[] rent;

        /// <summary>
        /// Boolean that indicates if the proprety is mortgaged.
        /// </summary>
        public bool mortgaged;
    };

    /// <summary>
    /// Contains information about each railroad station.
    /// </summary>
    public struct RailRoad {

        /// <summary>
        /// Name of the railroad
        /// </summary>
        public string name;

        /// <summary>
        /// Player who wons the railroad
        /// </summary>
        public int owner;

        /// <summary>
        /// Boolean that indicates if the railroad is mortgaged.
        /// </summary>
        public bool mortgaged;
    }

    /// <summary>
    /// Contains information about each utility.
    /// </summary>
    public struct Utility
    {
        /// <summary>
        /// Player who wons the utility.
        /// </summary>
        public int owner;

        /// <summary>
        /// Boolean that indicates if the railroad is mortgaged.
        /// </summary>
        public bool mortgaged;
    }

    /// <summary>
    /// Electrical utillity information.
    /// </summary>
    private Utility electrical;

    /// <summary>
    /// Water utility information.
    /// </summary>
    private Utility water;

    /// <summary>
    /// Contains information of each property.
    /// Key: name of the cell.
    /// Value: property information.
    /// </summary>
    private Dictionary<string, Property> propertyInformation;

    /// <summary>
    /// Contains information of each railroad station.
    /// Key: name of the cell.
    /// Value: railroad information.
    /// </summary>
    private Dictionary<string, RailRoad> railroadInformation;

    /// <summary>
    /// Contains the owner marker of each cell.
    /// Key: name of the cell.
    /// Value: owner marker gameobject.
    /// </summary>
    private Dictionary<string, GameObject> ownerPrefabs;

    /// <summary>
    /// Contains the houses gameobjects of each cell.
    /// Key: name of the cell.
    /// Value: houses gameobjects
    /// </summary>
    private Dictionary<string, GameObject[]> housesPrefabs;

    /// <summary>
    /// Method <c>InitializePropertyDictionary</c> initializes <c>propertyInformation</c>.
    /// </summary>
    private void InitializePropertyDictionary() {
        propertyInformation = new Dictionary<string, Property>();
        Property property = new()
        {
            name = "Old Kent Rd",
            cost = 60,
            houseCost = 50,
            houses = 0,
            owner = -1,
            rent = new int[] { 2, 10, 30, 90, 160, 250},
            mortgaged = false
        };
        propertyInformation.Add("Brown", property);

        property.name = "Whitechapel Rd";
        property.rent = new int[] { 4, 20, 60, 180, 320, 450 };
        propertyInformation.Add("Brown2", property);

        property.name = "The Angel, Islington";
        property.cost = 100;
        property.rent = new int[] { 6, 30, 90, 270, 400, 550 };
        propertyInformation.Add("LightBlue", property);

        property.name = "Euston Rd";
        propertyInformation.Add("LightBlue2", property);

        property.name = "Pentonville Rd";
        property.cost = 120;
        property.rent = new int[] { 8, 40, 100, 300, 450, 600};
        propertyInformation.Add("LightBlue3", property);

        property.name = "Pall Mall";
        property.cost = 140;
        property.houseCost = 100;
        property.rent = new int[] { 10, 50, 150, 450, 625, 750};
        propertyInformation.Add("Purple", property);

        property.name = "Whitehall";
        propertyInformation.Add("Purple2", property);

        property.name = "Northumberland Ave";
        property.cost = 160;
        property.rent = new int[] { 12, 60, 180, 500, 700, 900};
        propertyInformation.Add("Purple3", property);

        property.name = "Bow St";
        property.cost = 180;
        property.rent = new int[] { 14, 70, 200, 550, 750, 950};
        propertyInformation.Add("Orange", property);

        property.name = "Marlborough St";
        propertyInformation.Add("Orange2", property);

        property.name = "Vine St";
        property.cost = 200;
        property.rent = new int[] {16, 80, 220, 600, 800, 1000 };
        propertyInformation.Add("Orange3", property);

        property.name = "Strand";
        property.cost = 220;
        property.houseCost = 150;
        property.rent = new int[] { 18, 90, 250, 700, 875, 1050};
        propertyInformation.Add("Red", property);

        property.name = "Fleet St";
        propertyInformation.Add("Red2", property);

        property.name = "Trafalgar Sq";
        property.cost = 240;
        property.rent = new int[] { 20, 100, 300, 750, 925, 1100 };
        propertyInformation.Add("Red3", property);

        property.name = "Leicester St";
        property.cost = 260;
        property.rent = new int[] { 22, 110, 330, 800, 975, 1150 };
        propertyInformation.Add("Yellow", property);

        property.name = "Coventry St";
        propertyInformation.Add("Yellow2", property);

        property.name = "Piccadilly";
        property.cost = 280;
        property.rent = new int[] { 24, 120, 360, 850, 1025, 1200 };
        propertyInformation.Add("Yellow3", property);

        property.name = "Regent St";
        property.cost = 300;
        property.houseCost = 200;
        property.rent = new int[] { 26, 130, 390, 900, 1100, 1275 };
        propertyInformation.Add("Green", property);

        property.name = "Oxford St";
        propertyInformation.Add("Green2", property);

        property.name = "Bond St";
        property.cost = 320;
        property.rent = new int[] { 28, 150, 450, 1000, 1200, 1400 };
        propertyInformation.Add("Green3", property);

        property.name = "Park Lane";
        property.cost = 350;
        property.rent = new int[] { 35, 175, 500, 1100, 1300, 1500 };
        propertyInformation.Add("DarkBlue", property);

        property.name = "Mayfair";
        property.cost = 400;
        property.rent = new int[] { 50, 200, 600, 1400, 1700, 2000 };
        propertyInformation.Add("DarkBlue2", property);
    }

    /// <summary>
    /// Method <c>InitializeRailroadDictionary</c> initializes <c>railroadInformation</c>.
    /// </summary>
    private void InitializeRailroadDictionary() {
        railroadInformation = new Dictionary<string, RailRoad>();
        RailRoad railroad = new()
        {
            name = "Kings Cross",
            owner = -1,
            mortgaged = false
        };
        railroadInformation.Add("Station", railroad);
        railroad.name = "Marylebone";
        railroadInformation.Add("Station2", railroad);
        railroad.name = "Fenchurch St";
        railroadInformation.Add("Station3", railroad);
        railroad.name = "Liverpool St";
        railroadInformation.Add("Station4", railroad);
    }

    /// <summary>
    /// Method <c>InitializePrefabDictionary</c> initializes <c>ownerPrefabs</c>.
    /// </summary>
    private void InitializePrefabDictionary() {
        housesPrefabs = new Dictionary<string, GameObject[]>();
        GameObject[] prefabArray = new GameObject[5];
        housesPrefabs.Add("Brown", prefabArray);
        housesPrefabs.Add("Brown2", prefabArray);
        housesPrefabs.Add("LightBlue", prefabArray);
        housesPrefabs.Add("LightBlue2", prefabArray);
        housesPrefabs.Add("LightBlue3", prefabArray);
        housesPrefabs.Add("Purple", prefabArray);
        housesPrefabs.Add("Purple2", prefabArray);
        housesPrefabs.Add("Purple3", prefabArray);
        housesPrefabs.Add("Orange", prefabArray);
        housesPrefabs.Add("Orange2", prefabArray);
        housesPrefabs.Add("Orange3", prefabArray);
        housesPrefabs.Add("Red", prefabArray);
        housesPrefabs.Add("Red2", prefabArray);
        housesPrefabs.Add("Red3", prefabArray);
        housesPrefabs.Add("Yellow", prefabArray);
        housesPrefabs.Add("Yellow2", prefabArray);
        housesPrefabs.Add("Yellow3", prefabArray);
        housesPrefabs.Add("Green", prefabArray);
        housesPrefabs.Add("Green2", prefabArray);
        housesPrefabs.Add("Green3", prefabArray);
        housesPrefabs.Add("DarkBlue", prefabArray);
        housesPrefabs.Add("DarkBlue2", prefabArray);
    }

    /// <summary>
    /// Method <c>Start</c> initializes dictionaries and buttons.
    /// </summary>
    private void Start()
    {
        GameObject scripts = GameObject.Find("GameHandler");
        movements = scripts.GetComponent<Movements>();
        cashManagement = scripts.GetComponent<CashManagement>();
        bot = scripts.GetComponent<Bot>();

        InitializePropertyDictionary();
        InitializeRailroadDictionary();
        InitializePrefabDictionary();
        ownerPrefabs = new Dictionary<string, GameObject>();

        electrical = new Utility
        {
            owner = -1,
            mortgaged = false
        };
        water = new Utility
        {
            owner = -1,
            mortgaged = false
        };

        buyButton.onClick.AddListener(BuyCell);
        passButton.onClick.AddListener(PassClicked);
        buyHouseButton.onClick.AddListener(BuyHouse);
        sellHouseButton.onClick.AddListener(SellHouse);
        mortgage.onClick.AddListener(MortgageUnmortgage);
        trade.onClick.AddListener(Trading);
        cancel.onClick.AddListener(FinishTrade);
        offer.onClick.AddListener(MakeOffer);
        reject.onClick.AddListener(FinishTrade);
        accept.onClick.AddListener(MakeTrade);
    }

    /// <summary>
    /// Method <c>BuyHouse</c> sets the mode to buying houses/hotel.
    /// </summary>
    private void BuyHouse() {
        if (tradingSelected) return;
        buySelected = true;
        sellSelected = false;
        mortgageSelected = false;
    }

    /// <summary>
    /// Method <c>SellHouse</c> sets the mode to selling houses/hotel.
    /// </summary>
    private void SellHouse() {
        if (tradingSelected) return;
        sellSelected = true;
        buySelected = false;
        mortgageSelected = false;
    }

    /// <summary>
    /// Method <c>PassClicked</c> player doesn't buy the property/railroad/utility
    /// </summary>
    public void PassClicked() {
        DisableCard();
        justButtons = false;
        buyButton.gameObject.SetActive(false);
        passButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// Method <c>ShowRentCard</c> shows the property card of the indicated cell.
    /// </summary>
    /// <param name="cellName">Name of the cell.</param>
    private void ShowRentCard(string cellName) {
        Image topBar = propertyCard.transform.Find("Top").GetComponent<Image>();

        if (cellName == "Brown" || cellName == "Brown2") topBar.color = new Color32(142, 97, 64, 255);
        else if (cellName == "LightBlue" || cellName == "LightBlue2" || cellName == "LightBlue3") topBar.color = new Color32(48, 190, 217, 255);
        else if (cellName == "Purple" || cellName == "Purple2" || cellName == "Purple3") topBar.color = new Color32(219, 24, 174, 255);
        else if (cellName == "Orange" || cellName == "Orange2" || cellName == "Orange3") topBar.color = new Color32(243, 146, 55, 255);
        else if (cellName == "Red" || cellName == "Red2" || cellName == "Red3") topBar.color = new Color32(255, 66, 66, 255);
        else if (cellName == "Yellow" || cellName == "Yellow2" || cellName == "Yellow3") topBar.color = new Color32(255, 240, 0, 255);
        else if (cellName == "Green" || cellName == "Green2" || cellName == "Green3") topBar.color = new Color32(106, 181, 71, 255);
        else if (cellName == "DarkBlue" || cellName == "DarkBlue2") topBar.color = new Color32(18, 88, 219, 255);
        else return;

        propertyCard.transform.Find("Name").GetComponent<TMP_Text>().text = propertyInformation[cellName].name;
        propertyCard.transform.Find("Cost_value").GetComponent<TMP_Text>().text = propertyInformation[cellName].cost.ToString();
        propertyCard.transform.Find("Rent_value").GetComponent<TMP_Text>().text = propertyInformation[cellName].rent[0].ToString();
        propertyCard.transform.Find("Rent1_value").GetComponent<TMP_Text>().text = propertyInformation[cellName].rent[1].ToString();
        propertyCard.transform.Find("Rent2_value").GetComponent<TMP_Text>().text = propertyInformation[cellName].rent[2].ToString();
        propertyCard.transform.Find("Rent3_value").GetComponent<TMP_Text>().text = propertyInformation[cellName].rent[3].ToString();
        propertyCard.transform.Find("Rent4_value").GetComponent<TMP_Text>().text = propertyInformation[cellName].rent[4].ToString();
        propertyCard.transform.Find("RentH_value").GetComponent<TMP_Text>().text = propertyInformation[cellName].rent[5].ToString();
        propertyCard.transform.Find("CostHouse_value").GetComponent<TMP_Text>().text = propertyInformation[cellName].houseCost.ToString();
        propertyCard.transform.Find("CostHotel_value").GetComponent<TMP_Text>().text = propertyInformation[cellName].houseCost.ToString();
        propertyCard.transform.Find("Mortgage_value").GetComponent<TMP_Text>().text = (propertyInformation[cellName].cost / 2).ToString();

        if (propertyInformation[cellName].mortgaged) propertyCard.transform.Find("Body").GetComponentInChildren<Image>().color = new Color32(131, 133, 140, 255);
        else propertyCard.transform.Find("Body").GetComponentInChildren<Image>().color = new Color32(237, 231, 217, 255);
        propertyCard.SetActive(true);
        propertyCardShown = true;
    }

    /// <summary>
    /// Method <c>ShowRailroadCard</c> shows the railroad station card of the indicated cell.
    /// </summary>
    /// <param name="cellName">Name of the cell.</param>
    private void ShowRailroadCard(string cellName) {
        railroadCard.transform.Find("Name").GetComponent<TMP_Text>().text = railroadInformation[cellName].name;
        if (railroadInformation[cellName].mortgaged) railroadCard.transform.Find("Body").GetComponentInChildren<Image>().color = new Color32(120, 120, 120, 255);
        else railroadCard.transform.Find("Body").GetComponentInChildren<Image>().color = new Color32(245, 234, 223, 255);
        railroadCard.SetActive(true);
        railroadCardShown = true;
    }

    /// <summary>
    /// Method <c>ShowUtilityCard</c> shows the utility card of the indicated cell.
    /// </summary>
    /// <param name="cellName">Name of the cell.</param>
    private void ShowUtiliyCard(string cellName) {
        if (cellName == "Electric")
        {
            if (electrical.mortgaged) electricCard.transform.Find("Body").GetComponentInChildren<Image>().color = new Color32(120, 120, 120, 255);
            else electricCard.transform.Find("Body").GetComponentInChildren<Image>().color = new Color32(245, 234, 223, 255);
            electricCard.SetActive(true);
            electricCardShown = true;
        }
        else {
            if (water.mortgaged) waterCard.transform.Find("Body").GetComponentInChildren<Image>().color = new Color32(120, 120, 120, 255);
            else waterCard.transform.Find("Body").GetComponentInChildren<Image>().color = new Color32(245, 234, 223, 255);
            waterCard.SetActive(true);
            waterCardShown = true;
        }
    }

    /// <summary>
    /// Method <c>MakeTrade</c> confirms the trade and transfers assets.
    /// </summary>
    public void MakeTrade() {
        for (int i = 0; i < CellSelected1.Length; ++i) {
            if (CellSelected1[i]) NewOwner(tradingCells1[i], tradingPartner);
            if (CellSelected2[i]) NewOwner(tradingCells2[i], currentPlayer);
        }
        cashManagement.ModifyCash(currentPlayer, tradingCashPlayer2 - tradingCashPlayer1, false, false);
        cashManagement.ModifyCash(tradingPartner, tradingCashPlayer1 - tradingCashPlayer2, false, false);
        if ((actualCell == "Station" || actualCell == "Station2" || actualCell == "Station3" || actualCell == "Station4") && railroadInformation[actualCell].owner == currentPlayer) PossibilityToTravel();
        FinishTrade();
    }

    /// <summary>
    /// Method <c>NewOwner</c> changes the owner of a cell.
    /// </summary>
    /// <param name="cellName">Cell name to transfer.</param>
    /// <param name="player">New owner player of the cell.</param>
    private void NewOwner(string cellName, int player)
    {
        if (cellName == "Station" || cellName == "Station2" || cellName == "Station3" || cellName == "Station4")
        {
            RailRoad railroad = railroadInformation[cellName];
            railroadInformation.Remove(cellName);
            railroad.owner = player;
            railroadInformation.Add(cellName, railroad);
        }
        else if (cellName == "Electric") electrical.owner = player;
        else if (cellName == "Water") water.owner = player;
        else
        {
            Property property = propertyInformation[cellName];
            propertyInformation.Remove(cellName);
            property.owner = player;
            propertyInformation.Add(cellName, property);
        }
        RemoveOwnerMarker(cellName);
        AddOwnerMarker(cellName, player);
    }

    /// <summary>
    /// Method <c>RemoveOwnerMarker</c> removes the owner marker in the indicated cell.
    /// </summary>
    /// <param name="cellName">Cell name.</param>
    private void RemoveOwnerMarker(string cellName) {
        GameObject markerToRemove = ownerPrefabs[cellName];
        Destroy(markerToRemove);
        ownerPrefabs.Remove(cellName);
    }

    /// <summary>
    /// Method <c>FinishTrade</c> removes trading panels and sets the mode back to normal.
    /// </summary>
    public void FinishTrade() {
        tradingSelected = false;
        freezeTrading = false;
        tradePanel1.transform.Find("Cash").GetComponent<TMP_InputField>().readOnly = false;
        tradePanel2.transform.Find("Cash").GetComponent<TMP_InputField>().readOnly = false;
        cancel.gameObject.SetActive(false);
        offer.gameObject.SetActive(false);
        tradePanel1.SetActive(false);
        tradePanel2.SetActive(false);
        accept.gameObject.SetActive(false);
        reject.gameObject.SetActive(false);
    }

    /// <summary>
    /// Method <c>MakeOffer</c> make an offer to the trading partner.
    /// </summary>
    private void MakeOffer()
    {
        freezeTrading = true;
        tradePanel1.transform.Find("Cash").GetComponent<TMP_InputField>().readOnly = true;
        tradePanel2.transform.Find("Cash").GetComponent<TMP_InputField>().readOnly = true;
        cancel.gameObject.SetActive(false);
        offer.gameObject.SetActive(false);

        if (movements.IsBot(tradingPartner))
        {
            bot.AcceptRejectTrade();
            return;
        }

        accept.gameObject.SetActive(true);
        reject.gameObject.SetActive(true);

    }

    /// <summary>
    /// Method <c>TradingPartnerSelected</c> enables and show trading panels after selecting trading partner.
    /// </summary>
    /// <param name="tag">Relative number of player to trade with.</param>
    public void TradingPartnerSelected(int tag) {
        tradingButton1.SetActive(false);
        tradingButton2.SetActive(false);
        tradingButton3.SetActive(false);

        if (currentPlayer == 0) tradingPartner = tag;
        else if (currentPlayer == 1)
        {
            if (tag == 1) tradingPartner = 0;
            else tradingPartner = tag;
        }
        else if (currentPlayer == 2)
        {
            if (tag == 3) tradingPartner = 3;
            else tradingPartner = tag - 1;
        }
        else tradingPartner = tag - 1;

        cancel.gameObject.SetActive(true);
        offer.gameObject.SetActive(true);
        tradingSelected = true;
        ShowPlayerPanel(currentPlayer, 1, false);

        ShowPlayerPanel(tradingPartner, 2, false);
        tradePanel1.SetActive(true);
        tradePanel2.SetActive(true);
    }

    /// <summary>
    /// Method <c>EndgameForPlayer</c> sells houses/hotels and mortgages cells before losing the game.
    /// </summary>
    /// <param name="player">Player who </param>
    /// <returns><c>True</c> if the player doesn't have enough assets to be on the positives, <c>False</c> if the player hasn't lost yet.</returns>
    public bool EndgameForPlayer(int player) {
        //Sell houses + hotels while cash < 0
        HashSet<string> toDelete = new();
        foreach (string cellName in propertyInformation.Keys) {
            Property property = propertyInformation[cellName];
            if (property.owner != player) continue;
            toDelete.Add(cellName);
            while (property.houses > 0) {
                Property propertyModify = propertyInformation[cellName];
                propertyInformation.Remove(cellName);
                RemoveHouse(cellName, propertyModify.houses);
                --propertyModify.houses;
                propertyInformation.Add(cellName, propertyModify);
                cashManagement.ModifyCash(player, propertyModify.houseCost / 2, false, true);
                if (cashManagement.GetCash(player) >= 0) return false; 
            }
        }
        //Mortgage
        foreach (string cellName in toDelete) {
            Property property = propertyInformation[cellName];
            if (property.mortgaged) continue;
            propertyInformation.Remove(cellName);
            RemoveHouse(cellName, property.houses);
            property.mortgaged = true;
            propertyInformation.Add(cellName, property);
            cashManagement.ModifyCash(player, property.cost / 2, false, true);
            movements.Mortgage(cellName);
            if (cashManagement.GetCash(player) >= 0) return false;
        }
        foreach (string cellName in railroadInformation.Keys)
        {
            RailRoad railroad = railroadInformation[cellName];
            if (railroad.owner != player && !railroad.mortgaged) continue;
            railroadInformation.Remove(cellName);
            railroad.mortgaged = true;
            railroadInformation.Add(cellName, railroad);
            cashManagement.ModifyCash(player, 200 / 2, false, true);
            movements.Mortgage(cellName);
            if (cashManagement.GetCash(player) >= 0) return false;
        }
        if (water.owner == player && !water.mortgaged) {
            water.mortgaged = true;
            cashManagement.ModifyCash(player, 200 / 2, false, true);
            movements.Mortgage("Water");
            if (cashManagement.GetCash(player) >= 0) return false;
        }
        if (electrical.owner == player && !electrical.mortgaged)
        {
            electrical.mortgaged = true;
            cashManagement.ModifyCash(player, 200 / 2, false, true);
            movements.Mortgage("Electric");
            if (cashManagement.GetCash(player) >= 0) return false;
        }
        //Remove propertries, player has lost
        foreach (string cellName in toDelete) {
            Property property = propertyInformation[cellName];
            propertyInformation.Remove(cellName);
            property.owner = -1;
            propertyInformation.Add(cellName, property);
            RemoveOwnerMarker(cellName);
        }
        return true;
    }

    /// <summary>
    /// Method that show buttons to select the trading partner.
    /// </summary>
    /// <param name="numberOfPlayers">Number of players in game.</param>
    private void EnableTradingButtons(int numberOfPlayers) {
        if (numberOfPlayers == 3)
        {
            if (currentPlayer == 0)
            {
                tradingButton1.transform.Find("Circle").GetComponent<Image>().color = new Color32(14, 121, 178, 255);
                tradingButton2.transform.Find("Circle").GetComponent<Image>().color = new Color32(255, 240, 0, 255);
            }
            else if (currentPlayer == 1)
            {
                tradingButton1.transform.Find("Circle").GetComponent<Image>().color = new Color32(255, 66, 66, 255);
                tradingButton2.transform.Find("Circle").GetComponent<Image>().color = new Color32(255, 240, 0, 255);
            }
            else {
                tradingButton1.transform.Find("Circle").GetComponent<Image>().color = new Color32(255, 66, 66, 255);
                tradingButton2.transform.Find("Circle").GetComponent<Image>().color = new Color32(14, 121, 178, 255);
            }
        }
        else {
            if (currentPlayer == 0)
            {
                tradingButton1.transform.Find("Circle").GetComponent<Image>().color = new Color32(14, 121, 178, 255);
                tradingButton2.transform.Find("Circle").GetComponent<Image>().color = new Color32(255, 240, 0, 255);
                tradingButton3.transform.Find("Circle").GetComponent<Image>().color = new Color32(106, 181, 71, 255);
            }
            else if (currentPlayer == 1)
            {
                tradingButton1.transform.Find("Circle").GetComponent<Image>().color = new Color32(255, 66, 66, 255);
                tradingButton2.transform.Find("Circle").GetComponent<Image>().color = new Color32(255, 240, 0, 255);
                tradingButton3.transform.Find("Circle").GetComponent<Image>().color = new Color32(106, 181, 71, 255);
            }
            else if (currentPlayer == 2)
            {
                tradingButton1.transform.Find("Circle").GetComponent<Image>().color = new Color32(255, 66, 66, 255);
                tradingButton2.transform.Find("Circle").GetComponent<Image>().color = new Color32(14, 121, 178, 255);
                tradingButton3.transform.Find("Circle").GetComponent<Image>().color = new Color32(106, 181, 71, 255);
            }
            else {
                tradingButton1.transform.Find("Circle").GetComponent<Image>().color = new Color32(255, 66, 66, 255);
                tradingButton2.transform.Find("Circle").GetComponent<Image>().color = new Color32(14, 121, 178, 255);
                tradingButton3.transform.Find("Circle").GetComponent<Image>().color = new Color32(255, 240, 0, 255);
            }

            tradingButton3.SetActive(true);
        }
        tradingButton1.SetActive(true);
        tradingButton2.SetActive(true);
    }

    /// <summary>
    /// Method <c>Trading</c> starts trading mode.
    /// </summary>
    private void Trading() {
        if (freezeTrading) return;
        tradingSelected = true;
        int numberOfPlayers = movements.GetNumberOfPlayers();
        if (numberOfPlayers > 2) {
            EnableTradingButtons(numberOfPlayers);
            return;
        }
        cancel.gameObject.SetActive(true);
        offer.gameObject.SetActive(true);

        ShowPlayerPanel(currentPlayer, 1, false);

        if (currentPlayer == 1) tradingPartner = 0;
        else tradingPartner = 1;
        ShowPlayerPanel(tradingPartner, 2, false);
        tradePanel1.SetActive(true);
        tradePanel2.SetActive(true);
    }

    /// <summary>
    /// int function <c>Repairs</c> calculates the amount to pay for houses/htoels repairs.
    /// </summary>
    /// <param name="numberOfHouses">Number of houses the current player has.</param>
    /// <param name="numberOfHotels">Number of hotels the current player has.</param>
    /// <returns>Total to pay ffor repairs.</returns>
    private int Repairs(int numberOfHouses, int numberOfHotels) {
        int total = 0;
        foreach (Property property in propertyInformation.Values)
        {
            if (property.owner != currentPlayer || property.houses == 0) continue;
            if (property.houses == 5) total += numberOfHotels;
            else total += numberOfHouses * property.houses;
        }
        return total;
    }

    /// <summary>
    /// Method <c>ChestCard</c> shows the corresponding chest card and makes the action in the card.
    /// </summary>
    /// <param name="cardNumber">Chest card number.</param>
    private void ChestCard(int cardNumber) {
        TMP_Text tmp_Text = chestCard.transform.Find("Name").GetComponent<TMP_Text>();
        if (cardNumber == 1) {
            tmp_Text.text = "Advance to Go (Collect 200)";
            movements.MoveTo(actualCell, "Start");
        }
        else if (cardNumber == 2) {
            tmp_Text.text = "Bank error in your favour. Collect 200";
            cashManagement.ModifyCash(currentPlayer, 200, false, true);
        }
        else if (cardNumber == 3)
        {
            tmp_Text.text = "Doctor’s fee. Pay 50";
            cashManagement.ModifyCash(currentPlayer, -50, false, true);
        }
        else if (cardNumber == 4)
        {
            tmp_Text.text = "From sale of stock you get 50";
            cashManagement.ModifyCash(currentPlayer, 50, false, true);
        }
        else if (cardNumber == 5)
        {
            tmp_Text.text = "Get Out of Jail Free";
            movements.IncrementByOneOutOfJailCards(currentPlayer);
        }
        else if (cardNumber == 6)
        {
            tmp_Text.text = "Go directly to jail, do not pass Go, do not collect 200";
            movements.GoToJail();
        }
        else if (cardNumber == 7)
        {
            tmp_Text.text = "Holiday fund matures. Receive 100";
            cashManagement.ModifyCash(currentPlayer, 100, false, true);
        }
        else if (cardNumber == 8)
        {
            tmp_Text.text = "Income tax refund. Collect 20";
            cashManagement.ModifyCash(currentPlayer, 20, false, true);
        }
        else if (cardNumber == 9)
        {
            tmp_Text.text = "It is your birthday. Collect 10 from every player";
            cashManagement.CollectFromEverybody(currentPlayer, 10);
        }
        else if (cardNumber == 10)
        {
            tmp_Text.text = "Life insurance matures. Collect 100";
            cashManagement.ModifyCash(currentPlayer, 100, false, true);
        }
        else if (cardNumber == 11)
        {
            tmp_Text.text = "Pay hospital fees of 100";
            cashManagement.ModifyCash(currentPlayer, -100, false, true);
        }
        else if (cardNumber == 12)
        {
            tmp_Text.text = "Pay school fees of 50";
            cashManagement.ModifyCash(currentPlayer, -50, false, true);
        }
        else if (cardNumber == 13)
        {
            tmp_Text.text = "Receive 25 consultancy fee";
            cashManagement.ModifyCash(currentPlayer, 25, false, true);
        }
        else if (cardNumber == 14)
        {
            tmp_Text.text = "You are assessed for street repairs. 40 per house. 115 per hotel";
            cashManagement.ModifyCash(currentPlayer, -Repairs(40, 115), false, true);
        }
        else if (cardNumber == 15)
        {
            tmp_Text.text = "You have won second prize in a beauty contest. Collect 10";
            cashManagement.ModifyCash(currentPlayer, 10, false, true);
        }
        else if (cardNumber == 16)
        {
            tmp_Text.text = "You inherit 100";
            cashManagement.ModifyCash(currentPlayer, 100, false, true);
        }
        StartCoroutine(WaitAndDisableChestCard());
        chestCard.SetActive(true);
        chestCardShown = true;
    }

    /// <summary>
    /// Method <c>WaitAndDisableChanceCard</c> waits 1.5 seconds and disables the chance card.
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitAndDisableChanceCard()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        chanceCard.SetActive(false);
        chanceCardShown = false;
    }

    /// <summary>
    /// Method <c>WaitAndDisableChestCard</c> waits 1.5 seconds and disables the chest card.
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitAndDisableChestCard()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        chestCard.SetActive(false);
        chestCardShown = false;
    }

    /// <summary>
    /// Method <c>ChestCard</c> shows the corresponding chance card and makes the action in the card.
    /// </summary>
    /// <param name="cardNumber">Chance card number.</param>
    private void ChanceCard(int cardNumber)
    {
        TMP_Text tmp_Text = chanceCard.transform.Find("Name").GetComponent<TMP_Text>();
        if (cardNumber == 1)
        {
            tmp_Text.text = "Advance to Go (Collect 200)";
            movements.MoveTo(actualCell, "Start");
        }
        else if (cardNumber == 2)
        {
            tmp_Text.text = "Advance to Trafalgar Square. If you pass Go, collect 200";
            movements.MoveTo(actualCell, "Red3");
        }
        else if (cardNumber == 3)
        {
            tmp_Text.text = "Advance to Mayfair";
            movements.MoveTo(actualCell, "DarkBlue2");
        }
        else if (cardNumber == 4)
        {
            tmp_Text.text = "Advance to Pall Mall. If you pass Go, collect 200";
            movements.MoveTo(actualCell, "Purple");
        }
        else if (cardNumber == 5 || cardNumber == 6)
        {
            tmp_Text.text = "Advance to the nearest Station. If unowned, you may buy it from the Bank. If owned, pay wonder twice the rental to which they are otherwise entitled";
            if (actualCell == "Chance") movements.MoveTo(actualCell, "Station2");
            else if (actualCell == "Chance2") movements.MoveTo(actualCell, "Station3");
            else movements.MoveTo(actualCell, "Station");
            multiplier = 2;
        }
        else if (cardNumber == 7)
        {
            tmp_Text.text = "Advance token to nearest Utility. If unowned, you may buy it from the Bank. If owned, pay owner a total ten times amount thrown.";
            if (actualCell == "Chance2") movements.MoveTo(actualCell, "Water");
            else movements.MoveTo(actualCell, "Electric");
            multiplier = 10;
        }
        else if (cardNumber == 8)
        {
            tmp_Text.text = "Bank pays you dividend of 50";
            cashManagement.ModifyCash(currentPlayer, 50, false, true);
        }
        else if (cardNumber == 9)
        {
            tmp_Text.text = "Get Out of Jail Free";
            movements.IncrementByOneOutOfJailCards(currentPlayer);
        }
        else if (cardNumber == 10)
        {
            tmp_Text.text = "Go Back 3 Spaces";
            movements.MoveNumberOfCells(-3);
        }
        else if (cardNumber == 11)
        {
            tmp_Text.text = "Go directly to Jail, do not pass Go, do not collect 200";
            movements.GoToJail();
        }
        else if (cardNumber == 12)
        {
            tmp_Text.text = "Make general repairs on all your property. For each house pay 25. For each hotel pay 100";
            int repairMoney = -Repairs(25, 100);
            if (repairMoney < 0) cashManagement.ModifyCash(currentPlayer, repairMoney, false, true);
        }
        else if (cardNumber == 13)
        {
            tmp_Text.text = "Speeding fine 15";
            cashManagement.ModifyCash(currentPlayer, -15, false, true);
        }
        else if (cardNumber == 14)
        {
            tmp_Text.text = "Take a trip to Kings Cross Station. If you pass Go, collect 200";
            movements.MoveTo(actualCell, "Station");
        }
        else if (cardNumber == 15)
        {
            tmp_Text.text = "You have been elected Chairman of the Board. Pay each player 50";
            cashManagement.PayEverybody(currentPlayer, 50);
        }
        else if (cardNumber == 16)
        {
            tmp_Text.text = "Your building loan matures. Collect 150";
            cashManagement.ModifyCash(currentPlayer, 150, false, true);
        }
        StartCoroutine(WaitAndDisableChanceCard());
        chanceCard.SetActive(true);
        chanceCardShown = true;
    }

    /// <summary>
    /// Method <c>ShowTaxCard</c> enables and shows the corresponding tax card.
    /// </summary>
    /// <param name="cellName">Tax cell name.</param>
    private void ShowTaxCard(string cellName) {
        if (cellName == "Tax")
        {
            incomeTaxCard.SetActive(true);
            incomeCardShown = true;
        }
        else
        {
            luxuryTaxCard.SetActive(true);
            luxuryCardShown = true;
        }
    }

    /// <summary>
    /// Method <c>AddOwnerMarker</c> creates an owner marker gameobject and places it to the correct cell.
    /// </summary>
    /// <param name="cellName">Cell name to place the marker.</param>
    /// <param name="player">Player marker.</param>
    private void AddOwnerMarker(string cellName, int player) {
        Vector3 position = movements.CellPosition(cellName);
        if (cellName == "Brown" || cellName == "Brown2" || cellName == "LightBlue" || cellName == "LightBlue2" || cellName == "LightBlue3" || cellName == "Station") position.z += 1.5f;
        else if (cellName == "Purple" || cellName == "Purple2" || cellName == "Purple3" || cellName == "Electric" || cellName == "Orange" || cellName == "Station2" || cellName == "Orange2" || cellName == "Orange3") position.x += 1.5f;
        else if (cellName == "Red" || cellName == "Red2" || cellName == "Red3" || cellName == "Station3" || cellName == "Yellow" || cellName == "Yellow2" || cellName == "Yellow3" || cellName == "Water") position.z -= 1.5f;
        else if (cellName == "Green" || cellName == "Green2" || cellName == "Green3" || cellName == "DarkBlue" || cellName == "DarkBlue2" || cellName == "Station4") position.x -= 1.5f;
        
        GameObject newOwnerMark = Instantiate(ownerMark[player], new Vector3(position.x, 0.55f, position.z), ownerMark[player].transform.rotation);
        ownerPrefabs.Add(cellName, newOwnerMark);
    }

    /// <summary>
    /// Method <c>AddHouse</c> creates the gameobject of the selected house, or removes all houses of the cell and creates an hotel.
    /// </summary>
    /// <param name="cellName">Cell name to create the house/hotel.</param>
    /// <param name="houseNumber">Number of house to create.</param>
    private void AddHouse(string cellName, int houseNumber) {
        Vector3 position = movements.CellPosition(cellName);
        if (cellName == "Brown" || cellName == "Brown2" || cellName == "LightBlue" || cellName == "LightBlue2" || cellName == "LightBlue3")
        {
            position.z -= 1.9f;
            if (houseNumber == 1) position.x += 1;
            else if (houseNumber == 2) position.x += 0.33f;
            else if (houseNumber == 3) position.x -= 0.33f;
            else if (houseNumber == 4) position.x -= 1;
            else {
                for (int i = 0; i < 4; ++i) RemoveHouse(cellName, i + 1);
                housesPrefabs[cellName][4] = Instantiate(hotel, new Vector3(position.x, 1.4f, position.z), Quaternion.identity);
                housesPrefabs[cellName][4].name = cellName + "_H";
                return;
            }
        }
        else if (cellName == "Purple" || cellName == "Purple2" || cellName == "Purple3" || cellName == "Orange" || cellName == "Orange2" || cellName == "Orange3")
        {
            position.x -= 1.9f;
            if (houseNumber == 1) position.z -= 1;
            else if (houseNumber == 2) position.z -= 0.33f;
            else if (houseNumber == 3) position.z += 0.33f;
            else if (houseNumber == 4) position.z += 1;
            else
            {
                for (int i = 0; i < 4; ++i) RemoveHouse(cellName, i + 1);
                housesPrefabs[cellName][4] = Instantiate(hotel, new Vector3(position.x, 1.4f, position.z), Quaternion.identity);
                housesPrefabs[cellName][4].transform.Rotate(0, 90, 0);
                housesPrefabs[cellName][4].name = cellName + "_H";
                return;
            }
        }
        else if (cellName == "Red" || cellName == "Red2" || cellName == "Red3" || cellName == "Yellow" || cellName == "Yellow2" || cellName == "Yellow3")
        {
            position.z += 1.9f;
            if (houseNumber == 1) position.x -= 1;
            else if (houseNumber == 2) position.x -= 0.33f;
            else if (houseNumber == 3) position.x += 0.33f;
            else if (houseNumber == 4) position.x += 1;
            else
            {
                for (int i = 0; i < 4; ++i) RemoveHouse(cellName, i + 1);
                housesPrefabs[cellName][4] = Instantiate(hotel, new Vector3(position.x, 1.4f, position.z), Quaternion.identity);
                housesPrefabs[cellName][4].name = cellName + "_H";
                return;
            }
        }
        else if (cellName == "Green" || cellName == "Green2" || cellName == "Green3" || cellName == "DarkBlue" || cellName == "DarkBlue2") {
            position.x += 1.9f;
            if (houseNumber == 1) position.z += 1;
            else if (houseNumber == 2) position.z += 0.33f;
            else if (houseNumber == 3) position.z -= 0.33f;
            else if (houseNumber == 4) position.z -= 1;
            else
            {
                for (int i = 0; i < 4; ++i) RemoveHouse(cellName, i + 1);
                housesPrefabs[cellName][4] = Instantiate(hotel, new Vector3(position.x, 1.4f, position.z), Quaternion.identity);
                housesPrefabs[cellName][4].transform.Rotate(0, 90, 0);
                housesPrefabs[cellName][4].name = cellName + "_H";
                return;
            }
        }
        housesPrefabs[cellName][houseNumber - 1] = Instantiate(house, new Vector3(position.x, 0.7f, position.z), Quaternion.identity);
        housesPrefabs[cellName][houseNumber - 1].name = cellName + "_" + houseNumber.ToString();
    }

    /// <summary>
    /// Method <c>RemoveHouse</c> deletes the gameobject of the selected house, and if it's an hotel creates 4 houses.
    /// </summary>
    /// <param name="cellName">Cell name to delete the house/hotel.</param>
    /// <param name="houseNumber">Number of house to delete.</param>
    private void RemoveHouse (string cellName, int houseNumber) {
        if (houseNumber < 5) {
            GameObject houseToRemove = GameObject.Find(cellName + "_" + houseNumber.ToString());
            Destroy(houseToRemove);
            return;
        }
        GameObject hotelToRemove = GameObject.Find(cellName + "_H");
        Destroy(hotelToRemove);
        for (int i = 1; i <= 4; ++i) AddHouse(cellName, i);
    }

    /// <summary>
    /// Method <c>DisableCard</c> hides the current card.
    /// </summary>
    /// <returns><c>True</c> if there was an active card, <c>False</c> otherwise.</returns>
    public bool DisableCard() {
        if (propertyCardShown)
        {
            propertyCard.SetActive(false);
            propertyCardShown = false;
            return true;
        }
        if (railroadCardShown)
        {
            railroadCard.SetActive(false);
            railroadCardShown = false;
            return true;
        }
        if (electricCardShown)
        {
            electricCard.SetActive(false);
            electricCardShown = false;
            return true;
        }
        if (waterCardShown)
        {
            waterCard.SetActive(false);
            waterCardShown = false;
            return true;
        }
        if (incomeCardShown)
        {
            incomeTaxCard.SetActive(false);
            incomeCardShown = false;
            return true;
        }
        if (luxuryCardShown)
        {
            luxuryTaxCard.SetActive(false);
            luxuryCardShown = false;
            return true;
        }
        if (chestCardShown) {
            chestCard.SetActive(false);
            chestCardShown = false;
            return true;
        }
        if (chanceCardShown)
        {
            chanceCard.SetActive(false);
            chanceCardShown = false;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Method <c>BuyCell</c> buy the cell which the current player is.
    /// </summary>
    public void BuyCell() {
        if (actualCell == "Station" || actualCell == "Station2" || actualCell == "Station3" || actualCell == "Station4")
        {
            if (!cashManagement.ModifyCash(currentPlayer, -200, false, false)) return;
            RailRoad railroad = railroadInformation[actualCell];
            railroadInformation.Remove(actualCell);
            railroad.owner = currentPlayer;
            railroadInformation.Add(actualCell, railroad);
            PossibilityToTravel();
        }
        else if (actualCell == "Electric")
        {
            if (!cashManagement.ModifyCash(currentPlayer, -150, false, false)) return;
            electrical.owner = currentPlayer;
        }
        else if (actualCell == "Water")
        {
            if (!cashManagement.ModifyCash(currentPlayer, -150, false, false)) return;
            water.owner = currentPlayer;
        }
        else {
            Property property = propertyInformation[actualCell];
            if (!cashManagement.ModifyCash(currentPlayer, -property.cost, false, false)) return;
            propertyInformation.Remove(actualCell);
            property.owner = currentPlayer;
            propertyInformation.Add(actualCell, property);
        }
        DisableCard();
        justButtons = false;
        buyButton.gameObject.SetActive(false);
        passButton.gameObject.SetActive(false);
        AddOwnerMarker(actualCell, currentPlayer);
    }

    /// <summary>
    /// int function <c>CountStations</c> return the number of stations the indicated player has.
    /// </summary>
    /// <param name="player"></param>
    /// <returns>Number of stations.</returns>
    private int CountStations(int player) {
        int numberOfStations = 0;
        if (railroadInformation["Station"].owner == player && !railroadInformation["Station"].mortgaged) ++numberOfStations;
        if (railroadInformation["Station2"].owner == player && !railroadInformation["Station2"].mortgaged) ++numberOfStations;
        if (railroadInformation["Station3"].owner == player && !railroadInformation["Station3"].mortgaged) ++numberOfStations;
        if (railroadInformation["Station4"].owner == player && !railroadInformation["Station4"].mortgaged) ++numberOfStations;
        return numberOfStations;
    }

    /// <summary>
    /// Method <c>MortgageUnMortgage</c> enables the mode which you can mortgage and unmortgage cells.
    /// </summary>
    private void MortgageUnmortgage() {
        if (tradingSelected) return;
        mortgageSelected = true;
        buySelected = false;
        sellSelected = false;
    }

    /// <summary>
    /// bool function <c>ColorHasHousing</c> indicates if the color block has at least one house built.
    /// </summary>
    /// <param name="cellName">A cell name of the color block.</param>
    /// <returns><c>True</c> if the color block has at least one house built, <c>False</c> otherwise.</returns>
    private bool ColorHasHousing(string cellName) {
        if (cellName == "Brown" || cellName == "Brown2") return propertyInformation["Brown"].houses > 0 || propertyInformation["Brown2"].houses > 0;
        if (cellName == "LightBlue" || cellName == "LightBlue2" || cellName == "LightBlue3") return propertyInformation["LightBlue"].houses > 0 || propertyInformation["LightBlue2"].houses > 0 || propertyInformation["LightBlue3"].houses > 0;
        if (cellName == "Purple" || cellName == "Purple2" || cellName == "Purple3") return propertyInformation["Purple"].houses > 0 || propertyInformation["Purple2"].houses > 0 || propertyInformation["Purple3"].houses > 0;
        if (cellName == "Orange" || cellName == "Orange2" || cellName == "Orange3") return propertyInformation["Orange"].houses > 0 || propertyInformation["Orange2"].houses > 0 || propertyInformation["Orange3"].houses > 0;
        if (cellName == "Red" || cellName == "Red2" || cellName == "Red3") return propertyInformation["Red"].houses > 0 || propertyInformation["Red2"].houses > 0 || propertyInformation["Red3"].houses > 0;
        if (cellName == "Yellow" || cellName == "Yellow2" || cellName == "Yellow3") return propertyInformation["Yellow"].houses > 0 || propertyInformation["Yellow2"].houses > 0 || propertyInformation["Yellow3"].houses > 0;
        if (cellName == "Green" || cellName == "Green2" || cellName == "Green3") return propertyInformation["Green"].houses > 0 || propertyInformation["Green2"].houses > 0 || propertyInformation["Green3"].houses > 0;
        if (cellName == "DarkBlue" || cellName == "DarkBlue2") return propertyInformation["DarkBlue"].houses > 0 || propertyInformation["DarkBlue2"].houses > 0;
        return false;
    }

    /// <summary>
    /// bool function <c>ColorMOrtgaged</c> indicates if the color block has at least one cell mortgaged.
    /// </summary>
    /// <param name="cellName">A cell name of the color block.</param>
    /// <returns><c>True</c> if the color block has at least one cell mortgaged, <c>False</c> otherwise.</returns>
    public bool ColorMortgaged(string cellName)
    {
        if (cellName == "Brown" || cellName == "Brown2") return propertyInformation["Brown"].mortgaged || propertyInformation["Brown2"].mortgaged;
        if (cellName == "LightBlue" || cellName == "LightBlue2" || cellName == "LightBlue3") return propertyInformation["LightBlue"].mortgaged || propertyInformation["LightBlue2"].mortgaged || propertyInformation["LightBlue3"].mortgaged;
        if (cellName == "Purple" || cellName == "Purple2" || cellName == "Purple3") return propertyInformation["Purple"].mortgaged || propertyInformation["Purple2"].mortgaged || propertyInformation["Purple3"].mortgaged;
        if (cellName == "Orange" || cellName == "Orange2" || cellName == "Orange3") return propertyInformation["Orange"].mortgaged || propertyInformation["Orange2"].mortgaged || propertyInformation["Orange3"].mortgaged;
        if (cellName == "Red" || cellName == "Red2" || cellName == "Red3") return propertyInformation["Red"].mortgaged || propertyInformation["Red2"].mortgaged || propertyInformation["Red3"].mortgaged;
        if (cellName == "Yellow" || cellName == "Yellow2" || cellName == "Yellow3") return propertyInformation["Yellow"].mortgaged || propertyInformation["Yellow2"].mortgaged || propertyInformation["Yellow3"].mortgaged;
        if (cellName == "Green" || cellName == "Green2" || cellName == "Green3") return propertyInformation["Green"].mortgaged || propertyInformation["Green2"].mortgaged || propertyInformation["Green3"].mortgaged;
        if (cellName == "DarkBlue" || cellName == "DarkBlue2") return propertyInformation["DarkBlue"].mortgaged || propertyInformation["DarkBlue2"].mortgaged;
        return false;
    }

    /// <summary>
    /// bool function <c>PlayerHasAllColor</c> indicates if the current player is the owner of all cells of that color.
    /// </summary>
    /// <param name="cellName">A cell name of the color block.</param>
    /// <returns><c>True</c>if the current player is the owner of all cells of that color, <c>False</c> otherwise.</returns>
    public bool PlayerHasAllColor(string cellName) {
        if (cellName == "Brown" || cellName == "Brown2") return propertyInformation["Brown"].owner == propertyInformation["Brown2"].owner;
        if (cellName == "LightBlue" || cellName == "LightBlue2" || cellName == "LightBlue3") return propertyInformation["LightBlue"].owner == propertyInformation["LightBlue2"].owner && propertyInformation["LightBlue"].owner == propertyInformation["LightBlue3"].owner;
        if (cellName == "Purple" || cellName == "Purple2" || cellName == "Purple3") return propertyInformation["Purple"].owner == propertyInformation["Purple2"].owner && propertyInformation["Purple"].owner == propertyInformation["Purple3"].owner;
        if (cellName == "Orange" || cellName == "Orange2" || cellName == "Orange3") return propertyInformation["Orange"].owner == propertyInformation["Orange2"].owner && propertyInformation["Orange"].owner == propertyInformation["Orange3"].owner;
        if (cellName == "Red" || cellName == "Red2" || cellName == "Red3") return propertyInformation["Red"].owner == propertyInformation["Red2"].owner && propertyInformation["Red"].owner == propertyInformation["Red3"].owner;
        if (cellName == "Yellow" || cellName == "Yellow2" || cellName == "Yellow3") return propertyInformation["Yellow"].owner == propertyInformation["Yellow2"].owner && propertyInformation["Yellow"].owner == propertyInformation["Yellow3"].owner;
        if (cellName == "Green" || cellName == "Green2" || cellName == "Green3") return propertyInformation["Green"].owner == propertyInformation["Green2"].owner && propertyInformation["Green"].owner == propertyInformation["Green3"].owner;
        if (cellName == "DarkBlue" || cellName == "DarkBlue2") return propertyInformation["DarkBlue"].owner == propertyInformation["DarkBlue2"].owner;
        return false;
    }

    /// <summary>
    /// bool function <c>StationMortgaged</c> indicates if the station is mortgaged.
    /// </summary>
    /// <param name="station">Number of railroad station.</param>
    /// <returns><c>True</c> if the railroad station is mortgaged, <c>False</c> otherwise.</returns>
    public bool StationMortgaged(int station) {
        if (station == 1) return railroadInformation["Station"].mortgaged;
        if (station == 2) return railroadInformation["Station2"].mortgaged;
        if (station == 3) return railroadInformation["Station3"].mortgaged;
        return railroadInformation["Station4"].mortgaged;
    }

    /// <summary>
    /// Method <c>TravelSelected</c> sets the mode to travel.
    /// </summary>
    public void TravelSelected() {
        travelSelected = true;
    }

    /// <summary>
    /// bool function <c>CanTravel</c> indicates if the indicated player can travel or not.
    /// </summary>
    /// <param name="cellName">Staion cell name.</param>
    /// <param name="player">Player who wants to travel.</param>
    /// <returns><c>True</c> if the player can travel, <c>False</c> otherwise.</returns>
    public bool CanTravel(string cellName, int player) {
        if (railroadInformation[cellName].owner == player && actualCell != cellName && !railroadInformation[cellName].mortgaged) return true;
        return false;
    }

    /// <summary>
    /// Method <c>PossibiityToTravel</c> shows the traveling button if the current plyer can travel.
    /// </summary>
    private void PossibilityToTravel() {
        if (CountStations(currentPlayer) >= 2) movements.ShowTravelButton();
    }

    /// <summary>
    /// int function <c>CalculateRent</c> calculates the amount of rent the current player has to pay.
    /// </summary>
    /// <param name="cellName">Landed cell name</param>
    /// <returns>Amount of rent the current player has to pay.</returns>
    private int CalculateRent(string cellName) {
        if (propertyInformation[cellName].houses > 0) return propertyInformation[cellName].rent[propertyInformation[cellName].houses];
        if (PlayerHasAllColor(cellName)) return propertyInformation[cellName].rent[0] * 2;
        return propertyInformation[cellName].rent[0];
    }

    /// <summary>
    /// Method <c>BuyPassButtons</c> shows the buttons to buy a cell, or indicates the computer player the possibility to buy a cell.
    /// </summary>
    /// <param name="cellName">Cell name aviable to buy.</param>
    /// <param name="player">Player to buy the cell.</param>
    private void BuyPassButtons(string cellName, int player) {
        justButtons = true;
        if (movements.IsBot(player)) bot.BuyPropertyDecision(cellName);
        else
        {
            buyButton.gameObject.SetActive(true);
            passButton.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Method <c>LandedOn</c> makes certain acions after landing on a cell.
    /// </summary>
    /// <param name="cellName">Landed cell name.</param>
    /// <param name="player">Player who landed.</param>
    /// <param name="dices">Value of the dices rolled to land on.</param>
    public void LandedOn(string cellName, int player, int dices) {
        actualCell = cellName;
        currentPlayer = player;
        if (cellName == "Station" || cellName == "Station2" || cellName == "Station3" || cellName == "Station4")
        {
            if (railroadInformation[cellName].owner == -1) BuyPassButtons(cellName, player);
            else if (railroadInformation[cellName].owner == player) PossibilityToTravel();
            else if (!railroadInformation[cellName].mortgaged)
            {
                int numberOfStations = CountStations(railroadInformation[cellName].owner);
                int rentToPay = 0;
                if (numberOfStations == 1) rentToPay = 25 * multiplier;
                else if (numberOfStations == 2) rentToPay = 100 * multiplier;
                else if (numberOfStations == 3) rentToPay = 200 * multiplier;
                else if (numberOfStations == 4) rentToPay = 400 * multiplier;
                cashManagement.ModifyCash(player, -rentToPay, false, true);
                cashManagement.ModifyCash(railroadInformation[cellName].owner, rentToPay, false, true);
            }
            ShowRailroadCard(cellName);
            multiplier = 1;
        }
        else if (cellName == "Water")
        {
            ShowUtiliyCard(cellName);
            if (water.owner == -1) BuyPassButtons(cellName, player);
            else if (player != water.owner && !water.mortgaged)
            {
                if (water.owner == electrical.owner || multiplier != 1)
                {
                    cashManagement.ModifyCash(player, -10 * dices, false, true);
                    cashManagement.ModifyCash(water.owner, 10 * dices, false, true);
                }
                else
                {
                    cashManagement.ModifyCash(player, -4 * dices, false, true);
                    cashManagement.ModifyCash(water.owner, 4 * dices, false, true);
                }
            }
            multiplier = 1;
        }
        else if (cellName == "Electric")
        {
            ShowUtiliyCard(cellName);
            if (electrical.owner == -1) BuyPassButtons(cellName, player);
            else if (player != electrical.owner && !electrical.mortgaged)
            {
                if (water.owner == electrical.owner || multiplier != 1)
                {
                    cashManagement.ModifyCash(player, -10 * dices, false, true);
                    cashManagement.ModifyCash(electrical.owner, 10 * dices, false, true);
                }
                else
                {
                    cashManagement.ModifyCash(player, -4 * dices, false, true);
                    cashManagement.ModifyCash(electrical.owner, 4 * dices, false, true);
                }
                multiplier = 1;
            }
        }
        else if (cellName == "Chest" || cellName == "Chest2" || cellName == "Chest3") ChestCard(Random.Range(1, 17));
        else if (cellName == "Chance" || cellName == "Chance2" || cellName == "Chance3") ChanceCard(Random.Range(1, 17));
        else if (cellName == "Tax")
        {
            ShowTaxCard(cellName);
            cashManagement.ModifyCashPercent(player, -0.1f, true);
        }
        else if (cellName == "Tax2")
        {
            ShowTaxCard(cellName);
            cashManagement.ModifyCash(player, -100, true, true);
        }
        else if (cellName == "Jail") Debug.Log("Jail Card in progress");
        else if (cellName == "GoToJail") Debug.Log("GoToJail Card in progress");
        else if (cellName == "Parking")
        {
            Debug.Log("Parking Card in progress");
            cashManagement.RefundCash(player);
        }
        else if (cellName == "Start") Debug.Log("Start Card in progress");
        else
        {
            ShowRentCard(cellName);
            if (propertyInformation[cellName].owner == -1) BuyPassButtons(cellName, player);
            else if (player != propertyInformation[cellName].owner && !propertyInformation[cellName].mortgaged)
            {
                int rentToPay = CalculateRent(cellName);
                cashManagement.ModifyCash(player, -rentToPay, false, true);
                cashManagement.ModifyCash(propertyInformation[cellName].owner, rentToPay, false, true);
            }
        }
    }

    /// <summary>
    /// Method <c>ShowCard</c> shows the card from the indicated cell.
    /// </summary>
    /// <param name="cellName">Cell name.</param>
    public void ShowCard(string cellName) {
        if (cellName == "Station" || cellName == "Station2" || cellName == "Station3" || cellName == "Station4") {
            if (travelSelected && CanTravel(cellName, currentPlayer))
            {
                movements.MoveTo(actualCell, cellName);
                travelSelected = false;
                movements.UndoStationColor();
            }
            else if (!mortgageSelected) ShowRailroadCard(cellName);
            else if (railroadInformation[cellName].owner == currentPlayer && !travelSelected) MortgageUnmortgageStation(currentPlayer, cellName);
        } 
        else if (cellName == "Water" || cellName == "Electric") {
            if (!mortgageSelected)
            {
                ShowUtiliyCard(cellName);
                return;
            }

            MortgageUnmortgageUtility(currentPlayer, cellName);
        }
        else if (cellName == "Chest" || cellName == "Chest2" || cellName == "Chest3") Debug.Log("Chest Card in progress");
        else if (cellName == "Chance" || cellName == "Chance2" || cellName == "Chance3") Debug.Log("Chance Card in progress");
        else if (cellName == "Tax" || cellName == "Tax2") ShowTaxCard(cellName);
        else if (cellName == "Jail") Debug.Log("Jail Card in progress");
        else if (cellName == "GoToJail") Debug.Log("GoToJail Card in progress");
        else if (cellName == "Parking") Debug.Log("Parking Card in progress");
        else if (cellName == "Start") Debug.Log("Start Card in progress");
        else {
            int playerTorn = movements.GetPlayerTorn();
            if (buySelected && playerTorn == propertyInformation[cellName].owner && PlayerHasAllColor(cellName) && propertyInformation[cellName].houses < 5 && !ColorMortgaged(cellName)) BuyHouse(playerTorn, cellName);
            else if (sellSelected && playerTorn == propertyInformation[cellName].owner && propertyInformation[cellName].houses > 0 && !propertyInformation[cellName].mortgaged) SellHouse(playerTorn, cellName);
            else if (mortgageSelected && playerTorn == propertyInformation[cellName].owner && !ColorHasHousing(cellName)) MortgageUnmortgageProperty(playerTorn, cellName);
            else ShowRentCard(cellName);
        }
        
    }

    /// <summary>
    /// Method <c>BuyHouse</c> buys a house.
    /// </summary>
    /// <param name="player">Player that buys the house.</param>
    /// <param name="cellName">Cell where the house is bought.</param>
    public void BuyHouse(int player, string cellName) {
        Property property = propertyInformation[cellName];
        if (!cashManagement.ModifyCash(player, -propertyInformation[cellName].houseCost, false, false)) return;
        propertyInformation.Remove(cellName);
        ++property.houses;
        AddHouse(cellName, property.houses);
        propertyInformation.Add(cellName, property);
    }

    /// <summary>
    /// Method <c>SellHouse</c> buys a house.
    /// </summary>
    /// <param name="player">Player that sells the house.</param>
    /// <param name="cellName">Cell where the house is sold.</param>
    public void SellHouse(int player, string cellName) {
        Property property = propertyInformation[cellName];
        propertyInformation.Remove(cellName);
        RemoveHouse(cellName, property.houses);
        --property.houses;
        propertyInformation.Add(cellName, property);
        cashManagement.ModifyCash(player, propertyInformation[cellName].houseCost / 2, false, true);
    }

    /// <summary>
    /// Method <c>MortgeageUnmortgageProperty</c> mortgages the cell if it isn't mortgaged, unmortgages is otherwise.
    /// </summary>
    /// <param name="player">Player that mortgages/unmortgages the cell.</param>
    /// <param name="cellName">Cell name mortgaged/unmortgaged.</param>
    public void MortgageUnmortgageProperty(int player, string cellName) {
        Property property = propertyInformation[cellName];
        propertyInformation.Remove(cellName);
        property.mortgaged = !property.mortgaged;
        propertyInformation.Add(cellName, property);
        if (property.mortgaged)
        {
            cashManagement.ModifyCash(player, propertyInformation[cellName].cost / 2, false, true);
            movements.Mortgage(cellName);
            return;
        }
        if (!cashManagement.ModifyCash(player, (int)(-1.1f * (propertyInformation[cellName].cost / 2)), false, false))
        {
            propertyInformation.Remove(cellName);
            property.mortgaged = !property.mortgaged;
            propertyInformation.Add(cellName, property);
            return;
        }
        movements.Unmortgage(cellName);
    }

    /// <summary>
    /// Method <c>MortgeageUnmortgageStation</c> mortgages the station if it isn't mortgaged, unmortgages is otherwise.
    /// </summary>
    /// <param name="player">Player that mortgages/unmortgages the station.</param>
    /// <param name="cellName">Station name mortgaged/unmortgaged.</param>
    public void MortgageUnmortgageStation(int player, string cellName) {
        RailRoad railroad = railroadInformation[cellName];
        railroadInformation.Remove(cellName);
        railroad.mortgaged = !railroad.mortgaged;
        railroadInformation.Add(cellName, railroad);
        if (railroadInformation[cellName].mortgaged)
        {
            cashManagement.ModifyCash(player, 100, false, true);
            movements.Mortgage(cellName);
        }
        else
        {
            cashManagement.ModifyCash(player, -110, false, true);
            movements.Unmortgage(cellName);
        }
        if ((actualCell == "Station" || actualCell == "Station2" || actualCell == "Station3" || actualCell == "Station4") && railroadInformation[actualCell].owner == player) PossibilityToTravel();
    }

    /// <summary>
    /// Method <c>MortgeageUnmortgageUtility</c> mortgages the utility if it isn't mortgaged, unmortgages is otherwise.
    /// </summary>
    /// <param name="player">Player that mortgages/unmortgages the utility.</param>
    /// <param name="cellName">Utility name mortgaged/unmortgaged.</param>
    public void MortgageUnmortgageUtility(int player, string cellName)
    {
        bool mortgaged;
        if (cellName == "Water")
        {
            water.mortgaged = !water.mortgaged;
            mortgaged = water.mortgaged;
        }
        else
        {
            electrical.mortgaged = !electrical.mortgaged;
            mortgaged = electrical.mortgaged;
        }

        if (mortgaged)
        {
            cashManagement.ModifyCash(player, 75, false, true);
            movements.Mortgage(cellName);
        }
        else
        {
            cashManagement.ModifyCash(player, -83, false, true);
            movements.Unmortgage(cellName);
        }
    }

    /// <summary>
    /// Method <c>ShowPlayerPanel</c> shows the player panel with all properties/railroads/utilities the player owns.
    /// </summary>
    /// <param name="player">Player.</param>
    /// <param name="panel">Panel to show. <c>1</c> left panel, <c>2</c> right panel.</param>
    /// <param name="disableOnClick">Indicates if the panel can be disabled on click. (Useful to indicate the panel is for trading or just information).</param>
    public void ShowPlayerPanel(int player, int panel, bool disableOnClick) {
        int outofjail = movements.GetOutOfJail(player);
        GameObject tradePanel;
        if (panel == 1) {
            tradePanel = tradePanel1;
            if (disableOnClick) tradePanel1On = true;
            for (int k = 0; k < CellSelected1.Length; ++k) CellSelected1[k] = false;
            tradingCashPlayer1 = 0;
        } 
        else {
            tradePanel = tradePanel2;
            if (disableOnClick) tradePanel2On = true;
            for (int k = 0; k < CellSelected2.Length; ++k) CellSelected2[k] = false;
            tradingCashPlayer2 = 0;
        }

        if (player == 0) tradePanel.transform.Find("Panel1").gameObject.transform.Find("Circle").GetComponent<Image>().color = new Color32(255, 66, 66, 255);
        else if (player == 1) tradePanel.transform.Find("Panel1").gameObject.transform.Find("Circle").GetComponent<Image>().color = new Color32(14, 121, 178, 255);
        else if (player == 2) tradePanel.transform.Find("Panel1").gameObject.transform.Find("Circle").GetComponent<Image>().color = new Color32(242, 255, 73, 255);
        else tradePanel.transform.Find("Panel1").gameObject.transform.Find("Circle").GetComponent<Image>().color = new Color32(106, 181, 71, 255);

        if (disableOnClick)
        {
            tradePanel.transform.Find("Cash").gameObject.SetActive(false);
            tradePanel.transform.Find("Amount").gameObject.SetActive(false);
        }
        else {
            tradePanel.transform.Find("Cash").GetComponent<TMP_InputField>().text = "";
            tradePanel.transform.Find("Cash").gameObject.SetActive(true);
            tradePanel.transform.Find("Amount").gameObject.SetActive(true);
        }

        int i = 0;
        foreach (var property in propertyInformation) if (property.Value.owner == player) ShowMiniPropertyCard(property.Key, i++, tradePanel);
        foreach (var railroad in railroadInformation) if (railroad.Value.owner == player) ShowMiniRailroadCard(railroad.Key, i++, tradePanel);
        if (electrical.owner == player) ShowMiniUtilityCard("Electric", i++, tradePanel);
        if (water.owner == player) ShowMiniUtilityCard("Water", i++, tradePanel);
        for (int j = i; j < 30; ++j) tradePanel.transform.Find("targeta" + j).gameObject.SetActive(false);

        tradePanel.SetActive(true);
    }

    /// <summary>
    /// Method <c>ShowsMiniProppertyCard</c> shows a property card to the player panel.
    /// </summary>
    /// <param name="cardNumber">Position number of the card.</param>
    /// <param name="tradePanel">GameObject panel to modify.</param>
    private void ShowMiniPropertyCard(string cellName, int cardNumber, GameObject tradePanel)
    {
        if (tradePanel == tradePanel1) tradingCells1[cardNumber] = cellName;
        else tradingCells2[cardNumber] = cellName;

        //SPAWN CARD
        tradePanel.transform.Find("targeta" + cardNumber).gameObject.SetActive(true);
        GameObject minicard = tradePanel.transform.Find("targeta" + cardNumber).gameObject;
        Image franja = minicard.transform.Find("franja").gameObject.GetComponentInChildren<Image>();
        minicard.transform.Find("inicial").gameObject.SetActive(true);
        minicard.transform.Find("franja").gameObject.SetActive(true);
        minicard.transform.Find("cash").gameObject.SetActive(true);
        if (cellName == "Brown" || cellName == "Brown2") franja.color = new Color32(142, 97, 64, 255);
        else if (cellName == "LightBlue" || cellName == "LightBlue2" || cellName == "LightBlue3") franja.color = new Color32(48, 190, 217, 255);
        else if (cellName == "Purple" || cellName == "Purple2" || cellName == "Purple3") franja.color = new Color32(219, 24, 174, 255);
        else if (cellName == "Orange" || cellName == "Orange2" || cellName == "Orange3") franja.color = new Color32(243, 146, 55, 255);
        else if (cellName == "Red" || cellName == "Red2" || cellName == "Red3") franja.color = new Color32(255, 66, 66, 255);
        else if (cellName == "Yellow" || cellName == "Yellow2" || cellName == "Yellow3") franja.color = new Color32(255, 240, 0, 255);
        else if (cellName == "Green" || cellName == "Green2" || cellName == "Green3") franja.color = new Color32(106, 181, 71, 255);
        else if (cellName == "DarkBlue" || cellName == "DarkBlue2") franja.color = new Color32(18, 88, 219, 255);
        else return;

        Property property = propertyInformation[cellName];
        minicard.transform.Find("inicial").gameObject.GetComponentInChildren<TMP_Text>().text = property.name[0].ToString();
        minicard.transform.Find("cash").gameObject.GetComponentInChildren<TMP_Text>().text = property.cost.ToString();

        minicard.transform.Find("Train").gameObject.SetActive(false);
        minicard.transform.Find("WaterTap").gameObject.SetActive(false);
        minicard.transform.Find("inicial2").gameObject.SetActive(false);
        minicard.transform.Find("cash2").gameObject.SetActive(false);
        minicard.transform.Find("LightBulb").gameObject.SetActive(false);

        if (property.houses == 0 || property.houses == 5) {
            minicard.transform.Find("casa1").gameObject.SetActive(false);
            minicard.transform.Find("casa2").gameObject.SetActive(false);
            minicard.transform.Find("casa3").gameObject.SetActive(false);
            minicard.transform.Find("casa4").gameObject.SetActive(false);
            if (property.houses == 5) {
                minicard.transform.Find("inicial").gameObject.GetComponentInChildren<TMP_Text>().text = "H";
            }
        }
        else if (property.houses == 1)
        {
            minicard.transform.Find("casa1").gameObject.SetActive(true);
            minicard.transform.Find("casa2").gameObject.SetActive(false);
            minicard.transform.Find("casa3").gameObject.SetActive(false);
            minicard.transform.Find("casa4").gameObject.SetActive(false);
        }
        else if (property.houses == 2)
        {
            minicard.transform.Find("casa1").gameObject.SetActive(true);
            minicard.transform.Find("casa2").gameObject.SetActive(true);
            minicard.transform.Find("casa3").gameObject.SetActive(false);
            minicard.transform.Find("casa4").gameObject.SetActive(false);
        }
        else if (property.houses == 3)
        {
            minicard.transform.Find("casa1").gameObject.SetActive(true);
            minicard.transform.Find("casa2").gameObject.SetActive(true);
            minicard.transform.Find("casa3").gameObject.SetActive(true);
            minicard.transform.Find("casa4").gameObject.SetActive(false);
        }
        else if (property.houses == 4)
        {
            minicard.transform.Find("casa1").gameObject.SetActive(true);
            minicard.transform.Find("casa2").gameObject.SetActive(true);
            minicard.transform.Find("casa3").gameObject.SetActive(true);
            minicard.transform.Find("casa4").gameObject.SetActive(true);
        }

        if (property.mortgaged) minicard.GetComponentInChildren<Image>().color = new Color32(131, 133, 140, 255);
        else minicard.GetComponentInChildren<Image>().color = new Color32(242, 242, 242, 255);
    }

    /// <summary>
    /// Method <c>ShowsMiniRailroadCard</c> shows a railroad station card to the player panel.
    /// </summary>
    /// <param name="cardNumber">Position number of the card.</param>
    /// <param name="tradePanel">GameObject panel to modify.</param>
    private void ShowMiniRailroadCard(string cellName, int cardNumber, GameObject tradePanel)
    {
        if (tradePanel == tradePanel1) tradingCells1[cardNumber] = cellName;
        else tradingCells2[cardNumber] = cellName;

        tradePanel.transform.Find("targeta" + cardNumber).gameObject.SetActive(true);
        GameObject minicard = tradePanel.transform.Find("targeta" + cardNumber).gameObject;
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

        minicard.transform.Find("cash2").gameObject.GetComponentInChildren<TMP_Text>().text = "200";
        minicard.transform.Find("inicial2").gameObject.GetComponentInChildren<TMP_Text>().text = railroadInformation[cellName].name[0].ToString();

        if (railroadInformation[cellName].mortgaged) minicard.GetComponentInChildren<Image>().color = new Color32(131, 133, 140, 255);
        else minicard.GetComponentInChildren<Image>().color = new Color32(242, 242, 242, 255);
    }

    /// <summary>
    /// Method <c>ShowsMiniUtilityCard</c> shows a railroad station card to the player panel.
    /// </summary>
    /// <param name="cardNumber">Position number of the card.</param>
    /// <param name="tradePanel">GameObject panel to modify.</param>
    private void ShowMiniUtilityCard(string cellName, int cardNumber, GameObject tradePanel)
    {
        if (tradePanel == tradePanel1) tradingCells1[cardNumber] = cellName;
        else tradingCells2[cardNumber] = cellName;

        tradePanel.transform.Find("targeta" + cardNumber).gameObject.SetActive(true);
        GameObject minicard = tradePanel.transform.Find("targeta" + cardNumber).gameObject;
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

        minicard.transform.Find("cash2").gameObject.GetComponentInChildren<TMP_Text>().text = "150";

        if (cellName == "Electric")
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

    /// <summary>
    /// function int <c>CellNameToMiniCardNumber</c> returns the panel position where the cell card is.
    /// </summary>
    /// <param name="cellName">Cell name of the mini card.</param>
    /// <param name="panel">Trading panel to check for the cell card.</param>
    /// <returns>Panel position</returns>
    private int CellNameToMiniCardNumber(string cellName, int panel) {
        if (panel == 1) for (int i = 0; i < tradingCells1.Length; ++i) if (tradingCells1[i] == cellName) return i;
        if (panel == 2) for (int i = 0; i < tradingCells2.Length; ++i) if (tradingCells2[i] == cellName) return i;
        return -1;
    }

    /// <summary>
    /// Method <c>ToggleColor</c> changes color to selected/unselected when trading.
    /// </summary>
    /// <param name="panel">Trading panel where the card is.</param>
    /// <param name="cell">Cell position of the card.</param>
    /// <param name="cellName">Cell name position of the card.</param>
    private void ToggleColor(int panel, int cell, string cellName) {
        if (cellName == "Brown" || cellName == "Brown2") {
            if (propertyInformation["Brown"].houses > 0 || propertyInformation["Brown2"].houses > 0)
            {
                ToggleMiniCard(panel, CellNameToMiniCardNumber("Brown", panel));
                ToggleMiniCard(panel, CellNameToMiniCardNumber("Brown2", panel));
            }
            else ToggleMiniCard(panel, cell);
            return;
        }

        if (cellName == "LightBlue" || cellName == "LightBlue2" || cellName == "LightBlue3") {
            if (propertyInformation["LightBlue"].houses > 0 || propertyInformation["LightBlue2"].houses > 0 || propertyInformation["LightBlue3"].houses > 0) {
                ToggleMiniCard(panel, CellNameToMiniCardNumber("LightBlue", panel));
                ToggleMiniCard(panel, CellNameToMiniCardNumber("LightBlue2", panel));
                ToggleMiniCard(panel, CellNameToMiniCardNumber("LightBlue3", panel));
            }
            else ToggleMiniCard(panel, cell);
            return;
        }

        if (cellName == "Purple" || cellName == "Purple2" || cellName == "Purple3")
        {
            if (propertyInformation["Purple"].houses > 0 || propertyInformation["Purple2"].houses > 0 || propertyInformation["Purple3"].houses > 0)
            {
                ToggleMiniCard(panel, CellNameToMiniCardNumber("Purple", panel));
                ToggleMiniCard(panel, CellNameToMiniCardNumber("Purple2", panel));
                ToggleMiniCard(panel, CellNameToMiniCardNumber("Purple3", panel));
            }
            else ToggleMiniCard(panel, cell);
            return;
        }

        if (cellName == "Orange" || cellName == "Orange2" || cellName == "Orange3")
        {
            if (propertyInformation["Orange"].houses > 0 || propertyInformation["Orange2"].houses > 0 || propertyInformation["Orange3"].houses > 0)
            {
                ToggleMiniCard(panel, CellNameToMiniCardNumber("Orange", panel));
                ToggleMiniCard(panel, CellNameToMiniCardNumber("Orange2", panel));
                ToggleMiniCard(panel, CellNameToMiniCardNumber("Orange3", panel));
            }
            else ToggleMiniCard(panel, cell);
            return;
        }

        if (cellName == "Red" || cellName == "Red2" || cellName == "Red3")
        {
            if (propertyInformation["Red"].houses > 0 || propertyInformation["Red2"].houses > 0 || propertyInformation["Red3"].houses > 0)
            {
                ToggleMiniCard(panel, CellNameToMiniCardNumber("Red", panel));
                ToggleMiniCard(panel, CellNameToMiniCardNumber("Red2", panel));
                ToggleMiniCard(panel, CellNameToMiniCardNumber("Red3", panel));
            }
            else ToggleMiniCard(panel, cell);
            return;
        }

        if (cellName == "Yellow" || cellName == "Yellow2" || cellName == "Yellow3")
        {
            if (propertyInformation["Yellow"].houses > 0 || propertyInformation["Yellow2"].houses > 0 || propertyInformation["Yellow3"].houses > 0)
            {
                ToggleMiniCard(panel, CellNameToMiniCardNumber("Yellow", panel));
                ToggleMiniCard(panel, CellNameToMiniCardNumber("Yellow2", panel));
                ToggleMiniCard(panel, CellNameToMiniCardNumber("Yellow3", panel));
            }
            else ToggleMiniCard(panel, cell);
            return;
        }

        if (cellName == "Green" || cellName == "Green2" || cellName == "Green3")
        {
            if (propertyInformation["Green"].houses > 0 || propertyInformation["Green2"].houses > 0 || propertyInformation["Green3"].houses > 0)
            {
                ToggleMiniCard(panel, CellNameToMiniCardNumber("Green", panel));
                ToggleMiniCard(panel, CellNameToMiniCardNumber("Green2", panel));
                ToggleMiniCard(panel, CellNameToMiniCardNumber("Green3", panel));
            }
            else ToggleMiniCard(panel, cell);
            return;
        }

        if (cellName == "DarkBlue" || cellName == "DarkBlue2")
        {
            if (propertyInformation["DarkBlue"].houses > 0 || propertyInformation["DarkBlue2"].houses > 0)
            {
                ToggleMiniCard(panel, CellNameToMiniCardNumber("DarkBlue", panel));
                ToggleMiniCard(panel, CellNameToMiniCardNumber("DarkBlue2", panel));

            }
            else ToggleMiniCard(panel, cell);
            return;
        }
    }

    /// <summary>
    /// Method <c>ToggleTradingMiniCard</c> selects/unselects a trading mini card.
    /// </summary>
    /// <param name="panel">Trading panel where the card is.</param>
    /// <param name="cell">Cell position of the card.</param>
    public void ToggleTradingMiniCard(int panel, int cell) {
        if (freezeTrading) return;

        string cellName;
        if (panel == 1) cellName = tradingCells1[cell];
        else cellName = tradingCells2[cell];

        if (cellName == "Station" || cellName == "Station2" || cellName == "Station3" || cellName == "Station4" || cellName == "Electric" || cellName == "Water")
        {
            ToggleMiniCard(panel, cell);
            return;
        }
        ToggleColor(panel, cell, cellName);
    }

    /// <summary>
    /// bool function <c>CellMortgaged</c> indicates if the cell is mortgaged or not.
    /// </summary>
    /// <param name="cellName">Cell name.</param>
    /// <returns><c>True</c> if the cell is mortgaged, <c>False</c> otherwise.</returns>
    private bool CellMortgaged(string cellName) {
        if (cellName == "Electric") return electrical.mortgaged;
        if (cellName == "Water") return water.mortgaged;
        if (cellName == "Station" || cellName == "Station2" || cellName == "Station3" || cellName == "Station4") return railroadInformation[cellName].mortgaged;
        return propertyInformation[cellName].mortgaged; ;
    }

    /// <summary>
    /// Method <c>ToggleMiniCard</c> selects/unselects a card for trading.
    /// </summary>
    /// <param name="panel">Trading panel where the card is.</param>
    /// <param name="cell">Cell position of the card.</param>
    private void ToggleMiniCard(int panel, int cell) {
        if (panel == 1)
        {
            CellSelected1[cell] = !CellSelected1[cell];
            if (CellSelected1[cell]) tradePanel1.transform.Find("targeta" + cell).gameObject.GetComponent<Image>().color =  new Color32(243, 146, 55, 255);
            else if (CellMortgaged(tradingCells1[cell])) tradePanel1.transform.Find("targeta" + cell).gameObject.GetComponentInChildren<Image>().color = new Color32(131, 133, 140, 255);
            else tradePanel1.transform.Find("targeta" + cell).gameObject.GetComponent<Image>().color = new Color32(242, 242, 242, 255);
        }
        else if (panel == 2) {
            CellSelected2[cell] = !CellSelected2[cell];
            if (CellSelected2[cell]) tradePanel2.transform.Find("targeta" + cell).gameObject.GetComponent<Image>().color = new Color32(243, 146, 55, 255);
            else if (CellMortgaged(tradingCells2[cell])) tradePanel2.transform.Find("targeta" + cell).gameObject.GetComponentInChildren<Image>().color = new Color32(131, 133, 140, 255);
            else tradePanel2.transform.Find("targeta" + cell).gameObject.GetComponent<Image>().color = new Color32(242, 242, 242, 255);
        }
    }

    /// <summary>
    /// Method <c>UpdateTradingCash</c> assigns the amount of cash a player is willing to pay for a trade.
    /// </summary>
    /// <param name="panel">>Trading panel of the player.</param>
    /// <param name="amount">Amount of cash.</param>
    public void UpdateTradingCash(int panel, int amount) {
        if (panel == 1) tradingCashPlayer1 = amount;
        else tradingCashPlayer2 = amount;
    }

    /// <summary>
    /// Method <c>SetPlayer</c> sets the current player.
    /// </summary>
    /// <param name="player">Player.</param>
    public void SetPlayer(int player) {
        currentPlayer = player;
    }

    /// <summary>
    /// int function <c>GetTradigPlayer</c> indicates the player of each panel.
    /// </summary>
    /// <param name="panel"><c>1</c> left panel, <c>2</c> right panel.</param>
    /// <returns>Player of the panel.</returns>
    public int GetTradingPlayer(int panel) {
        if (panel == 2) return tradingPartner;
        return currentPlayer;
    }

    /// <summary>
    /// int function <c>CountUnownedCells</c> returns the number of cells that doesn't have an owner.
    /// </summary>
    /// <returns>Number of cells.</returns>
    public int CountUnownedCells() {
        int unowned = 0;

        foreach (Property property in propertyInformation.Values) if (property.owner == -1) unowned++;
        foreach (RailRoad railroad in railroadInformation.Values) if (railroad.owner == -1) unowned++;
        if (water.owner == -1) unowned++;
        if (electrical.owner == -1) unowned++;
        return unowned;
    }

    /// <summary>
    ///  Dictionary<string, Property> function <c>GetPropertyInformation</c> returns the property dictionary.
    /// </summary>
    /// <returns>Property dictionary</returns>
    public Dictionary<string, Property> GetPropertyInformation() {
        return propertyInformation;
    }

    /// <summary>
    /// Dictionary<string, RailRoad> function <c>GetRailroadInformation</c> returns the railroad dictionary.
    /// </summary>
    /// <returns>Railroad dictionary</returns>
    public Dictionary<string, RailRoad> GetRailroadInformation() {
        return railroadInformation;
    }

    /// <summary>
    /// Utility function <c>GetWater</c> returns the water utility.
    /// </summary>
    /// <returns>Water utility.</returns>
    public Utility GetWater() {
        return water;
    }

    /// <summary>
    /// Utility function <c>GetElectrical</c> returns the electrical utility.
    /// </summary>
    /// <returns>Electrical utility.</returns>
    public Utility GetElectrical() {
        return electrical;
    }

    /// <summary>
    /// Method <c>Update</c> Interacts with cells after clicking on them.
    /// </summary>
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        if (!justButtons && !DisableCard() && !tradingSelected)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                string cellName = hit.collider.gameObject.name;
                if (cellName != "Background") ShowCard(cellName);
            }
        }
        buySelected = false;
        sellSelected = false;
        mortgageSelected = false;
        if (tradePanel1On)
        {
            tradePanel1.SetActive(false);
            tradePanel1On = false;
            tradingSelected = false;
        }

        if (tradePanel2On)
        {
            tradePanel2.SetActive(false);
            tradePanel2On = false;
            tradingSelected = false;
        }
    }
}

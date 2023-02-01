using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;
using static CellInfo;

/// <summary>
/// Class <c>TradingPanel</c> contains methods and functions for the trading panel.
/// </summary>
public class TradingPanel : MonoBehaviour
{
    /// <summary>
    /// Panel number. <c>1</c> the panel on the left. <c>2</c> the panel on the right.
    /// </summary>
    public int panelNumber;

    /// <summary>
    /// GameObject array that contians the card instances.
    /// </summary>
    public GameObject[] card;

    /// <summary>
    /// GameObject of the input field.
    /// </summary>
    public GameObject inputFieldGameObject;

    /// <summary>
    /// GameObject of the "Cash" text.
    /// </summary>
    public GameObject cashTextGameObject;

    /// <summary>
    /// Circle that indicates the player.
    /// </summary>
    public Image circle;

    /// <summary>
    /// GameObject of GameHandler
    /// </summary>
    public GameObject scripts;

    /// <summary>
    /// Cash input field.
    /// </summary>
    private TMP_InputField inputfieldCash;

    /// <summary>
    /// CellInfo class.
    /// </summary>
    private CellInfo cellInfo;

    /// <summary>
    /// CashManagement class.
    /// </summary>
    private CashManagement cashManagement;

    /// <summary>
    /// Indicates for each card if it has been selected.
    /// </summary>
    private bool[] selected = new bool[30];
    
    /// <summary>
    /// Name of the cells the trading partner player can trade.
    /// </summary>
    private string[] tradingCells = new string[30];

    /// <summary>
    /// Amount of cash in the inputfield.
    /// </summary>
    private int cashSelected = 0;

    /// <summary>
    /// Indicates if you can modify the panel or not.
    /// </summary>
    private bool freezeTrading = false;

    /// <summary>
    /// Method <c>Initialize</c> assigns a button component to each card.
    /// </summary>
    private void Initialize()
    {
        cellInfo = scripts.GetComponent<CellInfo>();
        cashManagement = scripts.GetComponent<CashManagement>();
        inputfieldCash = inputFieldGameObject.GetComponent<TMP_InputField>();
        for (int i = 0; i < 30; ++i)
        {
            int num = i;
            Button b = card[i].AddComponent(typeof(Button)) as Button;
            b.onClick.AddListener(delegate { ToggleTradingMiniCard(num); });
        }
    }

    /// <summary>
    /// Method <c>ReadBox</c> reads the cash value from trading panel number 1.
    /// </summary>
    /// <param name="boxContent">Amount of cash indicated by the input field.</param>
    public void ReadBox(string boxContent)
    {
        if (cellInfo == null) Initialize();
        int cashOffered;
        if (int.TryParse(boxContent, out cashOffered))
        {
            if (cashOffered < 0)
            {
                cashSelected = 0;
                inputfieldCash.text = "";
                return;
            }
            int max = cashManagement.GetCash(cellInfo.GetTradingPlayer(panelNumber));
            if (cashOffered > max)
            {
                cashSelected = max;
                inputfieldCash.text = max.ToString();
                return;
            }
            cashSelected = cashOffered;
        }
        else cashSelected = 0;
    }

    /// <summary>
    /// Method <c>ShowPlayerPanel</c> shows the player panel with all properties/railroads/utilities the player owns.
    /// </summary>
    /// <param name="player">Player.</param>
    /// <param name="disableOnClick">Indicates if the panel can be disabled on click. (Useful to indicate the panel is for trading or just information).</param>
    /// <param name="propertyInformation">Property information dictionary.</param>
    /// <param name="railroadInformation">Railroad information dictionary</param>
    /// <param name="water">Water information</param>
    /// <param name="electrical">Electrical information</param>
    public void ShowPlayerPanel(int player, bool disableOnClick, Dictionary<string, Property> propertyInformation, Dictionary<string, RailRoad> railroadInformation, Utility water, Utility electrical)
    {
        if (cellInfo == null) Initialize();
        //int outofjail = movements.GetOutOfJail(player);
        for (int k = 0; k < selected.Length; ++k) selected[k] = false;
        cashSelected = 0;

        if (player == 0) circle.color = new Color32(255, 66, 66, 255);
        else if (player == 1) circle.color = new Color32(14, 121, 178, 255);
        else if (player == 2) circle.color = new Color32(242, 255, 73, 255);
        else circle.color = new Color32(106, 181, 71, 255);

        if (disableOnClick)
        {
            inputFieldGameObject.SetActive(false);
            cashTextGameObject.SetActive(false);
        }
        else
        {
            inputfieldCash.text = "";
            inputFieldGameObject.SetActive(true);
            cashTextGameObject.SetActive(true);
        }

        int i = 0;
        foreach (var property in propertyInformation) if (property.Value.owner == player) ShowMiniPropertyCard(property.Key, i++, propertyInformation);
        foreach (var railroad in railroadInformation) if (railroad.Value.owner == player) ShowMiniRailroadCard(railroad.Key, i++, railroadInformation);
        if (electrical.owner == player) ShowMiniUtilityCard("Electric", i++, electrical);
        if (water.owner == player) ShowMiniUtilityCard("Water", i++, water);
        for (int j = i; j < 30; ++j) card[j].SetActive(false);

        gameObject.SetActive(true);
    }

    /// <summary>
    /// Method <c>ShowsMiniProppertyCard</c> shows a property card to the player panel. 
    /// </summary>
    /// <param name="cellName">Name of the cell.</param>
    /// <param name="cardNumber">Position number of the card.</param>
    /// <param name="propertyInformation">Property information dictionary.</param>
    private void ShowMiniPropertyCard(string cellName, int cardNumber, Dictionary<string, Property> propertyInformation)
    {
        tradingCells[cardNumber] = cellName;
        card[cardNumber].SetActive(true);
        Property property = propertyInformation[cellName];
        card[cardNumber].GetComponent<MiniCard>().SetProperty(cellName, property.name[0], property.cost, property.houses, property.mortgaged);
    }

    /// <summary>
    /// Method <c>ShowsMiniRailroadCard</c> shows a railroad station card to the player panel.
    /// </summary>
    /// <param name="cellName">Name of the cell.</param>
    /// <param name="cardNumber">Position number of the card.</param>
    /// <param name="railroadInformation">RailRoad information dictionary.</param>
    private void ShowMiniRailroadCard(string cellName, int cardNumber, Dictionary<string, RailRoad> railroadInformation)
    {
        tradingCells[cardNumber] = cellName;
        card[cardNumber].SetActive(true);
        card[cardNumber].GetComponent<MiniCard>().SetRailRoad(railroadInformation[cellName].name[0], railroadInformation[cellName].mortgaged);
    }

    /// <summary>
    /// Method <c>ShowsMiniUtilityCard</c> shows a railroad station card to the player panel.
    /// </summary>
    /// <param name="cellName">Name of the cell.</param>
    /// <param name="cardNumber">Position number of the card.</param>
    /// <param name="utility">Utility information.</param>
    private void ShowMiniUtilityCard(string cellName, int cardNumber, Utility utility)
    {
        tradingCells[cardNumber] = cellName;
        card[cardNumber].SetActive(true);
        card[cardNumber].GetComponent<MiniCard>().SetUtility(cellName == "Water", utility.mortgaged);
    }

    /// <summary>
    /// Method <c>ToggleTradingMiniCard</c> toggles a card.
    /// </summary>
    /// <param name="cell">Number of the card.</param>
    public void ToggleTradingMiniCard(int cell)
    {
        if (freezeTrading) return;
        string cellName;
        cellName = tradingCells[cell];

        if (cellName == "Station" || cellName == "Station2" || cellName == "Station3" || cellName == "Station4" || cellName == "Electric" || cellName == "Water")
        {
            ToggleMiniCard(cell, cellInfo.CellMortgaged(cellName));
            return;
        }
        ToggleColor(cell, cellName);
    }

    /// <summary>
    /// Method <c>ToggleColor</c> changes color to selected/unselected when trading.
    /// </summary>
    /// <param name="cell">Cell position of the card.</param>
    /// <param name="cellName">Cell name position of the card.</param>
    private void ToggleColor(int cell, string cellName)
    {
        Dictionary<string, Property> propertyInformation = cellInfo.GetPropertyInformation();
        if (cellName == "Brown" || cellName == "Brown2")
        {
            if (propertyInformation["Brown"].houses > 0 || propertyInformation["Brown2"].houses > 0)
            {
                ToggleMiniCard(CellNameToMiniCardNumber("Brown"), propertyInformation["Brown"].mortgaged);
                ToggleMiniCard(CellNameToMiniCardNumber("Brown2"), propertyInformation["Brown"].mortgaged);
            }
            else ToggleMiniCard(cell, propertyInformation[cellName].mortgaged);
            return;
        }

        if (cellName == "LightBlue" || cellName == "LightBlue2" || cellName == "LightBlue3")
        {
            if (propertyInformation["LightBlue"].houses > 0 || propertyInformation["LightBlue2"].houses > 0 || propertyInformation["LightBlue3"].houses > 0)
            {
                ToggleMiniCard(CellNameToMiniCardNumber("LightBlue"), propertyInformation["LightBlue"].mortgaged);
                ToggleMiniCard(CellNameToMiniCardNumber("LightBlue2"), propertyInformation["LightBlue2"].mortgaged);
                ToggleMiniCard(CellNameToMiniCardNumber("LightBlue3"), propertyInformation["LightBlue3"].mortgaged);
            }
            else ToggleMiniCard(cell, propertyInformation[cellName].mortgaged);
            return;
        }

        if (cellName == "Purple" || cellName == "Purple2" || cellName == "Purple3")
        {
            if (propertyInformation["Purple"].houses > 0 || propertyInformation["Purple2"].houses > 0 || propertyInformation["Purple3"].houses > 0)
            {
                ToggleMiniCard(CellNameToMiniCardNumber("Purple"), propertyInformation["Purple"].mortgaged);
                ToggleMiniCard(CellNameToMiniCardNumber("Purple2"), propertyInformation["Purple2"].mortgaged);
                ToggleMiniCard(CellNameToMiniCardNumber("Purple3"), propertyInformation["Purple3"].mortgaged);
            }
            else ToggleMiniCard(cell, propertyInformation[cellName].mortgaged);
            return;
        }

        if (cellName == "Orange" || cellName == "Orange2" || cellName == "Orange3")
        {
            if (propertyInformation["Orange"].houses > 0 || propertyInformation["Orange2"].houses > 0 || propertyInformation["Orange3"].houses > 0)
            {
                ToggleMiniCard(CellNameToMiniCardNumber("Orange"), propertyInformation["Orange"].mortgaged);
                ToggleMiniCard(CellNameToMiniCardNumber("Orange2"), propertyInformation["Orange2"].mortgaged);
                ToggleMiniCard(CellNameToMiniCardNumber("Orange3"), propertyInformation["Orange3"].mortgaged);
            }
            else ToggleMiniCard(cell, propertyInformation[cellName].mortgaged);
            return;
        }

        if (cellName == "Red" || cellName == "Red2" || cellName == "Red3")
        {
            if (propertyInformation["Red"].houses > 0 || propertyInformation["Red2"].houses > 0 || propertyInformation["Red3"].houses > 0)
            {
                ToggleMiniCard(CellNameToMiniCardNumber("Red"), propertyInformation["Red"].mortgaged);
                ToggleMiniCard(CellNameToMiniCardNumber("Red2"), propertyInformation["Red2"].mortgaged);
                ToggleMiniCard(CellNameToMiniCardNumber("Red3"), propertyInformation["Red3"].mortgaged);
            }
            else ToggleMiniCard(cell, propertyInformation[cellName].mortgaged);
            return;
        }

        if (cellName == "Yellow" || cellName == "Yellow2" || cellName == "Yellow3")
        {
            if (propertyInformation["Yellow"].houses > 0 || propertyInformation["Yellow2"].houses > 0 || propertyInformation["Yellow3"].houses > 0)
            {
                ToggleMiniCard(CellNameToMiniCardNumber("Yellow"), propertyInformation["Yellow"].mortgaged);
                ToggleMiniCard(CellNameToMiniCardNumber("Yellow2"), propertyInformation["Yellow2"].mortgaged);
                ToggleMiniCard(CellNameToMiniCardNumber("Yellow3"), propertyInformation["Yellow3"].mortgaged);
            }
            else ToggleMiniCard(cell, propertyInformation[cellName].mortgaged);
            return;
        }

        if (cellName == "Green" || cellName == "Green2" || cellName == "Green3")
        {
            if (propertyInformation["Green"].houses > 0 || propertyInformation["Green2"].houses > 0 || propertyInformation["Green3"].houses > 0)
            {
                ToggleMiniCard(CellNameToMiniCardNumber("Green"), propertyInformation["Green"].mortgaged);
                ToggleMiniCard(CellNameToMiniCardNumber("Green2"), propertyInformation["Green2"].mortgaged);
                ToggleMiniCard(CellNameToMiniCardNumber("Green3"), propertyInformation["Green3"].mortgaged);
            }
            else ToggleMiniCard(cell, propertyInformation[cellName].mortgaged);
            return;
        }

        if (cellName == "DarkBlue" || cellName == "DarkBlue2")
        {
            if (propertyInformation["DarkBlue"].houses > 0 || propertyInformation["DarkBlue2"].houses > 0)
            {
                ToggleMiniCard(CellNameToMiniCardNumber("DarkBlue"), propertyInformation["DarkBlue"].mortgaged);
                ToggleMiniCard(CellNameToMiniCardNumber("DarkBlue2"), propertyInformation["DarkBlue2"].mortgaged);

            }
            else ToggleMiniCard(cell, propertyInformation[cellName].mortgaged);
            return;
        }
    }

    /// <summary>
    /// function int <c>CellNameToMiniCardNumber</c> returns the panel position where the cell card is.
    /// </summary>
    /// <param name="cellName">Cell name of the mini card.</param>
    /// <returns>Panel position</returns>
    private int CellNameToMiniCardNumber(string cellName)
    {
        for (int i = 0; i < tradingCells.Length; ++i) if (tradingCells[i] == cellName) return i;
        return -1;
    }

    /// <summary>
    /// Method <c>ToggleMiniCard</c> selects/unselects a card for trading.
    /// </summary>
    /// <param name="cell">Cell position of the card.</param>
    public void ToggleMiniCard(int cell, bool mortgaged)
    {
        selected[cell] = !selected[cell];
        card[cell].GetComponent<MiniCard>().Toggle(selected[cell], mortgaged);
    }

    /// <summary>
    /// bool <c>IsSelected</c> indicates if the card is selected.
    /// </summary>
    /// <param name="cell">Number of the card.</param>
    /// <returns><c>true</c> if the card is selected, <c>false</c> otherwise.</returns>
    public bool IsSelected(int cell) {
        return selected[cell];
    }

    /// <summary>
    /// string <c>GetCellName</c> returns the cellname.
    /// </summary>
    /// <param name="cell">Number of the card.</param>
    /// <returns>CellName of the card.</returns>
    public string GetCellName(int cell)
    {
        return tradingCells[cell];
    }

    /// <summary>
    /// int <c>GetCash</c> returns the amount of cash in the input field
    /// </summary>
    /// <returns>Amount of cash in the input field</returns>
    public int GetCash() {
        return cashSelected;
    }

    /// <summary>
    /// Method <c>SetInputFieldReadOnly</c> disables the ability to modify the trading elements of the panel.
    /// </summary>
    /// <param name="readOnly"><c>true</c> to disable the panel, <c>false</c> to enable</param>
    public void SetInputFieldReadOnly(bool readOnly) {
        inputfieldCash.GetComponent<TMP_InputField>().readOnly = readOnly;
        freezeTrading = readOnly;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>MiniCard</c> is the responsible for showing the correct information for a minicard.
/// </summary>
public class MiniCard : MonoBehaviour
{
    /// <summary>
    /// GameObject <c>colorStripe</c> contains the color stripe located on the top of the card
    /// </summary>
    public GameObject colorStripe;

    /// <summary>
    /// GameObject array <c>house</c> contains the houses icons.
    /// </summary>
    public GameObject[] house;

    /// <summary>
    /// GameObject <c>trainImage</c> contains the train icon
    /// </summary>
    public GameObject trainImage;

    /// <summary>
    /// GameObject <c>waterImage</c> contains the water tap icon
    /// </summary>
    public GameObject waterImage;

    /// <summary>
    /// GameObject <c>electricalImage</c> contains the light bulb icon.
    /// </summary>
    public GameObject electricalImage;

    /// <summary>
    /// GameObject <c>letter1</c> contains the first initial for properties.
    /// </summary>
    public GameObject letter1;

    /// <summary>
    /// GameObject <c>letter2</c> contains the first inital for railroads.
    /// </summary>
    public GameObject letter2;

    /// <summary>
    /// GameObject <c>cash1</c> contains the value text for properties
    /// </summary>
    public GameObject cash1;

    /// <summary>
    /// GameObject <c>cash2</c> contains the value text for railroads and utilities
    /// </summary>
    public GameObject cash2;

    /// <summary>
    /// Image <c>body</c> contains the background image.
    /// </summary>
    public Image body;

    /// <summary>
    /// Method <c>SetProperty</c> sets the card to the indicated property.
    /// </summary>
    /// <param name="cellName">Name of the property cell.</param>
    /// <param name="propertyInitial">First letter of the property</param>
    /// <param name="value">Value of the property</param>
    /// <param name="houseNumber">Number of houses built.</param>
    /// <param name="mortgaged">Indicates if the property is mortgaged.</param>
    public void SetProperty(string cellName, char propertyInitial, int value, int houseNumber, bool mortgaged) {
        Image stripeImage = colorStripe.GetComponentInChildren<Image>();
        letter1.SetActive(true);
        colorStripe.SetActive(true);
        cash1.SetActive(true);
        if (cellName == "Brown" || cellName == "Brown2") stripeImage.color = new Color32(142, 97, 64, 255);
        else if (cellName == "LightBlue" || cellName == "LightBlue2" || cellName == "LightBlue3") stripeImage.color = new Color32(48, 190, 217, 255);
        else if (cellName == "Purple" || cellName == "Purple2" || cellName == "Purple3") stripeImage.color = new Color32(219, 24, 174, 255);
        else if (cellName == "Orange" || cellName == "Orange2" || cellName == "Orange3") stripeImage.color = new Color32(243, 146, 55, 255);
        else if (cellName == "Red" || cellName == "Red2" || cellName == "Red3") stripeImage.color = new Color32(255, 66, 66, 255);
        else if (cellName == "Yellow" || cellName == "Yellow2" || cellName == "Yellow3") stripeImage.color = new Color32(255, 240, 0, 255);
        else if (cellName == "Green" || cellName == "Green2" || cellName == "Green3") stripeImage.color = new Color32(106, 181, 71, 255);
        else if (cellName == "DarkBlue" || cellName == "DarkBlue2") stripeImage.color = new Color32(18, 88, 219, 255);
        else return;

        letter1.GetComponentInChildren<TMP_Text>().text = propertyInitial.ToString();
        cash1.GetComponentInChildren<TMP_Text>().text = value.ToString();

        trainImage.SetActive(false);
        waterImage.SetActive(false);
        electricalImage.SetActive(false);
        letter2.SetActive(false);
        cash2.SetActive(false);

        if (houseNumber == 5) {
            letter1.GetComponentInChildren<TMP_Text>().text = "H";
            houseNumber = 0;
        }

        for (int i = 0; i < houseNumber; ++i) house[i].SetActive(true);
        for (int i = houseNumber; i < 4; ++i) house[i].SetActive(false);

        if (mortgaged) body.color = new Color32(131, 133, 140, 255);
        else body.color = new Color32(242, 242, 242, 255);
    }

    /// <summary>
    /// Method <c>SetRailRoad</c> sets the card to the indicated railroad station.
    /// </summary>
    /// <param name="railroadInitial">First letter of the station.</param>
    /// <param name="mortgaged">Indicates if the railroad is mortgaged</param>
    public void SetRailRoad(char railroadInitial, bool mortgaged) {
        letter1.SetActive(false);
        colorStripe.SetActive(false);
        cash1.SetActive(false);

        for (int i = 0; i < 4; ++i) house[i].SetActive(false);

        trainImage.SetActive(true);
        waterImage.SetActive(false);
        electricalImage.SetActive(false);

        cash2.GetComponentInChildren<TMP_Text>().text = "200";
        letter2.GetComponentInChildren<TMP_Text>().text = railroadInitial.ToString();

        letter2.SetActive(true);
        cash2.SetActive(true);

        if (mortgaged) body.color = new Color32(131, 133, 140, 255);
        else body.color = new Color32(242, 242, 242, 255);
    }

    /// <summary>
    /// Method <c>SetUtility</c> sets the card to the indicated utility.
    /// </summary>
    /// <param name="water">true if it's the water utility, false if it's the electrical utility</param>
    /// <param name="mortgaged">Indicates if the utility is mortgaged</param>
    public void SetUtility(bool water, bool mortgaged) {
        letter1.SetActive(false);
        colorStripe.SetActive(false);
        cash1.SetActive(false);

        for (int i = 0; i < 4; ++i) house[i].SetActive(false);

        trainImage.SetActive(false);
        waterImage.SetActive(water);
        electricalImage.SetActive(!water);

        cash2.GetComponentInChildren<TMP_Text>().text = "150";

        letter2.SetActive(false);
        cash2.SetActive(true);

        if (mortgaged) body.color = new Color32(120, 120, 120, 255);
        else body.color = new Color32(255, 255, 255, 255);
    }

    /// <summary>
    /// Method <c>Toggle</c> toggles the card by selecting or not.
    /// </summary>
    /// <param name="selected">Indicates if the card is selected.</param>
    /// <param name="mortgaged">Indicates if the property of the card is mortgaged.</param>
    public void Toggle(bool selected, bool mortgaged) {
        if (selected) body.color = new Color32(243, 146, 55, 255);
        else if (mortgaged) body.color = new Color32(131, 133, 140, 255);
        else body.color = new Color32(242, 242, 242, 255);
    }
}

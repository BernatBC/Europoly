using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>ChangeDiceUI</c> is the responsible for displaying the correct dice value.
/// </summary>
public class ChangeDiceUI : MonoBehaviour
{
    /// <summary>
    /// GameObject array <c>picture</c> cointains the 6 images of the dice, in order from 1 (position 0) to 6 (position 5).
    /// </summary>
    public GameObject[] picture;

    /// <summary>
    /// int <c>oldValue</c> contains the dice value of the current 
    /// </summary>
    private int oldValue = 6;

    /// <summary>
    /// int function <c>Roll</c> returns the rolled value and sets the ui.
    /// </summary>
    /// <returns>The value rolled.</returns>
    public int Roll()
    {
        picture[oldValue - 1].SetActive(false);
        int newValue = Random.Range(1, 7);
        picture[newValue - 1].SetActive(true);
        oldValue = newValue;
        return newValue;
    }
}

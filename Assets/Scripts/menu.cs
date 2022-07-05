using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{

    string k = "";
    public void Play() {
        int l;
        if (int.TryParse(k, out l)) DataHolder.initial_cash = l;
        else DataHolder.initial_cash = 2000;
        SceneManager.LoadScene("SampleScene");
    }

    public void ReadBox(string s) {
        k = s;
    }
}

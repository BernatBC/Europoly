using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{

    public Text players_text;
    public Slider slider;

    string k = "";
    int p = 2;

    private void Start()
    {
        slider.onValueChanged.AddListener((v) =>
        {
            p = Mathf.RoundToInt(v);
            players_text.text = Mathf.RoundToInt(v) + "";
        });
    }
    public void Play() {
        int l;
        if (int.TryParse(k, out l)) DataHolder.initial_cash = l;
        else DataHolder.initial_cash = 2000;
        DataHolder.n_players = p;
        SceneManager.LoadScene("SampleScene");
    }

    public void ReadBox(string s) {
        k = s;
    }
}

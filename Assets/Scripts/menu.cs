using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{

    public Text players_text;
    public Slider slider;
    public GameObject[] Player;

    string k = "";
    int p = 2;

    bool[] bot = new bool[4];

    private void Start()
    {
        for (int i = 0; i < 4; ++i) bot[i] = false;
        slider.onValueChanged.AddListener((v) =>
        {
            p = Mathf.RoundToInt(v);
            players_text.text = p + "";
            setPlayerPanels();
        });
    }
    public void Play() {
        int l;
        if (int.TryParse(k, out l))
        {
            if (l < 100) DataHolder.initial_cash = 100;
            else DataHolder.initial_cash = l;
        }
        else DataHolder.initial_cash = 2000;
        DataHolder.n_players = p;
        DataHolder.bot1 = bot[0];
        DataHolder.bot2 = bot[1];
        DataHolder.bot3 = bot[2];
        DataHolder.bot4 = bot[3];
        SceneManager.LoadScene("SampleScene");
    }

    private void setPlayerPanels() {
        if (p == 2)
        {
            Player[2].gameObject.SetActive(false);
            Player[3].gameObject.SetActive(false);
        }
        else if (p == 3)
        {
            Player[2].gameObject.SetActive(true);
            Player[3].gameObject.SetActive(false);
        }
        else {
            Player[2].gameObject.SetActive(true);
            Player[3].gameObject.SetActive(true);
        }
    }

    public void Toggle(int player) {
        bot[player] = !bot[player];
        if (bot[player])
        {
            Player[player].transform.Find("Selected").gameObject.SetActive(true);
            Player[player].transform.Find("Unselected").gameObject.SetActive(false);
        }
        else {
            Player[player].transform.Find("Selected").gameObject.SetActive(false);
            Player[player].transform.Find("Unselected").gameObject.SetActive(true);
        }
    }

    public void ReadBox(string s) {
        k = s;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bot : MonoBehaviour
{
    GameObject scripts;

    private void Start()
    {
        scripts = GameObject.Find("GameHandler");
    }

    public void RollDice() {
        //Always Roll
        Debug.Log("RollDice");

        StartCoroutine(WaitAndRoll(1));
    }

    public void BuyPass(string cell_name) {
        //Always buy if possible
        Debug.Log("Choose buy/pass property " + cell_name);

        StartCoroutine(WaitAndPay(1.5f));
    }

    public void AcceptRejectTrade() {
        //Calculate value and accept if Vafter > Vbefore
        Debug.Log("Choose accept/reject property");
    }

    public void LeaveJail(bool has_free_card) {
        //if cellsUnowned > K -> Pay (Use card if possible)
        //else roll
        Debug.Log("Choose roll/pay");

        if (scripts.GetComponent<Cell_info>().CountUnownedCells() <= 3) scripts.GetComponent<Movements>().roll_dices_doubles();
        else if (has_free_card) scripts.GetComponent<Movements>().use_card();
        else scripts.GetComponent<Movements>().pay();
    }

    public void Travel(string current, bool s1, bool s2, bool s3, bool s4) {
        //Travel to furthest station
        Debug.Log("Choose station to move/stay");

        string station = "";
        if (s4 || current == "Station4") station = "Station4";
        else if (s3 || current == "Station3") station = "Station3";
        else if (s2 || current == "Station2") station = "Station2";
        else if (s1 || current == "Station1") station = "Station1";

        if (station != current) scripts.GetComponent<Movements>().MoveTo(current, station);   
    }

    public void BeforeEndTorn() {
        //Some Strategy
        Debug.Log("End Torn");
        StartCoroutine(WaitAndEnd(1.5f));
    }

    IEnumerator WaitAndRoll(float s)
    {
        yield return new WaitForSecondsRealtime(s);
        scripts.GetComponent<Movements>().RollDice();
    }

    IEnumerator WaitAndEnd(float s)
    {
        yield return new WaitForSecondsRealtime(s);
        scripts.GetComponent<Movements>().NextTorn();
    }

    IEnumerator WaitAndPay(float s)
    {
        yield return new WaitForSecondsRealtime(s);
        scripts.GetComponent<Cell_info>().buy_property();
    }
}

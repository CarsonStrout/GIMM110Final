using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnUIScript : MonoBehaviour
{
    public GameObject PlaceGame;
    public GameObject PlaceObject1;
    public GameObject PlaceObject2;
    public GameObject Player1Turn;
    public GameObject Player2Turn;
    public GameObject Player1Wins;
    public GameObject Player2Wins;
    public GameObject TieGame;
    public GameObject controlUI;

    //Player 1
    public void PreTurn1()
    {
        controlUI.SetActive(false);
        Player1Turn.SetActive(true);
        Time.timeScale = 0f;
    }

    public void StartTurn1()
    {
        Player1Turn.SetActive(false);
        controlUI.SetActive(true);
        Time.timeScale = 1f;
    }

    //Player 2
    public void PreTurn2()
    {
        controlUI.SetActive(false);
        Player2Turn.SetActive(true);
        Time.timeScale = 0f;
    }

    public void StartTurn2()
    {
        Player2Turn.SetActive(false);
        controlUI.SetActive(true);
        Time.timeScale = 1f;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject spawnPoint;
    public ARCursor cursor;
    public TurnUIScript turnUI;
    public GameObject controlUI;

    public int turn = 0; //0 is player1 turn, 1 is player2 turn, 2 is player1 placement, 3 is player2 placement, 4 is player1 win, 5 is player2 win, 6 is tie game

    public bool player1Death = false;
    public bool player2Death = false;

    public bool player1Finish = false;
    public bool player2Finish = false;

    public int previousPlacement;

    public void PlayerTurns()
    {
        if (turn == 0) //player1 turn
        {
            player1Death = false;
            player1Finish = false;

            player2.SetActive(false);
            turnUI.PreTurn1();
            player1.transform.position = spawnPoint.transform.position;
            player1.SetActive(true);
        }
        else if (turn == 1) //player2 turn
        {
            player2Death = false;
            player2Finish = false;

            player1.SetActive(false);
            turnUI.PreTurn2();
            player2.transform.position = spawnPoint.transform.position;
            player2.SetActive(true);
        }
        else if (turn == 2) //player1 placement turn
        {
            previousPlacement = 0;
            player1.SetActive(false);
            player2.SetActive(false);
            controlUI.SetActive(false);
            turnUI.PlaceObject1.SetActive(true);
            cursor.canPlaceObstacle = true;
        }
        else if (turn == 3) //player2 placement turn
        {
            previousPlacement = 1;
            player1.SetActive(false);
            player2.SetActive(false);
            controlUI.SetActive(false);
            turnUI.PlaceObject2.SetActive(true);
            cursor.canPlaceObstacle = true;
        }
        else if (turn == 4) //player1 win
        {
            player1.SetActive(false);
            player2.SetActive(false);
            controlUI.SetActive(false);
            turnUI.Player1Wins.SetActive(true);
        }
        else if (turn == 5) //player2 win
        {
            player1.SetActive(false);
            player2.SetActive(false);
            controlUI.SetActive(false);
            turnUI.Player2Wins.SetActive(true);
        }
        else if (turn == 6) //tie game
        {
            player1.SetActive(false);
            player2.SetActive(false);
            controlUI.SetActive(false);
            turnUI.TieGame.SetActive(true);
        }

    }
}
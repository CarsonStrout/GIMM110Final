using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProgress : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Finish Checks
        if (collision.CompareTag("Finish") && gameManager.turn == 0)
        {
            gameManager.player1Finish = true;
            gameManager.turn = 1;
            gameManager.PlayerTurns();
        }
        else if (collision.CompareTag("Finish") && gameManager.turn == 1)
        {
            gameManager.player2Finish = true;

            if (gameManager.player1Death && gameManager.player2Finish)
            {
                gameManager.turn = 5;
                gameManager.PlayerTurns();
            }
            else if (gameManager.player1Finish && gameManager.player2Finish)
            {
                if (gameManager.previousPlacement == 0)
                {
                    gameManager.turn = 3;
                    gameManager.PlayerTurns();
                }
                else
                {
                    gameManager.turn = 2;
                    gameManager.PlayerTurns();
                }
            }

        }

        //Death Checks
        if (collision.CompareTag("Death") && gameManager.turn == 0)
        {
            gameManager.player1Death = true;
            gameManager.turn = 1;
            gameManager.PlayerTurns();
        }
        else if (collision.CompareTag("Death") && gameManager.turn == 1)
        {
            gameManager.player2Death = true;

            if (gameManager.player1Death && gameManager.player2Death)
            {
                gameManager.turn = 6;
                gameManager.PlayerTurns();
            }
            else if (gameManager.player1Finish && gameManager.player2Death)
            {
                gameManager.turn = 4;
                gameManager.PlayerTurns();
            }
        }
    }
}

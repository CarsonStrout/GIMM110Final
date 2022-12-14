using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThwompFollow : MonoBehaviour
{
    private Transform target1;
    private Transform target2;
    public float speed;
    public GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        if (gameManager.turn == 0)
        {
            target1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Transform>();
            transform.position = Vector2.MoveTowards(transform.position, target1.position, speed * Time.deltaTime);
        }
        else if (gameManager.turn == 1)
        {
            target2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Transform>();
            transform.position = Vector2.MoveTowards(transform.position, target2.position, speed * Time.deltaTime);
        }
    }
}

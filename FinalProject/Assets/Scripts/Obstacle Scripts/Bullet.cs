using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float xSpeed = 0f;
    public float ySpeed = 0f;

    private void Awake()
    {
        Destroy(gameObject, 1.25f);
    }

    void Update()
    {
        Vector2 position = transform.position;
        position.x += xSpeed * Time.deltaTime;
        position.y += ySpeed * Time.deltaTime;
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject, 0f);
        }
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            Destroy(gameObject, 0f);
        }
    }
}

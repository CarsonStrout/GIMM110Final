using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThwompFall : MonoBehaviour
{
    public LevelLoader levelLoader;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    public Vector3 origin;
    private float riseDelay = 2f;
    private float riseSpeed = 2f;
    private bool rising = false;
    private bool canfall = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        rb.bodyType = RigidbodyType2D.Static;
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (rising == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, origin, Time.deltaTime * riseSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (canfall == true && (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2")))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            canfall = false;
            StartCoroutine(Ryse());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player1"))
        {
            
        }
    }

    private IEnumerator Ryse()
    {
            yield return new WaitForSeconds(riseDelay);
            rb.bodyType = RigidbodyType2D.Static;
            //Debug.Log("yo mama");
            rising = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    [SerializeField] private float maxSpeed = 25f;

    private bool moveLeft;
    private bool moveRight;
    private float buttonMove;
    private bool isFacingRight = true;

    [SerializeField] private LayerMask jumpableGround;

    private enum MovementState { idle, running };

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        moveLeft = false;
        moveRight = false;
    }

    private void Update()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }

        dirX = Input.GetAxisRaw("Horizontal");

        Movement();
     
        rb.velocity = new Vector2(buttonMove, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void LeftButtonDown()
    {
        moveLeft = true;
    }

    public void LeftButtonUp()
    {
        moveLeft = false;
    }

    public void RightButtonDown()
    {
        moveRight = true;
    }

    public void RightButtonUp()
    {
        moveRight = false;
    }

    public void Movement()
    {
        MovementState state;
        Vector3 localScale = transform.localScale;

        if (moveLeft)
        {
            buttonMove = -moveSpeed;
            state = MovementState.running;
            if (isFacingRight)
            {
                isFacingRight = !isFacingRight;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }
        else if (moveRight)
        {
            buttonMove = moveSpeed;
            state = MovementState.running;
            if (!isFacingRight)
            {
                isFacingRight = !isFacingRight;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }
        else
        {
            state = MovementState.idle;
            buttonMove = 0;
        }
        anim.SetInteger("state", (int)state);
    }
    public void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region COMPONENTS
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    #endregion

    #region STATE PARAMETERS
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float maxSpeed = 25f;
    #endregion

    #region INPUT PARAMETERS
    public bool MoveLeft { get; private set; }
    public bool MoveRight { get; private set; }
    public float ButtonMove { get; private set; }
    #endregion

    #region LAYERS & TAGS
    [SerializeField] private LayerMask jumpableGround;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        MoveLeft = false;
        MoveRight = false;
    }

    private void Update()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }

        #region INPUT HANDLER
        Movement();

        rb.velocity = new Vector2(ButtonMove, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        #endregion
    }

    #region MOVEMENT METHODS
    public void LeftButtonDown()
    {
        MoveLeft = true;
    }

    public void LeftButtonUp()
    {
        MoveLeft = false;
    }

    public void RightButtonDown()
    {
        MoveRight = true;
    }

    public void RightButtonUp()
    {
        MoveRight = false;
    }

    public void Movement()
    {
        if (MoveLeft)
        {
            ButtonMove = -moveSpeed;
        }
        else if (MoveRight)
        {
            ButtonMove = moveSpeed;
        }
        else
        {
            
            ButtonMove = 0;
        }
    }
    public void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    #endregion

    #region CHECK METHODS
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    #endregion
}

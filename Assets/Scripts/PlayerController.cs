using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;
    public LayerMask whatIsGround;

    private Rigidbody2D rigidBody;
    private Transform overlapBoxTransform;

    private bool canMove;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        overlapBoxTransform = transform.Find("BoxOverlap");

        canMove = true;
    }

    void Update()
    {
        if(canMove)
        {
            float horizontalDirection = Input.GetAxisRaw("Horizontal");

            if(IsGrounded())
            {
                CheckJump();
            }

            rigidBody.velocity = new Vector2(horizontalDirection * movementSpeed, rigidBody.velocity.y);
        }
    }

    // Checks if the user is grounded. Called every frame.
    private bool IsGrounded()
    {
        Collider2D overlap = Physics2D.OverlapBox(overlapBoxTransform.position, new Vector2(0.4f, 0.02f), 0f, 
            whatIsGround);

        if(overlap != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Checks if the user pressed the jump key while grounded. Called every frame.
    private void CheckJump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}

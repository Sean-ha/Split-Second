using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed;
    public float timeToMoveValue;

    private float timeToMove;
    private bool canMove;
    private bool playerIsOnPlatform;

    private Rigidbody2D rigidBody;
    private Rigidbody2D playerRB;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerRB = FindObjectOfType<PlayerController>().gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            if(timeToMove > 0)
            {
                
                rigidBody.velocity = new Vector2(moveSpeed, 0);
                timeToMove -= Time.deltaTime;

                if (playerIsOnPlatform)
                {
                    playerRB.velocity = new Vector2(playerRB.velocity.x + moveSpeed, playerRB.velocity.y);
                }
            }
            else
            {
                rigidBody.velocity = Vector2.zero;
            }
        }
    }

    public void SetCanMove(bool value)
    {
        rigidBody.velocity = Vector2.zero;
        canMove = value;
        timeToMove = timeToMoveValue;
    }

    public void PlayerIsOnPlatform(bool value)
    {
        playerIsOnPlatform = value;
    }
}

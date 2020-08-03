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
    private GameObject clonePrefab;
    private ParticleSystem playerParticles;
    private Timer timer;

    private static bool canMove;
    private bool isJumping;
    private bool isMoving;

    private float jumpHelpTimer;

    private List<Vector3> positions;

    void Start()
    {
        clonePrefab = Resources.Load<GameObject>("Prefabs/ClonePrefab");
        rigidBody = GetComponent<Rigidbody2D>();
        timer = FindObjectOfType<Timer>();

        positions = new List<Vector3>();
        overlapBoxTransform = transform.Find("BoxOverlap");
        playerParticles = transform.Find("PlayerParticles").GetComponent<ParticleSystem>();

        jumpHelpTimer = 0.07f;
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            RecordPosition();
        }
    }

    void Update()
    {
        if(canMove)
        {
            float horizontalDirection = Input.GetAxisRaw("Horizontal");

            if(IsGrounded() || (!isJumping && jumpHelpTimer > 0))
            {
                CheckJump();
            }

            rigidBody.velocity = new Vector2(horizontalDirection * movementSpeed, rigidBody.velocity.y);

            // If the player presses R, it automatically starts a new round
            if (Input.GetKeyDown(KeyCode.R))
            {
                positions.Add(new Vector3(-20, 0, 0));
                timer.RestartLevelNewClone();
            }
        }
    }

    // Checks if the user is grounded. Called every frame.
    private bool IsGrounded()
    {
        Collider2D overlap = Physics2D.OverlapBox(overlapBoxTransform.position, new Vector2(0.4f, 0.02f), 0f, 
            whatIsGround);

        if(overlap != null)
        {
            isJumping = false;
            jumpHelpTimer = 0.07f;
            return true;
        }
        else
        {
            jumpHelpTimer -= Time.deltaTime;
            return false;
        }
    }

    // Checks if the user pressed the jump key while grounded. Called every frame.
    private void CheckJump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }
    }

    private void RecordPosition()
    {
        positions.Add(transform.position);
    }

    public static void SetCanMove(bool value)
    {
        canMove = value;
    }

    public void ResetPlayer(Vector3 initialPosition)
    {
        // Play particles on location right before returning to start pos.
        ParticleSystem particles = Instantiate(playerParticles, transform.position, Quaternion.identity);
        particles.Play();
        Destroy(particles.gameObject, 3);

        positions = new List<Vector3>();
        transform.position = initialPosition;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        playerParticles.Play();        
    }

    public void CreateClone(Vector3 initialPosition)
    {
        GameObject newClone = Instantiate(clonePrefab, initialPosition, Quaternion.identity);
        newClone.GetComponent<CloneMovement>().SetPositions(positions);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Portal"))
        {
            // Win, unlock the next level
        }
        // Moving platform
        if (collision.gameObject.layer == 12)
        {
            collision.gameObject.GetComponent<MovingPlatform>().PlayerIsOnPlatform(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Button
        if (collision.gameObject.layer == 11)
        {
            collision.gameObject.GetComponent<Button>().PushButton();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Button
        if(collision.gameObject.layer == 11)
        {
            collision.gameObject.GetComponent<Button>().UnpushButton();
        }
        else if(collision.gameObject.layer == 12)
        {
            Debug.Log("Called");
            collision.gameObject.GetComponent<MovingPlatform>().PlayerIsOnPlatform(false);
        }
    }
}

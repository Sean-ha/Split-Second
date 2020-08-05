using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;
    public float jumpLowForce;
    public LayerMask whatIsGround;

    private Rigidbody2D rigidBody;
    private Transform overlapBoxTransform;
    private GameObject clonePrefab;
    private ParticleSystem playerParticles;
    private ParticleSystem victoryParticles;
    private Timer timer;
    private GameObject pausePanel;
    private CameraShake cameraShaker;
    private PostProcessing postProcessor;

    private static bool canMove;
    private bool isJumping;
    private bool isMoving;
    public static bool isPaused;
    private static bool doneFirstMove;

    private float jumpHelpTimer;

    private List<Vector3> positions;

    private void Awake()
    {
        pausePanel = FindObjectOfType<PausePanel>().gameObject;
    }

    void Start()
    {
        clonePrefab = Resources.Load<GameObject>("Prefabs/ClonePrefab");
        rigidBody = GetComponent<Rigidbody2D>();
        timer = FindObjectOfType<Timer>();
        cameraShaker = FindObjectOfType<CameraShake>();
        postProcessor = FindObjectOfType<PostProcessing>();

        positions = new List<Vector3>();
        overlapBoxTransform = transform.Find("BoxOverlap");
        playerParticles = transform.Find("PlayerParticles").GetComponent<ParticleSystem>();
        victoryParticles = transform.Find("VictoryParticles").GetComponent<ParticleSystem>();

        jumpHelpTimer = 0.07f;
    }

    private void FixedUpdate()
    {
        if(canMove && doneFirstMove)
        {
            RecordPosition();
        }
    }

    void Update()
    {
        if(canMove)
        {
            float horizontalDirection = Input.GetAxisRaw("Horizontal");

            if(horizontalDirection != 0)
            {
                if (!doneFirstMove)
                {
                    doneFirstMove = true;
                    timer.BeginCountDown();
                }
            }

            bool grounded = IsGrounded();

            if(grounded || (!isJumping && jumpHelpTimer > 0))
            {
                CheckJump();
            }
            if(!grounded)
            {
                CheckCancelJump();
            }

            rigidBody.velocity = new Vector2(horizontalDirection * movementSpeed, rigidBody.velocity.y);

            // If the player presses R, it automatically starts a new round
            if (Input.GetKeyDown(KeyCode.R) && !isPaused)
            {
                positions.Add(new Vector3(-100, 0, 0));
                timer.RestartLevelNewClone(false);
            }
        }
        // If the user presses escape, it pauses the game.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0;
                isPaused = true;
            }
            else
            {
                pausePanel.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
            }
        }
    }

    // Checks if the user is grounded. Called every frame.
    private bool IsGrounded()
    {
        Collider2D overlap = Physics2D.OverlapBox(overlapBoxTransform.position, new Vector2(0.25f, 0.02f), 0f, 
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
            if(!doneFirstMove)
            {
                doneFirstMove = true;
                timer.BeginCountDown();
            }
            isJumping = true;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }
    }
    
    public void CheckCancelJump()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            if (rigidBody.velocity.y > jumpLowForce)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpLowForce);
            }
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

    public static void SetFirstMove(bool value)
    {
        doneFirstMove = value;
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
        cameraShaker.ShakeCamera(0.2f, .15f);
        postProcessor.BendScreen(0.2f);
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
            canMove = false;
            rigidBody.velocity = Vector2.zero;
            rigidBody.isKinematic = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            timer.StopTimer();
            victoryParticles.Play();

            cameraShaker.ShakeCamera(.75f, .15f);
            postProcessor.BendScreen(.75f);
            postProcessor.LensDistortion(1);

            // Win, unlock the next level
            collision.gameObject.GetComponent<Portal>().EnterPortal();
        }
        // Moving platform
        else if (collision.gameObject.layer == 12)
        {
            collision.gameObject.GetComponent<MovingPlatform>().PlayerIsOnPlatform(true);
        }
        // Any object that kills you
        else if(collision.gameObject.layer == 13)
        {
            positions.Add(new Vector3(-100, 0, 0));
            timer.RestartLevelNewClone(false);
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
            collision.gameObject.GetComponent<MovingPlatform>().PlayerIsOnPlatform(false);
        }
    }
}

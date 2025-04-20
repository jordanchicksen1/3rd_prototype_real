using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //player movement
    public float moveSpeed;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    //ground check for drag
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded = true;
    public float groundDrag;

    //jumping
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump = true;
    public KeyCode jumpKey = KeyCode.Space;

    //pause stuff
    public bool isPaused = false;
    public GameObject pauseScreen;

    //dodge
    public bool canDodge = true;
    public float dodgeLength = 5f;

    //coins
    public coinManager coinManager;

    //gems
    public gemManager gemManager;

    //playerHealth
    public playerHealth playerHealth;

    private void OnEnable()
    {

        // Create a new instance of the input actions
        var playerInput = new PlayerControls();

        // Enable the input actions
        playerInput.Player.Enable();

        //Subscribe to the pause
        playerInput.Player.Pause.performed += ctx => Pause();

        //Subscribe to the dodge
        playerInput.Player.Dodge.performed += ctx => Dodge();

        //Subscribe to the recentreCam
        playerInput.Player.RecentreCam.performed += ctx => RecentreCam();

        //Subscribe to the groundPound
        playerInput.Player.GroundPound.performed += ctx => GroundPound();
    }

    public void Pause()
    {
        if(isPaused == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isPaused = true;
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            Debug.Log("should pause");
        }

        else if(isPaused == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isPaused = false;
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
            Debug.Log("should unpause");
        }
    }

    public void Dodge()
    {
        if(canDodge == true)
        {
            rb.AddForce(transform.forward * dodgeLength, ForceMode.Impulse);
            canDodge = false;
            StartCoroutine(DodgeReset());
            Debug.Log("should dodge");
            //rethink this system, its kinda shit
        }
        

    }

    public void RecentreCam()
    {
        Debug.Log("should re-centre");
    }

    public void GroundPound()
    {
        Debug.Log("should ground pound");
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();
        SpeedControl();

        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        //handle drag
        if(grounded == true)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;    
        }

        //jumping 
        if(Input.GetKey(jumpKey) && readyToJump == true && grounded == true)
        {
            readyToJump = false;
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);

        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        if(grounded == true && isPaused == false)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }

        else if(grounded == false)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
    }
        

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        //limit velocity
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);   
        }
        
    }

    private void Jump()
    {
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Coin")
        {
            Destroy(other.gameObject);
            coinManager.addCoin();
            Debug.Log("collected coin ");
        }

        if (other.tag == "Gem")
        {
            Destroy(other.gameObject);
            gemManager.addGem();
            Debug.Log("collected gem");
        }

        if(other.tag == "Heart" && playerHealth.currentHealth < 5f)
        {
            Destroy(other.gameObject);
            playerHealth.PlayerHeal();
            Debug.Log("collected heart");
        }

        if(other.tag == "Crawler")
        {
            Destroy(other.gameObject);
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Crawler"))
        {
            playerHealth.PlayerHit();
        }

        
    }
    public IEnumerator DodgeReset()
    {
        yield return new WaitForSeconds(3f);
        canDodge = true;
    }
}

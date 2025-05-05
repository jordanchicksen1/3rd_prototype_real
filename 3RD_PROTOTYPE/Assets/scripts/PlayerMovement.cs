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
    public GameObject gemPic1;
    public GameObject gemPic2;
    public GameObject gemPic3;
    public GameObject gemPic4;
    public GameObject gemPic5;
    public GameObject gemPic6;
    public GameObject gemPic7;

    public bool gotGem7 = false;


    //dodge
    public bool canDodge = true;
    public float dodgeLength = 10f;

    //coins
    public coinManager coinManager;

    //gems
    public gemManager gemManager;
    public GameObject gotGemText;

    //playerHealth
    public playerHealth playerHealth;

    //boost icon
    public boostIcon boostIcon;

    //particle effects
    public ParticleSystem coinParticle;
    public ParticleSystem hitParticle;
    public ParticleSystem heartParticle;
    public ParticleSystem gemParticle;

    //sound effects
    public AudioSource sfx;
    public AudioClip ouch;
    public AudioClip coin;
    public AudioClip gem;
    public AudioClip health;
    
    public AudioSource sfx2;
    public AudioClip checkpointsfx;
    //public AudioClip enemyDeath;

    //checkpoints
    public bool checkpoint1 = false;
    public GameObject checkpoint1Gem;
    public bool checkpoint2 = false;
    public GameObject checkpoint2Gem;
    public bool checkpoint3 = false;
    public GameObject checkpoint3Gem;

    public GameObject checkpointTex;
    //game end
    public GameObject levelEndScreen;
    public AudioClip celebrationSFX;

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
            //rb.velocity = new Vector3(0f, 0f, 0f);
           // rb.AddForce(transform.forward * dodgeLength, ForceMode.Impulse);
            canDodge = false;
            moveSpeed = dodgeLength;
            boostIcon.UseBoost();
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
            rb.AddForce(moveDirection.normalized * moveSpeed *5f, ForceMode.Force);
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
            coinParticle.Play();
            sfx.clip = coin;
            sfx.Play();
        }

        if (other.tag == "Gem")
        {
            Destroy(other.gameObject);
            gemManager.addGem();
            Debug.Log("collected gem");
            StartCoroutine(GotGem());
            gemParticle.Play();
            sfx.clip = gem;
            sfx.Play();
        }

        if(other.tag == "Heart" && playerHealth.currentHealth < 5f)
        {
            Destroy(other.gameObject);
            playerHealth.PlayerHeal();
            Debug.Log("collected heart");
            heartParticle.Play();
            sfx.clip = health;
            sfx.Play();
        }

        if(other.tag == "Crawler")
        {
            Destroy(other.gameObject);
        }

        if(other.tag == "Checkpoint1" && checkpoint1 == false)
        {
            checkpoint1 = true;
            checkpoint1Gem.SetActive(true);
            checkpoint2 = false;
            checkpoint2Gem.SetActive(false);
            checkpoint3 = false;
            checkpoint3Gem.SetActive(false);
            StartCoroutine(CheckpointSet());
            sfx2.clip = checkpointsfx;
            sfx2.Play();
        }

        if (other.tag == "Checkpoint2" && checkpoint2 == false)
        {
            checkpoint1 = false;
            checkpoint1Gem.SetActive(false);
            checkpoint2 = true; 
            checkpoint2Gem.SetActive(true);
            checkpoint3 = false;
            checkpoint3Gem.SetActive(false);
            StartCoroutine(CheckpointSet());
            
            sfx2.clip = checkpointsfx;
            sfx2.Play();
        }

        if (other.tag == "Checkpoint3" && checkpoint3 == false)
        {
            checkpoint1 = false;
            checkpoint1Gem.SetActive(false);
            checkpoint2 = false;
            checkpoint2Gem.SetActive(false);
            checkpoint3 = true;
            checkpoint3Gem.SetActive(true);
            StartCoroutine(CheckpointSet());
            sfx2.clip = checkpointsfx;
            sfx2.Play();
        }

        if (other.tag == "KillBox" && checkpoint1 == false && checkpoint2 == false && checkpoint3 == false)
        {
            transform.position = new Vector3(457.3f, 551.1f, -473.3f);
            playerHealth.PlayerHit();
            sfx.clip = ouch;
            sfx.Play();
        }

        if (other.tag == "KillBox" && checkpoint1 == true && checkpoint2 == false && checkpoint3 == false)
        {
            transform.position = new Vector3(457.6f, 549f, -407f);
            playerHealth.PlayerHit();
            sfx.clip = ouch;
            sfx.Play();
        }

        if (other.tag == "KillBox" && checkpoint2 == true && checkpoint1 == false && checkpoint3 == false)
        {
            transform.position = new Vector3(439.78f, 579.19f, -298.66f);
            playerHealth.PlayerHit();
            sfx.clip = ouch;
            sfx.Play();
        }

        if (other.tag == "LevelEnd")
        {
            levelEndScreen.SetActive(true);
            sfx.clip = celebrationSFX;
            sfx.Play();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        //gem on triggers to make them disappear from map
        if((other.tag == "Gem1"))
        {
            Destroy(gemPic1);
            Destroy(other.gameObject);
        }

        if ((other.tag == "Gem2"))
        {
            Destroy(gemPic2);
            Destroy(other.gameObject);
        }

        if ((other.tag == "Gem3"))
        {
            Destroy(gemPic3);
            Destroy(other.gameObject);
        }

        if ((other.tag == "Gem4"))
        {
            Destroy(gemPic4);
            Destroy(other.gameObject);
        }

        if ((other.tag == "Gem5"))
        {
            Destroy(gemPic5);
            Destroy(other.gameObject);
        }

        if ((other.tag == "Gem6"))
        {
            Destroy(gemPic6);
            Destroy(other.gameObject);
        }

        if ((other.tag == "Gem7"))
        {
            gotGem7 = true;
            Destroy(gemPic7);
            Destroy(other.gameObject);
            Debug.Log("should turn off picture");
        }

    }

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Crawler"))
        {
            playerHealth.PlayerHit();
            hitParticle.Play();
            sfx.clip = ouch;
            sfx.Play();
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("hit player");
            playerHealth.PlayerHit();
            Destroy(other.gameObject);
            hitParticle.Play();
            sfx.clip = ouch;
            sfx.Play();
        }

        
    }
    public IEnumerator DodgeReset()
    {
        yield return new WaitForSeconds(0.5f);
        moveSpeed = 7f;
        boostIcon.shouldFillBar = true;
        yield return new WaitForSeconds(3f);
        canDodge = true;
    }

    public IEnumerator GotGem()
    {
        yield return new WaitForSeconds(0f);
        gotGemText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        gotGemText.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        gotGemText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        gotGemText.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        gotGemText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        gotGemText.SetActive(false);
    }

    public IEnumerator CheckpointSet()
    {
        yield return new WaitForSeconds(0f);
        checkpointTex.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        checkpointTex.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        checkpointTex.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        checkpointTex.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        checkpointTex.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        checkpointTex.SetActive(false);
    }
}

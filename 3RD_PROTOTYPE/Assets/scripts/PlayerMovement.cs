using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
    public ParticleSystem redCoinParticle;

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

    public bool isAtHub = true;
    public bool isAtGrassLevel = false;
    public bool isAtAutumnLevel = false;
    public bool isAtCaveLevel = false;

    public GameObject checkpointTex;

   public GameObject coinSurpriseText;

    //game end
    public GameObject levelEndScreen;
    public AudioClip celebrationSFX;

    //shop stuff
    public GameObject shop1Panel;
    public GameObject shop2Panel;
    public GameObject shop3Panel;

    //hat stand stuff
    public GameObject hatStandPanel;
   

    //pickaxe and mining stuff
    public bool hasPickaxe = false;
    public GameObject pickaxe;
    public GameObject gotPickaxeText;
    public ParticleSystem gotPickaxeParticle;
    public GameObject gemPieceMeter;
    public GameObject mineText;
    public bool readyToMine = false;
    public GameObject needTool;

    //gem pieces
    public Transform playerNose;
    public float mineRange = 1f;
    public gemPieceMeter gemPieceMeterScript;
    public AudioClip gemDrop;

    //search leaf baskets
    public GameObject searchText;
    public GameObject foundCoinText;
    public GameObject found5CoinsText;
    public GameObject foundRedCoinText;

    //grass red coins
    public bool grassR1 = false;
    public bool grassR2 = false;
    public bool grassR3 = false;
    public bool grassR4 = false;
    public bool grassR5 = false;
    public bool grassR6 = false;
    public bool grassR7 = false;
    public bool grassR8 = false;

    //maple red coins
    public bool mapleR1 = false;
    public bool mapleR2 = false;
    public bool mapleR3 = false;
    public bool mapleR4 = false;
    public bool mapleR5 = false;
    public bool mapleR6 = false;
    public bool mapleR7 = false;
    public bool mapleR8 = false;

    //gem gate stuff
    public GameObject payGemPage;
    public GameObject payGemPage2;
    public GameObject payGemPage3;


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

        //Subscribe to the Mine
        playerInput.Player.Mine.performed += ctx => Mine();

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

    public void Mine()
    {
        Ray ray = new Ray(playerNose.position, playerNose.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, mineRange))
        {
            if (hit.collider.CompareTag("GemPiece") && hasPickaxe == true)
            {
                Destroy(hit.collider.gameObject, 0.5f);
                StartCoroutine(PickaxeHit());
                gemPieceMeterScript.GotGemPiece();
                sfx2.clip = gemDrop;
                sfx2.Play();
            }

            if (hit.collider.CompareTag("GemPiece") && hasPickaxe == false)
            {
                StartCoroutine(NeedTool());
            }

            if (hit.collider.CompareTag("LeafCoin"))
            {
                Destroy(hit.collider);
                coinManager.addCoin();
                coinParticle.Play();
                sfx.clip = coin;
                sfx.Play();
                StartCoroutine(GotCoin());
            }

            if (hit.collider.CompareTag("Leaf5Coin"))
            {
                Destroy(hit.collider);
                coinManager.Add5Coins();
                coinParticle.Play();
                sfx.clip = coin;
                sfx.Play();
                StartCoroutine(Got5Coins());
            }

            if (hit.collider.CompareTag("LeafRedCoin"))
            {
                Destroy(hit.collider);
                coinManager.addCoin();
                redCoinParticle.Play();
                mapleR1 = true;
                sfx.clip = coin;
                sfx.Play();
                StartCoroutine(GotRedCoin());
            }

            if (hit.collider.CompareTag("LeafRedCoin2"))
            {
                Destroy(hit.collider);
                coinManager.addCoin();
                redCoinParticle.Play();
                mapleR2 = true;
                sfx.clip = coin;
                sfx.Play();
                StartCoroutine(GotRedCoin());
            }

            if (hit.collider.CompareTag("LeafRedCoin3"))
            {
                Destroy(hit.collider);
                coinManager.addCoin();
                redCoinParticle.Play();
                mapleR3 = true;
                sfx.clip = coin;
                sfx.Play();
                StartCoroutine(GotRedCoin());
            }

        }
    }

    public void checkForGemPiece()
    {
        Ray ray = new Ray(playerNose.position, playerNose.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, mineRange))
        {
            if (hit.collider.CompareTag("GemPiece"))
            {
                mineText.SetActive(true);
            }

            if (hit.collider.CompareTag("LeafCoin"))
            {
                searchText.SetActive(true);
            }

            if (hit.collider.CompareTag("Leaf5Coin"))
            {
                searchText.SetActive(true);
            }

            if (hit.collider.CompareTag("LeafRedCoin"))
            {
                searchText.SetActive(true);
            }

            if (hit.collider.CompareTag("LeafRedCoin2"))
            {
                searchText.SetActive(true);
            }

            if (hit.collider.CompareTag("LeafRedCoin3"))
            {
                searchText.SetActive(true);
            }
        }

        else
        {
            mineText.SetActive(false);
            searchText.SetActive(false);
        }
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

        checkForGemPiece();
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

        //checkpoints in grass level
        if(other.tag == "Checkpoint1" && checkpoint1 == false)
        {
            checkpoint1 = true;
            checkpoint1Gem.SetActive(true);
            checkpoint2 = false;
            checkpoint2Gem.SetActive(false);
            checkpoint3 = false;
            checkpoint3Gem.SetActive(false);
            isAtGrassLevel = false;
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
            isAtGrassLevel = false;
            StartCoroutine(CheckpointSet());
            
            sfx2.clip = checkpointsfx;
            sfx2.Play();
        }

        //checkpoints in autumn level
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

        if (other.tag == "KillBox" && isAtHub == true)
        {
            transform.position = new Vector3(529.469971f, 528.460022f, -7326.3999f);
            playerHealth.PlayerHit();
            sfx.clip = ouch;
            sfx.Play();
            hitParticle.Play();
        }

        if (other.tag == "KillBox" && isAtGrassLevel == true)
        {
            transform.position = new Vector3(457.3f, 551.1f, -473.3f);
            playerHealth.PlayerHit();
            sfx.clip = ouch;
            sfx.Play();
            hitParticle.Play();
        }


        if (other.tag == "KillBox" && checkpoint1 == true)
        {
            transform.position = new Vector3(457.6f, 549f, -407f);
            playerHealth.PlayerHit();
            sfx.clip = ouch;
            sfx.Play();
            hitParticle.Play();
        }

        if (other.tag == "KillBox" && checkpoint2 == true)
        {
            transform.position = new Vector3(439.78f, 579.19f, -298.66f);
            playerHealth.PlayerHit();
            sfx.clip = ouch;
            sfx.Play();
            hitParticle.Play();
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
            Destroy(coinSurpriseText);
            Debug.Log("should turn off picture");
        }

        if (other.tag == "ShopTrigger1")
        {
            Debug.Log("in shop");
            shop1Panel.SetActive(true);
            shop2Panel.SetActive(false);
            shop3Panel.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (other.tag == "ShopTrigger2")
        {
            Debug.Log("in shop");
            shop2Panel.SetActive(true);
            shop1Panel.SetActive(false);
            shop3Panel.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (other.tag == "ShopTrigger3")
        {
            Debug.Log("in shop");
            shop3Panel.SetActive(true);
            shop2Panel.SetActive(false);
            shop1Panel.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if(other.tag == "Pickaxe")
        {
            Debug.Log("got pickaxe");
            Destroy(other.gameObject);
            hasPickaxe = true;
            StartCoroutine(GotPickaxe());
            gotPickaxeParticle.Play();
            gemPieceMeter.SetActive(true);
        }

        if (other.tag == "GemPiece")
        {
            readyToMine = true;
            mineText.SetActive(true);
        }

        //grass level red coins
        if(other.tag == "GrassRedOne")
        {
            Destroy(other.gameObject);
            coinManager.addCoin();
            Debug.Log("collected red coin ");
            redCoinParticle.Play();
            sfx.clip = coin;
            sfx.Play();
            grassR1 = true;
        }

        if (other.tag == "GrassRedTwo")
        {
            Destroy(other.gameObject);
            coinManager.addCoin();
            Debug.Log("collected red coin ");
            redCoinParticle.Play();
            sfx.clip = coin;
            sfx.Play();
            grassR2 = true;
        }

        if (other.tag == "GrassRedThree")
        {
            Destroy(other.gameObject);
            coinManager.addCoin();
            Debug.Log("collected red coin ");
            redCoinParticle.Play();
            sfx.clip = coin;
            sfx.Play();
            grassR3 = true;
        }

        if (other.tag == "GrassRedFour")
        {
            Destroy(other.gameObject);
            coinManager.addCoin();
            Debug.Log("collected coin ");
            redCoinParticle.Play();
            sfx.clip = coin;
            sfx.Play();
            grassR4 = true;
        }

        if (other.tag == "GrassRedFive")
        {
            Destroy(other.gameObject);
            coinManager.addCoin();
            Debug.Log("collected coin ");
            redCoinParticle.Play();
            sfx.clip = coin;
            sfx.Play();
            grassR5 = true;
        }

        if (other.tag == "GrassRedSix")
        {
            Destroy(other.gameObject);
            coinManager.addCoin();
            Debug.Log("collected coin ");
            redCoinParticle.Play();
            sfx.clip = coin;
            sfx.Play();
            grassR6 = true;
        }

        if (other.tag == "GrassRedSeven")
        {
            Destroy(other.gameObject);
            coinManager.addCoin();
            Debug.Log("collected coin ");
            redCoinParticle.Play();
            sfx.clip = coin;
            sfx.Play();
            grassR7 = true;
        }

        if (other.tag == "GrassRedEight")
        {
            Destroy(other.gameObject);
            coinManager.addCoin();
            Debug.Log("collected coin ");
            redCoinParticle.Play();
            sfx.clip = coin;
            sfx.Play();
            grassR8 = true;
        }

        if((other.tag == "HatStand"))
        {
            hatStandPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if((other.tag == "PayGemTrigger"))
        {
            payGemPage.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if ((other.tag == "PayGemTrigger2"))
        {
            payGemPage2.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if ((other.tag == "PayGemTrigger3"))
        {
            payGemPage3.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "ShopTrigger1")
        {
            Debug.Log("out of shop");
            shop1Panel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (other.tag == "ShopTrigger2")
        {
            Debug.Log("out of shop");
            shop2Panel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (other.tag == "ShopTrigger3")
        {
            Debug.Log("out of shop");
            shop3Panel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (other.tag == "GemPiece")
        {
            readyToMine = false;
            mineText.SetActive(false);
        }

        if(other.tag == "HatStand")
        {
            hatStandPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if ((other.tag == "PayGemTrigger"))
        {
            payGemPage.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if ((other.tag == "PayGemTrigger2"))
        {
            payGemPage2.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if ((other.tag == "PayGemTrigger3"))
        {
            payGemPage3.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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

        if (other.gameObject.CompareTag("Stalagmite"))
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

    public IEnumerator GotPickaxe()
    {
        yield return new WaitForSeconds(0f);
        gotPickaxeText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        gotPickaxeText.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        gotPickaxeText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        gotPickaxeText.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        gotPickaxeText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        gotPickaxeText.SetActive(false);
    }

    public IEnumerator NeedTool()
    {
        yield return new WaitForSeconds(0f);
        needTool.SetActive(true);
        yield return new WaitForSeconds(2f);
        needTool.SetActive(false);
    }

    public IEnumerator PickaxeHit()
    {
        yield return new WaitForSeconds(0f);
        pickaxe.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        pickaxe.SetActive(false);
    }

    public IEnumerator GotCoin()
    {
        yield return new WaitForSeconds(0f);
        foundCoinText.SetActive(true);
        yield return new WaitForSeconds(2f);
        foundCoinText.SetActive(false);
    }

    public IEnumerator Got5Coins()
    {
        yield return new WaitForSeconds(0f);
        found5CoinsText.SetActive(true);
        yield return new WaitForSeconds(2f);
        found5CoinsText.SetActive(false);
    }

    public IEnumerator GotRedCoin()
    {
        yield return new WaitForSeconds(0f);
        foundRedCoinText.SetActive(true);
        yield return new WaitForSeconds(2f);
        foundRedCoinText.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class payTrigger : MonoBehaviour
{
    public bool paid = false;
    public bool isInPayingTrigger = false;

    public GameObject gate;
    public GameObject payingInstructions;
    public GameObject payingIntructionsButton;
    public GameObject notEnoughMoney;
    public GameObject thanksForBusinness;

    public coinManager coinManager;

    public gemPieceMeter gemPieceMeter;

    public AudioSource sfx;
    public AudioClip moneySFx;
    public AudioClip deniedSFX;

    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            payingInstructions.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Pay()
    {
        if(gemPieceMeter.currentGemPiece > 3.99)
        {
            sfx.clip = moneySFx;
            sfx.Play();
            gemPieceMeter.SubtractGemPiece();
            thanksForBusinness.SetActive(true);
            payingIntructionsButton.SetActive(false);
            Destroy(gate);
            
        }

        if(gemPieceMeter.currentGemPiece < 3.99)
        {
            sfx.clip = deniedSFX;
            sfx.Play();
            payingInstructions.SetActive(false);
            notEnoughMoney.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            payingInstructions.SetActive(false);
            notEnoughMoney.SetActive(false);
            thanksForBusinness.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
} 

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
        if(coinManager.coin > 14.99)
        {
            coinManager.subtractCoin();
            thanksForBusinness.SetActive(true);
            payingIntructionsButton.SetActive(false);
            Destroy(gate);
            
        }

        if(coinManager.coin < 14.99)
        {
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

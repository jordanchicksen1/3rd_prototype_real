using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopOneButtons : MonoBehaviour
{
    public coinManager coinManager;
    public gemManager gemManager;
    public playerHealth playerHealth;


    public GameObject hint;
    public GameObject grassyHat;
    public GameObject grassyHatDisplay;
    public GameObject gemDisplay;

    public GameObject buyButtonOne;
    public GameObject buyButtonFour;

    public GameObject itemPurchased;
    public GameObject tooPoor;
    public GameObject healthFull;

    public AudioSource sfx;
    public AudioClip purchaseSfx;
    public AudioClip purchaseDenied;

    public bool purchaseGoneThrough = false;

    public void buyItemOne()
    {
        if(coinManager.coin > 24.99 && purchaseGoneThrough == false)
        {
            coinManager.boughtItemOne();
            gemManager.addGem();
            gemDisplay.SetActive(false);
            buyButtonOne.SetActive(false);
            StartCoroutine(ItemPurchased());
            tooPoor.SetActive(false);
            healthFull.SetActive(false);
            sfx.clip = purchaseSfx;
            sfx.Play();
            purchaseGoneThrough = true;
        }

        if(coinManager.coin < 24.99 && purchaseGoneThrough == false)
        {
            StartCoroutine(TooPoor());
            sfx.clip = purchaseDenied;
            itemPurchased.SetActive(false);
            healthFull.SetActive(false);
            sfx.Play();
        }
    }
    public void buyItemTwo() 
    {
        if (coinManager.coin > 4.99 && playerHealth.currentHealth < 4.99 && purchaseGoneThrough == false)
        {
            coinManager.boughtItemTwo();
            playerHealth.PlayerHeal();
            StartCoroutine(ItemPurchased());
            tooPoor.SetActive(false);
            healthFull.SetActive(false);
            sfx.clip = purchaseSfx;
            sfx.Play();
            purchaseGoneThrough = true;
        }

        if(playerHealth.currentHealth < 4.99 &&  coinManager.coin < 4.99 && purchaseGoneThrough == false)
        {
            StartCoroutine(TooPoor());
            sfx.clip = purchaseDenied;
            itemPurchased.SetActive(false);
            healthFull.SetActive(false);
            sfx.Play();
        }

        if(playerHealth.currentHealth > 4.99 && coinManager.coin > 4.99 && purchaseGoneThrough == false)
        {
            StartCoroutine(HealthFull());
            sfx.clip = purchaseDenied;
            itemPurchased.SetActive(false);
            tooPoor.SetActive(false);
            sfx.Play();
        }

        if (playerHealth.currentHealth > 4.99 && coinManager.coin < 4.99 && purchaseGoneThrough == false)
        {
            StartCoroutine(HealthFull());
            sfx.clip = purchaseDenied;
            itemPurchased.SetActive(false);
            tooPoor.SetActive(false);
            sfx.Play();
        }


    }
    public void buyItemThree() 
    {
        if (coinManager.coin > 4.99 && purchaseGoneThrough == false)
        {
            coinManager.boughtItemThree();
            hint.SetActive(true);
            StartCoroutine(ItemPurchased());
            tooPoor.SetActive(false);
            healthFull.SetActive(false);
            sfx.clip = purchaseSfx;
            sfx.Play();
            purchaseGoneThrough = true;
        }

        if (coinManager.coin < 4.99 && purchaseGoneThrough == false)
        {
            StartCoroutine(TooPoor());
            sfx.clip = purchaseDenied;
            itemPurchased.SetActive(false);
            healthFull.SetActive(false);
            sfx.Play();
        }
    }
    public void buyItemFour() 
    {
        if (coinManager.coin > 9.99 && purchaseGoneThrough == false)
        {
            coinManager.boughtItemFour();
            grassyHat.SetActive(true);
            grassyHatDisplay.SetActive(false);
            buyButtonFour.SetActive(false);
            StartCoroutine(ItemPurchased());
            tooPoor.SetActive(false);
            healthFull.SetActive(false);
            sfx.clip = purchaseSfx;
            sfx.Play();
            purchaseGoneThrough = true;
        }

        if (coinManager.coin < 9.99 && purchaseGoneThrough == false)
        {
            StartCoroutine(TooPoor());
            sfx.clip = purchaseDenied;
            itemPurchased.SetActive(false);
            healthFull.SetActive(false);
            sfx.Play();
        }
    }

    public IEnumerator ItemPurchased()
    {
        yield return new WaitForSeconds(0f);
        itemPurchased.SetActive(true);
        yield return new WaitForSeconds(3f);
        itemPurchased.SetActive(false);
        purchaseGoneThrough = false;
    }

    public IEnumerator TooPoor()
    {
        yield return new WaitForSeconds(0f);
        tooPoor.SetActive(true);
        yield return new WaitForSeconds(3f);
        tooPoor.SetActive(false);
    }

    public IEnumerator HealthFull()
    {
        yield return new WaitForSeconds(0f);
        healthFull.SetActive(true);
        yield return new WaitForSeconds(3f);
        healthFull.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopThreeManager : MonoBehaviour
{
    public coinManager coinManager;
    public gemManager gemManager;
    public playerHealth playerHealth;


    public GameObject hint3;
    public GameObject grassyHat;
    public GameObject hardHat;
    public GameObject beanie;
    public GameObject hardHatDisplay;
    public GameObject gemDisplay;

    public GameObject buyButtonOne;
    public GameObject buyButtonFour;

    public GameObject itemPurchased3;
    public GameObject tooPoor3;
    public GameObject healthFull3;

    public AudioSource sfx;
    public AudioClip purchaseSfx;
    public AudioClip purchaseDenied;

    public bool purchaseGoneThrough = false;

    public void buyItemOne()
    {
        if (coinManager.coin > 24.99 && purchaseGoneThrough == false)
        {
            coinManager.boughtItemOne();
            gemManager.addGem();
            gemDisplay.SetActive(false);
            buyButtonOne.SetActive(false);
            StartCoroutine(ItemPurchased());
            tooPoor3.SetActive(false);
            healthFull3.SetActive(false);
            sfx.clip = purchaseSfx;
            sfx.Play();
            purchaseGoneThrough = true;
        }

        if (coinManager.coin < 24.99 && purchaseGoneThrough == false)
        {
            StartCoroutine(TooPoor());
            sfx.clip = purchaseDenied;
            itemPurchased3.SetActive(false);
            healthFull3.SetActive(false);
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
            tooPoor3.SetActive(false);
            healthFull3.SetActive(false);
            sfx.clip = purchaseSfx;
            sfx.Play();
            purchaseGoneThrough = true;
        }

        if (playerHealth.currentHealth < 4.99 && coinManager.coin < 4.99 && purchaseGoneThrough == false)
        {
            StartCoroutine(TooPoor());
            sfx.clip = purchaseDenied;
            itemPurchased3.SetActive(false);
            healthFull3.SetActive(false);
            sfx.Play();
        }

        if (playerHealth.currentHealth > 4.99 && coinManager.coin > 4.99 && purchaseGoneThrough == false)
        {
            StartCoroutine(HealthFull());
            sfx.clip = purchaseDenied;
            itemPurchased3.SetActive(false);
            tooPoor3.SetActive(false);
            sfx.Play();
        }

        if (playerHealth.currentHealth > 4.99 && coinManager.coin < 4.99 && purchaseGoneThrough == false)
        {
            StartCoroutine(HealthFull());
            sfx.clip = purchaseDenied;
            itemPurchased3.SetActive(false);
            tooPoor3.SetActive(false);
            sfx.Play();
        }


    }
    public void buyItemThree()
    {
        if (coinManager.coin > 4.99 && purchaseGoneThrough == false)
        {
            coinManager.boughtItemThree();
            hint3.SetActive(true);
            StartCoroutine(ItemPurchased());
            tooPoor3.SetActive(false);
            healthFull3.SetActive(false);
            sfx.clip = purchaseSfx;
            sfx.Play();
            purchaseGoneThrough = true;
        }

        if (coinManager.coin < 4.99 && purchaseGoneThrough == false)
        {
            StartCoroutine(TooPoor());
            sfx.clip = purchaseDenied;
            itemPurchased3.SetActive(false);
            healthFull3.SetActive(false);
            sfx.Play();
        }
    }
    public void buyItemFour()
    {
        if (coinManager.coin > 9.99 && purchaseGoneThrough == false)
        {
            coinManager.boughtItemFour();
            beanie.SetActive(false);
            grassyHat.SetActive(false);
            hardHat.SetActive(true);
            hardHatDisplay.SetActive(false);
            buyButtonFour.SetActive(false);
            StartCoroutine(ItemPurchased());
            tooPoor3.SetActive(false);
            healthFull3.SetActive(false);
            sfx.clip = purchaseSfx;
            sfx.Play();
            purchaseGoneThrough = true;
        }

        if (coinManager.coin < 9.99 && purchaseGoneThrough == false)
        {
            StartCoroutine(TooPoor());
            sfx.clip = purchaseDenied;
            itemPurchased3.SetActive(false);
            healthFull3.SetActive(false);
            sfx.Play();
        }
    }

    public IEnumerator ItemPurchased()
    {
        yield return new WaitForSeconds(0f);
        itemPurchased3.SetActive(true);
        yield return new WaitForSeconds(3f);
        itemPurchased3.SetActive(false);
        purchaseGoneThrough = false;
    }

    public IEnumerator TooPoor()
    {
        yield return new WaitForSeconds(0f);
        tooPoor3.SetActive(true);
        yield return new WaitForSeconds(3f);
        tooPoor3.SetActive(false);
    }

    public IEnumerator HealthFull()
    {
        yield return new WaitForSeconds(0f);
        healthFull3.SetActive(true);
        yield return new WaitForSeconds(3f);
        healthFull3.SetActive(false);
    }
}

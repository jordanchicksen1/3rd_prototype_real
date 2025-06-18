using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopTwoManager : MonoBehaviour
{
    public coinManager coinManager;
    public gemManager gemManager;
    public playerHealth playerHealth;


    public GameObject hint2;
    public GameObject grassyHat;
    public GameObject hardHat;
    public GameObject beanie;
    public GameObject beanieDisplay;
    public GameObject gemDisplay;

    public GameObject buyButtonOne;
    public GameObject buyButtonFour;

    public GameObject itemPurchased2;
    public GameObject tooPoor2;
    public GameObject healthFull2;

    public AudioSource sfx;
    public AudioClip purchaseSfx;
    public AudioClip purchaseDenied;

    public bool purchaseGoneThrough = false;

    public hatStandMenu hatStandMenu;
    public void buyItemOne()
    {
        if (coinManager.coin > 24.99 && purchaseGoneThrough == false)
        {
            coinManager.boughtItemOne();
            gemManager.addGem();
            gemDisplay.SetActive(false);
            buyButtonOne.SetActive(false);
            StartCoroutine(ItemPurchased());
            tooPoor2.SetActive(false);
            healthFull2.SetActive(false);
            sfx.clip = purchaseSfx;
            sfx.Play();
            purchaseGoneThrough = true;
        }

        if (coinManager.coin < 24.99 && purchaseGoneThrough == false)
        {
            StartCoroutine(TooPoor());
            sfx.clip = purchaseDenied;
            itemPurchased2.SetActive(false);
            healthFull2.SetActive(false);
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
            tooPoor2.SetActive(false);
            healthFull2.SetActive(false);
            sfx.clip = purchaseSfx;
            sfx.Play();
            purchaseGoneThrough = true;
        }

        if (playerHealth.currentHealth < 4.99 && coinManager.coin < 4.99 && purchaseGoneThrough == false)
        {
            StartCoroutine(TooPoor());
            sfx.clip = purchaseDenied;
            itemPurchased2.SetActive(false);
            healthFull2.SetActive(false);
            sfx.Play();
        }

        if (playerHealth.currentHealth > 4.99 && coinManager.coin > 4.99 && purchaseGoneThrough == false)
        {
            StartCoroutine(HealthFull());
            sfx.clip = purchaseDenied;
            itemPurchased2.SetActive(false);
            tooPoor2.SetActive(false);
            sfx.Play();
        }

        if (playerHealth.currentHealth > 4.99 && coinManager.coin < 4.99 && purchaseGoneThrough == false)
        {
            StartCoroutine(HealthFull());
            sfx.clip = purchaseDenied;
            itemPurchased2.SetActive(false);
            tooPoor2.SetActive(false);
            sfx.Play();
        }


    }
    public void buyItemThree()
    {
        if (coinManager.coin > 4.99 && purchaseGoneThrough == false)
        {
            coinManager.boughtItemThree();
            hint2.SetActive(true);
            StartCoroutine(ItemPurchased());
            tooPoor2.SetActive(false);
            healthFull2.SetActive(false);
            sfx.clip = purchaseSfx;
            sfx.Play();
            purchaseGoneThrough = true;
        }

        if (coinManager.coin < 4.99 && purchaseGoneThrough == false)
        {
            StartCoroutine(TooPoor());
            sfx.clip = purchaseDenied;
            itemPurchased2.SetActive(false);
            healthFull2.SetActive(false);
            sfx.Play();
        }
    }
    public void buyItemFour()
    {
        if (coinManager.coin > 9.99 && purchaseGoneThrough == false)
        {
            coinManager.boughtItemFour();
            beanie.SetActive(true);
            grassyHat.SetActive(false);
            hardHat.SetActive(false);
            beanieDisplay.SetActive(false);
            buyButtonFour.SetActive(false);
            StartCoroutine(ItemPurchased());
            tooPoor2.SetActive(false);
            healthFull2.SetActive(false);
            sfx.clip = purchaseSfx;
            sfx.Play();
            purchaseGoneThrough = true;
            hatStandMenu.boughtBeanie();
            gemManager.addHat();
        }

        if (coinManager.coin < 9.99 && purchaseGoneThrough == false)
        {
            StartCoroutine(TooPoor());
            sfx.clip = purchaseDenied;
            itemPurchased2.SetActive(false);
            healthFull2.SetActive(false);
            sfx.Play();
        }
    }

    public IEnumerator ItemPurchased()
    {
        yield return new WaitForSeconds(0f);
        itemPurchased2.SetActive(true);
        yield return new WaitForSeconds(3f);
        itemPurchased2.SetActive(false);
        purchaseGoneThrough = false;
    }

    public IEnumerator TooPoor()
    {
        yield return new WaitForSeconds(0f);
        tooPoor2.SetActive(true);
        yield return new WaitForSeconds(3f);
        tooPoor2.SetActive(false);
    }

    public IEnumerator HealthFull()
    {
        yield return new WaitForSeconds(0f);
        healthFull2.SetActive(true);
        yield return new WaitForSeconds(3f);
        healthFull2.SetActive(false);
    }
}

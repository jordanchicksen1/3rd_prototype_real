using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class coinManager : MonoBehaviour
{
    public int coin;
    public TextMeshProUGUI coinText;
    public GameObject rewardGem;
    public GameObject gem7Trigger;
    public GameObject gem7UI;
    public PlayerMovement playerMovement;

    public GameObject coinsText;

    public AudioSource sfx2;
    public AudioClip coinSurpriseSFX;

    public void Update()
    {
        CheckCoin();
    }

    public void addCoin()
    {
        coin = coin + 1;
        coinText.text = coin.ToString();
        CheckCoin();
    }

    public void subtractCoin() 
    {
        coin = coin - 5;
        coinText.text = coin.ToString();
    }

    public void CheckCoin()
    {
        if (playerMovement.grassR1 == true && playerMovement.grassR2 == true && playerMovement.grassR3 == true && playerMovement.grassR4 == true && playerMovement.grassR5 == true && playerMovement.grassR6 == true && playerMovement.grassR7 == true && playerMovement.grassR8 == true)
        {
            rewardGem.SetActive(true);
            gem7Trigger.SetActive(true);
            gem7UI.SetActive(true);
            StartCoroutine(CoinSurprise());
            sfx2.clip = coinSurpriseSFX;
            sfx2.Play();
            playerMovement.grassR1 = false;
        }
        else
        {
            return;
        }
    }

    public void subtractNut()
    {
        coin = coin - 1;
        coinText.text = coin.ToString();
    }

    //shop 1 stuff
    public void boughtItemOne()
    {
        coin = coin - 25;
        coinText.text = coin.ToString();
    }

    public void boughtItemTwo()
    {
        coin = coin - 5;
        coinText.text = coin.ToString();
    }

    public void boughtItemThree()
    {
        coin = coin - 5;
        coinText.text = coin.ToString();
    }

    public void boughtItemFour()
    {
        coin = coin - 10;
        coinText.text = coin.ToString();
    }

    public IEnumerator CoinSurprise()
    {
        yield return new WaitForSeconds(0f);
        coinsText.SetActive(true);
        yield return new WaitForSeconds(5f);
        coinsText.SetActive(false);
    }
}

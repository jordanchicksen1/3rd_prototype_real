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
        
    }

    public void addCoin()
    {
        coin = coin + 1;
        coinText.text = coin.ToString();
        CheckCoin();
    }

    public void CheckCoin()
    {
        if (coin > 41.99 && playerMovement.gotGem7 == false)
        {
            rewardGem.SetActive(true);
            gem7Trigger.SetActive(true);
            gem7UI.SetActive(true);
            StartCoroutine(CoinSurprise());
            sfx2.clip = coinSurpriseSFX;
            sfx2.Play();
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

    public IEnumerator CoinSurprise()
    {
        yield return new WaitForSeconds(0f);
        coinsText.SetActive(true);
        yield return new WaitForSeconds(5f);
        coinsText.SetActive(false);
    }
}

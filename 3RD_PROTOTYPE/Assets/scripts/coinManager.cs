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

    public void Update()
    {
        if (coin > 34.99 && playerMovement.gotGem7 == false)
        {
            rewardGem.SetActive(true);
            gem7Trigger.SetActive(true);    
            gem7UI.SetActive(true);
        }
        else
        {
            return;
        }
    }

    public void addCoin()
    {
        coin = coin + 1;
        coinText.text = coin.ToString();
    }

    public void subtractNut()
    {
        coin = coin - 1;
        coinText.text = coin.ToString();
    }
}

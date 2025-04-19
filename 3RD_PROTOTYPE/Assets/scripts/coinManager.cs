using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class coinManager : MonoBehaviour
{
    public int coin;
    public TextMeshProUGUI coinText;


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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gemManager : MonoBehaviour
{
    public int gem;
    public TextMeshProUGUI gemText;
    public TextMeshProUGUI endgameGemText;


    public void addGem()
    {
        gem = gem + 1;
        gemText.text = gem.ToString();
        endgameGemText.text = gem.ToString();
    }
}

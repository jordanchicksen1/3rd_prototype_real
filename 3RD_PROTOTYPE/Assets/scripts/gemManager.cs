using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gemManager : MonoBehaviour
{
    public int gem;
    public TextMeshProUGUI gemText;
    public TextMeshProUGUI endgameGemText;

    public int hatCount;
    public TextMeshProUGUI hatCountText;
    public void addGem()
    {
        gem = gem + 1;
        gemText.text = gem.ToString();
        endgameGemText.text = gem.ToString();
    }
    
    public void addHat() 
    { 
        hatCount = hatCount + 1;
        hatCountText.text = hatCount.ToString();
    }

    public void payGem()
    {
        gem = gem - 3;
        gemText.text = gem.ToString();
       
    }

    public void payGemMaple()
    {
        gem = gem - 5;
        gemText.text = gem.ToString();
        
    }

    public void payGemShallow()
    {
        gem = gem - 7;
        gemText.text = gem.ToString();
        
    }


}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class nutManager : MonoBehaviour
{
    public int nut;
    public TextMeshProUGUI nutText;


    public void addNut()
    {
        nut = nut + 1;
        nutText.text = nut.ToString();
    }

    public void subtractNut()
    {
        nut = nut - 1;
        nutText.text = nut.ToString();
    }
}

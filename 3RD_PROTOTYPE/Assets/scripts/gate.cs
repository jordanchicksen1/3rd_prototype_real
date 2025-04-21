using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gate : MonoBehaviour
{
     public buttonMechanism buttonMechanism;
    public buttonMechanism2 buttonMechanism2;
    void Update()
    {
        if(buttonMechanism.button1Pressed == true && buttonMechanism2.button2Pressed == true)
        {
            Destroy(this.gameObject);
        }
    }
}

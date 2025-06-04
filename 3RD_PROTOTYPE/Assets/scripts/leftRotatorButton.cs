using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftRotatorButton : MonoBehaviour
{
    public rotator rotator;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            rotator.isOnLeftButton = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            rotator.isOnLeftButton = false;
        }
    }
}

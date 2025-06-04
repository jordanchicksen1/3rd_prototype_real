using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightRotatorButton : MonoBehaviour
{
    public rotator rotator;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            rotator.isOnRightButton = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            rotator.isOnRightButton = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deletePay : MonoBehaviour
{
    public GameObject payTrigger;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            payTrigger.SetActive(false);
        }
    }
}

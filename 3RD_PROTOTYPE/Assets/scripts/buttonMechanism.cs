using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonMechanism : MonoBehaviour
{
    public bool button1Pressed = false;
    public GameObject buttonUp;
    public GameObject buttonDown;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("player pressed button");
            button1Pressed = true;
            StartCoroutine(ButtonMovement());
            buttonDown.SetActive(true);
        }
    }

    public IEnumerator ButtonMovement()
    {
        yield return new WaitForSeconds(0.1f);
        buttonUp.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateTimer : MonoBehaviour
{
    public bool pressedButton = false;
    public GameObject buttonUp;
    public GameObject gate;
    public GameObject arrow;

    public GameObject timerSound;


    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && pressedButton == false)
        {
            StartCoroutine(GateDown());
            buttonUp.SetActive(false);
            pressedButton = true;
            timerSound.SetActive(true);
            arrow.SetActive(true);
            
        }
    }

    public IEnumerator GateDown()
    {
        yield return new WaitForSeconds(0f);
        gate.SetActive(false);
        yield return new WaitForSeconds(20f);
        gate.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        gate.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        gate.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        gate.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        gate.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        gate.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        gate.SetActive(true);
        buttonUp.SetActive(true);
        pressedButton = false;
        timerSound.SetActive(false);
        arrow.SetActive(false);

    }
}

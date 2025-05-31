using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateDestroyer : MonoBehaviour
{
    public GameObject gate;
    public GameObject buttonUp;
    public GameObject buttonTrigger;
    public GameObject timerSound;
    public GameObject arrow;
    public AudioSource sfx;
    public AudioClip gateOpen;

    public gateTimer gateTimer;

    public ParticleSystem sparkles;


    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && gateTimer.pressedButton == true)
        {
            StartCoroutine(DestroyGate());
        }
    }

    public IEnumerator DestroyGate()
    {
        yield return new WaitForSeconds(0f);
        timerSound.SetActive(false);
        sfx.clip = gateOpen;
        sfx.Play();
        sparkles.Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(gate);
        Destroy(buttonUp);
        Destroy(arrow);
        Destroy(buttonTrigger);
    }
}

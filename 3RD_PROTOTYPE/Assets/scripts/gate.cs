using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gate : MonoBehaviour
{
    public buttonMechanism buttonMechanism;
    public buttonMechanism2 buttonMechanism2;
    public AudioSource soundFX;
    public AudioClip gateFX;

    void Update()
    {
        if(buttonMechanism.button1Pressed == true && buttonMechanism2.button2Pressed == true)
        {
            
            StartCoroutine(OpenGate());
        }
    }

    public IEnumerator OpenGate()
    {
        
        yield return new WaitForSeconds(1f);
        soundFX.clip = gateFX;
        soundFX.Play();
        Destroy(this.gameObject);
    }
}

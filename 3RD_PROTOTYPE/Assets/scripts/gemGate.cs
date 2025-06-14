using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class gemGate : MonoBehaviour
{
    public GameObject theGemGate;
    public gemManager gemManager;

    public GameObject needMoreGems;

    public ParticleSystem sparkle;

    public AudioSource SFX;
    public AudioClip openedGate;

    public void PayGem()
    {
        if(gemManager.gem > 2.99)
        {
            StartCoroutine(OpenGate());
        }

        if(gemManager.gem < 2.99)
        {
            StartCoroutine(NotEnoughGems());
        }
    }

    public IEnumerator OpenGate()
    {
        yield return new WaitForSeconds(0f);
        gemManager.payGem();
        sparkle.Play();
        SFX.clip = openedGate;
        SFX.Play();
        gemManager.payGem();
        yield return new WaitForSeconds(0.1f);
        Destroy(theGemGate);
    }

    public IEnumerator NotEnoughGems()
    {
        yield return new WaitForSeconds(0f);   
        needMoreGems.SetActive(true);
        yield return new WaitForSeconds(2f);
        needMoreGems.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemGate3 : MonoBehaviour
{
    public GameObject theGemGate;
    public gemManager gemManager;

    public GameObject payPanel;
    public GameObject needMoreGems;

    public ParticleSystem sparkle;

    public AudioSource SFX;
    public AudioClip openedGate;
    public AudioClip denied;

    public bool hasPressedButton = false;

    public void PayGem()
    {
        if (gemManager.gem > 2.99 && hasPressedButton == false)
        {
            StartCoroutine(OpenGate());
            gemManager.payGem();
            hasPressedButton = true;
        }

        if (gemManager.gem < 2.99 && hasPressedButton == false)
        {
            StartCoroutine(NotEnoughGems());
            hasPressedButton = true;
        }
    }

    public IEnumerator OpenGate()
    {

        yield return new WaitForSeconds(0f);
        sparkle.Play();
        SFX.clip = openedGate;
        SFX.Play();
        payPanel.SetActive(false);
        hasPressedButton = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yield return new WaitForSeconds(0.1f);
        Destroy(theGemGate);
    }

    public IEnumerator NotEnoughGems()
    {
        yield return new WaitForSeconds(0f);
        needMoreGems.SetActive(true);
        SFX.clip = denied;
        SFX.Play();
        hasPressedButton = false;
        yield return new WaitForSeconds(2f);
        needMoreGems.SetActive(false);
    }
}

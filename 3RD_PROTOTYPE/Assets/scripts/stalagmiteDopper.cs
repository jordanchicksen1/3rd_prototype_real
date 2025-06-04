using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stalagmiteDopper : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject stalagmite;
    public ParticleSystem stalagmiteParticles;
    public float destroyTime = 1.5f;

    public AudioSource sfx;
    public AudioClip stalagmiteSFX;
    public AudioClip rockFallSFX;

    void Start()
    {
       
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(StalagmiteDrop());
        }
    }

    public IEnumerator StalagmiteDrop()
    {
        yield return new WaitForSeconds(0f);
        stalagmiteParticles.Play();
        sfx.clip = stalagmiteSFX;
        sfx.Play(); 
        yield return new WaitForSeconds(0.5f);
        rb.isKinematic = false;
        
        Destroy(stalagmite, destroyTime);
        
    }
}

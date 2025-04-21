using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public playerHealth playerHealth;
    
    
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit player");
            playerHealth.PlayerHit();
            Destroy(this.gameObject);

        }
    }
}

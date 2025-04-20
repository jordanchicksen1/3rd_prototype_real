using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upDownPlatform : MonoBehaviour
{
    public bool moveUp  = false;
    public float moveSpeed = 5f;
    private Rigidbody platformRB;

    public void Start()
    {
        platformRB = GetComponent<Rigidbody>();
        platformRB.velocity = new Vector3(0, moveSpeed, 0);

    }

    public void Update()
    {
        if (moveUp == true)
        {
            platformRB.velocity = new Vector3(0, moveSpeed, 0);

        }

        if (moveUp == false)
        {
            platformRB.velocity = new Vector3(0, -moveSpeed, 0);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Up")
        {
            moveUp = false;
            Debug.Log("hit top trigger");
        }

        if (other.tag == "Down")
        {
            moveUp = true;
            Debug.Log("hit bottom trigger");
        }
    }
}

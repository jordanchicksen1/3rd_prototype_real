using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forwardBackwardPlatform : MonoBehaviour
{
    public bool moveForward = false;
    public float moveSpeed = 5f;
    private Rigidbody platformRB;

    public void Start()
    {
        platformRB = GetComponent<Rigidbody>();
        platformRB.velocity = new Vector3(0, 0, moveSpeed);
        Debug.Log("should start the platform");
    }

    public void Update()
    {
        if (moveForward == true)
        {
            platformRB.velocity = new Vector3(0, 0, moveSpeed);

        }

        if (moveForward == false)
        {
            platformRB.velocity = new Vector3(0, 0, -moveSpeed);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Front")
        {
            moveForward = false;
            Debug.Log("hit top trigger");
        }

        if (other.tag == "Back")
        {
            moveForward = true;
            Debug.Log("hit bottom trigger");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crawlerSide : MonoBehaviour
{
    public bool moveForward = false;
    public float moveSpeed = 5f;
    private Rigidbody crawlerRB;

    public void Start()
    {
        crawlerRB = GetComponent<Rigidbody>();
        crawlerRB.velocity = new Vector3(0, 0, -moveSpeed);

    }

    public void Update()
    {
        if (moveForward == true)
        {
            crawlerRB.velocity = new Vector3(moveSpeed, 0, 0);
        }

        if (moveForward == false)
        {
            crawlerRB.velocity = new Vector3(-moveSpeed, 0, 0);
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

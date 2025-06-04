using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour
{
    public bool isOnLeftButton = false;
    public bool isOnRightButton = false;

    void Update()
    {
        if (isOnLeftButton == true)
        {
            float rotateSpeed = 45f * Time.deltaTime;
            this.transform.Rotate(0f, -rotateSpeed, 0f);
            Debug.Log("is it working?");
        }

        if (isOnRightButton == true)
        {
            float rotateSpeed = 45f * Time.deltaTime;
            this.transform.Rotate(0f, rotateSpeed, 0f);
            Debug.Log("is it working?");
        }
        
    }
}

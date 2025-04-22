using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject tutorial3;
    public GameObject tutorial4;
    public GameObject tutorial5;
    public GameObject button1;
    public GameObject button2;


    public void Start()
    {
        StartCoroutine(Tutorial());
    }
    public IEnumerator Tutorial()
    {
        yield return new WaitForSeconds(1f);
        tutorial1.SetActive(true);
        yield return new WaitForSeconds(3f);
        tutorial1.SetActive(false);
        tutorial2.SetActive(true);
        yield return new WaitForSeconds(3f);
        tutorial2.SetActive(false);
        tutorial3.SetActive(true);
        yield return new WaitForSeconds(3f);
        tutorial3.SetActive(false);
        tutorial4.SetActive(true);
        yield return new WaitForSeconds(3f);
        tutorial4.SetActive(false);
        tutorial5.SetActive(true);
        yield return new WaitForSeconds(3f);
        tutorial5.SetActive(false);
        button1.SetActive(true);
        button2.SetActive(true);
    }
}

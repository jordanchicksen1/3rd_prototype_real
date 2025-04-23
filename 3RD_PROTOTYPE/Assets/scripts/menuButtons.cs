using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuButtons : MonoBehaviour
{
    public GameObject title;
    public GameObject playButton;
    public GameObject quitButton;
    public GameObject startButton;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject player;
    public GameObject gem;

    public GameObject screen1;
    public GameObject screen2;
    public GameObject screen3;
    public GameObject screen4;
    public GameObject screen5;

    public void StartGame()
    {
        title.SetActive(false);
        startButton.SetActive(false);
        playButton.SetActive(true);
        quitButton.SetActive(true);
    }

    public void PlayButton()
    {
        screen1.SetActive(true);
    }

    public void NextOne()
    {
        screen1.SetActive(false);
        screen2.SetActive(true);
    }

    public void NextTwo()
    {
        screen2.SetActive(false);
        screen3.SetActive(true);
    }

    public void NextThree()
    {
        screen3.SetActive(false);
        screen4.SetActive(true);
    }
    public void NextFour()
    {
        screen4.SetActive(false);
        screen5.SetActive(true);
    }

    public void RetryButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitButton()
    {
        Application.Quit();
    }


}

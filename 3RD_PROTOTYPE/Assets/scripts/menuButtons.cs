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
    public void StartGame()
    {
        title.SetActive(false);
        startButton.SetActive(false);
        playButton.SetActive(true);
        quitButton.SetActive(true);
    }

    public void PlayButton()
    {

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

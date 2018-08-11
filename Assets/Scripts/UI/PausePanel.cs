using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PausePanel : MonoBehaviour {

    public GameObject pausePanel;
    public GameObject pauseButton;

    public void GamePause()
    {
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);

    }

    public void Continue()
    {
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);

    }

    public void Restart()
    {

        pausePanel.SetActive(false);
        pauseButton.SetActive(true);

        SceneManager.LoadScene("SampleScene");

    }

    public void MainMenu()
    {
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);

        SceneManager.LoadScene("MainMenu");

    }
}

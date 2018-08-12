using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour {

    public GameObject losePanel;
    public GameObject pauseButton;
    public GameObject bagpackButton;

    public void Lose()
    {
        losePanel.SetActive(true);
        pauseButton.SetActive(false);
        bagpackButton.SetActive(false);

     }

  
    public void Restart()
    {

        losePanel.SetActive(false);
        pauseButton.SetActive(true);
        bagpackButton.SetActive(true);

        SceneManager.LoadScene("SampleScene");

    }

    public void MainMenu()
    {
        losePanel.SetActive(false);
        pauseButton.SetActive(true);
        bagpackButton.SetActive(true);

        SceneManager.LoadScene("MainMenu");

    }
}


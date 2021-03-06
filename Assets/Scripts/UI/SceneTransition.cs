﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

    public void GameLoader()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void CreditsLoader()
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}

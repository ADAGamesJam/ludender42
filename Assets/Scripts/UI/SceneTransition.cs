using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

    public void GameLoader()
    {

        SceneManager.LoadScene("SampleScene");

    }

    public void CreditsLoader()
    {
        SceneManager.LoadScene("Credits");

    }
}

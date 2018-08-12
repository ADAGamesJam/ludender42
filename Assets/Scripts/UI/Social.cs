using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Social : MonoBehaviour
{

    public void Twitter()
    {

        Application.OpenURL("https://twitter.com/adagamesoff");

    }

    public void Instagram()
    {

        Application.OpenURL("https://www.instagram.com/adagamesofficial");

    }

    public void Vk()
    {
        Application.OpenURL("https://vk.com/adagames");

    }

    public void Web()

    {
        Application.OpenURL("https://www.adagames.org");
    }
}

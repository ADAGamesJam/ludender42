using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour {

    public GameObject soundButton;
    private Image image;
    public Sprite sprite1;
    public Sprite sprite2;

    private void Awake()
    {
        image = soundButton.GetComponent<Image>();

    }

    public bool isSound = true;

    public void soundSwitch()
    {

        if (isSound == true)
        {
            isSound = false;

        }
        else
            isSound = true;

    }

    private void Update()
    {
        if(isSound == true)
        {
            image.color = Color.red;

        }
        else
        {

            image.color = Color.black;
        }
            
    }


}

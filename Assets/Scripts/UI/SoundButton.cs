using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour {



    public GameObject soundButton;
    private Image image;
    public Sprite sprite1;
    public Sprite sprite1Hltd;
    public Sprite sprite2;
    public Sprite sprite2Hltd;


    public static SoundButton instance = null;
    [HideInInspector]
    public bool isSound;

    private void Awake()
    {
        image = soundButton.GetComponent<Image>();
        isSound = true;
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }



    public void soundSwitch()
    {
        if (isSound == true)
        {
            isSound = false;
        }
        else 
            isSound = true;

    }

    public void Update()
    {
        if(isSound == true)
        {
            SpriteState s1h = new SpriteState();
            image.sprite = sprite1;
            s1h.highlightedSprite = sprite1Hltd;
            
        }
        else
        {
            SpriteState s2h = new SpriteState();
            image.sprite = sprite2;
            s2h.highlightedSprite = sprite2Hltd;


        }
            
    }


}

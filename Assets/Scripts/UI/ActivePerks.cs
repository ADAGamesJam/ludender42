using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivePerks : MonoBehaviour {



    public Image slot1;
    public Image slot2;
    public Image image1;
    public Sprite image1d;
    public Sprite image1h;
    public bool isActive = false;


   

    public void Update()
    {   
        if( image1.transform.parent.name == slot1.name || image1.transform.parent.name == slot2.name)
        {
            image1.sprite = image1h;
           

        }
        else
        {

            image1.sprite = image1d;
           

        }
            


    }

}

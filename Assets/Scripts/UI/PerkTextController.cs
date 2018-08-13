using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkTextController : MonoBehaviour
{

    public enum ImgType
    {
        img_1, img_2, img_3
    }

    public Image slot1;
    public Image slot2;
    public Image slot3;
    public Image image;
    public Text text;
    public GameObject plate;
    public ImgType type;

    public void Enter()
    {   
        if(image.transform.parent.name == slot1.name || image.transform.parent.name == slot2.name || image.transform.parent.name == slot3.name)
        {   
            plate.SetActive(true);

            /*if (type == ImgType.img_1)
                text.text = "1";
            else
            if (type == ImgType.img_2)
                text.text = "2";
             else
            if (type == ImgType.img_3)
                text.text = "3";
            Debug.Log(gameObject.name);*/
        }
    }

    public void Exit()
    {

        plate.SetActive(false);
       // text.SetActive(false);
    }

}

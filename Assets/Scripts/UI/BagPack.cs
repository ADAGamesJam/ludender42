using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPack : MonoBehaviour {

    public GameObject bagPack;
    public GameObject pauseButton;
    public GameObject bagPackButton;

    public void OpenBagpack()
    {
        pauseButton.SetActive(false);
        bagPack.SetActive((true));
        bagPackButton.SetActive(false);

    }

    public void CloseBagpack()
    {
        pauseButton.SetActive(true);
        bagPack.SetActive((false));
        bagPackButton.SetActive(true);


    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Slot : MonoBehaviour, IDropHandler
{

    public AudioClip equip;

    private bool canPlaySound = true;

    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (this.gameObject.transform.childCount < 1)
            DragHandler.itemBeingDragged.transform.SetParent(transform);
    }

    public void Update()
    {

        // Upgrate Fight
        if (transform.childCount > 0 && (gameObject.name == "Slot1" || gameObject.name == "Slot2"))
        {

            if (transform.GetChild(0).name == "Image")
            {
                HeroControlls.instance.upgradeFight = true;
            }

        }
        if (transform.childCount > 0 && (gameObject.name == "Slot3" || gameObject.name == "Slot4" || gameObject.name == "Slot5"))
        {
            if (transform.childCount > 0)
            {
                if (transform.GetChild(0).name == "Image")
                    HeroControlls.instance.upgradeFight = false;
            }

        }





        // Upgrade Hack
        if (transform.childCount > 0 && (gameObject.name == "Slot1" || gameObject.name == "Slot2"))
        {

            if (transform.childCount > 0 && transform.GetChild(0).name == "Image 1")
            {
                HeroControlls.instance.upgradeHack = true;
            }


        }
        if (gameObject.name == "Slot3" || gameObject.name == "Slot4" || gameObject.name == "Slot5")
        {
            if (transform.childCount > 0)
            {
                if (transform.GetChild(0).name == "Image 1")
                    HeroControlls.instance.upgradeHack = false;
            }
        }

        //Upgrade Jump

        if (transform.childCount > 0 && (gameObject.name == "Slot1" || gameObject.name == "Slot2"))
        {

            if (transform.childCount > 0 && transform.GetChild(0).name == "Image 2")
            {
                HeroControlls.instance.upgradeJump = true;
            }

            
        }
        if (gameObject.name == "Slot3" || gameObject.name == "Slot4" || gameObject.name == "Slot5")
        {
            if (transform.childCount > 0)
            {


                if (transform.childCount > 0 && transform.GetChild(0).name == "Image 2")
                    HeroControlls.instance.upgradeJump = false;
            }

        }

        // Звук вставки апгрейда
        if (transform.childCount > 0 && canPlaySound)
        {
            canPlaySound = false;
            MusicaManager.instance.PlaySound(equip);
        }
        if (transform.childCount == 0)
        {
            canPlaySound = true;
        }

    }
}

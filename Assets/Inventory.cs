using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour {

    public static Inventory instance;

    public GameObject Slot1;
    public GameObject Slot2;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        if (instance != this)
            Destroy(this);
    }

    public string[] SlotInfo()
    {
        string[] inventoryInfo = new string[2];

        if (Slot1.gameObject.transform.GetChild(0) != null)
        {
            inventoryInfo[0] = Slot1.gameObject.transform.GetChild(0).name;
        }
        else
            inventoryInfo[0] = "";

        if (Slot2.gameObject.transform.GetChild(0) != null)
        {
            inventoryInfo[1] = Slot1.gameObject.transform.GetChild(0).name;
        }
        else
            inventoryInfo[1] = "";

        return inventoryInfo;
        }

    }


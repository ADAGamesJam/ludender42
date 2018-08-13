using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class InteractableObject : MonoBehaviour, IComparable<InteractableObject>
{
    public enum Type
    {
        NPC,
        Sign,
        Collectable
    }


    public Type type;
    public string message;
    public int priority;
    public List<GameObject> npc;
    private GameObject[] targetObjects;

    public int CompareTo(InteractableObject other)
    {
        return priority.CompareTo(other.priority);
    }

    public void Start()
    {
        targetObjects = GameObject.FindGameObjectsWithTag("OurObjects");
        npc.AddRange(targetObjects);
        foreach (var o in npc)
        {
            Field.instance.AddElements(Vector3Int.CeilToInt(o.transform.position));
        }
    }

    
}
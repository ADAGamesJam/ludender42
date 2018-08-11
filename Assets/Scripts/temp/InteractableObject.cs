using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    public int CompareTo(InteractableObject other)
    {
        return priority.CompareTo(other.priority);
    }
}

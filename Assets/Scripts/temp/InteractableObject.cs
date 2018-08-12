using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class InteractableObject : MonoBehaviour, IComparable<InteractableObject>
{
    [Serializable]
    public class SingleDialog
    {
        public string message;
        public IconType icon;
    }

    public Type type;
    public List<SingleDialog> phrases;
    
    public int priority;

    public int CompareTo(InteractableObject other)
    {
        return priority.CompareTo(other.priority);
    }
}

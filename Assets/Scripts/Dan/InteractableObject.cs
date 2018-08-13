using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class InteractableObject : MonoBehaviour//, IComparable<InteractableObject>
{
    [Serializable]
    public class SingleDialog
    {
        public string message;
        public IconType icon;
    }

    public bool canFight = false;
    public Type type;
    public List<SingleDialog> phrases;

    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite deadSprite;

    //public int CompareTo(InteractableObject other)
    //{
    //    return priority.CompareTo(other.priority);
    //}

    public void Kill()
    {
        //spriteRenderer.sprite = deadSprite;
        Destroy(gameObject);
    }

}

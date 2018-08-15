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

    public bool isIntercom = false;
    public bool canFight = false;
    public Type type;
    public List<SingleDialog> phrases;

    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite deadSprite;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    //public int CompareTo(InteractableObject other)
    //{
    //    return priority.CompareTo(other.priority);
    //}

    public void Kill()
    {
        spriteRenderer.sprite = deadSprite;
        Destroy(GetComponent<Animator>());
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<EnemyController>());
        Destroy(this);



    }

}

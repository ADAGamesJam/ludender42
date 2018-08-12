using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Sprite openSprite;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
	
	void Update()
    {
		
	}

    public void Open()
    {
        spriteRenderer.sprite = openSprite;
        gameObject.layer = 0;
    }


}

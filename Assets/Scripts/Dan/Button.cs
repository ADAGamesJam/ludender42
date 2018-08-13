using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public Door door;
    public Sprite used;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Stay");
        if (collision.gameObject.tag.Contains("Player"))
        {
            if (door != null)   
                door.Open();
            Destroy(GetComponent<Animator>());
            GetComponent<SpriteRenderer>().sprite = used;
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(this);
        }
    }
}

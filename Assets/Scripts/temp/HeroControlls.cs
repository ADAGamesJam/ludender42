using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class HeroControlls : MonoBehaviour
{
    public static HeroControlls instance;
    [Header("References")]
    [Tooltip("Любай тайлмап. Он будет дергать из нее размер клетки")]
    public Tilemap tilemap;
    [HideInInspector]
    public float tileSize;
    [Header("Parameters")]
    [Tooltip("Время между нажатиями кнопок управления")]
    public float keystrokePause = 0.2f;

    [HideInInspector]
    public bool heroMoved = false;
    [HideInInspector]
    public bool canMove = true;
    [HideInInspector]
    public bool isTyping = false;

    private LayerMask maskWalls = 1 << 8;
    private LayerMask maskInteractables = 1 << 9;
    private LayerMask maskPitfall = 1 << 10;
    private LayerMask maskCombined = 1 << 8 | 1 << 9 | 1 << 10;
    private Vector3 direction;
    private Coroutine coroutine;
    private Vector3 destination;
    private float timer;
    private SpriteRenderer spriteRenderer;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != null && instance != this)
        {
            Destroy(this);
        }

        tileSize = tilemap.cellSize.x;
        transform.position = new Vector3((float)Math.Floor(transform.position.x) + tileSize/2, (float)Math.Floor(transform.position.y) + tileSize/2, transform.position.z);
        timer = keystrokePause;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	void Update()
    {
        timer -= Time.deltaTime;
        direction = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.W) && timer <= 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.up * tileSize, 1f, maskCombined.value);
            if (!hit)
            {
                //transform.Translate(Vector3.up * tileSize);
                //CheckSurroundings();
                direction = Vector3.up;
            }
            else if (!isTyping)
            {
                InteractableObject obj = hit.collider.gameObject.GetComponent<InteractableObject>();
                if (obj != null)
                {
                    if (obj.phrases.Count > 0)
                        Dialog.instance.SetDialog(obj);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.A) && timer <= 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.left * tileSize, 1f, maskCombined.value);
            if (!hit)
            {
                //transform.Translate(Vector3.left * tileSize);
                //CheckSurroundings();
                direction = Vector3.left;
            }
            else if (!isTyping)
            {
                InteractableObject obj = hit.collider.gameObject.GetComponent<InteractableObject>();
                if (obj != null)
                {
                    if (obj.phrases.Count > 0)
                        Dialog.instance.SetDialog(obj);
                }
            }
            spriteRenderer.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.S) && timer <= 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down * tileSize, 1f, maskCombined.value);
            if (!hit)
            {
                //transform.Translate(Vector3.down * tileSize);
                //CheckSurroundings();
                direction = Vector3.down;
            }
            else if (!isTyping)
            {
                InteractableObject obj = hit.collider.gameObject.GetComponent<InteractableObject>();
                if (obj != null)
                {
                    if (obj.phrases.Count > 0)
                        Dialog.instance.SetDialog(obj);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.D) && timer <= 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right * tileSize, 1f, maskCombined.value);
            if (!hit)
            {
                //transform.Translate(Vector3.right * tileSize);
                //CheckSurroundings();
                direction = Vector3.right;
            }
            else if (!isTyping)
            {
                InteractableObject obj = hit.collider.gameObject.GetComponent<InteractableObject>();
                if (obj != null)
                {
                    if (obj.phrases.Count > 0)
                        Dialog.instance.SetDialog(obj);
                }
            }
            spriteRenderer.flipX = true;
        }
        //if (Input.GetKeyDown(KeyCode.E) && !isTyping)
        //{
        //    CheckSurroundings();
        //}
        if (direction != Vector3.zero && canMove && heroMoved == false)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                transform.position = destination;
                //Dialog.Hide();
                //CheckSurroundings();
            }
            coroutine = StartCoroutine(Move());
            timer = keystrokePause;
        }
    }


    private void CheckSurroundings()
    {
        List<InteractableObject> objects;
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, (tileSize * tileSize) / 2, maskInteractables.value);
        if (cols.Length > 0)
        {
            objects = new List<InteractableObject>();
            foreach (var col in cols)
            {
                if (col.gameObject.GetComponent<InteractableObject>() != null)
                {
                    objects.Add(col.gameObject.GetComponent<InteractableObject>());
                }
            }

            if (objects.Count > 0)
            {
                objects.Sort();
                if (objects[0].phrases.Count > 0)
                    Dialog.instance.SetDialog(objects[0]);
            }
        }
        else
        {
            Dialog.instance.Hide();
        }
    }

    private IEnumerator Move()
    {
        destination = transform.position + direction;
        while ((transform.position - destination).magnitude > 0.02f)
        {
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime*10);
            yield return 0;
        }
        transform.position = destination;
        heroMoved = true;
    }
}

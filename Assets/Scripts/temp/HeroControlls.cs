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

    private LayerMask maskWalls = 1 << 8;
    private LayerMask maskInteractables = 1 << 9;
    private LayerMask maskCombined = 1 << 8 | 1 << 9;
    private Vector3 direction;
    private Coroutine coroutine;
    private Vector3 destination;
    private float timer;

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
    }
	
	void Update()
    {
        timer -= Time.deltaTime;
        direction = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.W) && timer <= 0)
        {
            if (!Physics2D.Raycast(transform.position, Vector3.up * tileSize, 1f, maskCombined.value))
            {
                //transform.Translate(Vector3.up * tileSize);
                //CheckSurroundings();
                direction = Vector3.up;
            }
        }
        if (Input.GetKeyDown(KeyCode.A) && timer <= 0)
        {
            if (!Physics2D.Raycast(transform.position, Vector3.left * tileSize, 1f, maskCombined.value))
            {
                //transform.Translate(Vector3.left * tileSize);
                //CheckSurroundings();
                direction = Vector3.left;
            }
        }
        if (Input.GetKeyDown(KeyCode.S) && timer <= 0)
        {
            if (!Physics2D.Raycast(transform.position, Vector3.down * tileSize, 1f, maskCombined.value))
            {
                //transform.Translate(Vector3.down * tileSize);
                //CheckSurroundings();
                direction = Vector3.down;
            }
        }
        if (Input.GetKeyDown(KeyCode.D) && timer <= 0)
        {
            if (!Physics2D.Raycast(transform.position, Vector3.right * tileSize, 1f, maskCombined.value))
            {
                //transform.Translate(Vector3.right * tileSize);
                //CheckSurroundings();
                direction = Vector3.right;
            }
        }
        if (direction != Vector3.zero && heroMoved == false)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                transform.position = destination;
                Dialog.Hide();
                //CheckSurroundings();
            }
            coroutine = StartCoroutine(Move());
            timer = keystrokePause;
        }
    }


    private void CheckSurroundings()
    {
        List<InteractableObject> objects;
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, (tileSize*tileSize)/2, maskInteractables.value);
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
                Dialog.Log(objects[0].message);
            }
        }
        else
        {
            Dialog.Hide();
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
        CheckSurroundings();
        heroMoved = true;
    }
}

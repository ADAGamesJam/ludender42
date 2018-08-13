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
    [HideInInspector]
    public List<int> keyList = new List<int>();

    public Camera camera;

    private LayerMask maskWalls = 1 << 8;
    private LayerMask maskInteractables = 1 << 9;
    private LayerMask maskPitfall = 1 << 10;
    private LayerMask maskDoor = 1 << 12;
    private LayerMask maskCombined = 1 << 8 | 1 << 9 | 1 << 10 | 1 << 12;
    private Vector3 direction;
    private Coroutine coroutine;
    private Vector3 destination;
    private float timer;
    private SpriteRenderer spriteRenderer;

    [HideInInspector]
    public bool upgradeHack = false;
    [HideInInspector]
    public bool upgradeJump = false;
    [HideInInspector]
    public bool upgradeFight = false;




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
        keyList.Add(7);
    }
	
	void Update()
    {
        timer -= Time.deltaTime;
        direction = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.W) && timer <= 0)
        {
            keyList.Add(3);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.up * tileSize, 1f, maskCombined.value);
            if (!hit)
            {
                //transform.Translate(Vector3.up * tileSize);
                //CheckSurroundings();
                direction = Vector3.up;
            }
            else if (!isTyping)
            {
                Skill(hit, Vector3.up);
            }
        }
        if (Input.GetKeyDown(KeyCode.A) && timer <= 0)
        {
            keyList.Add(2);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.left * tileSize, 1f, maskCombined.value);
            if (!hit)
            {
                //transform.Translate(Vector3.left * tileSize);
                //CheckSurroundings();
                direction = Vector3.left;
            }
            else if (!isTyping)
            {
                Skill(hit, Vector3.left);
            }
            spriteRenderer.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.S) && timer <= 0)
        {
            keyList.Add(4);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down * tileSize, 1f, maskCombined.value);
            if (!hit)
            {
                //transform.Translate(Vector3.down * tileSize);
                //CheckSurroundings();
                direction = Vector3.down;
            }
            else if (!isTyping)
            {
                Skill(hit, Vector3.down);
            }
        }
        if (Input.GetKeyDown(KeyCode.D) && timer <= 0)
        {
            keyList.Add(1);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right * tileSize, 1f, maskCombined.value);
            if (!hit)
            {
                //transform.Translate(Vector3.right * tileSize);
                //CheckSurroundings();
                direction = Vector3.right;
            }
            else if (!isTyping)
            {
                Skill(hit, Vector3.right);
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


    private void Skill(RaycastHit2D hit, Vector3 dir)
    {
        // Говорить
        InteractableObject obj = hit.collider.gameObject.GetComponent<InteractableObject>();
        if (obj != null)
        {
            if (obj.canFight && upgradeFight)
            {
                obj.Kill();
            }
            else
            {
                if (obj.phrases.Count > 0)
                {
                    Dialog.instance.SetDialog(obj);
                }
            }
        }


        // Открывать двери
        Door obj1 = hit.collider.gameObject.GetComponent<Door>();
        if (obj1 != null)
        {
            if (upgradeHack)
            obj1.Open();
        }

        // Прыгать
        if (hit.collider.gameObject.layer.Equals(10))
        {
            RaycastHit2D[] hitPit = Physics2D.RaycastAll(transform.position, dir, 3f, maskPitfall.value);
            
            if (upgradeJump && hitPit.Length < 2)
            {
                direction = ((Vector3)hit.point - transform.position).normalized*2;
            }
        }
    }

    //private void CheckSurroundings()
    //{
    //    List<InteractableObject> objects;
    //    Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, (tileSize * tileSize) / 2, maskInteractables.value);
    //    if (cols.Length > 0)
    //    {
    //        objects = new List<InteractableObject>();
    //        foreach (var col in cols)
    //        {
    //            if (col.gameObject.GetComponent<InteractableObject>() != null)
    //            {
    //                objects.Add(col.gameObject.GetComponent<InteractableObject>());
    //            }
    //        }

    //        if (objects.Count > 0)
    //        {
    //            objects.Sort();
    //            if (objects[0].phrases.Count > 0)
    //                Dialog.instance.SetDialog(objects[0]);
    //        }
    //    }
    //    else
    //    {
    //        Dialog.instance.Hide();
    //    }
    //}

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

    public void Kill()
    {
        //Сюда логику смерти
        Debug.Log("E11 is DEAD!");
    }
}

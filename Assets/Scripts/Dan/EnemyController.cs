using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private LayerMask maskWalls = 1 << 8;
    private LayerMask maskPitfall = 1 << 10;
    private LayerMask maskPlayer = 1 << 11;
    private LayerMask maskCombined = 1 << 8 | 1 << 10 | 1 << 11;
    private float tileSize;
    private MoveInst moveInst = MoveInst.stay;


    void Start()
    {
        EnemyManager.instance.Add(this);
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        tileSize = HeroControlls.instance.tileSize;
    }

    private void FindPath()
    {
        Vector3 screenPoint = HeroControlls.instance.camera.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        if (onScreen)
        {
            Vector3 dir = (HeroControlls.instance.gameObject.transform.position - transform.position);

            if (HeroControlls.instance.keyList.Last() == 1)
            {
                //if (dir.x > 0)
                //{
                moveInst = MoveInst.right;
                HeroControlls.instance.keyList.Remove(1);
                //}
            }
            else if (HeroControlls.instance.keyList.Last() == 2)
            {
                moveInst = MoveInst.left;
                HeroControlls.instance.keyList.Remove(2);
            }
            else if (HeroControlls.instance.keyList.Last() == 3)
            {
                moveInst = MoveInst.up;
                HeroControlls.instance.keyList.Remove(3);
            }

            else if (HeroControlls.instance.keyList.Last() == 4)
            {
                moveInst = MoveInst.down;
               Debug.Log(HeroControlls.instance.keyList.Last());
                HeroControlls.instance.keyList.Remove(4);
            }
        }
    }

    public void Move()
    {
        if (HeroControlls.instance.heroMoved)
        {
            if (moveInst == MoveInst.up)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.up * tileSize, 1f, maskCombined.value);
                if (!hit)
                {
                    transform.Translate(Vector3.up * tileSize);
                    //CheckSurroundings();
                    //direction = Vector3.up;
                }
                else
                {
                    if (hit.collider.gameObject.tag.Equals("Player"))
                        HeroControlls.instance.Kill();
                }
            }
            if (moveInst == MoveInst.left)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.left * tileSize, 1f, maskCombined.value);
                if (!hit)
                {
                    transform.Translate(Vector3.left * tileSize);
                    //CheckSurroundings();
                    //direction = Vector3.left;
                    spriteRenderer.flipX = false;
                }
                else
                {
                    if (hit.collider.gameObject.tag.Equals("Player"))
                        HeroControlls.instance.Kill();
                }
            }
            if (moveInst == MoveInst.down)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down * tileSize, 1f, maskCombined.value);
                if (!hit)
                {
                    transform.Translate(Vector3.down * tileSize);
                    //CheckSurroundings();
                    //direction = Vector3.down;
                }
                else
                {
                    if (hit.collider.gameObject.tag.Equals("Player"))
                        HeroControlls.instance.Kill();
                }
            }
            if (moveInst == MoveInst.right)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right * tileSize, 1f, maskCombined.value);
                if (!hit)
                {
                    transform.Translate(Vector3.right * tileSize);
                    //CheckSurroundings();
                    //direction = Vector3.right;
                    spriteRenderer.flipX = true;
                }
                else
                {
                    if (hit.collider.gameObject.tag.Equals("Player"))
                        HeroControlls.instance.Kill();
                }
            }
        }
    }

    private void Update()
    {
       
            FindPath();
        
    }

    public void OnDestroy()
    {
        EnemyManager.instance.Remove(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    private HeroControlls hero;
    [Header("References")]
    [Tooltip("Любай тайлмап. Он будет дергать из нее размер клетки")]
    public Tilemap tilemap;
    [HideInInspector]
    public float tileSize;

    private Vector3 direction;
    private LayerMask maskCombined = 1 << 8 | 1 << 9;

    // Update is called once per frame
    void Update () {
		
	}

    public void DoMove(Enemy enemy)
    {
        if (!hero.heroMoved)
        {
            if (!Physics2D.Raycast(transform.position, Vector3.down * tileSize, 1f, maskCombined.value))
            {
                transform.Translate(Vector3.down * tileSize);
                //CheckSurroundings();
                //direction = Vector3.down;
            }
        }
    }
}

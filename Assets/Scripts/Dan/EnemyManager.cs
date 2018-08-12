using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public List<EnemyController> enemies;

	void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            Destroy(this);
        }
	}
	
	void Update()
    {
        if (HeroControlls.instance.heroMoved)
        {
            
            foreach (var enemy in enemies)
            {
                enemy.Move();
            }

            HeroControlls.instance.heroMoved = false;
        }
	}

    public void Add(EnemyController enemy)
    {
        enemies.Add(enemy);
    }

    public void Remove(EnemyController enemy)
    {
        enemies.Remove(enemy);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public List<Enemy> enemyList;
    
    
	void Start ()
	{
	}
	
	void Update () {
	    if (HeroControlls.instance.heroMoved)
	    {
	        foreach (var enemy in enemyList)
	        {
	           // enemy.DoMove(enemy);
	        }

	       HeroControlls.instance.heroMoved = false;

	    }
	       
	}

    
}

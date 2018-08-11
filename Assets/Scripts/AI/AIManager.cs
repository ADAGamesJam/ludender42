using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public List<Enemy> enemyList;
    public Renderer renderManager;
	void Start ()
	{
	    renderManager = GetComponent<Renderer>();
	}
	
	
	void Update () {
	    if (renderManager.isVisible)
	    {
	        foreach (var enemy in enemyList)
	        {
	           enemy.DoMove(enemy);
	        }
	    }
	}

    
}

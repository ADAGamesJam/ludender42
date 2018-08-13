using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaManager : MonoBehaviour
{
    public static MusicaManager instance;

    [SerializeField]
    private AudioClip mainMenu;
    [SerializeField]
    private AudioClip game;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        GetComponent<AudioSource>().Play();
        Debug.Log("Awake был");
	}
    private void Start()
    {
        GetComponent<AudioSource>().Play();
    }
    void Update()
    {
		
	}


}

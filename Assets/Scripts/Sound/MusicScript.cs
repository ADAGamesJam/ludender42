using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{
    public static MusicScript instance;
    public AudioClip musicClip1;
    public AudioClip musicClip2;

    public AudioSource musicSource;
    // Use this for initialization
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);
       // musicSource.clip = musicClip1;
        //musicSource.clip = musicClip2;
            musicSource.clip = musicClip2;
            musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {


    }
}

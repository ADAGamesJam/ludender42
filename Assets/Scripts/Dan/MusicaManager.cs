using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicaManager : MonoBehaviour
{
    public static MusicaManager instance;

    [Header("Music Clips")]
    [SerializeField]
    private AudioClip mainMenu;
    [SerializeField]
    private AudioClip game;

    [Header("Sound Clips")]
    [SerializeField]
    private AudioSource musicAudioSrc;
    [SerializeField]
    private AudioSource soundAudioSrc;

    private bool didOnce = false; 

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
        musicAudioSrc.clip = mainMenu;
        musicAudioSrc.Play();
        Invoke("PlaySecondPart", mainMenu.length);

    }

    //private void OnLevelWasLoaded(int level)
    //{
    //    if (level.Equals(0))
    //    {
    //        musicAudioSrc.clip = mainMenu;
    //        musicAudioSrc.Play();
    //        Invoke("PlaySecondPart", mainMenu.length);
    //    }
    //    //if (level.Equals(1))
    //    //{
    //    //    musicAudioSrc.clip = game;
    //    //    musicAudioSrc.Play();
    //    //}
    //}

    public void PlaySound(AudioClip clip)
    {
        soundAudioSrc.PlayOneShot(clip);
    }

    //public void PlaySoundLoop(AudioClip clip)
    //{
    //    soundAudioSrc.clip = clip;
    //    soundAudioSrc.loop = true;
    //    soundAudioSrc.Play();
    //}

    private void PlaySecondPart()
    {
        musicAudioSrc.clip = game;
        musicAudioSrc.Play();
    }
}

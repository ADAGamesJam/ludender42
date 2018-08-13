using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour {

    public AudioClip musicClip1;
    public AudioClip musicClip2;

    public AudioSource musicSource;
	// Use this for initialization
	void Start () {
        musicSource.clip = musicClip1;
        //musicSource.clip = musicClip2;
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!SoundButton.instance.isSound)  //&& SceneManager.GetActiveScene().name == "MainMenu")
        {
            musicSource.clip = musicClip1;
            musicSource.Play();

        }
        /*if(SoundButton.instance.isSound ) //&& SceneManager.GetActiveScene().name == "SampleScene"){
        {
            musicSource.clip = musicClip2;
            musicSource.Play();

        }*/

	}
}

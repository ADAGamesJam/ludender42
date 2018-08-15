using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCaller : MonoBehaviour {



    public void Call(AudioClip clip)
    {
        MusicaManager.instance.PlaySound(clip);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneSound : MonoBehaviour
{
    public AudioSource menuMusic;

    private void Awake()
    {
        ApplicationState.musicVolume = 0.5f;
    }

    public void StopMusic()
    {
        menuMusic.Stop();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for the sound/music in the main menu
/// </summary>
public class MenuSceneSound : MonoBehaviour
{
    public AudioSource menuMusic;

    /// <summary>
    /// Set the default music volume.
    /// </summary>
    private void Awake()
    {
        ApplicationState.musicVolume = 0.5f;
    }

    /// <summary>
    /// Stop the music.
    /// </summary>
    public void StopMusic()
    {
        menuMusic.Stop();
    }
}

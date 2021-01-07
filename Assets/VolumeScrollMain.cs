using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

/// <summary>
/// Sets the music volume according to user input
/// </summary>
public class VolumeScrollMain : MonoBehaviour
{
    public AudioSource music;
    
    /// <summary>
    /// Sets music volume according to user input (the slider value).
    /// </summary>
    /// <param name="sliderValue"></param>
    public void SetVol(float sliderValue)
    {
        ApplicationState.musicVolume = sliderValue;
        music.volume = ApplicationState.musicVolume;
    }
}

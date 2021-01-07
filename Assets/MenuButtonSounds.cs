using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonSounds : MonoBehaviour
{
    public AudioSource buttonSound1;

    // Update is called once per frame
    public void playButtonSound()
    {
        buttonSound1.Play();
    }

    public void playPlayButtonSound()
    {
        //
    }
}

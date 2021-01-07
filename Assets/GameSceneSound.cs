using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Log;

/// <summary>
/// Responsible for the sound and music during the game scene. 
/// Utilizes the "singleton method" for allowing the music to survive continuously between scene restarts.
/// </summary>
public class GameSceneSound : MonoBehaviour
{
    public AudioSource music;
    public float pausePitchChange = 0.95f;
    public float pauseVolumeChange = 0.3f;
    private bool alreadyChangedMusicSettings = false;

    public AudioSource audioHit;
    public AudioSource audioLose;

    private static GameSceneSound _instance;

    public static GameSceneSound instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameSceneSound>();

                //Unity should not destroy object upon loading new scene.
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    /// <summary>
    /// At the start of the scene, set the music equal to the chosen volume from the main menu.
    /// If the user puased the game, returned to the main menu, then started the game again, make music normal again.
    /// Continue singleton process.
    /// </summary>
    void Awake()
    {
        music.volume = ApplicationState.musicVolume;

        if (alreadyChangedMusicSettings)
        {
            musicNormalAgain();
        }

        if (_instance == null)
        {
            //If it is the first instance, the it is set as the singleton.
            _instance = this;
            DontDestroyOnLoad(this);
        }
        //If there is already a singleton but another reference is found,
        //destroy the new reference.
        else if (this != _instance)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// If the music is not playing, play the music.
    /// If the main menu has been loaded, stop the music. The game is no longer paused.
    /// If the game is paused and the music tone hasn't been changed, changed the tone of music.
    /// If the game is not paused and the music tone as been changed, change the tone of the music back to normal.
    /// </summary>
    public void Update()
    {
        if (!music.isPlaying && ApplicationState.countDownOver)
        {
            music.Play();
        }
        if (ApplicationState.menuLoaded)
        {
            music.Stop();
            ApplicationState.gamePaused = false;
        }
        else 
        {
            if (ApplicationState.gamePaused && !alreadyChangedMusicSettings)
            {
               musicToneChange();
            }
            if (!ApplicationState.gamePaused && alreadyChangedMusicSettings)
            {
                musicNormalAgain();
            }
        }
    }
    
    /// <summary>
    /// Change the tone of the music while the pause menu is visible.
    /// </summary>
    private void musicToneChange()
    {
        music.pitch *= pausePitchChange;
        music.volume *= pauseVolumeChange;
        alreadyChangedMusicSettings = true;
    }

    /// <summary>
    /// Make the music sound normal again when the pause menu is no longer visible.
    /// </summary>
    public void musicNormalAgain()
    {
        music.pitch /= pausePitchChange;
        music.volume /= pauseVolumeChange;
        alreadyChangedMusicSettings = false;
    }
}

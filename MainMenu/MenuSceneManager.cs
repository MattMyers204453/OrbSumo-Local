using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Log;

public class MenuSceneManager : MonoBehaviour
{
    public AudioSource menuMusic;

    private void Awake()
    {
        ApplicationState.musicVolume = 0.5f;
        ScoreLog.P1wins = 0;
        ScoreLog.P2wins = 0;
    }

    public void PlayGame()
    {
        menuMusic.Stop();
        ScoreLog.soundPlayed = false;
        ApplicationState.menuLoaded = false;
        ////MyPauseMenu.menuLoaded = false;
        SceneManager.LoadScene("Game");

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

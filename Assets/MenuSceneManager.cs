using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Log;

/// <summary>
/// Responsible for handling the flow of the main menu.
/// </summary>
public class MenuSceneManager : MonoBehaviour
{
    public void PlayGame()
    {
        FindObjectOfType<MenuSceneSound>().StopMusic();
        ApplicationState.loseSoundAlreadyPlayed = false;
        ApplicationState.menuLoaded = false;
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Quit the application.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}

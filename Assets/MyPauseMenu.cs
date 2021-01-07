using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Log;

/// <summary>
/// Responsible for the pause menu behaviour.
/// </summary>
public class MyPauseMenu : MonoBehaviour
{
    private string sceneLoadedOnMenuButton = "Main Menu";

    public GameObject pauseMenuUI;
    public GameObject pauseButton;

    /// <summary>
    /// Checks to see if the user wants to pause the game by hitting a key.
    /// If the game is already paused, reusme game.
    /// If the game is not paused, pause the game.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ApplicationState.gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (ApplicationState.countDownOver)
        {
            pauseButton.SetActive(true);
        }
    }

    /// <summary>
    /// Called when the user clicks the pause button.
    /// If the game is paused, resume the game.
    /// If the game is not paused, pause the game.
    /// </summary>
    public void pauseButtonPressed()
    {
        if (ApplicationState.gamePaused) 
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    /// <summary>
    /// Resume the game.
    /// </summary>
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        ApplicationState.gamePaused = false;
    }

    /// <summary>
    /// Pause the game.
    /// </summary>
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        ApplicationState.gamePaused = true;
    }

    /// <summary>
    /// Load the Main Menu scene.
    /// </summary>
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        ApplicationState.menuLoaded = true;
        ApplicationState.gamePaused = false;
        ApplicationState.loseSoundAlreadyPlayed = false;
        ApplicationState.countDownOver = false;
        ScoreTracker.P1wins = 0;
        ScoreTracker.P2wins = 0;
        SceneManager.LoadScene(sceneLoadedOnMenuButton);
    }

    /// <summary>
    /// Quit the application.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}

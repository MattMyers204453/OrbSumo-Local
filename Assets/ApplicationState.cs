using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Overarching static class that deals with variables relevant across all scenes. The "state" of the application.
/// </summary>
public static class ApplicationState
{
    public static float musicVolume;
    public static bool menuLoaded;
    public static bool gamePaused;
    public static bool loseSoundAlreadyPlayed;
    public static string playerOneUserName;
    public static string playerTwoUserName;
    public static bool playerOneUserNameSelected;
    public static bool playerTwoUserNameSelected;
    public static bool countDownOver;
    public static bool startOfNewGameScene;
}

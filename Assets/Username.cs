using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Username : MonoBehaviour
{
    public TMP_InputField userName;
    public int whichPlayer;

    public GameObject playButton;

    public void saveUsername()
    {
        if (whichPlayer == 1) 
        {
            ApplicationState.playerOneUserName = userName.text;
            ApplicationState.playerOneUserNameSelected = true;
        }
        else if (whichPlayer == 2)
        {
            ApplicationState.playerTwoUserName = userName.text;
            ApplicationState.playerTwoUserNameSelected = true;
        }
    }
}

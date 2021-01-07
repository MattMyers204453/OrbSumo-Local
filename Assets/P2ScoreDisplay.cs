using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Log;
using TMPro;

/// <summary>
/// Responsible for displaying player two's score.
/// </summary>
public class P2ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI score2;

    /// <summary>
    /// Update player two's score.
    /// </summary>
    void Update()
    {
        if (ScoreTracker.P2wins > 9)
        {
            score2.fontSize = 40;
        }
        score2.text = ScoreTracker.P2wins.ToString(); 
    }
}

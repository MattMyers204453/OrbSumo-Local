using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Log;
using TMPro;

/// <summary>
/// Responsible for displaying player one's score.
/// </summary>
public class P1ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI score1;

    /// <summary>
    /// Update player one's score.
    /// </summary>
    void Update()
    {
        if (ScoreTracker.P1wins > 9)
        {
            score1.fontSize = 40;
        }
        score1.text = ScoreTracker.P1wins.ToString();
    }
}

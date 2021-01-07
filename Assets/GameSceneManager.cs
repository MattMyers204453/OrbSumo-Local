using UnityEngine;
using UnityEngine.SceneManagement;
using Log;

/// <summary>
/// Responsible for managing the flow of the game scene. This includes restarting the scene after each "round" and updating the score.
/// </summary>
public class GameSceneManager : MonoBehaviour
{
    public Rigidbody P1;
    public Rigidbody P2;
    public float restartDelay = 0.0f;
    public int P1Wins;
    public int P2Wins;

    private float countDownTimer;
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject fight;
    public bool countDownDone;

    public AudioSource fightAudio;
    public AudioSource fightAudio2;
    private bool fightAudioPlayed;
    public AudioSource countAudio;
    private bool countAudioPlayedThree;
    private bool countAudioPlayedTwo;
    private bool countAudioPlayedOne;

    private bool countDownHappened;

    /// <summary>
    /// If the round is the first round, do not play the losing sound.
    /// Otherwise, play the losing sound.
    /// </summary>
    public void Start()
    {
        if (!ApplicationState.loseSoundAlreadyPlayed)
        {

            ApplicationState.loseSoundAlreadyPlayed = true;
        }
        else
        {
            countDownHappened = true;
            countDownDone = true;
            FindObjectOfType<GameSceneSound>().audioLose.Play();
        }
    }

    /// <summary>
    /// Once a player falls over the ledge, restart the scene.
    /// </summary>
    public void EndGameAndRestart()
    {
        UpdateScore(P1.position.y, P2.position.y);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// If player one is in a higher vertical position at the time this method is called, 
    /// increase player one's score by 1.
    /// 
    /// Otherwise, increase player two's score by 1.
    /// </summary>
    /// <param name="P1y"></param>
    /// <param name="P2y"></param>
    private void UpdateScore(float P1y, float P2y)
    {
        if (P1y > P2y)
        {
            ScoreTracker.P1wins++;
        }
        else
        {
            ScoreTracker.P2wins++;
        }
    }

    private void Update()
    {
        if (!countDownHappened)
        {
            countDownTimer += Time.deltaTime;

            if (countDownTimer > 1 && countDownTimer < 2)
            {
                three.SetActive(true);
                if (!countAudioPlayedThree)
                {
                    countAudio.Play();
                    countAudioPlayedThree = true;
                }
            }
            if (countDownTimer > 2 && countDownTimer < 3)
            {
                three.SetActive(false);
                two.SetActive(true);
                if (!countAudioPlayedTwo)
                {
                    countAudio.Play();
                    countAudioPlayedTwo = true;
                }
            }
            else if (countDownTimer > 3 && countDownTimer < 4)
            {
                two.SetActive(false);
                one.SetActive(true);
                if (!countAudioPlayedOne)
                {
                    countAudio.Play();
                    countAudioPlayedOne = true;
                }
            }
            else if (countDownTimer > 4 && countDownTimer < 5)
            {
                one.SetActive(false);
                fight.SetActive(true);
                if (!fightAudioPlayed)
                {
                    fightAudio.Play();
                    fightAudio2.Play();
                    fightAudioPlayed = true;
                }
            }
            else if (countDownTimer > 5)
            {
                countDownDone = true;
                fight.SetActive(false);
                ApplicationState.countDownOver = true;
            }
        }
        else
        {
            three.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class ResultScreen : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [Header("Settings")]
    [SerializeField]
    private Scores scoreContainer;
    [SerializeField]
    private string remainingTimePrefix = "Remaining Time: ";
    [SerializeField]
    private string totalScorePrefix = "Total Score: ";
    [Header("UI References")]
    [SerializeField]
    private Text[] headerSuccess;
    [SerializeField]
    private Text[] headerFailure;
    [SerializeField]
    private Text remainingTimeText;
    [SerializeField]
    private Text totalScoreText;
    [SerializeField]
    private Text player1ScoreText;
    [SerializeField]
    private Text player2ScoreText;
    [SerializeField]
    private Text player3ScoreText;
    [SerializeField]
    private Image player1Crown;
    [SerializeField]
    private Image player2Crown;
    [SerializeField]
    private Image player3Crown;
    [SerializeField]
    private Image buttonIcon;
    [SerializeField]
    private Text pressStartText;

    // Private
    private bool iconSizeChanged = false;
    private bool inputAllowed = false;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Start () 
	{
        UpdateTime();
        UpdateScores();
        DisplayMvp();

        buttonIcon.enabled = false;
        pressStartText.enabled = false;

        Invoke("AllowInput", 2f);
        InvokeRepeating("ToggleIconSize", 0.6f, 0.6f);
    }

    public void Update()
    {
        if (inputAllowed && Input.GetButtonDown(Constants.INPUT_A))
            Continue();
    }
    #endregion



    #region Public Functions

    #endregion



    #region Private Functions
    private void UpdateTime()
    {
        if (scoreContainer.TimeLeft > 0f)
        {
            int minutes = Mathf.FloorToInt(scoreContainer.TimeLeft / 60f);
            int seconds = Mathf.FloorToInt(scoreContainer.TimeLeft - minutes * 60f);
            string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);

            remainingTimeText.text = remainingTimePrefix + timeString;
        }
        else
            remainingTimeText.enabled = false;

        foreach (Text t in headerSuccess)
            if (scoreContainer.TimeLeft > 0f)
                t.enabled = false;
            else
                t.enabled = true;
        foreach (Text t in headerFailure)
            if (scoreContainer.TimeLeft > 0f)
                t.enabled = true;
            else
                t.enabled = false;
    }

    private void UpdateScores()
    {
        totalScoreText.text = totalScorePrefix + scoreContainer.ScoreGlobal.ToString();
        player1ScoreText.text = scoreContainer.ScorePlayer1.ToString();
        player2ScoreText.text = scoreContainer.ScorePlayer2.ToString();
        player3ScoreText.text = scoreContainer.ScorePlayer3.ToString();
    }

    private void DisplayMvp()
    {
        int highscore = Mathf.Max(scoreContainer.ScorePlayer1, scoreContainer.ScorePlayer2, scoreContainer.ScorePlayer3);
        player1Crown.enabled = scoreContainer.ScorePlayer1 == highscore;
        player2Crown.enabled = scoreContainer.ScorePlayer2 == highscore;
        player3Crown.enabled = scoreContainer.ScorePlayer3 == highscore;
    }

    private void Continue()
    {
        // TODO
        SceneManager.LoadScene(Constants.SCENE_TITLE, LoadSceneMode.Single);
    }

    private void AllowInput()
    {
        buttonIcon.enabled = true;
        pressStartText.enabled = true;
        inputAllowed = true;
    }

    private void ToggleIconSize()
    {
        Vector2 newSize = iconSizeChanged ? new Vector2(1f, 1f) : new Vector2(0.8f, 0.8f);
        iconSizeChanged = !iconSizeChanged;
        buttonIcon.rectTransform.localScale = newSize;
    }
    #endregion



    #region Coroutines

    #endregion
}


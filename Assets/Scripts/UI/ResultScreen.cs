using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private string totalScorePrefix = "Total Score: ";
    [Header("UI References")]
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
    // Private
    private int totalScore;
    private int player1Score;
    private int player2Score;
    private int player3Score;

    private bool iconSizeChanged = false;
    private bool inputAllowed = false;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Start () 
	{
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
    private void UpdateScores()
    {
        // TODO
    }

    private void DisplayMvp()
    {
        int highscore = Mathf.Max(player1Score, player2Score, player3Score);
        // TODO
    }

    private void Continue()
    {
        // TODO
    }

    private void AllowInput()
    {
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


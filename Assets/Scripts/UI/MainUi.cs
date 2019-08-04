using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class MainUi : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [Header("UI References")]
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text timerText;
    [SerializeField]
    private Image[] defectTerminalIcons;
    [Header("Colors")]
    [SerializeField]
    private Color defectTerminalIconDefaultColor;
    [SerializeField]
    private Color defectTerminalIconActiveColor;
    // Private
    private int numberOfDestroyedTerminals = 0;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Start()
    {
        UpdateScoreDisplay(0);
        //UpdateTimerDisplay();
        UpdateDefectDisplay();
    }
    #endregion



    #region Public Functions
    public void UpdateScoreDisplay(int newScore)
    {
        scoreText.text = newScore.ToString();
    }

    public void UpdateTimerDisplay(float newTime, float totalTime)
    {
        int minutes = Mathf.FloorToInt(newTime / 60f);
        int seconds = Mathf.FloorToInt(newTime - minutes * 60f);
        string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);

        timerText.text = timeString;
    }

    public void OnTerminalDestroyed(TerminalController terminal)
    {
        numberOfDestroyedTerminals++;
        UpdateDefectDisplay();
    }

    public void OnTerminalRepaired(TerminalController terminal)
    {
        numberOfDestroyedTerminals--;
        UpdateDefectDisplay();
    }
    #endregion



    #region Private Functions
    private void UpdateDefectDisplay()
    {
        for(int i = 0; i < defectTerminalIcons.Length; i++)
        {
            if(numberOfDestroyedTerminals > i)
                defectTerminalIcons[i].color = defectTerminalIconActiveColor;
            else
                defectTerminalIcons[i].color = defectTerminalIconDefaultColor;
        }
    }
    #endregion



    #region Coroutines

    #endregion
}


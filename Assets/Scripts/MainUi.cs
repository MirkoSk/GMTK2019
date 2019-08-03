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
    [Header("Settings")]
    [SerializeField]
    private string scorePrefix = "Score: ";
    [SerializeField]
    private string timerPrefix = "Time: ";
    [Header("UI References")]
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text timerText;
    // Private

    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Start () 
	{
		
	}
	#endregion
	
	
	
	#region Public Functions
	public void UpdateScoreDisplay(int newScore)
    {
        scoreText.text = scorePrefix + newScore;
    }

    public void UpdateTimerDisplay(float newTime, float totalTime)
    {
        int minutes = Mathf.FloorToInt(newTime / 60f);
        int seconds = Mathf.FloorToInt(newTime - minutes * 60f);
        string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);

        timerText.text = timerPrefix + timeString;
    }
	#endregion
	
	
	
	#region Private Functions

	#endregion
	
	
	
	#region Coroutines
	
	#endregion
}


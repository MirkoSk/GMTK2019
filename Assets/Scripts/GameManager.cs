using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the game on a global Scale
/// Timer for the Game
/// Score Record
/// Player-Scores (which Player repaired how much Terminals)
/// </summary>
public class GameManager : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [Tooltip("How long is the Level? (In Seconds)")]
    [SerializeField] float gameLengh = 600f;
    [SerializeField] float tickInterval = .1f;
    [SerializeField] GameEvent scoreUpdatedEvent;
    [SerializeField] GameEvent gameTimeUpdatedEvent;
    [SerializeField] CharacterController player1;
    [SerializeField] CharacterController player2;
    [SerializeField] CharacterController player3;
    [SerializeField] Scores score;

    // Private
    int globalScore = 0;
    float gameTimer;
    

    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Start()
    {
        gameTimer = gameLengh;
        InvokeRepeating("Tick", tickInterval, tickInterval);
    }
    #endregion



    #region Public Functions
    public void UpdateScore(TerminalController terminalController, CharacterController characterController, int points)
    {
        if(characterController == player1)
        {
            score.ScorePlayer1 += points;
        }else if(characterController == player2)
        {
            score.ScorePlayer2 += points;
        }
        else
        {
            score.ScorePlayer3 += points;
        }
        globalScore += points;
        RaiseGlobalScoreUpdated(globalScore);
    }

    
    #endregion



    #region Private Functions
    private void Tick()
    {
        gameTimer -= tickInterval;
        score.TimeLeft = gameTimer;
        RaiseGameTimerUpdated(gameTimer, gameLengh);
    }

    private void RaiseGameTimerUpdated(float newTime, float gameLengh)
    {
        gameTimeUpdatedEvent.Raise(this, newTime, this.gameLengh);
    }

    private void RaiseGlobalScoreUpdated(int newScore)
    {
        scoreUpdatedEvent.Raise(this, newScore);
    }
    #endregion



    #region Coroutines

    #endregion
}


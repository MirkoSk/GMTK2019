using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class GameManager : MonoBehaviour 
{

    /* This Class controls the game on a global Scale
     * Timer for the Game
     * Score Record
     * Player-Scores (which Player repaired how much Terminals)
     * 
     */



    #region Variable Declarations
    // Serialized Fields
    [Tooltip("How long is the Level? (In Seconds)")]
    [SerializeField] float gameLengh = 600f;
    [SerializeField] float tickInterval = .1f;
    [SerializeField] GameEvent scoreUpdatedEvent;
    [SerializeField] GameEvent gameTimeUpdatedEvent;

    // Private
    int globalScore = 0;
    float gameTimer = 0f;

    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Start()
    {
        InvokeRepeating("Tick", tickInterval, tickInterval);
    }
    #endregion



    #region Public Functions
    public void UpdateScore(TerminalController terminalController, CharacterController characterController, int points)
    {
        globalScore += points;
        RaiseGlobalScoreUpdated(globalScore);
    }

    
    #endregion



    #region Private Functions
    private void Tick()
    {
        gameTimer += tickInterval;
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


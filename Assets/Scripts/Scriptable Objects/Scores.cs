using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

[CreateAssetMenu(menuName = "Scriptable Objects/Score")]
public class Scores : ScriptableObject 
{

    #region Variable Declarations
    // Serialized Fields

    // Private
    private int scorePlayer1;
    private int scorePlayer2;
    private int scorePlayer3;

    private float timeLeft;
    #endregion



    #region Public Properties
    public int ScorePlayer1 { get { return scorePlayer1; } set { scorePlayer1 = value; } }
    public int ScorePlayer2 { get { return scorePlayer2; } set { scorePlayer2 = value; } }
    public int ScorePlayer3 { get { return scorePlayer3; } set { scorePlayer3 = value; } }

    public int ScoreGlobal { get { return scorePlayer1 + scorePlayer2 + scorePlayer3; } }
    public float TimeLeft { get { return timeLeft; } set { timeLeft = value; } }
    #endregion

    #region Unity Event Functions

    #endregion

    #region Public Functions

    #endregion



    #region Private Functions

    #endregion



    #region Coroutines

    #endregion
}


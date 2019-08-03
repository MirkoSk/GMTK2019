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
    [SerializeField] float gameTime = 600f;


    // Private
    int globalScore = 0;
    float gameTimer = 0f;

    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Update()
    {
        
    }
    #endregion



    #region Public Functions

    #endregion



    #region Private Functions

    #endregion



    #region Coroutines

    #endregion
}


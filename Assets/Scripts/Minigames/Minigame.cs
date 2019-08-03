using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public abstract class Minigame : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [Header("Minigame Properties")]
    [SerializeField]
    private int pointsWhenSucceeded = 100;
    [SerializeField]
    private float timePenaltyWhenFailed = 1f;
    [Header("Game Events")]
    [SerializeField]
    protected GameEvent minigameSucceededEvent;
    [SerializeField]
    protected GameEvent minigameFailedEvent;
    // Private
    private TerminalController terminal = null;
    private CharacterController player = null;
	#endregion
	
	
	
	#region Public Properties
	
	#endregion
	
	
	
	#region Unity Event Functions
	private void Start () 
	{
		
	}
	#endregion
	
	
	
	#region Public Functions
	public virtual void StartMinigame(TerminalController terminal, CharacterController player)
    {
        // Remember terminal that started the minigame:
        this.terminal = terminal;
        // Remember player playing the minigame:
        this.player = player;
    }
	#endregion
	
	
	
	#region Private Functions
    protected void FinishMinigame(bool result)
    {
        // Minigame successful:
        if (result)
            RaiseMinigameSucceeded(terminal, player, pointsWhenSucceeded);

        // Minigame failed:
        else
            RaiseMinigameFailed(terminal, player, timePenaltyWhenFailed);

        // Reset references:
        terminal = null;
        player = null;
    }

    protected abstract void RaiseMinigameSucceeded(TerminalController terminal, CharacterController player, int points);
    protected abstract void RaiseMinigameFailed(TerminalController terminal, CharacterController player, float timePenalty);
	#endregion
	
	
	
	#region Coroutines
	
	#endregion
}


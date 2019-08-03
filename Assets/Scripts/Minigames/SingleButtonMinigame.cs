using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class SingleButtonMinigame : Minigame 
{

    #region Variable Declarations
    // Serialized Fields
    [SerializeField]
    private Image buttonIcon;
    // Private
    private string buttonToPress = null;
	#endregion
	
	
	
	#region Public Properties
	
	#endregion
	
	
	
	#region Unity Event Functions
	private void Start () 
	{
		
	}

    private void Update()
    {
        if (isRunning && buttonToPress != null)
            if (Input.GetKeyDown(buttonToPress))
                FinishMinigame(true);
    }
    #endregion



    #region Public Functions
    public override void StartMinigame(TerminalController terminal, CharacterController player)
    {
        // Call base class:
        base.StartMinigame(terminal, player);

        // Get random button to press:
        string button = inputController.GetUnusedButton();

        // Display button in UI:
        buttonIcon.sprite = inputController.GetInputIcon(button);
        buttonIcon.enabled = true;
    }
    #endregion



    #region Private Functions
    protected override void RaiseMinigameSucceeded(TerminalController terminal, CharacterController player, int points)
    {
        minigameSucceededEvent.Raise(this, terminal, player, points);
    }

    protected override void RaiseMinigameFailed(TerminalController terminal, CharacterController player, float timePenalty)
    {
        minigameFailedEvent.Raise(this, terminal, player, timePenalty);
    }
    #endregion



    #region Coroutines

    #endregion
}


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
    [Header("Necessary Buttons")]
    [SerializeField]
    private InputType inputType = InputType.BUTTON;
    [SerializeField]
    private int inputNumber = 1;
    [Header("Minigame UI")]
    [SerializeField]
    private Canvas minigameUi;
    [Header("Game Events")]
    [SerializeField]
    protected GameEvent minigameSucceededEvent;
    [SerializeField]
    protected GameEvent minigameFailedEvent;
    // Private
    private TerminalController terminal = null;
    private CharacterController player = null;
    protected InputController inputController;
    protected bool isRunning = false;
	#endregion
	
	
	
	#region Public Properties
	public InputType InputType
    {
        get { return inputType; }
    }

    public int InputNumber
    {
        get { return inputNumber; }
    }
	#endregion
	
	
	
	#region Unity Event Functions
	protected virtual void Start () 
	{
        inputController = FindObjectOfType<InputController>();
        minigameUi.enabled = false;
	}
	#endregion
	
	
	
	#region Public Functions
	public virtual void StartMinigame(TerminalController terminal, CharacterController player)
    {
        // Remember terminal that started the minigame:
        this.terminal = terminal;
        // Remember player playing the minigame:
        this.player = player;

        // Display UI:
        minigameUi.enabled = true;

        // Start minigame:
        isRunning = true;
    }
	#endregion
	
	
	
	#region Private Functions
    protected virtual void FinishMinigame(bool successful)
    {
        // Cancel minigame:
        isRunning = false;

        // Hide UI:
        minigameUi.enabled = false;

        // Minigame successful:
        if (successful)
            RaiseMinigameSucceeded(terminal, player, pointsWhenSucceeded);

        // Minigame failed:
        else
            RaiseMinigameFailed(terminal, player, timePenaltyWhenFailed);

        // Reset references:
        terminal = null;
        player = null;

        // Release locked buttons:
        ReleaseInputs();
    }

    protected abstract void RaiseMinigameSucceeded(TerminalController terminal, CharacterController player, int points);
    protected abstract void RaiseMinigameFailed(TerminalController terminal, CharacterController player, float timePenalty);
    protected abstract void ReleaseInputs();
	#endregion
	
	
	
	#region Coroutines
	
	#endregion
}


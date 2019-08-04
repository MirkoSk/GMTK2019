using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private float timeLimit = 5f;
    [Header("Necessary Buttons")]
    [SerializeField]
    private InputType inputType = InputType.BUTTON;
    [SerializeField]
    private int inputNumber = 1;
    [Header("Minigame UI")]
    [SerializeField]
    private Canvas minigameUi;
    [SerializeField]
    private Image timerUi;
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
    private float timer = 0f;
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

        // Rotate UI to always face up:
        minigameUi.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        minigameUi.enabled = false;
	}

    protected virtual void Update()
    {
        // Check if time limit expired:
        if (isRunning)
        {
            timer += Time.deltaTime;
            timerUi.fillAmount = 1f - (timer / timeLimit);
            if (timer >= timeLimit)
                FinishMinigame(false);
            Debug.Log(timer + " / " + timeLimit);
        }
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

        // Start timer:
        timer = 0f;

        // Start minigame:
        isRunning = true;
    }
	#endregion
	
	
	
	#region Private Functions
    protected virtual void FinishMinigame(bool successful)
    {
        // Cancel minigame:
        isRunning = false;

        // Reset timer:
        timer = 0f;

        // Hide UI:
        minigameUi.enabled = false;

        // Minigame successful:
        if (successful)
        {
            RaiseMinigameSucceeded(terminal, player, pointsWhenSucceeded);
            /*SoundManager.PlaySound(successSound);*/
        }

        // Minigame failed:
        else
        {
            RaiseMinigameFailed(terminal, player, timePenaltyWhenFailed);
            /*SoundManager.PlaySound(failSound);*/
        }

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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class KeepPressedMinigame : Minigame 
{

    #region Variable Declarations
    // Serialized Fields
    [Header("Minigame Specific")]
    [SerializeField]
    private Image buttonIcon;
    [SerializeField]
    private Image progressBar;
    [SerializeField]
    private float targetHoldTime = 5f;
    // Private
    private string buttonToPress = null;
    private float currentHoldTime = 0f;
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
        {
            if (Input.GetKey(buttonToPress))
                currentHoldTime += Time.deltaTime;
            else
                currentHoldTime = 0;
            UpdateProgressBar();
        }
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
    private void UpdateProgressBar()
    {
        float fillAmount = currentHoldTime / targetHoldTime;
        progressBar.rectTransform.sizeDelta = new Vector2(1f, fillAmount);
    }

    protected override void FinishMinigame(bool successful)
    {
        // Call base class:
        base.FinishMinigame(successful);
    }

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


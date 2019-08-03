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
    private bool failWhenButtonReleased = false;
    [SerializeField]
    private float targetHoldTime = 5f;
    // Private
    private string buttonToPress = null;
    private float currentHoldTime = 0f;
    private float displayedFillAmount = 0f;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    protected override void Start () 
	{
        // Call base class:
        base.Start();
	}

    private void Update()
    {
        if (isRunning && buttonToPress != null)
        {
            // Count time that button has been pressed:
            if (Input.GetButton(buttonToPress))
                currentHoldTime += Time.deltaTime;
            else
            {
                // Fail when button is released too early:
                if (failWhenButtonReleased && currentHoldTime > 0f)
                {
                    FinishMinigame(false);
                    return;
                }

                // Reset progress:
                currentHoldTime = 0;
            }

            UpdateProgressBar();

            // Check if finished:
            if (currentHoldTime >= targetHoldTime)
                FinishMinigame(true);
        }
    }
    #endregion



    #region Public Functions
    public override void StartMinigame(TerminalController terminal, CharacterController player)
    {
        // Call base class:
        base.StartMinigame(terminal, player);

        // Get random button to press:
        buttonToPress = inputController.GetUnusedButton();

        // Initialize UI:
        buttonIcon.sprite = inputController.GetInputIcon(buttonToPress);
        displayedFillAmount = 0f;
    }
    #endregion



    #region Private Functions
    private void UpdateProgressBar()
    {
        // Animate progress bar smoothly:
        float fillAmount = currentHoldTime / targetHoldTime;
        displayedFillAmount = Mathf.Min(0.9f * displayedFillAmount + 0.1f * fillAmount, 1f);
        progressBar.rectTransform.localScale = new Vector2(1f, displayedFillAmount);
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

    protected override void ReleaseInputs()
    {
        inputController.ReleaseInputs(new string[] { buttonToPress });
        buttonToPress = null;
    }
    #endregion



    #region Coroutines

    #endregion
}


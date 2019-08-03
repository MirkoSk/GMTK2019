using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class ButtonMasherMinigame : Minigame 
{

    #region Variable Declarations
    // Serialized Fields
    [Header("Minigame Specific")]
    [SerializeField]
    private int targetPresses = 20;
    [SerializeField]
    private float timeLimit = 5f;
    [SerializeField]
    private Image buttonIcon;
    // Private
    private string buttonToPress = null;
    private int currentButtonPresses = 0;
    private float timer = 0f;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    protected override void Start()
    {
        // Call base class:
        base.Start();
    }

    private void Update()
    {
        if (isRunning && buttonToPress != null)
        {
            timer += Time.deltaTime;

            // Count button presses:
            if (Input.GetButtonDown(buttonToPress))
            {
                if (++currentButtonPresses >= targetPresses)
                {
                    FinishMinigame(true);
                    return;
                }
            }

            // Check if time limit expired:
            else if (timer > timeLimit)
                FinishMinigame(false);
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

        // Display button in UI:
        buttonIcon.sprite = inputController.GetInputIcon(buttonToPress);

        // Reset press counter:
        currentButtonPresses = 0;

        // Start timer:
        timer = 0f;
    }
    #endregion



    #region Private Functions
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


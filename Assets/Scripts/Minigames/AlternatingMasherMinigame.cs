using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class AlternatingMasherMinigame : Minigame 
{

    #region Variable Declarations
    // Serialized Fields
    [Header("Minigame Specific")]
    [SerializeField]
    private int targetPresses = 20;
    [SerializeField]
    private Image buttonIcon1;
    [SerializeField]
    private Image buttonIcon2;
    // Private
    private string buttonToPress1 = null;
    private string buttonToPress2 = null;
    private string currentButton = null;
    private int currentButtonPresses = 0;
    private bool iconSizeChanged = false;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    protected override void Start()
    {
        // Call base class:
        base.Start();

        InvokeRepeating("ToggleIconSize", 0.15f, 0.15f);
    }

    protected override void Update()
    {
        // Call base class:
        base.Update();

        if (isRunning && buttonToPress1 != null && buttonToPress2 != null)
        {
            // Count button presses:
            if (Input.GetButtonDown(currentButton))
            {
                if (++currentButtonPresses >= targetPresses)
                {
                    FinishMinigame(true);
                    return;
                }

                // Alternate button to the other one:
                else
                    currentButton = currentButton == buttonToPress1 ? buttonToPress2 : buttonToPress1;
            }
        }
    }
    #endregion



    #region Public Functions
    public override void StartMinigame(TerminalController terminal, CharacterController player)
    {
        // Call base class:
        base.StartMinigame(terminal, player);

        // Get random buttons to press:
        string[] unusedButtons = inputController.GetUnusedButtons(2);

        // Cancel minigame as successful if no inputs are available:
        if (unusedButtons == null)
        {
            Debug.LogWarning("Minigame has been canceled, no unused inputs available");
            FinishMinigame(true);
            return;
        }

        // Assign previously selected buttons:
        buttonToPress1 = unusedButtons[0];
        buttonToPress2 = unusedButtons[1];
        currentButton = buttonToPress1;

        // Display buttons in UI:
        buttonIcon1.sprite = inputController.GetInputIcon(buttonToPress1);
        buttonIcon2.sprite = inputController.GetInputIcon(buttonToPress2);

        // Reset press counter:
        currentButtonPresses = 0;
    }
    #endregion



    #region Private Functions
    protected override void FinishMinigame(bool successful)
    {
        // Call base class:
        base.FinishMinigame(successful);
    }

    private void ToggleIconSize()
    {
        Vector2 newSize1 = iconSizeChanged ? new Vector2(0.8f, 0.8f) : new Vector2(0.6f, 0.6f);
        Vector2 newSize2 = iconSizeChanged ? new Vector2(0.6f, 0.6f) : new Vector2(0.8f, 0.8f);

        iconSizeChanged = !iconSizeChanged;

        buttonIcon1.rectTransform.localScale = newSize1;
        buttonIcon2.rectTransform.localScale = newSize2;
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
        inputController.ReleaseInputs(new string[] { buttonToPress1, buttonToPress2 });
        buttonToPress1 = null;
        buttonToPress2 = null;
    }
    #endregion



    #region Coroutines

    #endregion
}


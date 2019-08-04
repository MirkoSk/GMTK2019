using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class ButtonPressMinigame : Minigame 
{

    #region Variable Declarations
    // Serialized Fields
    [Header("Minigame Specific")]
    [SerializeField]
    private int numberOfButtons = 1;
    [SerializeField]
    private Image[] buttonIcons;
    [SerializeField]
    private Vector3 buttonIconCheckedSize = new Vector3(0.6f, 0.6f, 1f);
    // Private
    private string[] buttonsToPress = null;
    private List<string> currentlyPressedButtons;
    private Vector3 buttonIconRegularSize;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    protected override void Start()
    {
        // Call base class:
        base.Start();

        // Save regular size for button icons:
        buttonIconRegularSize = buttonIcons[0].rectTransform.localScale;
    }

    protected override void Update()
    {
        // Call base class:
        base.Update();

        if (isRunning && buttonsToPress != null)
        {
            // Check which of the desired buttons are pressed:
            foreach(string s in buttonsToPress)
            {
                if (Input.GetButtonDown(s) && !currentlyPressedButtons.Contains(s))
                {
                    currentlyPressedButtons.Add(s);
                    CheckButtonIcon(buttonIcons[System.Array.IndexOf(buttonsToPress, s)]);
                }
                if (Input.GetButtonUp(s))
                {
                    currentlyPressedButtons.Remove(s);
                    UncheckButtonIcon(buttonIcons[System.Array.IndexOf(buttonsToPress, s)]);
                }
            }

            // Check if all buttons have been pressed:
            if (currentlyPressedButtons.Count >= numberOfButtons)
                FinishMinigame(true);
        }
    }
    #endregion



    #region Public Functions
    public override void StartMinigame(TerminalController terminal, CharacterController player)
    {
        currentlyPressedButtons = new List<string>();

        // Call base class:
        base.StartMinigame(terminal, player);

        // Get random buttons to press:
        buttonsToPress = inputController.GetUnusedButtons(numberOfButtons);

        // Cancel minigame as successful if no inputs are available:
        if (buttonsToPress == null)
        {
            Debug.LogWarning("Minigame has been canceled, no unused inputs available");
            FinishMinigame(true);
        }

        // Display buttons in UI:
        for (int i = 0; i < numberOfButtons; i++)
        {
            buttonIcons[i].sprite = inputController.GetInputIcon(buttonsToPress[i]);
            UncheckButtonIcon(buttonIcons[i]);
        }
    }
    #endregion



    #region Private Functions
    private void CheckButtonIcon(Image buttonIcon)
    {
        buttonIcon.rectTransform.localScale = buttonIconCheckedSize;
    }

    private void UncheckButtonIcon(Image buttonIcon)
    {
        buttonIcon.rectTransform.localScale = buttonIconRegularSize;
    }

    protected override void FinishMinigame(bool successful)
    {
        // Call base class:
        base.FinishMinigame(successful);

        currentlyPressedButtons = new List<string>();
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
        inputController.ReleaseInputs(buttonsToPress);
        buttonsToPress = null;
    }
    #endregion



    #region Coroutines

    #endregion
}


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
    private float timeLimit = 5f;
    [SerializeField]
    private Image[] buttonIcons;
    // Private
    private string[] buttonsToPress = null;
    private List<string> currentlyPressedButtons;
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
        if (isRunning && buttonsToPress != null)
        {
            timer += Time.deltaTime;

            // Check which of the desired buttons are pressed:
            foreach(string s in buttonsToPress)
            {
                if (Input.GetButtonDown(s) && !currentlyPressedButtons.Contains(s))
                    currentlyPressedButtons.Add(s);
                if (Input.GetButtonUp(s))
                    currentlyPressedButtons.Remove(s);
            }

            // Check if all buttons have been pressed:
            if (currentlyPressedButtons.Count >= numberOfButtons)
                FinishMinigame(true);

            // Check if time limit expired:
            else if (timer > timeLimit)
                FinishMinigame(false);
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
            buttonIcons[i].sprite = inputController.GetInputIcon(buttonsToPress[i]);

        // Start timer:
        timer = 0f;
    }
    #endregion



    #region Private Functions
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


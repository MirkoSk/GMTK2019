using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class BalanceMinigame : Minigame 
{

    #region Variable Declarations
    // Serialized Fields
    [Header("Minigame Specific")]
    [SerializeField]
    [MinMaxRange(0f, 1f)]
    private RangedFloat targetArea;
    [SerializeField]
    private float targetHoldTime = 1f;
    [SerializeField]
    private Image buttonIcon;
    [SerializeField]
    private Image barCursor;
    [SerializeField]
    private Image barTargetArea;
    // Private
    private string triggerUsed = null;
    private float currentHoldTime = 0f;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    protected override void Start () 
	{
        // Call base class:
        base.Start();
	}

    protected override void Update()
    {
        // Call base class:
        base.Update();

        if (isRunning && triggerUsed != null)
        {
            // Read value from trigger:
            float triggerValue = Input.GetAxis(triggerUsed);
            UpdateCursorPosition(triggerValue);

            // Count time that cursor is inside target area:
            if (triggerValue >= targetArea.minValue && triggerValue <= targetArea.maxValue)
                currentHoldTime += Time.deltaTime;

            // Reset progress when leaving target area:
            else
                currentHoldTime = 0f;

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
        triggerUsed = inputController.GetUnusedTrigger();

        // Initialize UI:
        buttonIcon.sprite = inputController.GetInputIcon(triggerUsed);
        UpdateTargetArea();

        // Cancel minigame as successful if no inputs are available:
        if (triggerUsed == null)
        {
            Debug.LogWarning("Minigame has been canceled, no unused inputs available");
            FinishMinigame(true);
        }
    }
    #endregion



    #region Private Functions
    private void UpdateTargetArea()
    {
        float targetAreaPosition = targetArea.minValue + targetArea.maxValue;
        float targetAreaScale = targetArea.maxValue - targetArea.minValue;

        barTargetArea.rectTransform.anchoredPosition = new Vector2(0f, targetAreaPosition);
        barTargetArea.rectTransform.localScale = new Vector2(1f, targetAreaScale);
    }

    private void UpdateCursorPosition(float triggerValue)
    {
        float cursorPosition = triggerValue * 2f;
        barCursor.rectTransform.anchoredPosition = new Vector2(0f, cursorPosition);
    }

    protected override void FinishMinigame(bool successful)
    {
        // Call base class:
        base.FinishMinigame(successful);

        // Reset values:
        currentHoldTime = 0f;
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
        inputController.ReleaseInputs(new string[] { triggerUsed });
        triggerUsed = null;
    }
    #endregion



    #region Coroutines

    #endregion
}


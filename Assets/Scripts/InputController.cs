using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class InputController : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [Header("Game Events")]
    [SerializeField]
    private GameEvent playerAxisChangedEvent;
    [Header("Player References")]
    [SerializeField]
    private CharacterController player1;
    [SerializeField]
    private CharacterController player2;
    [SerializeField]
    private CharacterController player3;
    [Header("Sprites")]
    [SerializeField]
    private Sprite spriteA;
    [SerializeField]
    private Sprite spriteB;
    [SerializeField]
    private Sprite spriteX;
    [SerializeField]
    private Sprite spriteY;
    [SerializeField]
    private Sprite spriteLB;
    [SerializeField]
    private Sprite spriteRB;
    [SerializeField]
    private Sprite spriteSelect;
    [SerializeField]
    private Sprite spriteStart;
    [SerializeField]
    private Sprite spriteLeftStickButton;
    [SerializeField]
    private Sprite spriteRightStickButton;
    [SerializeField]
    private Sprite spriteLeftStick;
    [SerializeField]
    private Sprite spriteRightStick;
    [SerializeField]
    private Sprite spriteDpad;
    [SerializeField]
    private Sprite spriteLT;
    [SerializeField]
    private Sprite spriteRT;
    // Private
    private Dictionary<string, bool> inputUsed;
    private string[] buttonCodes = { Constants.INPUT_A, Constants.INPUT_B, Constants.INPUT_X, Constants.INPUT_Y, Constants.INPUT_SELECT, Constants.INPUT_START };
    private string[] triggerCodes = { Constants.INPUT_LT, Constants.INPUT_RT };
    private string[] axisCodes = { Constants.INPUT_LEFT_STICK_X, Constants.INPUT_RIGHT_STICK_X, Constants.INPUT_DPAD_X };
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Start () 
	{
        // Add all inputs and mark them as currently not in use:
        inputUsed = new Dictionary<string, bool>();
        inputUsed.Add(Constants.INPUT_A, false);
        inputUsed.Add(Constants.INPUT_B, false);
        inputUsed.Add(Constants.INPUT_X, false);
        inputUsed.Add(Constants.INPUT_Y, false);
        inputUsed.Add(Constants.INPUT_LB, false);
        inputUsed.Add(Constants.INPUT_RB, false);
        inputUsed.Add(Constants.INPUT_SELECT, false);
        inputUsed.Add(Constants.INPUT_START, false);
        /*inputUsed.Add(Constants.INPUT_LEFT_STICK_BUTTON, false);
        inputUsed.Add(Constants.INPUT_RIGHT_STICK_BUTTON, false);*/
        inputUsed.Add(Constants.INPUT_LEFT_STICK_X, false);
        /*inputUsed.Add(Constants.INPUT_LEFT_STICK_Y, false);*/
        inputUsed.Add(Constants.INPUT_RIGHT_STICK_X, false);
        /*inputUsed.Add(Constants.INPUT_RIGHT_STICK_Y, false);*/
        inputUsed.Add(Constants.INPUT_DPAD_X, false);
        /*inputUsed.Add(Constants.INPUT_DPAD_Y, false);*/
        inputUsed.Add(Constants.INPUT_LT, false);
        inputUsed.Add(Constants.INPUT_RT, false);

        // Shuffle initial player control axes:
        ShufflePlayerAxes();
    }

    private void Update()
    {
        // Debug:
        //LogInputs();
    }
    #endregion



    #region Public Functions
    public void ShufflePlayerAxes()
    {
        // Select random X axis per player:
        string player1X = GetUnusedAxis();
        string player2X = GetUnusedAxis();
        string player3X = GetUnusedAxis();

        // Choose corresponding Y axis per player:
        string player1Y = GetCorrespondingAxis(player1X);
        string player2Y = GetCorrespondingAxis(player2X);
        string player3Y = GetCorrespondingAxis(player3X);

        // Raise game events to publish changed movement axes:
        RaisePlayerAxisChanged(player1, player1X, player1Y);
        RaisePlayerAxisChanged(player2, player2X, player2Y);
        RaisePlayerAxisChanged(player3, player3X, player3Y);
    }

    public string GetUnusedButton()
    {
        // Get all unused buttons first:
        string[] unusedButtons = GetAllUnusedButtons();

        // Randomly select a single unused button:
        string input = unusedButtons[Random.Range(0, unusedButtons.Length)];

        // Mark button as used:
        inputUsed[input] = true;
        return input;
    }

    public string[] GetUnusedButtons(int number)
    {
        // Cancel and return null if not enough buttons are available:
        string[] allUnusedButtons = GetAllUnusedButtons();
        if (allUnusedButtons.Length < number)
            return null;
        if (allUnusedButtons.Length == number)
            return allUnusedButtons;

        // Pick wanted number of buttons randomly:
        List<string> result = new List<string>();
        for (int i = 0; i < number; i++)
            result.Add(GetUnusedButton());
        return result.ToArray();
    }

    public string GetUnusedTrigger()
    {
        // Get all unused triggers first:
        string[] unusedTriggers = GetAllUnusedTriggers();

        // Randomly select a single unused trigger:
        string input = unusedTriggers[Random.Range(0, unusedTriggers.Length)];

        // Mark trigger as used:
        inputUsed[input] = true;
        return input;
    }

    public string[] GetUnusedTriggers(int number)
    {
        // Cancel and return null if not enough triggers are available:
        string[] allUnusedTriggers = GetAllUnusedTriggers();
        if (allUnusedTriggers.Length < number)
            return null;
        if (allUnusedTriggers.Length == number)
            return allUnusedTriggers;

        // Pick wanted number of triggers randomly:
        List<string> result = new List<string>();
        for (int i = 0; i < number; i++)
            result.Add(GetUnusedTrigger());
        return result.ToArray();
    }

    public string GetUnusedAxis()
    {
        // Get all unused axes first:
        string[] unusedAxes = GetAllUnusedAxes();

        // Randomly select a single unused axis:
        string input = unusedAxes[Random.Range(0, unusedAxes.Length)];

        // Mark axis as used:
        inputUsed[input] = true;
        return input;
    }

    public string[] GetUnusedAxes(int number)
    {
        // Cancel and return null if not enough axes are available:
        string[] allUnusedAxes = GetAllUnusedAxes();
        if (allUnusedAxes.Length < number)
            return null;
        if (allUnusedAxes.Length == number)
            return allUnusedAxes;

        // Pick wanted number of axes randomly:
        List<string> result = new List<string>();
        for (int i = 0; i < number; i++)
            result.Add(GetUnusedAxis());
        return result.ToArray();
    }

    public void ReleaseInputs(string[] inputs)
    {
        foreach (string s in inputs)
            inputUsed[s] = false;
    }

    public Sprite GetInputIcon(string input)
    {
        if (input == Constants.INPUT_A)
            return spriteA;
        else if (input == Constants.INPUT_B)
            return spriteB;
        else if (input == Constants.INPUT_X)
            return spriteX;
        else if (input == Constants.INPUT_Y)
            return spriteY;
        else if (input == Constants.INPUT_LB)
            return spriteLB;
        else if (input == Constants.INPUT_RB)
            return spriteRB;
        else if (input == Constants.INPUT_SELECT)
            return spriteSelect;
        else if (input == Constants.INPUT_START)
            return spriteStart;
        else if (input == Constants.INPUT_LEFT_STICK_BUTTON)
            return spriteLeftStickButton;
        else if (input == Constants.INPUT_RIGHT_STICK_BUTTON)
            return spriteRightStickButton;
        else if (input == Constants.INPUT_LEFT_STICK_X || input == Constants.INPUT_LEFT_STICK_Y)
            return spriteLeftStick;
        else if (input == Constants.INPUT_RIGHT_STICK_X || input == Constants.INPUT_RIGHT_STICK_Y)
            return spriteRightStick;
        else if (input == Constants.INPUT_DPAD_X || input == Constants.INPUT_DPAD_Y)
            return spriteDpad;
        else
            return null;
    }
    #endregion



    #region Private Functions
    private void RaisePlayerAxisChanged(CharacterController player, string inputAxisX, string inputAxisY)
    {
        playerAxisChangedEvent.Raise(this, player, inputAxisX, inputAxisY);
    }

    private string[] GetAllUnusedButtons()
    {
        // Return all currently unused buttons (not triggers or axes):
        List<string> result = new List<string>();
        foreach (string s in buttonCodes)
            if (!inputUsed[s])
                result.Add(s);
        return result.ToArray();
    }

    private string[] GetAllUnusedTriggers()
    {
        // Return all currently unused triggers (not buttons or axes):
        List<string> result = new List<string>();
        foreach (string s in triggerCodes)
            if (!inputUsed[s])
                result.Add(s);
        return result.ToArray();
    }

    private string[] GetAllUnusedAxes()
    {
        // Return all currently unused triggers (not buttons or triggers):
        List<string> result = new List<string>();
        foreach (string s in axisCodes)
            if (!inputUsed[s])
                result.Add(s);
        return result.ToArray();
    }

    private string GetCorrespondingAxis(string axis)
    {
        // Return corresponding Y axis for given X axis or vice versa:
        if (axis == Constants.INPUT_LEFT_STICK_X)
            return Constants.INPUT_LEFT_STICK_Y;
        else if (axis == Constants.INPUT_LEFT_STICK_Y)
            return Constants.INPUT_LEFT_STICK_X;
        else if (axis == Constants.INPUT_RIGHT_STICK_X)
            return Constants.INPUT_RIGHT_STICK_Y;
        else if (axis == Constants.INPUT_RIGHT_STICK_Y)
            return Constants.INPUT_RIGHT_STICK_X;
        else if (axis == Constants.INPUT_DPAD_X)
            return Constants.INPUT_DPAD_Y;
        else if (axis == Constants.INPUT_DPAD_Y)
            return Constants.INPUT_DPAD_X;
        else return null;
    }

    private void LogInputs()
    {
        // Buttons
        if (Input.GetButtonDown(Constants.INPUT_A))
            Debug.Log("A");
        if (Input.GetButtonDown(Constants.INPUT_B))
            Debug.Log("B");
        if (Input.GetButtonDown(Constants.INPUT_X))
            Debug.Log("X");
        if (Input.GetButtonDown(Constants.INPUT_Y))
            Debug.Log("Y");
        if (Input.GetButtonDown(Constants.INPUT_LB))
            Debug.Log("LB");
        if (Input.GetButtonDown(Constants.INPUT_RB))
            Debug.Log("RB");
        if (Input.GetButtonDown(Constants.INPUT_SELECT))
            Debug.Log("Select");
        if (Input.GetButtonDown(Constants.INPUT_START))
            Debug.Log("Start");
        if (Input.GetButtonDown(Constants.INPUT_LEFT_STICK_BUTTON))
            Debug.Log("LeftStickButton");
        if (Input.GetButtonDown(Constants.INPUT_RIGHT_STICK_BUTTON))
            Debug.Log("RightStickButton");

        // Axes
        if (Input.GetAxis(Constants.INPUT_LEFT_STICK_X) != 0)
            Debug.Log("LeftStickX " + Input.GetAxis(Constants.INPUT_LEFT_STICK_X));
        if (Input.GetAxis(Constants.INPUT_LEFT_STICK_Y) != 0)
            Debug.Log("LeftStickY " + Input.GetAxis(Constants.INPUT_LEFT_STICK_Y));
        if (Input.GetAxis(Constants.INPUT_RIGHT_STICK_X) != 0)
            Debug.Log("RightStickX " + Input.GetAxis(Constants.INPUT_RIGHT_STICK_X));
        if (Input.GetAxis(Constants.INPUT_RIGHT_STICK_Y) != 0)
            Debug.Log("RightStickY " + Input.GetAxis(Constants.INPUT_RIGHT_STICK_Y));
        if (Input.GetAxis(Constants.INPUT_DPAD_X) != 0)
            Debug.Log("DpadX " + Input.GetAxis(Constants.INPUT_DPAD_X));
        if (Input.GetAxis(Constants.INPUT_DPAD_Y) != 0)
            Debug.Log("DpadY " + Input.GetAxis(Constants.INPUT_DPAD_Y));
        if (Input.GetAxis(Constants.INPUT_LT) != 0)
            Debug.Log("LT " + Input.GetAxis(Constants.INPUT_LT));
        if (Input.GetAxis(Constants.INPUT_RT) != 0)
            Debug.Log("RT " + Input.GetAxis(Constants.INPUT_RT));
    }
    #endregion



    #region Coroutines

    #endregion
}
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

    // Private
    private Dictionary<string, bool> inputUsed;
    private string[] buttonCodes = { Constants.INPUT_A, Constants.INPUT_B, Constants.INPUT_X, Constants.INPUT_Y, Constants.INPUT_SELECT, Constants.INPUT_START };
    private string[] triggerCodes = { Constants.INPUT_LT, Constants.INPUT_RT };
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
        inputUsed.Add(Constants.INPUT_RIGHT_STICK_BUTTON, false);
        inputUsed.Add(Constants.INPUT_LEFT_STICK_X, false);
        inputUsed.Add(Constants.INPUT_LEFT_STICK_Y, false);
        inputUsed.Add(Constants.INPUT_RIGHT_STICK_X, false);
        inputUsed.Add(Constants.INPUT_RIGHT_STICK_Y, false);
        inputUsed.Add(Constants.INPUT_DPAD_X, false);
        inputUsed.Add(Constants.INPUT_DPAD_Y, false);*/
        inputUsed.Add(Constants.INPUT_LT, false);
        inputUsed.Add(Constants.INPUT_RT, false);
    }

    private void Update()
    {
        // Debug:
        LogInputs();
    }
    #endregion



    #region Public Functions
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

    public void ReleaseInputs(string[] inputs)
    {
        foreach (string s in inputs)
            inputUsed[s] = false;
    }
    #endregion



    #region Private Functions
    private string[] GetAllUnusedButtons()
    {
        // Return all currently unused buttons (not triggers):
        List<string> result = new List<string>();
        foreach (string s in buttonCodes)
            if (inputUsed[s])
                result.Add(s);
        return result.ToArray();
    }

    private string[] GetAllUnusedTriggers()
    {
        // Return all currently unused triggers (not buttons):
        List<string> result = new List<string>();
        foreach (string s in triggerCodes)
            if (inputUsed[s])
                result.Add(s);
        return result.ToArray();
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
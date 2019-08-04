using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class TerminalDestroyer : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    

    [Header("Event Settings")]
    [SerializeField] int maxErroredAndDestroyedTerminals = 10;
    [SerializeField] float tickInterval = .3f;
    [Tooltip("Trying to counter the effects of repeatedly calling the terminal malfunctions on a tick basis.")]
    [SerializeField] float repetitionDampener = 0.5f;

    [Header("Game Start Balancing")]
    [SerializeField] float terminalActivationInterval = 3f;
    [Tooltip("Time in seconds added each time a terminal is activated the first time.")]
    [SerializeField] float activationIntervalIncrease = 1.5f;

    [Header("References")]
    [SerializeField] InputController inputController = null;
    [SerializeField] SpawnManager spawnManager = null;

    // Private
    int erroredAndDestroyedTerminals = 0;
    float timer = 0f;
    bool activationPhase = true;
    List<TerminalController> terminals = new List<TerminalController>();
    List<TerminalController> tempTerminals = new List<TerminalController>();
    int activatedTerminals = 0;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Start () 
	{
        tempTerminals = spawnManager.Terminals;
	}

    private void Update()
    {
        if (activationPhase && timer >= terminalActivationInterval + (activationIntervalIncrease * activatedTerminals))
        {
            if (tempTerminals.Count < 1)
            {
                activationPhase = false;
            }
            else
            {
                int number = Random.Range(0, tempTerminals.Count);
                terminals.Add(tempTerminals[number]);
                tempTerminals[number].InvokeError();
                tempTerminals.RemoveAt(number);
                erroredAndDestroyedTerminals++;
            }

            activatedTerminals++;
            timer = 0f;
        }

        else if (!activationPhase && timer >= tickInterval)
        {
            Tick();
            timer = 0f;
        }

        timer += Time.deltaTime;
    }
    #endregion



    #region Public Functions
    public void MinigameSucceeded()
    {
        erroredAndDestroyedTerminals--;
    }
    #endregion



    #region Private Functions
    private void Tick()
    {
        if (erroredAndDestroyedTerminals >= maxErroredAndDestroyedTerminals) return;

        for (int i = 0; i < terminals.Count; i++)
        {
            TerminalController terminal = terminals[i];

            // Calculate Probability
            if (terminal.TimerToFail >= terminal.TimeToFail.minValue)
            {
                float probability;
                if (terminal.TimerToFail > terminal.TimeToFail.maxValue)
                    probability = 100f;
                else
                    probability = terminal.TimerToFail.Remap(terminal.TimeToFail.minValue, terminal.TimeToFail.maxValue, 0f, 100f) * repetitionDampener;

                if (probability > Random.Range(0f, 100f))
                {
                    if (inputController.CheckUnusedInputs(terminal.LinkedMinigame.InputType, terminal.LinkedMinigame.InputNumber))
                    {
                        erroredAndDestroyedTerminals++;
                        terminal.InvokeError();
                    }
                }
            }
        }
    }
    #endregion



    #region Coroutines

    #endregion
}


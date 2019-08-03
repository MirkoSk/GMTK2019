using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class TerminalController : MonoBehaviour 
{
    public enum TerminalState
    {
        Idle,
        Error,
        Fixing,
        Destroyed
    }

    #region Variable Declarations
    // Serialized Fields
    [Header("Terminal Stats")]
    [MinMaxRange(0.1f, 20f)]
    public RangedFloat TimeToFail = new RangedFloat();
    public float TimeToExplode = 10f;
    public float TimeToRepair = 10f;
    public GameObject Minigame = null;

    [Header("Terminal State")]
    [SerializeField] TerminalState terminalState = TerminalState.Idle;

    // Private
    private float timerToFail = 0f;
    private float timerToExplode = 0f;
	#endregion
	
	
	
	#region Public Properties
	public float TimerToFail { get { return timerToFail; } }
    public float TimerToExplode { get { return timerToExplode; } }
    #endregion



    #region Unity Event Functions
    private void Update()
    {
        switch (terminalState)
        {
            case TerminalState.Idle:
                timerToFail += Time.deltaTime;
                break;

            case TerminalState.Error:
                timerToExplode += Time.deltaTime;
                break;

            case TerminalState.Fixing:
                break;

            case TerminalState.Destroyed:
                break;

            default:
                break;
        }
    }
    #endregion



    #region Public Functions
    public void InvokeError()
    {
        if (terminalState != TerminalState.Idle)
        {
            Debug.LogError("Tried to fail Terminal in wrong state: " + terminalState + "State.", gameObject);
            return;
        }

        timerToExplode = 0f;
        terminalState = TerminalState.Error;
    }

    public void StartFixing(TerminalController terminal, CharacterController player)
    {
        if (terminal != this) return;

        if (terminalState != TerminalState.Error)
        {
            Debug.LogError("Tried to start fixing Terminal in wrong state: " + terminalState + "State.", gameObject);
            return;
        }

        terminalState = TerminalState.Fixing;
    }

    public void FixTerminal(TerminalController terminal, CharacterController player, int points)
    {
        if (terminal != this) return;

        if (terminalState != TerminalState.Fixing)
        {
            Debug.LogError("Tried to fix Terminal in wrong state: " + terminalState + "State.", gameObject);
            return;
        }

        timerToFail = 0f;
        terminalState = TerminalState.Idle;
    }

    public void FailFixing(TerminalController terminal, CharacterController player, float timePenalty)
    {
        if (terminal != this) return;

        if (terminalState != TerminalState.Fixing)
        {
            Debug.LogError("Tried to fix Terminal in wrong state: " + terminalState + "State.", gameObject);
            return;
        }

        timerToExplode += timePenalty;
        // TODO: Switch to destroyed state when implemented
        terminalState = TerminalState.Idle;
    }
    #endregion



    #region Private Functions

    #endregion



    #region Coroutines

    #endregion
}


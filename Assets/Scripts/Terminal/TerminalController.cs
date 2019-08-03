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
        Destroyed,
        Repairing
    }

    #region Variable Declarations
    // Serialized Fields
    [Header("Terminal Stats")]
    [MinMaxRange(0.1f, 20f)]
    public RangedFloat TimeToFail = new RangedFloat();
    public float TimeToExplode = 10f;
    public float TimeToRepair = 10f;
    public Minigame LinkedMinigame = null;
    public Minigame RepairMinigame = null;

    [Header("Terminal State")]
    [SerializeField] TerminalState terminalState = TerminalState.Idle;

    [Header("References")]
    [SerializeField] Collider2D trigger = null;
    [SerializeField] Transform minigamePosition = null;
    [SerializeField] TerminalUi terminalUi = null;

    // Private
    private float timerToFail = 0f;
    private float timerToExplode = 0f;
	#endregion
	
	
	
	#region Public Properties
	public float TimerToFail { get { return timerToFail; } }
    public float TimerToExplode { get { return timerToExplode; } }
    public Transform MinigamePosition { get { return minigamePosition; } }
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
                if (timerToExplode >= TimeToExplode)
                {
                    terminalState = TerminalState.Destroyed;
                }

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

        trigger.enabled = true;
        timerToFail = 0f;
        ChangeState(TerminalState.Error);
    }    

    public void StartFixingOrRepairing (TerminalController terminal, CharacterController player)
    {
        if (terminal == this && terminalState == TerminalState.Error) StartFixing(terminal, player);
        else if (terminal == this && terminalState == TerminalState.Destroyed) StartRepairing(terminal, player);
    }

    public void FixOrRepair(TerminalController terminal, CharacterController player, int points)
    {
        if (terminal == this && terminalState == TerminalState.Fixing) FixTerminal(terminal, player, points);
        else if (terminal == this && terminalState == TerminalState.Repairing) Repair(terminal, player, points);
    }

    public void FailFixOrRepair(TerminalController terminal, CharacterController player, float timePenalty)
    {
        if (terminal == this && terminalState == TerminalState.Fixing) FailFixing(terminal, player, timePenalty);
        else if (terminal == this && terminalState == TerminalState.Repairing) FailRepairing(terminal, player, timePenalty);
    }
    #endregion



    #region Private Functions
    private void StartFixing(TerminalController terminal, CharacterController player)
    {
        if (terminal != this) return;

        if (terminalState != TerminalState.Error)
        {
            Debug.LogError("Tried to start fixing Terminal in wrong state: " + terminalState + "State.", gameObject);
            return;
        }

        trigger.enabled = false;
        player.SetMovable(false);
        LinkedMinigame.StartMinigame(this, player);
        ChangeState(TerminalState.Fixing);
    }

    private void FixTerminal(TerminalController terminal, CharacterController player, int points)
    {
        if (terminal != this) return;

        if (terminalState != TerminalState.Fixing)
        {
            Debug.LogError("Tried to fix Terminal in wrong state: " + terminalState + "State.", gameObject);
            return;
        }

        player.SetMovable(true);
        timerToExplode = 0f;
        ChangeState(TerminalState.Idle);
    }

    private void FailFixing(TerminalController terminal, CharacterController player, float timePenalty)
    {
        if (terminal != this) return;

        if (terminalState != TerminalState.Fixing)
        {
            Debug.LogError("Tried to fix Terminal in wrong state: " + terminalState + "State.", gameObject);
            return;
        }

        trigger.enabled = true;
        player.SetMovable(true);
        timerToExplode += timePenalty;
        ChangeState(TerminalState.Destroyed);
    }

    private void StartRepairing(TerminalController terminal, CharacterController player)
    {
        if (terminal != this) return;

        if (terminalState != TerminalState.Destroyed)
        {
            Debug.LogError("Tried to start repairing Terminal in wrong state: " + terminalState + "State.", gameObject);
            return;
        }

        trigger.enabled = false;
        player.SetMovable(false);
        RepairMinigame.StartMinigame(this, player);
        ChangeState(TerminalState.Repairing);
    }

    private void Repair(TerminalController terminal, CharacterController player, int points)
    {
        if (terminal != this) return;

        if (terminalState != TerminalState.Repairing)
        {
            Debug.LogError("Tried to repair Terminal in wrong state: " + terminalState + "State.", gameObject);
            return;
        }

        trigger.enabled = true;
        player.SetMovable(true);
        timerToExplode = 0f;
        ChangeState(TerminalState.Idle);
    }

    private void FailRepairing(TerminalController terminal, CharacterController player, float timePenalty)
    {
        if (terminal != this) return;

        if (terminalState != TerminalState.Repairing)
        {
            Debug.LogError("Tried to repair Terminal in wrong state: " + terminalState + "State.", gameObject);
            return;
        }

        player.SetMovable(true);
        ChangeState(TerminalState.Destroyed);
    }

    private void ChangeState(TerminalState state)
    {
        terminalUi.OnStateChanged(state);
        terminalState = state;
    }
    #endregion



    #region Coroutines

    #endregion
}


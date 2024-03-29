﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

/// <summary>
/// 
/// </summary>
[SelectionBase]
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
    [MinMaxRange(1f, 60f)]
    public RangedFloat TimeToFail = new RangedFloat();
    public float TimeToExplode = 10f;
    [Tooltip("When the warning should appear that the terminal is about to explode. Given as percentage of TimeToExplode.")]
    [Range(0f, 1f)]
    public float DangerTime = 0.5f;
    public Minigame LinkedMinigame = null;
    public Minigame RepairMinigame = null;

    [Header("OnFail")]
    public float StunDuration = 3f;
    public float StunForce = 1f;

    [Header("OnExplosion")]
    public float cameraShakeFadeIn = 0.1f;
    public float cameraShakeFadeOut = 1f;

    [Header("Terminal State")]
    [SerializeField] TerminalState terminalState = TerminalState.Idle;
    [SerializeField] bool debug = false;

    [Header("References")]
    [SerializeField] TerminalTriggerController triggerController = null;
    [SerializeField] Transform minigamePosition = null;
    [SerializeField] TerminalUi terminalUi = null;

    [Header("Sounds")]
    [SerializeField] private AudioClip errorSound;
    [SerializeField] private AudioClip fixSound;
    [SerializeField] private AudioClip failSound;
    [SerializeField] private AudioClip destroySound;
    [SerializeField] private AudioClip repairSound;

    [Header("Particle Effects")]
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private ParticleSystem destroyedEffect;

    [Header("Game Events")]
    [SerializeField] GameEvent terminalDestroyed = null;
    [SerializeField] GameEvent terminalRepaired = null;

    // Private
    private float timerToFail = 0f;
    private float timerToExplode = 0f;
    private bool dangerWarningSend = false;
	#endregion
	
	
	
	#region Public Properties
	public float TimerToFail { get { return timerToFail; } }
    public float TimerToExplode { get { return timerToExplode; } }
    public Transform MinigamePosition { get { return minigamePosition; } }
    #endregion



    #region Unity Event Functions
    private void Start()
    {
        // Random rotation for explosion effect:
        explosionEffect.transform.Rotate(0f, 0f, Random.Range(0f, 360f));
    }

    private void Update()
    {
        switch (terminalState)
        {
            case TerminalState.Idle:
                timerToFail += Time.deltaTime;
                break;

            case TerminalState.Error:
                if (dangerWarningSend == false && timerToExplode >= TimeToExplode * DangerTime)
                {
                    terminalUi.ActivateDangerMode();
                    dangerWarningSend = true;
                }

                if (timerToExplode >= TimeToExplode)
                {
                    timerToExplode = 0f;
                    dangerWarningSend = false;
                    ChangeState(TerminalState.Destroyed);
                }

                timerToExplode += Time.deltaTime;
                break;

            case TerminalState.Fixing:
                break;

            case TerminalState.Destroyed:
                break;

            case TerminalState.Repairing:
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

        triggerController.enabled = true;
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

        triggerController.enabled = false;
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
        dangerWarningSend = false;
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

        player.Stun(StunDuration, StunForce, (player.transform.position - terminal.transform.position).normalized);
        triggerController.enabled = true;
        timerToExplode += timePenalty;
        ChangeState(TerminalState.Error);
    }

    private void StartRepairing(TerminalController terminal, CharacterController player)
    {
        if (terminal != this) return;

        if (terminalState != TerminalState.Destroyed)
        {
            Debug.LogError("Tried to start repairing Terminal in wrong state: " + terminalState + "State.", gameObject);
            return;
        }

        triggerController.enabled = false;
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

        player.Stun(StunDuration, StunForce, (player.transform.position - terminal.transform.position).normalized);
        triggerController.enabled = true;
        ChangeState(TerminalState.Destroyed);
    }

    private void ChangeState(TerminalState state)
    {
        if (debug) Debug.Log("Changing State to " + state, gameObject);
        terminalUi.OnStateChanged(state);
        TerminalState previousState = terminalState;
        terminalState = state;

        // Add/Remove Terminal as camera target
        if (state == TerminalState.Error && previousState == TerminalState.Idle)
            MultipleTargetCamera.Instance.AddTarget(transform);
        else if (state == TerminalState.Idle)
            MultipleTargetCamera.Instance.RemoveTarget(transform);

        // Explosion
        if(state == TerminalState.Destroyed && previousState != TerminalState.Destroyed && previousState != TerminalState.Repairing)
        {
            explosionEffect.Play();
            Invoke("StopExplosion", 1f);
            CameraShaker.Instance.ShakeOnce(CameraShakePresets.Explosion.Magnitude, CameraShakePresets.Explosion.Roughness, cameraShakeFadeIn, cameraShakeFadeOut);
            RaiseTerminalDestroyed(this);
        }

        // Terminal repaired
        if (previousState == TerminalState.Repairing && state == TerminalState.Idle) RaiseTerminalRepaired(this);

        // Destroyed Particle Effect:
        if (state == TerminalState.Destroyed) destroyedEffect.Play();
        else if (previousState == TerminalState.Repairing && state != TerminalState.Destroyed) destroyedEffect.Stop();

        // Play sounds:
        switch (state)
        {
            case TerminalState.Idle:
                if (previousState == TerminalState.Fixing)
                    SoundManager.PlaySound(fixSound);
                if (previousState == TerminalState.Repairing)
                    SoundManager.PlaySound(repairSound);
                break;
            case TerminalState.Error:
                SoundManager.PlaySound(errorSound);
                break;
            case TerminalState.Destroyed:
                if (previousState == TerminalState.Repairing)
                    SoundManager.PlaySound(failSound);
                else
                    SoundManager.PlaySound(destroySound);
                break;
        }
    }

    private void StopExplosion()
    {
        explosionEffect.Stop();
    }

    void RaiseTerminalDestroyed(TerminalController terminal)
    {
        terminalDestroyed.Raise(this, terminal);
    }

    void RaiseTerminalRepaired(TerminalController terminal)
    {
        terminalRepaired.Raise(this, terminal);
    }
    #endregion



    #region Coroutines

    #endregion
}


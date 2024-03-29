﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [Header("Movement")]
    [SerializeField] protected float movementSpeed = 1f;
    [SerializeField] protected float acceleration = 10f;
    [SerializeField] protected float deceleration = 10f;
    [SerializeField] protected float rotateSpeed = 10f;

    [Header("References")]
    [SerializeField] Transform playerSprite = null;
    [SerializeField] PlayerUi playerUi = null;
    [SerializeField] ParticleSystem stunnedEffect = null;
    [SerializeField] Transform thrusterPivot = null;
    
    // Private Variables
    // Movement
    protected float horizontalInput;
    protected float verticalInput;
    protected bool active = true;
    private bool firstMove = true;

    private float stunTime = 0f;
    private float stunTimer = 0f;

    // References
    protected new Rigidbody2D rigidbody;
    protected string currentAxisX;
    protected string currentAxisY;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        if (active)
        {
            MoveCharacter();
            RotateCharacter();
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (active)
        {
            horizontalInput = Input.GetAxis(currentAxisX) * movementSpeed;
            verticalInput = Input.GetAxis(currentAxisY) * movementSpeed;
        }

        if (stunTime != 0)
        {
            stunTimer += Time.deltaTime;
            if (stunTimer >= stunTime)
            {
                stunnedEffect.Stop();
                SetMovable(true);
                stunTime = 0f;
                stunTimer = 0f;
            }
        }
    }
    #endregion



    #region Public Functions
    /// <summary>
    /// Sets if the character is allowed to move or not and stops his current movement
    /// </summary>
    /// <param name="movable">Character allowed to move?</param>
    public void SetMovable(bool active)
    {
        this.active = active;

        if (!active)
        {
            horizontalInput = 0;
            verticalInput = 0;
            rigidbody.velocity = Vector2.zero;
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            UpdateThruster();
        }
        else
        {
            rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public void SetInputAxes(CharacterController player, string xAxis, string yAxis, Sprite axisSprite)
    {
        if (player != this) return;

        currentAxisX = xAxis;
        currentAxisY = yAxis;

        firstMove = true;
    }

    public void Stun(float duration, float force, Vector3 direction)
    {
        stunnedEffect.Play();
        transform.Translate(direction * force);
        SetMovable(false);
        stunTime = duration;
    }
    #endregion



    #region Private Functions
    /// <summary>
    /// Sets the velocity of the characters Rigidbody component
    /// </summary>
    private void MoveCharacter()
    {
        Vector2 targetVelocity = rigidbody.velocity;
        targetVelocity.x = horizontalInput;
        targetVelocity.y = verticalInput;

        if(firstMove && targetVelocity.magnitude >= 0.2f)
        {
            firstMove = false;
            playerUi.MarkPlayerMove();
        }

        if (targetVelocity.magnitude >= rigidbody.velocity.magnitude)
            rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, targetVelocity, acceleration * Time.deltaTime);
        else
            rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, targetVelocity, deceleration * Time.deltaTime);

        UpdateThruster();
    }

    private void RotateCharacter()
    {
        if (rigidbody.velocity.magnitude <= 0.01f) return;

        playerSprite.rotation = Quaternion.LookRotation(Vector3.forward, rigidbody.velocity);
    }

    void UpdateThruster()
    {
        float stretch = rigidbody.velocity.magnitude;
        stretch.Remap(0f, 1.4f, 0f, 1f);

        thrusterPivot.localScale = new Vector3(thrusterPivot.localScale.x, stretch, thrusterPivot.localScale.z);
    }
    #endregion



    #region Coroutines

    #endregion
}


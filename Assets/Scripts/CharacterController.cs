using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class CharacterController : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [Header("Movement")]
    [SerializeField] protected float movementSpeed = 1f;
    [SerializeField] protected float acceleration = 10f;
    [SerializeField] protected float deceleration = 10f;

    [Header("Properties")]
    [Range(1, 4)]
    [SerializeField] protected int playerNumber;
    
    // Private Variables
    // Movement
    protected float horizontalInput;
    protected float verticalInput;
    protected bool active = true;

    // Component References
    protected new Rigidbody2D rigidbody;
    protected AudioSource audioSource;
    #endregion



    #region Public Properties
    public int PlayerNumber
    {
        get { return playerNumber; }
        set
        {
            if (value >= 0 && value <= 4) playerNumber = value;
            else Debug.LogWarning("Trying to access playerNumber out of range. PlayerNumber = " + value);
        }
    }
    #endregion



    #region Unity Event Functions
    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void FixedUpdate()
    {
        if (active)
        {
            MoveCharacter();
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (active)
        {
            horizontalInput = Input.GetAxis(Constants.INPUT_LEFT_STICK_X) * movementSpeed;
            verticalInput = Input.GetAxis(Constants.INPUT_LEFT_STICK_Y) * movementSpeed;
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
        }
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

        if (targetVelocity.magnitude >= rigidbody.velocity.magnitude)
            rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, targetVelocity, acceleration * Time.deltaTime);
        else
            rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, targetVelocity, deceleration * Time.deltaTime);
    }
    #endregion



    #region Coroutines

    #endregion
}


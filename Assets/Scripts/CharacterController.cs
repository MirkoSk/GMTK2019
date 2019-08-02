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
    [SerializeField] protected float movementSpeed = 10;

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
            horizontalInput = Input.GetAxis(Constants.INPUT_HORIZONTAL) * movementSpeed;
            verticalInput = Input.GetAxis(Constants.INPUT_VERTICAL) * movementSpeed;
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
        Vector2 newVelocity = rigidbody.velocity;
        newVelocity.x = horizontalInput;
        newVelocity.y = verticalInput;
        rigidbody.velocity = newVelocity;
    }
    #endregion



    #region Coroutines

    #endregion
}


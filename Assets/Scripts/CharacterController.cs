using System.Collections;
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
    
    // Private Variables
    // Movement
    protected float horizontalInput;
    protected float verticalInput;
    protected bool active = true;

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
        }
    }

    public void SetInputAxes(CharacterController player, string xAxis, string yAxis, Sprite axisSprite)
    {
        if (player != this) return;

        currentAxisX = xAxis;
        currentAxisY = yAxis;
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

    private void RotateCharacter()
    {
        playerSprite.rotation = Quaternion.LookRotation(Vector3.forward, rigidbody.velocity);
    }
    #endregion



    #region Coroutines

    #endregion
}


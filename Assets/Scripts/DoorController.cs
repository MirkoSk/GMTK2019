using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
[SelectionBase]
public class DoorController : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [SerializeField] float playbackSpeed = 1f;

    // Private
    Animator animator = null;
	#endregion
	
	
	
	#region Public Properties
	
	#endregion
	
	
	
	#region Unity Event Functions
	private void Awake() 
	{
        animator = GetComponent<Animator>();
        animator.speed = playbackSpeed;
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Contains(Constants.TAG_PLAYER))
        {
            animator.SetTrigger("OpenDoor");
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag.Contains(Constants.TAG_PLAYER))
        {
            animator.SetTrigger("CloseDoor");
        }
    }
    #endregion



    #region Public Functions

    #endregion



    #region Private Functions

    #endregion



    #region Coroutines

    #endregion
}


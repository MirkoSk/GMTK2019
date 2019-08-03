using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class TerminalTriggerController : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [SerializeField] GameEvent playerAccessedTerminal = null;
    [SerializeField] TerminalController terminalController = null;

	// Private
	
	#endregion
	
	
	
	#region Public Properties
	
	#endregion
	
	
	
	#region Unity Event Functions
	private void Start () 
	{
		
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Contains(Constants.TAG_PLAYER))
        {
            RaisePlayerAccessedTerminal(collider.transform.GetComponentInParent<CharacterController>().PlayerNumber, terminalController);
        }
    }
    #endregion



    #region Public Functions

    #endregion



    #region Private Functions
    void RaisePlayerAccessedTerminal(int playerNumber, TerminalController terminal)
    {
        playerAccessedTerminal.Raise(this, playerNumber, terminal);
    }
    #endregion



    #region Coroutines

    #endregion
}


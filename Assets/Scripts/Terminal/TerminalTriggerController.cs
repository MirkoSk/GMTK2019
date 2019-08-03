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
            RaisePlayerAccessedTerminal(terminalController, collider.transform.GetComponentInParent<CharacterController>());
        }
    }
    #endregion



    #region Public Functions

    #endregion



    #region Private Functions
    void RaisePlayerAccessedTerminal(TerminalController terminal, CharacterController player)
    {
        playerAccessedTerminal.Raise(this, terminal, player);
    }
    #endregion



    #region Coroutines

    #endregion
}


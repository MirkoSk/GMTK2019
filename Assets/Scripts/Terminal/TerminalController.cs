using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class TerminalController : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [Header("Terminal Stats")]
    public RangedFloat timeToFail = new RangedFloat();
    public float timeToExplode = 10f;
    public float timeToRepair = 10f;
    public GameObject minigame = null;

	// Private
	
	#endregion
	
	
	
	#region Public Properties
	
	#endregion
	
	
	
	#region Unity Event Functions
	private void Start () 
	{
		
	}
	#endregion
	
	
	
	#region Public Functions
	
	#endregion
	
	
	
	#region Private Functions

	#endregion
	
	
	
	#region Coroutines
	
	#endregion
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[CreateAssetMenu(menuName = "Scriptable Objects/Terminal")]
public class TerminalType : ScriptableObject 
{

    #region Variable Declarations
    // Serialized Fields
    [MinMaxRange(1f, 100f)]
    public RangedFloat TimeToFail;
    public GameObject Minigame;

	// Private
	
	#endregion
	
	
	
	#region Public Properties
	
	#endregion
	
	
	
	#region Public Functions
	
	#endregion
	
	
	
	#region Private Functions

	#endregion
	
	
	
	#region Coroutines
	
	#endregion
}


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
    public RangedFloat TimeToFail = new RangedFloat();
    public float TimeToExplode = 10f;
    public float TimeToRepair = 10f;
    public GameObject Minigame = null;

    // Private
    private float timerToFail = 0f;
    private float timerToExplode = 0f;
	#endregion
	
	
	
	#region Public Properties
	public float TimerToFail { get { return timerToFail; } }
    public float TimerToExplode { get { return timerToExplode; } }
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


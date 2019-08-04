using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class ShipExplosion : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [SerializeField]
    private ParticleSystem explosion;
    // Private
    private bool isExploding = false;
	#endregion
	
	
	
	#region Public Properties
	
	#endregion
	
	
	
	#region Unity Event Functions

	#endregion
	
	
	
	#region Public Functions
	public void OnGameOver(bool success)
    {
        if (!success)
            StartExplosion();
    }
	#endregion
	
	
	
	#region Private Functions
    private void StartExplosion()
    {
        if (!isExploding)
        {
            isExploding = true;
            explosion.Play();
        }
    }

    private void StopExplosion()
    {
        if (isExploding)
        {
            isExploding = false;
            explosion.Stop();
        }
    }
    #endregion



    #region Coroutines

    #endregion
}


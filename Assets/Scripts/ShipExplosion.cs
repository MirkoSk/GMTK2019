using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

/// <summary>
/// 
/// </summary>
public class ShipExplosion : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [Header("Camera Shake")]
    [SerializeField] float magnitude = 1f;
    [SerializeField] float roughness = 1f;
    [SerializeField] float fadeInTime = 1f;
    [SerializeField] float fadeOutTime = 10f;

    [Space]
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
            CameraShaker.Instance.StartShake(magnitude, roughness, fadeInTime);
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


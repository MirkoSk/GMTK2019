using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class SoundManager : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [SerializeField]
    private AudioSource audioSource;
	// Private
	private static SoundManager instance;
	#endregion
	
	
	
	#region Public Properties
	
	#endregion
	
	
	
	#region Unity Event Functions
	private void Awake () 
	{
        // Singleton:
        if (FindObjectsOfType<SoundManager>().Length > 1)
            Destroy(gameObject);
        else
            instance = this;
	}
	#endregion
	
	
	
	#region Public Functions
	public static void PlaySound(AudioClip clip)
    {
        if (instance == null)
            return;

        instance.audioSource.PlayOneShot(clip);
    }
	#endregion
	
	
	
	#region Private Functions

	#endregion
	
	
	
	#region Coroutines
	
	#endregion
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class PlayerAudioController : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [SerializeField] AudioClip[] audioClips;

    // Private
    AudioSource audioSource = null;
	#endregion
	
	
	
	#region Public Properties
	
	#endregion
	
	
	
	#region Unity Event Functions
	private void Awake() 
	{
        audioSource = GetComponent<AudioSource>();
	}
	#endregion
	
	
	
	#region Public Functions
	
	#endregion
	
	
	
	#region Private Functions
    public void PlaySound(int playerNumber, TerminalController terminal)
    {
        audioSource.Play();
    }
	#endregion
	
	
	
	#region Coroutines
	
	#endregion
}


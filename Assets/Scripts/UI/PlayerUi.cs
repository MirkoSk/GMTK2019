using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class PlayerUi : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [SerializeField]
    private Canvas playerCanvas;
    [SerializeField]
    private Image axisImage;
    // Private
    private CharacterController player;
	#endregion
	
	
	
	#region Public Properties
	
	#endregion
	
	
	
	#region Unity Event Functions
	private void Awake () 
	{
        HideCanvas();
        player = GetComponentInParent<CharacterController>();
	}
	#endregion
	
	
	
	#region Public Functions
    public void OnPlayerAxisChanged(CharacterController player, string axisX, string axisY, Sprite axisSprite)
    {
        if (this.player == player)
            DisplayAxis(axisSprite);
    }

	public void DisplayAxis(Sprite axisSprite)
    {
        // Show new axis for certain number of seconds:
        axisImage.sprite = axisSprite;
        playerCanvas.enabled = true;
        CancelInvoke("HideCanvas");
        Invoke("HideCanvas", 5f);
    }
	#endregion
	
	
	
	#region Private Functions
    private void HideCanvas()
    {
        playerCanvas.enabled = false;
    }
	#endregion
	
	
	
	#region Coroutines
	
	#endregion
}


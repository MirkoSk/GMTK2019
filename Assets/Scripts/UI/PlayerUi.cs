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
    private float canvasDisplayDuration = 5f;
    private bool canvasChangeAllowed = false;
    private bool movedAfterAxisChange = false;
    private bool iconSizeChanged = false;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Awake () 
	{
        playerCanvas.enabled = false;
        player = GetComponentInParent<CharacterController>();
        InvokeRepeating("ToggleIconSize", 0.3f, 0.3f);
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
        CancelInvoke("AllowCanvasHide");
        Invoke("AllowCanvasHide", canvasDisplayDuration);
    }

    public void MarkPlayerMove()
    {
        movedAfterAxisChange = true;
        if (canvasChangeAllowed)
            playerCanvas.enabled = false;
    }
	#endregion
	
	
	
	#region Private Functions
    private void AllowCanvasHide()
    {
        canvasChangeAllowed = true;
        if (movedAfterAxisChange)
            playerCanvas.enabled = false;
    }

    private void ToggleIconSize()
    {
        Vector2 newSize = iconSizeChanged ? new Vector2(0.8f, 0.8f) : new Vector2(0.6f, 0.6f);
        iconSizeChanged = !iconSizeChanged;
        axisImage.rectTransform.localScale = newSize;
    }
    #endregion



    #region Coroutines

    #endregion
}


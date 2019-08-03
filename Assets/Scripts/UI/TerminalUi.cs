using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class TerminalUi : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [SerializeField]
    private Canvas terminalCanvas;
    [SerializeField]
    private Image warningIcon;
    [SerializeField]
    private Image destroyedIcon;
    // Private
    private bool warningIconSizeChanged;
    private bool destroyedIconSizeChanged;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Awake () 
	{
        HideWarning();
        HideDestroyed();

        // Warning sign animations:
        InvokeRepeating("ToggleWarningIconSize", 0.3f, 0.3f);
        InvokeRepeating("ToggleDestroyedIconSize", 0.15f, 0.15f);
    }
	#endregion
	
	
	
	#region Public Functions
	public void OnStateChanged(TerminalController.TerminalState state)
    {
        switch (state)
        {
            case TerminalController.TerminalState.Idle:
                HideWarning();
                HideDestroyed();
                break;
            case TerminalController.TerminalState.Error:
                DisplayWarning();
                HideDestroyed();
                break;
            case TerminalController.TerminalState.Fixing:
                HideWarning();
                HideDestroyed();
                break;
            case TerminalController.TerminalState.Destroyed:
                HideWarning();
                DisplayDestroyed();
                break;
            case TerminalController.TerminalState.Repairing:
                HideWarning();
                HideDestroyed();
                break;
        }
    }
    #endregion



    #region Private Functions
    private void ToggleWarningIconSize()
    {
        Vector2 newSize = warningIconSizeChanged ? new Vector2(1f, 1f) : new Vector2(0.85f, 0.85f);
        warningIconSizeChanged = !warningIconSizeChanged;
        warningIcon.rectTransform.localScale = newSize;
    }

    private void ToggleDestroyedIconSize()
    {
        Vector2 newSize = destroyedIconSizeChanged ? new Vector2(1f, 1f) : new Vector2(0.85f, 0.85f);
        destroyedIconSizeChanged = !destroyedIconSizeChanged;
        destroyedIcon.rectTransform.localScale = newSize;
    }

    private void DisplayWarning()
    {
        warningIcon.enabled = true;
    }

    private void HideWarning()
    {
        warningIcon.enabled = false;
    }

    private void DisplayDestroyed()
    {
        destroyedIcon.enabled = true;
    }

    private void HideDestroyed()
    {
        destroyedIcon.enabled = false;
    }
    #endregion



    #region Coroutines

    #endregion
}


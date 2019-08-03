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

    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Awake () 
	{
        HideWarning();
        HideDestroyed();
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
                HideWarning();
                DisplayDestroyed();
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


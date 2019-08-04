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
    private Image dangerIcon;
    [SerializeField]
    private Image destroyedIcon;
    // Private
    private bool warningIconSizeChanged = false;
    private bool dangerIconSizeChanged = false;
    private bool destroyedIconSizeChanged = false;
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
        InvokeRepeating("ToggleDangerIconSize", 0.15f, 0.15f);
        InvokeRepeating("ToggleDestroyedIconSize", 0.6f, 0.6f);
    }

    private void Start()
    {
        // Rotate to always face up:
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
    }
    #endregion



    #region Public Functions
    public void OnStateChanged(TerminalController.TerminalState state)
    {
        switch (state)
        {
            case TerminalController.TerminalState.Idle:
                HideWarning();
                HideDanger();
                HideDestroyed();
                break;
            case TerminalController.TerminalState.Error:
                DisplayWarning();
                HideDanger();
                HideDestroyed();
                break;
            case TerminalController.TerminalState.Fixing:
                HideWarning();
                HideDanger();
                HideDestroyed();
                break;
            case TerminalController.TerminalState.Destroyed:
                HideWarning();
                HideDanger();
                DisplayDestroyed();
                break;
            case TerminalController.TerminalState.Repairing:
                HideWarning();
                HideDanger();
                HideDestroyed();
                break;
        }
    }

    public void ActivateDangerMode()
    {
        HideWarning();
        DisplayDanger();
        HideDestroyed();
    }
    #endregion



    #region Private Functions
    private void ToggleWarningIconSize()
    {
        Vector2 newSize = warningIconSizeChanged ? new Vector2(1f, 1f) : new Vector2(0.85f, 0.85f);
        warningIconSizeChanged = !warningIconSizeChanged;
        warningIcon.rectTransform.localScale = newSize;
    }

    private void ToggleDangerIconSize()
    {
        Vector2 newSize = dangerIconSizeChanged ? new Vector2(1f, 1f) : new Vector2(0.85f, 0.85f);
        dangerIconSizeChanged = !dangerIconSizeChanged;
        dangerIcon.rectTransform.localScale = newSize;
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

    private void DisplayDanger()
    {
        dangerIcon.enabled = true;
    }

    private void HideDanger()
    {
        dangerIcon.enabled = false;
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

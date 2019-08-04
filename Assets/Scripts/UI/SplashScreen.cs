using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class SplashScreen : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [SerializeField]
    private Image buttonIcon;
    
    // Private
    private bool iconSizeChanged = false;
    private bool inputAllowed = false;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Start () 
	{
        Invoke("AllowInput", 2f);
        InvokeRepeating("ToggleIconSize", 0.6f, 0.6f);
    }

    public void Update()
    {
        if (inputAllowed && Input.GetButtonDown(Constants.INPUT_A))
            StartGame();
    }
    #endregion



    #region Public Functions

    #endregion



    #region Private Functions
    private void StartGame()
    {
        // TODO
        SceneManager.LoadScene(Constants.SCENE_LEVEL, LoadSceneMode.Single);
        SceneManager.LoadScene(Constants.SCENE_UI, LoadSceneMode.Additive);
    }

    private void AllowInput()
    {
        inputAllowed = true;
    }

    private void ToggleIconSize()
    {
        Vector2 newSize = iconSizeChanged ? new Vector2(1f, 1f) : new Vector2(0.8f, 0.8f);
        iconSizeChanged = !iconSizeChanged;
        buttonIcon.rectTransform.localScale = newSize;
    }
    #endregion



    #region Coroutines

    #endregion
}


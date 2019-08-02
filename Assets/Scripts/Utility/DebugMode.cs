using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Provides a Debug Mode Menu
/// </summary>
public class DebugMode : MonoBehaviour
{

    private bool debugMode = false;



    private void Update ()
    {
        // Switch the Games Debug Mode On/Off
        if (Input.GetButtonDown(Constants.INPUT_DEBUGMODE))
        {
            debugMode = !debugMode;
        }
    }

    // Draws the GUI for the Debug Mode and declares it's functionality
    private void OnGUI()
    {
        if (debugMode)
        {
            // Setup of the box and title
            GUILayout.BeginVertical("box");

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Debug Menu");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();


            // Button for loading the next scene by buildIndex
            GUILayout.BeginVertical("box");
            GUILayout.Label("Scene Management:");
            if (GUILayout.Button("Load next scene"))
            {
                Debug.Log("Debug Mode: Loading next scene.");
                int activeScene = SceneManager.GetActiveScene().buildIndex;
                if (activeScene + 1 < SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene(activeScene + 1);
                }
                else
                {
                    SceneManager.LoadScene(0);
                }
            }
            if (GUILayout.Button("Reload scene"))
            {
                Debug.Log("Debug Mode: Reloading current scene.");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            GUILayout.EndVertical();
        }
    }
}

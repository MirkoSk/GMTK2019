using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helper class containing all strings and constants used in the game
/// </summary>
public class Constants 
{
    #region Inputs
    public static readonly string INPUT_A = "A";
    public static readonly string INPUT_B = "B";
    public static readonly string INPUT_X = "X";
    public static readonly string INPUT_Y = "Y";
    public static readonly string INPUT_LB = "LB";
    public static readonly string INPUT_RB = "RB";
    public static readonly string INPUT_SELECT = "Select";
    public static readonly string INPUT_START = "Start";
    public static readonly string INPUT_LEFT_STICK_BUTTON = "LeftStickButton";
    public static readonly string INPUT_RIGHT_STICK_BUTTON = "RightStickButton";
    public static readonly string INPUT_LEFT_STICK_X = "LeftStickX";
    public static readonly string INPUT_LEFT_STICK_Y = "LeftStickY";
    public static readonly string INPUT_RIGHT_STICK_X = "RightStickX";
    public static readonly string INPUT_RIGHT_STICK_Y = "RightStickY";
    public static readonly string INPUT_DPAD_X = "DpadX";
    public static readonly string INPUT_DPAD_Y = "DpadY";
    public static readonly string INPUT_LT = "LT";
    public static readonly string INPUT_RT = "RT";
    public static readonly string INPUT_DEBUG_MODE = "DebugMode";
    #endregion

    #region Tags and Layers
    public static readonly string TAG_PLAYER = "Player";
    #endregion

    #region Scenes
    public static readonly string SCENE_TITLE = "SplashScreen";
    public static readonly string SCENE_OUTRO = "ResultScreen";
    public static readonly string SCENE_UI = "MainUi";
    public static readonly string SCENE_LEVEL = "MainLevel";
    #endregion

    #region Audio
    // Exposed Parameters in Mixers
    public static readonly string MIXER_SFX_VOLUME = "SFXVolume";
    public static readonly string MIXER_MUSIC_VOLUME = "MusicVolume";
    #endregion
}
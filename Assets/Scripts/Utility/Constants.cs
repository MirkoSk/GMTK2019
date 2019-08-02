using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helper class containing all strings and constants used in the game
/// </summary>
public class Constants 
{
    #region Inputs
    public static readonly string INPUT_HORIZONTAL = "Horizontal";
    public static readonly string INPUT_VERTICAL = "Vertical";
    public static readonly string INPUT_FIRE1 = "Fire1";
    public static readonly string INPUT_MOUSE_X = "Mouse X";
    public static readonly string INPUT_MOUSE_Y = "Mouse Y";
    public static readonly string INPUT_JUMP = "Jump";
    public static readonly string INPUT_SUBMIT = "Submit";
    public static readonly string INPUT_CANCEL = "Cancel";
    public static readonly string INPUT_DEBUGMODE = "Fire3";
    #endregion

    #region Tags and Layers
    public static readonly string TAG_PLAYER = "Player";
    #endregion

    #region Scenes
    public static readonly string SCENE_TITLE = "TitleScreen";
    public static readonly string SCENE_MAIN_MENU = "MainMenu";
    public static readonly string SCENE_INTRO = "Intro";
    public static readonly string SCENE_CREDITS = "Credits";
    public static readonly string SCENE_LEVEL01 = "Level01";
    #endregion

    #region Audio
    // Exposed Parameters in Mixers
    public static readonly string MIXER_SFX_VOLUME = "SFXVolume";
    public static readonly string MIXER_MUSIC_VOLUME = "MusicVolume";
    #endregion
}
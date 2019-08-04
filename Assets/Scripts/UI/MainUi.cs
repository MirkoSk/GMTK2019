using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class MainUi : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [Header("UI References")]
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text timerText;
    [SerializeField]
    private Text[] countdownText;
    [SerializeField]
    private Image[] defectTerminalIcons;
    [SerializeField]
    private Image alarmOverlay;
    [SerializeField]
    private Image explosionOverlay;
    [Header("Colors")]
    [SerializeField]
    private Color defectTerminalIconDefaultColor;
    [SerializeField]
    private Color defectTerminalIconActiveColor;
    [Header("Sounds")]
    [SerializeField]
    private AudioClip countdownSound;
    [SerializeField]
    private AudioClip alarmSound;
    // Private
    private int numberOfDestroyedTerminals = 0;
    private bool isExploding = false;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Start()
    {
        UpdateScoreDisplay(0);
        UpdateTimerDisplay(60f, 60f);
        UpdateDefectDisplay();
        explosionOverlay.enabled = false;
    }
    #endregion



    #region Public Functions
    public void UpdateScoreDisplay(int newScore)
    {
        scoreText.text = newScore.ToString();
    }

    public void UpdateTimerDisplay(float newTime, float totalTime)
    {
        int minutes = Mathf.FloorToInt(newTime / 60f);
        int seconds = Mathf.FloorToInt(newTime - minutes * 60f);
        string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);

        timerText.text = timeString;

        // Display countdown during last 10 seconds:
        foreach (Text t in countdownText)
            if (newTime <= 10f)
                t.text = timeString;
            else
                t.text = "";
    }

    public void OnTerminalDestroyed(TerminalController terminal)
    {
        numberOfDestroyedTerminals++;
        UpdateDefectDisplay();
    }

    public void OnTerminalRepaired(TerminalController terminal)
    {
        numberOfDestroyedTerminals--;
        UpdateDefectDisplay();
    }

    public void OnGameOver(bool successful)
    {
        if (!successful)
            ActivateExplosionOverlay();
    }
    #endregion



    #region Private Functions
    private void UpdateDefectDisplay()
    {
        // Update flame icons:
        for(int i = 0; i < defectTerminalIcons.Length; i++)
        {
            if(numberOfDestroyedTerminals > i)
                defectTerminalIcons[i].color = defectTerminalIconActiveColor;
            else
                defectTerminalIcons[i].color = defectTerminalIconDefaultColor;
        }

        // Toggle alarm overlay:
        if (numberOfDestroyedTerminals >= 2)
            ActivateAlarmOverlay();
        else
            DeactivateAlarmOverlay();
    }

    private void ActivateAlarmOverlay()
    {
        if (!IsInvoking("ToogleAlarmOverlay"))
        {
            CancelInvoke("ToggleAlarmOverlay");
            InvokeRepeating("ToggleAlarmOverlay", 1f, 1f);
            alarmOverlay.enabled = true;
        }
    }

    private void DeactivateAlarmOverlay()
    {
        CancelInvoke("ToggleAlarmOverlay");
        alarmOverlay.enabled = false;
    }

    private void ToggleAlarmOverlay()
    {
        alarmOverlay.enabled = !alarmOverlay.enabled;
        if (alarmOverlay.enabled)
            SoundManager.PlaySound(alarmSound);
    }

    private void ActivateExplosionOverlay()
    {
        if (!isExploding)
        {
            isExploding = true;
            explosionOverlay.enabled = true;
            explosionOverlay.color = new Color(1f, 1f, 1f, 0f);
            explosionOverlay.DOColor(new Color(1f, 1f, 1f, 1f), 8f);
        }
    }
    #endregion



    #region Coroutines

    #endregion
}


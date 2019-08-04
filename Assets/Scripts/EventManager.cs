using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class EventManager : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    

    [Header("Event Settings")]
    [MinMaxRange(0.5f, 1.5f)]
    [SerializeField] RangedFloat difficultyModifier;
    [SerializeField] float tickInterval = .3f;

    [Header("References")]
    [SerializeField] InputController inputController = null;
    [SerializeField] SpawnManager spawnManager = null;

    // Private
    float difficulty = 1f;
    int erroredTerminals = 0;
    float timer = 0f;
	#endregion
	
	
	
	#region Public Properties
	
	#endregion
	
	
	
	#region Unity Event Functions
	private void Awake () 
	{
        
	}

    private void Update()
    {
        if (timer >= tickInterval)
        {
            Tick();
            timer = 0f;
        }

        timer += Time.deltaTime;
    }
    #endregion



    #region Public Functions
    public void MinigameSucceeded()
    {
        erroredTerminals--;
    }
    #endregion



    #region Private Functions
    private void Tick()
    {
        difficulty = ((float)erroredTerminals).Remap(0, spawnManager.Terminals.Count, difficultyModifier.minValue, difficultyModifier.maxValue);

        for (int i = 0; i < spawnManager.Terminals.Count; i++)
        {
            if (spawnManager.Terminals[i].TimerToFail >= spawnManager.Terminals[i].TimeToFail.minValue)
            {
                float probability;
                if (spawnManager.Terminals[i].TimerToFail > spawnManager.Terminals[i].TimeToFail.maxValue)
                    probability = 100f;
                else
                    probability = spawnManager.Terminals[i].TimerToFail.Remap(spawnManager.Terminals[i].TimeToFail.minValue, spawnManager.Terminals[i].TimeToFail.maxValue, 0f, 100f);
                float temp = Random.Range(0f, 100f) * difficulty;

                //TODO: If Probability is 100 and difficulty > 1 this probability >= 100 statement is meant for fail savety NOT FINAL
                if (probability >= 100 || temp < probability)
                {
                    if (inputController.CheckUnusedInputs(spawnManager.Terminals[i].LinkedMinigame.InputType, spawnManager.Terminals[i].LinkedMinigame.InputNumber))
                    {
                        spawnManager.Terminals[i].InvokeError();
                        erroredTerminals++;
                    }
                }
            }
        }
    }
    #endregion



    #region Coroutines

    #endregion
}


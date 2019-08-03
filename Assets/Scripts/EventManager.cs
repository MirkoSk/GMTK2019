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
    [Header("Terminal Settings")]
    [SerializeField] List<GameObject> terminalPrefabs;
    [Tooltip("Number of Terminals that get placed in the Level at game start")]
    [SerializeField] int numberOfTerminals;

    [Header("MiniGames")]
    [SerializeField] List<GameObject> miniGamePrefabs;

    [Header("Event Settings")]
    [MinMaxRange(0.5f, 1.5f)]
    [SerializeField] RangedFloat difficultyModifier;
    [SerializeField] float tickInterval = .2f;

    // Private
    List<TerminalController> terminals = new List<TerminalController>();
    float difficulty = 1f;
    int erroredTerminals = 0;
	#endregion
	
	
	
	#region Public Properties
	
	#endregion
	
	
	
	#region Unity Event Functions
	private void Awake () 
	{
		for(int i=0; i < numberOfTerminals; i++)
        {
            int spawnTerminalNumber = Random.Range(0, terminalPrefabs.Count);
            GameObject terminalPrefab = Instantiate(terminalPrefabs[spawnTerminalNumber]);
            TerminalController terminalController = terminalPrefab.GetComponent<TerminalController>();
            GameObject minigamePrefab = Instantiate(miniGamePrefabs[Random.Range(0, miniGamePrefabs.Count)]);
            Minigame minigame = minigamePrefab.GetComponent<Minigame>();
            terminalController.LinkedMinigame = minigame;
            terminals.Add(terminalController);
            terminalPrefabs.RemoveAt(spawnTerminalNumber);
        }
        InvokeRepeating("Tick", tickInterval, tickInterval);
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
        difficulty = ((float)erroredTerminals).Remap(0, terminals.Count, difficultyModifier.minValue, difficultyModifier.maxValue);

        for (int i = 0; i < terminals.Count; i++)
        {
            if (terminals[i].TimerToFail >= terminals[i].TimeToFail.minValue)
            {
                float probability = terminals[i].TimerToFail.Remap(terminals[i].TimeToFail.minValue, terminals[i].TimeToFail.maxValue, 0f, 100f);
                float temp = Random.Range(0f, 100f) * difficulty;


                if (temp < probability)
                {
                    terminals[i].InvokeError();
                    erroredTerminals++;
                }
            }
        }
    }
    #endregion



    #region Coroutines

    #endregion
}


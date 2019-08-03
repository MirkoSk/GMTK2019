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


    // Private
    List<TerminalController> terminals;
    float difficulty = 1f;
    int erroredTerminals = 0;

    float probability = 0f;
    float temp = 0f;
	#endregion
	
	
	
	#region Public Properties
	
	#endregion
	
	
	
	#region Unity Event Functions
	private void Start () 
	{
		for(int i=0; i < numberOfTerminals; i++)
        {
            int spawnTerminalNumber = Random.Range(0, terminalPrefabs.Count);
            GameObject terminalPrefab = Instantiate(terminalPrefabs[spawnTerminalNumber]);
            TerminalController terminalController = terminalPrefab.GetComponent<TerminalController>();
            terminalController.Minigame = Instantiate(miniGamePrefabs[Random.Range(0, miniGamePrefabs.Count)]);
            terminals.Add(terminalController);
            terminalPrefabs.RemoveAt(spawnTerminalNumber);
        }
	}

    private void Update()
    {
        difficulty = ExtensionMethods.Remap((float)erroredTerminals, 0, (float)terminals.Count, difficultyModifier.minValue, difficultyModifier.maxValue);

        for(int i = 0; i < terminals.Count; i++)
        {
            probability = ExtensionMethods.Remap(terminals[i].TimerToFail, terminals[i].TimeToFail.minValue, terminals[i].TimeToFail.maxValue, 0f, 100f);
            temp = Random.Range(0f, 100f) * difficulty;
            if (temp < probability)
            {
                //TODO: Terminal Fail (when Method is available)
                terminals[i].InvokeError();
                erroredTerminals++;
            }

        }
    }
    #endregion



    #region Public Functions
    public void MinigameSucceeded()
    {
        erroredTerminals--;
    }
    #endregion



    #region Private Functions

    #endregion



    #region Coroutines

    #endregion
}


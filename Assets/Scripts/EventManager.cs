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
    [SerializeField] List<Transform> terminalPositions;
    [SerializeField] List<GameObject> terminalPrefabs;
    [Tooltip("Number of Terminals that get placed in the Level at game start")]
    [SerializeField] int numberOfTerminals;

    [Header("MiniGames")]
    [SerializeField] List<GameObject> miniGamePrefabs;
    [SerializeField] GameObject repairMinigamePrefab;

    [Header("Event Settings")]
    [MinMaxRange(0.5f, 1.5f)]
    [SerializeField] RangedFloat difficultyModifier;
    [SerializeField] float tickInterval = .5f;

    [Header("General")]
    [SerializeField] InputController inputController;

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
            //Spawn Terminal
            int spawnTerminalNumber = Random.Range(0, terminalPrefabs.Count);
            int parentNumber = Random.Range(0, terminalPositions.Count);
            Transform parent = terminalPositions[parentNumber];
            parent.gameObject.SetActive(true);
            GameObject terminalPrefab = Instantiate(terminalPrefabs[spawnTerminalNumber], parent);
            TerminalController terminalController = terminalPrefab.GetComponent<TerminalController>();

            //Spawn Minigame
            GameObject minigame = Instantiate(miniGamePrefabs[Random.Range(0, miniGamePrefabs.Count)], terminalController.MinigamePosition);
            Minigame minigameController = minigame.GetComponent<Minigame>();

            //Spawn RepairMinigame
            GameObject repairMinigame = Instantiate(repairMinigamePrefab, terminalController.MinigamePosition);
            Minigame repairMinigameController = repairMinigame.GetComponent<Minigame>();

            terminalController.LinkedMinigame = minigameController;
            terminalController.RepairMinigame = repairMinigameController;
            terminals.Add(terminalController);
            terminalPositions.RemoveAt(parentNumber);
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
                float probability;
                if (terminals[i].TimerToFail > terminals[i].TimeToFail.maxValue)
                    probability = 100f;
                else
                    probability = terminals[i].TimerToFail.Remap(terminals[i].TimeToFail.minValue, terminals[i].TimeToFail.maxValue, 0f, 100f);
                float temp = Random.Range(0f, 100f) * difficulty;

                //TODO: If Probability is 100 and difficulty > 1 this probability >= 100 statement is meant for fail savety NOT FINAL
                if (probability >= 100 || temp < probability)
                {
                    if (inputController.CheckUnusedInputs(terminals[i].LinkedMinigame.InputType, terminals[i].LinkedMinigame.InputNumber))
                    {
                        terminals[i].InvokeError();
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


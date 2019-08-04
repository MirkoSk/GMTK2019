using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class SpawnManager : MonoBehaviour 
{
    [System.Serializable]
    public class TerminalSpawns
    {
        public TerminalType TerminalType;
        public int TimesToSpawn;
    }

    #region Variable Declarations
    // Serialized Fields
    [Header("Terminal Settings")]
    [Tooltip("Number of Terminals that get placed in the Level at game start")]
    [SerializeField] int numberOfTerminals;
    [SerializeField] GameObject terminalPrefab;
    [SerializeField] List<TerminalSpawns> terminalTypes = new List<TerminalSpawns>();
    [SerializeField] List<Transform> terminalPositions = new List<Transform>();
    [SerializeField] TerminalType fillerTerminal = null;
    

    [Header("Minigames")]
    [SerializeField] List<GameObject> miniGamePrefabs = new List<GameObject>();
    [SerializeField] GameObject repairMinigamePrefab;

    // Private
    List<TerminalController> terminals = new List<TerminalController>();
    #endregion



    #region Public Properties
    public List<TerminalController> Terminals { get { return terminals; } }
    #endregion



    #region Unity Event Functions
    private void Awake () 
	{
        SpawnTerminals();
	}
	#endregion
	
	
	
	#region Public Functions
	
	#endregion
	
	
	
	#region Private Functions
    void SpawnTerminals()
    {
        List<TerminalSpawns> availableTerminals = terminalTypes;

        for (int i = 0; i < numberOfTerminals; i++)
        {
            int parentNumber = Random.Range(0, terminalPositions.Count);
            Transform parent = terminalPositions[parentNumber];

            int terminalTypeNumber = Random.Range(0, availableTerminals.Count);
            TerminalType terminalType = null;
            if (availableTerminals.Count > 0)
                terminalType = availableTerminals[terminalTypeNumber].TerminalType;
            else
                terminalType = fillerTerminal;

            //Spawn Terminal
            parent.gameObject.SetActive(true);
            GameObject terminal = Instantiate(terminalPrefab, parent.position, parent.rotation, parent);
            TerminalController terminalController = terminal.GetComponent<TerminalController>();

            //Spawn Minigame
            GameObject minigame = Instantiate(terminalType.Minigame, terminalController.MinigamePosition);
            Minigame minigameController = minigame.GetComponent<Minigame>();

            //Spawn RepairMinigame
            GameObject repairMinigame = Instantiate(repairMinigamePrefab, terminalController.MinigamePosition);
            Minigame repairMinigameController = repairMinigame.GetComponent<Minigame>();

            //Link Minigames
            terminalController.LinkedMinigame = minigameController;
            terminalController.RepairMinigame = repairMinigameController;

            // Set TimeToFail
            terminalController.TimeToFail = terminalType.TimeToFail;

            terminals.Add(terminalController);

            // Cleanup
            terminalPositions.RemoveAt(parentNumber);
            availableTerminals[terminalTypeNumber].TimesToSpawn--;
            if (availableTerminals[terminalTypeNumber].TimesToSpawn <= 0) availableTerminals.RemoveAt(terminalTypeNumber);
        }
    }
	#endregion
	
	
	
	#region Coroutines
	
	#endregion
}


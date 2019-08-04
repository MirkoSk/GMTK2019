using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class SpawnManager : MonoBehaviour 
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
        for (int i = 0; i < numberOfTerminals; i++)
        {
            int prefabNumber = Random.Range(0, terminalPrefabs.Count);
            int parentNumber = Random.Range(0, terminalPositions.Count);
            Transform parent = terminalPositions[parentNumber];

            //Spawn Terminal
            parent.gameObject.SetActive(true);
            GameObject terminalPrefab = Instantiate(terminalPrefabs[prefabNumber], parent.position, parent.rotation, parent);
            TerminalController terminalController = terminalPrefab.GetComponent<TerminalController>();

            //Spawn Minigame
            GameObject minigame = Instantiate(miniGamePrefabs[Random.Range(0, miniGamePrefabs.Count)], terminalController.MinigamePosition);
            Minigame minigameController = minigame.GetComponent<Minigame>();

            //Spawn RepairMinigame
            GameObject repairMinigame = Instantiate(repairMinigamePrefab, terminalController.MinigamePosition);
            Minigame repairMinigameController = repairMinigame.GetComponent<Minigame>();

            //Link Minigames
            terminalController.LinkedMinigame = minigameController;
            terminalController.RepairMinigame = repairMinigameController;

            terminals.Add(terminalController);
            terminalPositions.RemoveAt(parentNumber);
        }
    }
	#endregion
	
	
	
	#region Coroutines
	
	#endregion
}


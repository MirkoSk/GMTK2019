using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class BackgroundScroller : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [SerializeField] float backgroundWidth;
    [SerializeField] float movingSpeed;
    [SerializeField] float xValue;

    [SerializeField] GameObject[] tiles;

    // Private
    #endregion



    #region Public Properties
    public float BackgroundWidth { get { return BackgroundWidth; } }
    #endregion



    #region Unity Event Functions
    private void Update()
    {
        foreach (GameObject tile in tiles)
        {
            if(tile.transform.position.x <= xValue)
            {
                tile.transform.position = new Vector3(tile.transform.position.x + backgroundWidth * tiles.Length, tile.transform.position.y, transform.position.z);
            }
            tile.transform.position = new Vector3(tile.transform.position.x - 1 * movingSpeed, tile.transform.position.y, tile.transform.position.z);
        }
        
    }
    #endregion



    #region Public Functions

    #endregion



    #region Private Functions

    #endregion



    #region Coroutines

    #endregion
}


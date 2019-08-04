using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class ParallaxController : MonoBehaviour
{

    #region Variable Declarations
    // Serialized Fields
    [MinMaxRange(.25f, 4f)]
    [SerializeField] RangedFloat movementSpeed;
    [SerializeField] GameObject[] starPrefabs;
    [MinMaxRange(.25f, 2f)]
    [SerializeField] RangedFloat sizeModifier;
    [MinMaxRange(0f, 2f)]
    [SerializeField] RangedFloat spawnInterval;
    [MinMaxRange(-10f, 10f)]
    [SerializeField] RangedFloat spawnPositionYAxis;
    [SerializeField] float spawnPositionXAxis;
    [SerializeField] float destroyPositionXAxis;
    [SerializeField] [SortingLayer] int sortingLayer;
    // Private

    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Start()
    {
        Invoke("SpawnParticle", Random.Range(spawnInterval.minValue, spawnInterval.maxValue));
    }
    #endregion



    #region Public Functions

    #endregion



    #region Private Functions
    private void SpawnParticle()
    {
        GameObject starGameObject = starPrefabs[Random.Range(0, starPrefabs.Length)];
        float speed = Random.Range(movementSpeed.minValue, movementSpeed.maxValue);
        float size = Random.Range(sizeModifier.minValue, sizeModifier.maxValue);
        GameObject spawnedObject = Instantiate(starGameObject, new Vector3(spawnPositionXAxis, Random.Range(spawnPositionYAxis.minValue, spawnPositionYAxis.maxValue), 0f), Quaternion.identity, transform);
        spawnedObject.transform.localScale = new Vector3(size, size, size);
        Star star = spawnedObject.GetComponent<Star>();
        SpriteRenderer starSprite = spawnedObject.GetComponent<SpriteRenderer>();
        starSprite.sortingLayerID = sortingLayer;
        star.Speed = speed;
        star.DestroyPositionXAxis = destroyPositionXAxis;
        Invoke("SpawnParticle", Random.Range(spawnInterval.minValue, spawnInterval.maxValue));
    }
    #endregion



    #region Coroutines

    #endregion
}


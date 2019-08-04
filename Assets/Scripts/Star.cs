using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Star : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields

    // Private
    private float speed = 0f;
    private float destroyPositionXAxis = 0f;
	#endregion
	
	
	
	#region Public Properties
	public float Speed { set { speed = value; } }
    public float DestroyPositionXAxis { set { destroyPositionXAxis = value;  } }
    #endregion



    #region Unity Event Functions
    private void Update()
    {
        if (speed != 0 && destroyPositionXAxis != 0)
        {
            transform.position = new Vector3(transform.position.x - .1f * speed, transform.position.y, transform.position.z);
            if (transform.position.x <= destroyPositionXAxis)
            {
                Destroy(gameObject);
            }
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


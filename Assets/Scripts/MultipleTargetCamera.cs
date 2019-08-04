using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class MultipleTargetCamera : MonoBehaviour
{
    #region Variable Declarations
    public static MultipleTargetCamera Instance = null;

    public List<Transform> targets;

    [SerializeField] float smoothTime = 0.5f;
    [SerializeField] float minZoom = 40f;
    [SerializeField] float maxZoom = 10f;
    [SerializeField] float maxDistance = 100f;

    private Vector3 velocity;
    private Camera[] cams;
    private Vector3 offset;
    #endregion



    #region Unity Event Functions
    private void Awake()
    {
        if (MultipleTargetCamera.Instance == null) MultipleTargetCamera.Instance = this;
        else Destroy(this);
    }
    private void Start()
    {
        cams = transform.GetComponentsInChildren<Camera>();
        offset = transform.position;
	}
	
	private void LateUpdate()
    {
        if (targets.Count == 0) return;

        Move();
        Zoom();
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(GetCenterPoint(), 0.1f);
    }
    #endregion



    #region Public Functions
    public void AddTarget(Transform target)
    {
        if (targets.Contains(target))
        {
            Debug.LogError("Couldn't add target for camera. Target already contained in current list.", target);
            return;
        }

        targets.Add(target);
    }

    public void RemoveTarget(Transform target)
    {
        if (!targets.Remove(target)) Debug.LogError("Couldn't remove target for camera.", target);
    }
    #endregion



    #region Private Functions
    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        transform.position = Vector3.SmoothDamp(transform.position, centerPoint + offset, ref velocity, smoothTime);
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / maxDistance);
        foreach (Camera cam in cams) {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
        }
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++) {
            bounds.Encapsulate(targets[i].position);
        }

        if (bounds.size.x > (1.778f * bounds.size.y)) {
            return bounds.size.x;
        }
        else {
            return bounds.size.y * 1.778f;
        }
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 0) return Vector3.zero;

        if (targets.Count == 1) return targets[0].position;

        else {
            var bounds = new Bounds(targets[0].position, Vector3.zero);
            for (int i = 0; i < targets.Count; i++) {
                bounds.Encapsulate(targets[i].position);
            }

            return bounds.center;
        }
    }
    #endregion



    #region Coroutines

    #endregion
}

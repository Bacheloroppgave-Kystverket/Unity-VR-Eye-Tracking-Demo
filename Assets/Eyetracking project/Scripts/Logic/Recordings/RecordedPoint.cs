using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a point of interest where the user has looked at an object.
/// </summary>
[Serializable]
public class RecordedPoint
{
    [Header("Fields values")]
    [SerializeField, Tooltip("The local position as a vector")]
    private Vector3 localPosition;

    [SerializeField, Tooltip("The world position of the impact")]
    private Vector3 worldPosition;

    private TrackableObject trackableObject;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="pointOfInterestOrder"></param>
    /// <param name="hit"></param>
    public RecordedPoint(Vector3 point, Transform parentTransform) {
        CheckIfObjectIsNull(point, "raycast hit");
        this.worldPosition = point;
        this.localPosition = parentTransform.gameObject.transform.InverseTransformPoint(worldPosition);
        TrackableObjectController trackController = parentTransform.GetComponent<TrackableObjectController>();
        if(trackController != null )
        {
            this.trackableObject = trackController.GetTrackableObject();
        }
    }

   

    /// <summary>
    /// Gets the position of the point of interest.
    /// </summary>
    /// <returns>the point of interest</returns>
    public Vector3 GetLocalPosition() => localPosition;

    /// <summary>
    /// Gets the world position.
    /// </summary>
    /// <returns>the world position</returns>
    public Vector3 GetWorldPosition() => worldPosition;
    

    /// <summary>
    /// Checks if the object is null or not. Throws an exception if the object is null.
    /// </summary>
    /// <param name="objecToCheck">the object to check</param>
    /// <param name="error">the error to be in the string.</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the object to check is null.</exception>
    protected void CheckIfObjectIsNull(object objecToCheck, string error)
    {
        if (objecToCheck == null)
        {
            throw new IllegalArgumentException("The " + error + " cannot be null.");
        }
    }
}

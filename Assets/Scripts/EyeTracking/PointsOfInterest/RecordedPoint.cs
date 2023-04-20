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
    [SerializeField, Tooltip("The local position as a vector")]
    private Vector3 localPosition;

    [SerializeField, Tooltip("The world position of the impact")]
    private Vector3 worldPosition;

    [SerializeField, Tooltip("The transform of the object this point hit.")]
    private Transform parentTransform;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pointOfInterestOrder"></param>
    /// <param name="hit"></param>
    public RecordedPoint(RaycastHit hit) {
        CheckIfObjectIsNull(hit, "raycast hit");
        this.worldPosition = hit.point;
        this.parentTransform = hit.collider.gameObject.transform;
        this.localPosition = parentTransform.InverseTransformPoint(worldPosition);
    }

    /// <summary>
    /// Gets the parent transform
    /// </summary>
    /// <returns>the parent transform</returns>
    public Transform GetParentTransform() => parentTransform;

   

    /// <summary>
    /// Gets the position of the point of interest.
    /// </summary>
    /// <returns>the point of interest</returns>
    public Vector3 GetLocalPosition() => localPosition;

    

    /// <summary>
    /// Checks if the object is null or not. Throws an exception if the object is null.
    /// </summary>
    /// <param name="objecToCheck">the object to check</param>
    /// <param name="error">the error to be in the string.</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the object to check is null.</exception>
    private void CheckIfObjectIsNull(object objecToCheck, string error)
    {
        if (objecToCheck == null)
        {
            throw new IllegalArgumentException("The " + error + " cannot be null.");
        }
    }
}

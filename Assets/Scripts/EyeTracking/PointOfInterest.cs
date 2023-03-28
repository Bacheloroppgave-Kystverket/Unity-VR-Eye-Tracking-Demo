using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a point of interest where the user has looked at an object.
/// </summary>
[Serializable]
public class PointOfInterest
{
    [SerializeField, Tooltip("The point of interest.")]
    private int order;

    [SerializeField, Tooltip("The local position as a vector")]
    private Vector3 localPosition;

    [SerializeField, Tooltip("The world position of the impact")]
    private Vector3 worldPosition;

    [SerializeField, Tooltip("The transform of the object this point hit.")]
    private Transform parentTransform;


    public PointOfInterest(int pointOfInterestOrder, RaycastHit hit) {
        CheckIfNumberIsAboveZero(pointOfInterestOrder, "point of inteterst order");
        CheckIfObjectIsNull(hit, "raycast hit");
        this.worldPosition = hit.point;
        this.parentTransform = hit.collider.gameObject.transform;
        this.localPosition = parentTransform.InverseTransformPoint(worldPosition);
        this.order = pointOfInterestOrder;
    }

    /// <summary>
    /// Gets the parent transform
    /// </summary>
    /// <returns>the parent transform</returns>
    public Transform GetParentTransform() => parentTransform;

    /// <summary>
    /// Gets the point of interest order.
    /// </summary>
    /// <returns></returns>
    public int GetPointOfInterestOrder() => order;

    /// <summary>
    /// Gets the position of the point of interest.
    /// </summary>
    /// <returns>the point of interest</returns>
    public Vector3 GetLocalPosition() => localPosition;

    /// <summary>
    /// Checks if the number is above zero.
    /// </summary>
    /// <param name="number">the number to check</param>
    /// <param name="error">what the number is to be shown in the error.</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the number is below zero.</exception>
    private void CheckIfNumberIsAboveZero(long number, string error)
    {
        if (number < 0)
        {
            throw new IllegalArgumentException("The " + error + " needs to be larger or equal to 0");
        }
    }

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

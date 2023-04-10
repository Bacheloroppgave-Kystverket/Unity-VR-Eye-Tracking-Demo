using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PointOfInterest : RecordedPoint
{
    [SerializeField, Tooltip("The point of interest.")]
    private int order;

    /// <summary>
    /// Represents a point of interest.
    /// </summary>
    /// <param name="pointOfInterestOrder">the order of the point of interest.</param>
    /// <param name="hit">the raycast hit</param>
    public PointOfInterest(int pointOfInterestOrder, RaycastHit hit) : base(hit)
    {
        CheckIfNumberIsAboveZero(pointOfInterestOrder, "point of interest order");
        this.order = pointOfInterestOrder;
    }

    /// <summary>
    /// Gets the point of interest order.
    /// </summary>
    /// <returns></returns>
    public int GetPointOfInterestOrder() => order;

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
}

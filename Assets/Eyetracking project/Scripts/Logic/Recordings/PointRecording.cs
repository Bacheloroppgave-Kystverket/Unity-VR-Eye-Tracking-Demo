using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a point of interest as recording.
/// </summary>
[Serializable]
public class PointRecording : RecordedPoint
{
    [SerializeField]
    private int orderId;

    [SerializeField, Tooltip("The amount of times for this point of interest")]
    private int amountOfTimes;

    /// <summary>
    /// Represents a point of interest.
    /// </summary>
    /// <param name="pointOfInterestOrder">the order of the point of interest.</param>
    /// <param name="hit">the raycast hit</param>
    /// <param name="point">the hitpoint</param>
    /// <param name="parentTransform">the parents transform</param>
    public PointRecording(int pointOfInterestOrder, Vector3 point, Transform parentTransform) : base(point, parentTransform)
    {
        CheckIfNumberIsAboveZero(pointOfInterestOrder, "point of interest order");
        this.orderId = pointOfInterestOrder;
        this.amountOfTimes = 1;
    }

    /// <summary>
    /// Gets the time of this recording.
    /// </summary>
    /// <returns>the time</returns>
    public int GetTime() {
        return amountOfTimes;
    }

    /// <summary>
    /// Increments the amount of time this recording has been watched.
    /// </summary>
    public void IncrementAmountOfTimes() => this.amountOfTimes++;

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
    /// Gets the point of interest order.
    /// </summary>
    /// <returns></returns>
    public int GetOrderId() => this.orderId;
}

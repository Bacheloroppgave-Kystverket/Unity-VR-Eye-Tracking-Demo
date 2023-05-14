using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Represents the point of interest as a container.
/// </summary>
/// <typeparam name="T">the type of recorded point</typeparam>
[Serializable]
public abstract class PointRecordContainer<T>
{
    [SerializeField, Tooltip("The point of interest")]
    private RecordedPoint recordedPoint;

    [SerializeField, Tooltip("The parents transform")]
    private Transform parentTransform;

    /// <summary>
    /// Represents a point container.
    /// </summary>
    /// <param name="recordedPoint">the recorded point</param>
    /// <param name="parentTransform">the parent transform</param>
    public PointRecordContainer(RecordedPoint recordedPoint, Transform parentTransform)
    {
        CheckIfObjectIsNull(recordedPoint, "point recording");
        this.recordedPoint = recordedPoint;
        this.parentTransform = parentTransform;
    }

    /// <summary>
    /// Gets the record that is stored in this class.
    /// </summary>
    /// <returns>the record</returns>
    public abstract T  GetRecord();

    /// <summary>
    /// Gets the parent transform
    /// </summary>
    /// <returns>the transform</returns>
    public Transform GetParentTransform() => parentTransform;

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

    /// <summary>
    /// Gets the recorded point.
    /// </summary>
    /// <returns>the recorded point</returns>
    protected RecordedPoint GetRecordedPoint() => recordedPoint;
}

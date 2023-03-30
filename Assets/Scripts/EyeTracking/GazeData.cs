using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a class that holds for how long you have looked at a location. 
/// </summary>
[Serializable]
public class GazeData{

    [SerializeField, Tooltip("The position that has observed this object")]
    private ReferencePosition referencePosition;

    [SerializeField, Tooltip("The amount of times this object has been looked at")]
    private int fixations;

    [SerializeField, Tooltip("The amount of time all the fixations have been in total.")]
    private float fixationDuration;

    /// <summary>
    /// Makes an instance of the GazeData object
    /// </summary>
    /// <param name="referencePosition">the reference position</param>
    public GazeData(ReferencePosition referencePosition) {
        CheckIfObjectIsNull(referencePosition, "reference position");
        this.referencePosition = referencePosition;
    }

    /// <summary>
    /// Increments the fixation amount
    /// </summary>
    public void IncrementFixation() {
        fixations++;
    }

    /// <summary>
    /// Adds time to the gaze data.
    /// </summary>
    public void AddTime() {
        fixationDuration += Time.deltaTime;
    }

    /// <summary>
    /// Gets the amount of fixations.
    /// </summary>
    /// <returns>The fixations</returns>
    public int GetFixations() => fixations;

    /// <summary>
    /// Gets the total fixation duration
    /// </summary>
    /// <returns>the fixation duration</returns>
    public float GetFixationDuration() => fixationDuration;

    /// <summary>
    /// Gets the reference position.
    /// </summary>
    /// <returns>the reference position</returns>
    public ReferencePosition GetReferencePosition() => referencePosition;

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

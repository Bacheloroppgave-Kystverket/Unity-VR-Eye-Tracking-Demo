using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a recording of the positon and the time spent there.
/// </summary>
[Serializable]
public class PositionRecord{

    [SerializeField, Tooltip("The reference position this record represents")]
    private ReferencePosition referencePosition;

    [SerializeField, Tooltip("The time spent at this position")]
    private float positionDuration;

    [SerializeField, Tooltip("The adaptive feedback of this position")]
    private List<AdaptiveFeedback> adaptiveFeedbacks = new List<AdaptiveFeedback>();

    /// <summary>
    /// Makes an instance of reference postition.
    /// </summary>
    /// <param name="referencePosition">the reference position</param>
    public PositionRecord(ReferencePosition referencePosition) {
        CheckIfObjectIsNull(referencePosition, "reference position");
        this.positionDuration = 0;
        this.referencePosition = referencePosition;

    }

    /// <summary>
    /// Gets the reference position.
    /// </summary>
    /// <returns>the reference position</returns>
    public ReferencePosition GetReferencePosition() => referencePosition;

    /// <summary>
    /// Adds feedback to the position record.
    /// </summary>
    /// <param name="adaptiveFeedback">the adaptive feedback</param>
    /// <exception cref="CouldNotAddFeedbackException">gets thrown if the feedback could not be added and is already in the system</exception>
    public void AddFeedback(AdaptiveFeedback adaptiveFeedback) {
        CheckIfObjectIsNull(adaptiveFeedback, "adaptive feedback");
        if (!adaptiveFeedbacks.Contains(adaptiveFeedback))
        {
            adaptiveFeedbacks.Add(adaptiveFeedback);
        }
        else {
            throw new CouldNotAddFeedbackException("The feedback is already added.");
        }
    }

    /// <summary>
    /// Gets the duration of this position.
    /// </summary>
    /// <returns>the position duration</returns>
    public float GetPositionDuration() { return positionDuration; }

    /// <summary>
    /// Adds time to the gaze data.
    /// </summary>
    public void AddTime(float timeToAdd)
    {
        positionDuration += timeToAdd;
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

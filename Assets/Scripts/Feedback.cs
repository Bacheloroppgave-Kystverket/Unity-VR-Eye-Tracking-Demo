using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents feedback that a user is given per object.
/// </summary>
[System.Serializable]
public class Feedback{

    [SerializeField]
    private string feedbackName;

    [SerializeField, Tooltip("The trackable object that should have feedback")]
    private TrackableObject trackableObject;

    [SerializeField, TextArea, Tooltip("The feedback that should be given to the user")]
    private string feedbackText;

    [SerializeField, Range(0.01f, 0.99f), Tooltip("The threshold that the object should be looked at")]
    private float threshold = 0.5f;

    /// <summary>
    /// Makes an instance of the feedback object.
    /// </summary>
    /// <param name="trackableObject"></param>
    public Feedback(TrackableObject trackableObject) {
        this.trackableObject = trackableObject;
    }

    /// <summary>
    /// Gets the threshold
    /// </summary>
    /// <returns>the threshold</returns>
    public float GetThreshold() {
        return threshold;
    }

    /// <summary>
    /// Gets the feedback text.
    /// </summary>
    /// <returns>the feedback text</returns>
    public string GetFeedbackText() {
        return feedbackText;
    }

    /// <summary>
    /// Gets the trackable object.
    /// </summary>
    /// <returns>the trackable object</returns>
    public TrackableObject GetTrackableObject() {
        return trackableObject;
    }
}

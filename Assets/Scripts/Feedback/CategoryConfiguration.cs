using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents feedback that a user is given per object.
/// </summary>
[System.Serializable]
public class CategoryConfiguration{

    [SerializeField, Tooltip("The trackable object that should have feedback")]
    private TrackableType trackableType;

    [SerializeField, Range(0.01f, 0.99f), Tooltip("The threshold that the object should be looked at")]
    private float threshold = 0.5f;

    /// <summary>
    /// Makes an instance of the feedback object.
    /// </summary>
    /// <param name="trackableType">the trackable type</param>
    /// <param name="threshold">the threshold of the trackable type</param>
    public CategoryConfiguration(TrackableType trackableType, float threshold) {
        this.trackableType = trackableType;
        this.threshold = threshold;
    }

    /// <summary>
    /// Gets the threshold
    /// </summary>
    /// <returns>the threshold</returns>
    public float GetThreshold() {
        return threshold;
    }

    /// <summary>
    /// Gets the trackable type.
    /// </summary>
    /// <returns>the trackable type</returns>
    public TrackableType GetTrackableType() {
        return trackableType;
    }
}

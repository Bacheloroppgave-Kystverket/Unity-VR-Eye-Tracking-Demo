using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CategoryFeedback
{
    [SerializeField, Tooltip("The trackable type.")]
    private TrackableType trackableType;

    [SerializeField, Tooltip("The time spent at this category.")]
    private float time;

    /// <summary>
    /// Makes an instance of the calculated feedback.
    /// </summary>
    /// <param name="trackableType">the trackable type</param>
    /// <param name="time">the time of views</param>
    public CategoryFeedback(TrackableType trackableType, float time) { 
        this.trackableType = trackableType;
        this.time = time;
    }

    /// <summary>
    /// Adds time to the feedback object.
    /// </summary>
    /// <param name="timeToAdd">the time to add.</param>
    public void AddTime(float timeToAdd) {
        if (timeToAdd > 0) {
            this.time += timeToAdd;
        }
    }

    /// <summary>
    /// Gets the trackable type.
    /// </summary>
    /// <returns>the trackable type</returns>
    public TrackableType GetTrackableType() => trackableType;

    /// <summary>
    /// Gets the time.
    /// </summary>
    /// <returns>the time</returns>
    public float GetTime() => time;

    /// <summary>
    /// Calculates the prosentage that this position was watched.
    /// </summary>
    /// <param name="totalTime">the total time of the seat</param>
    /// <returns>the total time spent at this position as a prosentage.</returns>
    public float CalculateProsentage(float totalTime)
    {
        return Mathf.Round(((time / totalTime)) * 100);
    }
}

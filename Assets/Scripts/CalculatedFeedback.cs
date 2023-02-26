using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatedFeedback : MonoBehaviour
{
    [SerializeField, Tooltip("The trackable type.")]
    private TrackableType trackableType;

    [SerializeField, Tooltip("The prosentage of the views")]
    private float prosentage;

    /// <summary>
    /// Makes an instance of the calculated feedback.
    /// </summary>
    /// <param name="trackableType">the trackable type</param>
    /// <param name="prosentage">the prosentage of views</param>
    public CalculatedFeedback(TrackableType trackableType, float prosentage) { 
        this.trackableType = trackableType;
        this.prosentage = prosentage;
    }

    /// <summary>
    /// Gets the trackable type.
    /// </summary>
    /// <returns>the trackable type</returns>
    public TrackableType GetTrackableType() => trackableType;

    /// <summary>
    /// Gets the prosentage.
    /// </summary>
    /// <returns>the prosentage</returns>
    public float GetProsentage() => prosentage;
}

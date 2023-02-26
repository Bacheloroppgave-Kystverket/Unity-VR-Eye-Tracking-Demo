using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ReferencePosition
{

    [SerializeField, Tooltip("The name of the location")]
    private string locationName;

    [SerializeField, Tooltip("The time that this position has been used")]
    private float positionDuration;

    /// <summary>
    /// Gets the location name.
    /// </summary>
    /// <returns>the location name</returns>
    public string GetLocationName() => locationName;


    /// <summary>
    /// Gets the duration that was spent at this position.
    /// </summary>
    /// <returns>the position duration</returns>
    public float GetPositionDuration() => positionDuration;

    /// <summary>
    /// Adds time to the gaze data.
    /// </summary>
    public void AddTime(float timeToAdd)
    {
        positionDuration += Time.deltaTime;
    }
}

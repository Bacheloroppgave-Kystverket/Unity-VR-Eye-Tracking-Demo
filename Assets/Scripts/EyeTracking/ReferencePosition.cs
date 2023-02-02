using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a position that a user can be in space. 
/// </summary>
public class ReferencePosition : MonoBehaviour
{
    [SerializeField]
    private string locationID;

    [SerializeField]
    private string locationName;

    [SerializeField]
    private float positionDuration;


    /// <summary>
    /// Gets the location ID.
    /// </summary>
    /// <returns>the location ID</returns>
    public string GetLocationId() => locationID;

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
    public void AddTime() {
        positionDuration += Time.deltaTime;
    }
}

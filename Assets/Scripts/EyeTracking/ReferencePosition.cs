using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a position that a user can be in space. 
/// </summary>
public class ReferencePosition : MonoBehaviour
{
    [SerializeField, Tooltip("The id of the location that this represents")]
    private string locationID;

    [SerializeField, Tooltip("The name of the location")]
    private string locationName;

    [SerializeField, Tooltip("The time that this position has been used")]
    private float positionDuration;

    private void Awake() {
        gameObject.tag = typeof(ReferencePosition).Name;
    }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a position that a user can be in space. 
/// </summary>
public class ReferencePositionController : MonoBehaviour
{

    [SerializeField, Tooltip("The reference position")]
    private ReferencePosition referencePosition;

    private void Awake() {
        gameObject.tag = typeof(ReferencePositionController).Name;
    }

    /// <summary>
    /// Gets the location ID.
    /// </summary>
    /// <returns>the location ID</returns>
    public string GetLocationId() => referencePosition.GetLocationId();

    /// <summary>
    /// Gets the location name.
    /// </summary>
    /// <returns>the location name</returns>
    public string GetLocationName() => referencePosition.GetLocationName();

    /// <summary>
    /// Gets the duration that was spent at this position.
    /// </summary>
    /// <returns>the position duration</returns>
    public float GetPositionDuration() => referencePosition.GetPositionDuration();

    /// <summary>
    /// Adds time to the gaze data.
    /// </summary>
    public void AddTime() {
        referencePosition.AddTime(Time.deltaTime);
    }

    /// <summary>
    /// Gets the reference position.
    /// </summary>
    /// <returns>the reference position.</returns>
    public ReferencePosition GetReferencePosition() => referencePosition;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a record of a trackable object.
/// </summary>
[Serializable]
public class TrackableRecord
{
    [SerializeField, Tooltip("The trackable data id")]
    private long trackableDataId;

    [SerializeField, Tooltip("The trackable object that this record is for.")]
    private TrackableObject trackableObject;

    [SerializeField, Tooltip("The list of all the positions that has looked at this object.")]
    private List<GazeData> gazeList = new List<GazeData>();

    [SerializeField, Tooltip("The distance to this object")]
    private ViewDistance viewDistance;

    /// <summary>
    /// Makes an instance of the trackable record.
    /// </summary>
    /// <param name="trackableRecordBuilder">the trackable record builder</param>
    public TrackableRecord(TrackableRecordBuilder trackableRecordBuilder) {
        CheckIfObjectIsNull(trackableRecordBuilder, "trackable record builder");
        this.trackableObject = trackableRecordBuilder.GetTrackableObject();
        this.viewDistance = trackableRecordBuilder.GetViewDistance();
    }

    /// <summary>
    /// Gets the gaze data that belongs to that reference position.
    /// </summary>
    /// <param name="referencePosition">the reference position</param>
    /// <returns>the gaze data that matches that reference position</returns>
    public GazeData GetGazeDataForPosition(ReferencePosition referencePosition)
    {
        GazeData gazeData = gazeList.Find(gazeData => gazeData.GetReferencePosition() == referencePosition);
        if (gazeData == null) {
            gazeData = new GazeData(referencePosition);
            gazeList.Add(gazeData);
        }
        return gazeData;
    }

    /// <summary>
    /// Gets the trackable object.
    /// </summary>
    /// <returns>the trackable object</returns>
    public TrackableObject GetTrackableObject() => trackableObject;

    /// <summary>
    /// Sets the viewdistance for the object.
    /// </summary>
    /// <param name="viewDistance">the view distance</param>
    public void SetViewDistance(ViewDistance viewDistance)
    {
        CheckIfObjectIsNull(viewDistance, "view distance");
        this.viewDistance = viewDistance;
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

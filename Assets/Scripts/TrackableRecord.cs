using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    /// Gets the gaze data that has that location id.
    /// </summary>
    /// <param name="locationID">the location id</param>
    /// <returns>the gaze data that matches that location id. Is null if location does not exsist</returns>
    private GazeData GetGazeDataForPosition(string locationID)
    {
        CheckIfStringIsValid(locationID, "location id");
        return gazeList.Find(gazeData => gazeData.GetLocationID() == locationID);
    }

    /// <summary>
    /// Checks if the string is null or empty. Throws exceptions if one of these conditions are true.
    /// </summary>
    /// <param name="stringToCheck">the string to check</param>
    /// <param name="error">the error of the string</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the string to check is empty or null.</exception>
    private void CheckIfStringIsValid(string stringToCheck, string error)
    {
        CheckIfObjectIsNull(stringToCheck, error);
        if (stringToCheck.Trim().Length == 0)
        {
            throw new IllegalArgumentException("The" + error + " cannot be empty.");
        }
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

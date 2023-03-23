using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrackableObject
{
    [Header("Configure object")]
    [SerializeField, Tooltip("The name of the object")]
    private string nameOfObject;

    [SerializeField, Tooltip("Defines the type of object that we are looking at.")]
    private TrackableType trackableType = TrackableType.UNDEFINED;

    /// <summary>
    /// Sets the name of the object.
    /// </summary>
    /// <param name="nameOfObject">the new object name</param>
    public void SetGameObjectName(string nameOfObject) {
        if (nameOfObject != null && nameOfObject != "") {
            this.nameOfObject = nameOfObject;
        }
    }

    /// <summary>
    /// Gets the trackable object type
    /// </summary>
    /// <returns>the trackable object type</returns>
    public TrackableType GetTrackableType()
    {
        return trackableType;
    }

    /// <summary>
    /// Gets the name of the object
    /// </summary>
    /// <returns>the name of the object</returns>
    public string GetNameOfObject()
    {
        return nameOfObject;
    }

    /// <summary>
    /// Gets the gaze data that has that location id.
    /// </summary>
    /// <param name="locationID">the location id</param>
    /// <returns>the gaze data that matches that location id. Is null if location does not exsist</returns>
    public GazeData GetGazeDataForLocation(string locationID) {
        GazeData gazeData = null;
        if (locationID != null && locationID != ""){
            gazeData = GetGazeDataForPosition(locationID);
            if (gazeData == null){
                gazeData = new GazeData(locationID);
                gazeList.Add(gazeData);
            }
        }
        return gazeData;
    }
}

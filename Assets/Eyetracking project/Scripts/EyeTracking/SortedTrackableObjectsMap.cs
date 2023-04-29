using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a Sorted trackable objects map that sorts the objects based on their category.
/// </summary>
[Serializable]
public class SortedTrackableObjectsMap 
{

    [SerializeField, Tooltip("A list with the trackable types")]
    private List<TrackableType> trackableTypes;

    private Dictionary<TrackableType, List<TrackableObjectController>> trackableObjectMap;

    /// <summary>
    /// Makes an instance of the SortedTrackableObjectsMap
    /// </summary>
    /// <param name="trackableObjects">the trackable objects</param>
    /// <param name="visualizeKeysAndValues">true if the keys and values should show in editor. False otherwise</param>
    public SortedTrackableObjectsMap(List<TrackableObjectController> trackableObjects, bool visualizeKeysAndValues) {
        trackableTypes = new List<TrackableType>();
        trackableObjectMap = new Dictionary<TrackableType, List<TrackableObjectController>>();
        foreach (TrackableObjectController trackableObject in trackableObjects)
        {
            AddTrackableObject(trackableObject);
        }
    }

    /// <summary>
    /// Adds a trackable object to a list based on its type.
    /// </summary>
    /// <param name="trackableObject">the trackable object</param>
    private void AddTrackableObject(TrackableObjectController trackableObject) {
        if (trackableObject != null) {
            TrackableType trackableType = trackableObject.GetTrackableType();
            if (!trackableObjectMap.ContainsKey(trackableType)) {
                trackableObjectMap.Add(trackableType, new List<TrackableObjectController>());
                trackableTypes.Add(trackableType);
            }
            trackableObjectMap[trackableType].Add(trackableObject);
        }
    }

    /// <summary>
    /// Gets the iterator for the keys.
    /// </summary>
    /// <returns>the iterator</returns>
    public IEnumerator<TrackableType> GetEnumerator() {
        return trackableObjectMap.Keys.GetEnumerator();
    }

    /// <summary>
    /// Gets the list for the trackable type.
    /// </summary>
    /// <param name="trackableType">the trackable type</param>
    /// <returns>the list for the trackable type</returns>
    public List<TrackableObjectController> GetListForTrackableType(TrackableType trackableType) {
        return trackableObjectMap.ContainsKey(trackableType) ? trackableObjectMap[trackableType] : new List<TrackableObjectController>();
    }
}

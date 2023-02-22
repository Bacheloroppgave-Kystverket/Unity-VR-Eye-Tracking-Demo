using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a Sorted trackable objects map that sorts the objects based on their category.
/// </summary>
public class SortedTrackableObjectsMap : MonoBehaviour
{
    [SerializeField, Tooltip("The sorted trackable objects map")]
    private HashmapVisualiser<TrackableType, List<TrackableObjectController>> sortedMap;

    /// <summary>
    /// Makes an instance of the SortedTrackableObjectsMap
    /// </summary>
    /// <param name="trackableObjects">the trackable objects</param>
    /// <param name="visualizeKeysAndValues">true if the keys and values should show in editor. False otherwise</param>
    public SortedTrackableObjectsMap(List<TrackableObjectController> trackableObjects, bool visualizeKeysAndValues) {
        sortedMap = new HashmapVisualiser<TrackableType, List<TrackableObjectController>>(visualizeKeysAndValues);
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
            if (!sortedMap.CheckForKey(trackableType)) {
                sortedMap.Add(trackableType, new List<TrackableObjectController>());
            }
            sortedMap.GetValue(trackableType).Add(trackableObject);
        }
    }

    /// <summary>
    /// Gets the iterator for the keys.
    /// </summary>
    /// <returns>the iterator</returns>
    public IEnumerator<TrackableType> GetEnumerator() {
        return sortedMap.GetKeyIterator();
    }

    /// <summary>
    /// Gets the list for the trackable type.
    /// </summary>
    /// <param name="trackableType">the trackable type</param>
    /// <returns>the list for the trackable type</returns>
    public List<TrackableObjectController> GetListForTrackableType(TrackableType trackableType) {
        return sortedMap.CheckForKey(trackableType) ? sortedMap.GetValue(trackableType) : new List<TrackableObjectController>();
    }
}

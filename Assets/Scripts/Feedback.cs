using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class Feedback : MonoBehaviour
{
    [SerializeField, Tooltip("The hashmap itself")]
    private HashmapVisualiser<TrackableObject, float> hashmapVisualiser;

    /// <summary>
    /// Makes an instance of the feedback object.
    /// </summary>
    /// <param name="dictionary">the dictionary with the values.</param>
    public Feedback(Dictionary<TrackableObject, float> dictionary) {
        hashmapVisualiser = new HashmapVisualiser<TrackableObject, float>(dictionary);
    }

    /// <summary>
    /// Gets the feedback as a basic string.
    /// </summary>
    /// <returns></returns>
    public string GetFeedbackAsString() {
        StringBuilder stringBuilder = new StringBuilder();
        IEnumerator<TrackableObject> it = GetTrackableObjectIterator();
        while (it.MoveNext()) {
            TrackableObject currentObject = it.Current;
            stringBuilder.Append(currentObject.GetNameOfObject());
            stringBuilder.Append(" : ");
            stringBuilder.AppendLine(GetTrackableObjectValue(currentObject).ToString());
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Gets the iterator for the keys.
    /// </summary>
    /// <returns>the iterator</returns>
    private IEnumerator<TrackableObject> GetTrackableObjectIterator() {
        return hashmapVisualiser.GetKeyIterator();
    }

    /// <summary>
    /// Gets the value of the trackable object.
    /// </summary>
    /// <param name="trackableObject">the trackable object</param>
    /// <returns>the corrensponding value</returns>
    private float GetTrackableObjectValue(TrackableObject trackableObject)
    {
        return hashmapVisualiser.GetValue(trackableObject);
    }
}

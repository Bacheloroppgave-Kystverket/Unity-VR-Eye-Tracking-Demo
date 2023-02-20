using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// Reprents the feedback that the user gets. 
/// </summary>
[Serializable]
public class ProsentageTypeFeedback : Feedback
{
    [SerializeField, Tooltip("True if the keys and values should show in editor. False otherwise")]
    private bool visualizeKeysAndValues = true;

    [SerializeField, Tooltip("The hashmap itself")]
    private HashmapVisualiser<TrackableType, float> hashmapVisualiser;

    /// <summary>
    /// Makes an instance of the feedback object.
    /// </summary>
    /// <param name="dictionary">the dictionary with the values.</param>
    public ProsentageTypeFeedback(Dictionary<TrackableType, float> dictionary) : base() {
        hashmapVisualiser = new HashmapVisualiser<TrackableType, float>(dictionary, visualizeKeysAndValues);
    }

    /// <inheritdoc/>
    public override string GetFeedback()
    {
        return GetFeedbackAsString();
    }

    /// <summary>
    /// Gets the feedback as a basic string.
    /// </summary>
    /// <returns>the feedback as string with prosentages of all objects</returns>
    private string GetFeedbackAsString() {
        StringBuilder stringBuilder = new StringBuilder();
        IEnumerator<TrackableType> it = GetTrackableObjectIterator();
        while (it.MoveNext()) {
            TrackableType currentType = it.Current;
            stringBuilder.Append(currentType);
            stringBuilder.Append(" : ");
            stringBuilder.Append(GetTrackableObjectValue(currentType).ToString());
            stringBuilder.AppendLine("%");
        }
        return stringBuilder.ToString();
    }

    /// <summary>
    /// Gets the iterator for the keys.
    /// </summary>
    /// <returns>the iterator</returns>
    private IEnumerator<TrackableType> GetTrackableObjectIterator() {
        return hashmapVisualiser.GetKeyIterator();
    }

    /// <summary>
    /// Gets the value of the trackable object.
    /// </summary>
    /// <param name="trackableObject">the trackable object</param>
    /// <returns>the corrensponding value</returns>
    private float GetTrackableObjectValue(TrackableType trackableObject)
    {
        return hashmapVisualiser.GetValue(trackableObject);
    }
}

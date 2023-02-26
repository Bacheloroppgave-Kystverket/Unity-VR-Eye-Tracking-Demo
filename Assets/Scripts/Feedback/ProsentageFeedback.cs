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

    [SerializeField]
    private List<CalculatedFeedback> feedbackList;

    /// <summary>
    /// Makes an instance of the feedback object.
    /// </summary>
    /// <param name="feedbackList">the list with the values.</param>
    public ProsentageTypeFeedback(List<CalculatedFeedback> feedbackList) : base() {
        
        //ONLY FOR TESTING
        Dictionary<TrackableType, float> map = new Dictionary<TrackableType, float>();

        this.feedbackList = feedbackList;

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
        IEnumerator<CalculatedFeedback> it = feedbackListIEnumerator();
        while (it.MoveNext()) {
            CalculatedFeedback calculatedFeedback = it.Current;
            stringBuilder.Append(calculatedFeedback.GetTrackableType());
            stringBuilder.Append(" : ");
            stringBuilder.Append(calculatedFeedback.GetProsentage().ToString());
            stringBuilder.AppendLine("%");
        }
        return stringBuilder.ToString();
    }

    /// <summary>
    /// Gets the iterator for the keys.
    /// </summary>
    /// <returns>the iterator</returns>
    private IEnumerator<CalculatedFeedback> feedbackListIEnumerator() {
        return feedbackList.GetEnumerator();
    }
}

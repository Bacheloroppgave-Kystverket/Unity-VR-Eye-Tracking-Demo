using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// Reprents the feedback that the user gets. 
/// </summary>
[Serializable]
public class AdaptiveFeedback : Feedback
{
    [SerializeField, Tooltip("The position name")]
    private string positionName;

    [SerializeField, Tooltip("The time of the position")]
    private float positionTime;

    [SerializeField, Tooltip("The list with the feedbacks")]
    private List<CategoryFeedback> feedbackList;

    /// <summary>
    /// Makes an instance of the feedback object.
    /// </summary>
    /// <param name="positionName">the name of the position</param>
    /// <param name="positionTime">the time for the position</param>
    /// <param name="feedbackList">the list with the values</param>
    public AdaptiveFeedback(string positionName, float positionTime ,List<CategoryFeedback> feedbackList) : base() {
        this.positionName = positionName;
        this.positionTime = positionTime;
        this.feedbackList = feedbackList;
        float totalTime = 0;
        foreach (CategoryFeedback feedback in feedbackList) {
            totalTime += feedback.GetTime();
        }
        feedbackList.Add(new CategoryFeedback(TrackableType.OTHER, positionTime - totalTime));
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
        IEnumerator<CategoryFeedback> it = feedbackListIEnumerator();
        while (it.MoveNext()) {
            CategoryFeedback calculatedFeedback = it.Current;
            stringBuilder.Append(calculatedFeedback.GetTrackableType());
            stringBuilder.Append(" : ");
            stringBuilder.Append(calculatedFeedback.CalculateProsentage(positionTime).ToString());
            stringBuilder.AppendLine("%");
        }
        return stringBuilder.ToString();
    }

    /// <summary>
    /// Gets the iterator for the keys.
    /// </summary>
    /// <returns>the iterator</returns>
    private IEnumerator<CategoryFeedback> feedbackListIEnumerator() {
        return feedbackList.GetEnumerator();
    }
}

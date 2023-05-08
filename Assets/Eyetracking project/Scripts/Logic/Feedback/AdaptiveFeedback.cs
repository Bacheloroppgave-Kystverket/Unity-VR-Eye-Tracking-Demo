using Oculus.Voice.Windows;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Reprents the feedback that the user gets. 
/// </summary>
[Serializable]
public class AdaptiveFeedback : Feedback
{
    [SerializeField, Tooltip("The time of the position")]
    private float positionTime;

    [SerializeField, Tooltip("The list with the feedbacks")]
    private List<CategoryFeedback> feedbackList;


    /// <summary>
    /// Makes an instance of the feedback object.
    /// </summary>
    /// <param name="positionTime">the time of the position</param>
    /// <param name="feedbackList">the list with the values</param>
    public AdaptiveFeedback(float positionTime ,List<CategoryFeedback> feedbackList) : base() {
        this.positionTime = positionTime;
        this.feedbackList = feedbackList;
        float totalTime = 0;

        CategoryFeedback categoryOther = null;

        foreach (CategoryFeedback feedback in feedbackList) {
            totalTime += feedback.GetTime();
            if (feedback.GetTrackableType() == TrackableType.OTHER) {
                categoryOther = feedback;
            }
        }


        float time = positionTime - totalTime;

        if (categoryOther != null){
            categoryOther.AddTime(Math.Abs(time));
        }
        else {
            categoryOther = new CategoryFeedback(TrackableType.OTHER, time > 0 ? time : 0);
            this.feedbackList.Add(categoryOther);
        }
    }

    /// <inheritdoc/>
    public override string GetFeedback()
    {
        return GetFeedbackAsString();
    }

    /// <summary>
    /// Gets the least viewed object as a string.
    /// </summary>
    /// <param name="feedbackConfigurations">the feedback configuration of this position</param>
    /// <returns>the message with the biggest difference object</returns>
    public string GetLeastViewedObjectAsString(List<CategoryConfiguration> feedbackConfigurations) {
        StringBuilder stringBuilder = new StringBuilder();
        float biggestDifference = 0;
        CategoryFeedback differenceHolder = null;
        feedbackConfigurations.ForEach(feedbackConfiguration => {
            CategoryFeedback feedback = feedbackList.Find(categoryFeedback => categoryFeedback.GetTrackableType() == feedbackConfiguration.GetTrackableType());
            float difference = feedback == null ? 0 : (feedbackConfiguration.GetThreshold() * 100) - feedback.CalculateProsentage(positionTime);
            if (Math.Abs(difference) > Math.Abs(biggestDifference)) {
                biggestDifference = difference;
                differenceHolder = feedback;
            }
        });
        stringBuilder.Append(differenceHolder.GetTrackableType());
        if (biggestDifference < 0) {
            stringBuilder.Append(" needs to get less time since its ");
            stringBuilder.Append(biggestDifference);
            stringBuilder.Append("% over the threshold.");
        }
        else {
            stringBuilder.Append(" needs more time since its ");
            stringBuilder.Append(Math.Abs(biggestDifference));
            stringBuilder.Append("% under the threshold.");
        }

        return stringBuilder.ToString();
    }


    /// <summary>
    /// Gets the feedback as a basic string.
    /// </summary>
    /// <returns>the feedback as string with prosentages of all objects</returns>
    private string GetFeedbackAsString() {
        StringBuilder stringBuilder = new StringBuilder();
        IEnumerator<CategoryFeedback> it = FeedbackListIEnumerator();
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
    private IEnumerator<CategoryFeedback> FeedbackListIEnumerator() {
        return feedbackList.GetEnumerator();
    }
}

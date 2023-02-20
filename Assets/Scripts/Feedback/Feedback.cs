using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// Reprents the feedback that the user gets. 
/// </summary>
[Serializable]
public abstract class Feedback : MonoBehaviour
{
    [SerializeField, Tooltip("The date of this feedback")]
    private DateTime feedbackTime;

    /// <summary>
    /// Makes an instance of the feedback.
    /// </summary>
    public Feedback() {
        feedbackTime = DateTime.Now;
    }

    /// <summary>
    /// Gets the feedback
    /// </summary>
    /// <returns>the feedback</returns>
    public abstract string GetFeedback();
}

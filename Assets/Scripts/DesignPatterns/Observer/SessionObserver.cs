using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SessionObserver
{
    /// <summary>
    /// Updates the feedback.
    /// </summary>
    /// <param name="feedback">the feedback to be updated.</param>
    void UpdateFeedback(Feedback feedback);
}

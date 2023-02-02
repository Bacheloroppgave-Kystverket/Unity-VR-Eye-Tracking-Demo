using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an observer that listens to a trackable object.
/// </summary>
public interface TrackableObserver : Observer {

    /// <summary>
    /// Updates the amount of fixations.
    /// </summary>
    /// <param name="fixations">the amount of fixations</param>
    public void UpdateFixations(int fixations);

    /// <summary>
    /// Updates the fixation durations.
    /// </summary>
    /// <param name="fixationDuration">the fixation duration</param>
    public void UpdateFixationDuration(float fixationDuration);

    /// <summary>
    /// Updates the average fixation durations
    /// </summary>
    /// <param name="averageFixationDuration">the average fixation duration</param>
    public void UpdateAverageFixationDuration(float averageFixationDuration);
}

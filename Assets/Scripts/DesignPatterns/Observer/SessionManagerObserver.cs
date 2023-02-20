using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a Session manager observer that starts and stops eye tracking.
/// </summary>
public interface SessionManagerObserver
{
    /// <summary>
    /// Starts the eye tracking.
    /// </summary>
    public void StartEyeTracking();

    /// <summary>
    /// Stops the eye tracking.
    /// </summary>
    public void StopEyeTracking();
}

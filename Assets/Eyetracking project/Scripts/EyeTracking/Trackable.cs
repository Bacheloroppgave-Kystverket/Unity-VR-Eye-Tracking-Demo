using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an object that is trackable and reacts to gaze enter and exit.
/// </summary>
public interface Trackable
{
    /// <summary>
    /// Is called once the object is watched.
    /// </summary>
    void OnGazeEnter();

    /// <summary>
    /// Is called when the eyegaze is exited.
    /// </summary>
    void OnGazeExit();
}

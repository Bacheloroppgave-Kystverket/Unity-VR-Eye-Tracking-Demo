using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

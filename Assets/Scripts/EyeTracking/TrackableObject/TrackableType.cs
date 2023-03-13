using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum TrackableType
{

    /// <summary>
    /// Represents objects that are walls
    /// </summary>
    WALL,

    /// <summary>
    /// Represents objects that are windows
    /// </summary>
    WINDOW,

    /// <summary>
    /// Represents objects that are mirrors
    /// </summary>
    MIRROR,

    /// <summary>
    /// Represents other observable objects
    /// </summary>
    OTHER,

    /// <summary>
    /// Represents undefined objects
    /// </summary>
    UNDEFINED,
}

using System.Collections;
using System.Collections.Generic;
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
    /// Represents objects that are spheres
    /// </summary>
    SPHERE,

    /// <summary>
    /// Represents objects that are cubes
    /// </summary>
    CUBE,

    /// <summary>
    /// Represents objects that are cubes
    /// </summary>
    LONGCUBE,

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

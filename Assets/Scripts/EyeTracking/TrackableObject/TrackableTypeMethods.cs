using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Has methods that can identify the diffrent trackable types and their properties.
/// </summary>
public static class TrackableTypeMethods
{
    /// <summary>
    /// Checks if a trackable is a solid or not.
    /// </summary>
    /// <param name="category">the trackable type</param>
    /// <returns>true if the trackable is a solid. False otherwise.</returns>
    public static bool IsTrackableSolid(TrackableType category) {
        return !(category == TrackableType.WINDOW);
    }
}

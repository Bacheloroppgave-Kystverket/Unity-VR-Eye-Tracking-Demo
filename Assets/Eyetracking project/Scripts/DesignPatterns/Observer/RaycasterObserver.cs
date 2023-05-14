using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a observer that can look at a raycaster.
/// </summary>
public interface RaycasterObserver
{

    /// <summary>
    /// Gets called when the raycaster has observed objects.
    /// </summary>
    /// <param name="raycastHits">the raycast hits</param>
    /// <param name="lookPosition">the look position</param>
    void ObservedObjects(RaycastHit[] raycastHits, Vector3 lookPosition);

}

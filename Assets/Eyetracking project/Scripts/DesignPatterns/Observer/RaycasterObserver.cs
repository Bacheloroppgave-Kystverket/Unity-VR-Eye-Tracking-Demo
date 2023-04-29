using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface RaycasterObserver
{

    /// <summary>
    /// Gets called when the raycaster has observed objects.
    /// </summary>
    /// <param name="raycastHits">the raycast hits</param>
    /// <param name="lookPosition">the look position</param>
    void ObservedObjects(RaycastHit[] raycastHits, Vector3 lookPosition);

}

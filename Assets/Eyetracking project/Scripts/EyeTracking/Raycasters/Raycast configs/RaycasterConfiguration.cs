using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a configuration of a ray 
/// </summary>
public interface RaycasterConfiguration
{
    /// <summary>
    /// Shoots a single object and returns the hit array.
    /// </summary>
    /// <param name="position">the position of the raycast</param>
    /// <param name="direction">the direction of the raycast.</param>
    /// <param name="range">the range of the ray</param>
    /// <returns>the single object</returns>
    RaycastHit[] ShootSingleConfiguration(Vector3 position, Vector3 direction, int range);

    /// <summary>
    /// Shoots multiple objects.
    /// </summary>
    /// <param name="position">the position of the raycast</param>
    /// <param name="direction">the direction of the raycast.</param>
    /// <param name="range">the range of the ray</param>
    /// <returns>the multiple objects</returns>
    RaycastHit[] ShootMultipleObjectsConfiguration(Vector3 position, Vector3 direction, int range);


}

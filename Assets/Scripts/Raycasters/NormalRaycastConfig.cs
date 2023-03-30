using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a normal raycast that only shoots one simple raycast.
/// </summary>
[Serializable]
public class NormalRaycastConfig : RaycasterConfiguration
{
    /// <inheritdoc/>
    public RaycastHit[] ShootMultipleObjectsConfiguration(Vector3 position, Vector3 direction, int range)
    {
        RaycastHit[] hits = Physics.RaycastAll(position, direction, range); //Physics.RaycastAll(position, direction, range);
        return hits;
    }

    /// <inheritdoc/>
    public RaycastHit[] ShootSingleConfiguration(Vector3 position, Vector3 direction, int range)
    {
        RaycastHit raycastHit;
        Physics.Raycast(position, direction, out raycastHit, range);
        RaycastHit[] hits = { raycastHit };
        return hits;
    }
}

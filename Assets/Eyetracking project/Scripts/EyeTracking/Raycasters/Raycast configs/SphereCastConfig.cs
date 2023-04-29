using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Represents a sphere cast in raycasting.
/// </summary>
[Serializable]
public class SphereCastConfig : RaycasterConfiguration
{
    [SerializeField, Range(0.01f, 0.5f), Tooltip("The size of the sphere that we shoot to determine what you look at.")]
    private float sphereSize = 0.03f;

    /// <inheritdoc/>
    public RaycastHit[] ShootMultipleObjectsConfiguration(Vector3 position, Vector3 direction, int range)
    {
        int layerMask = 1 << 11;
        layerMask = ~layerMask;
        RaycastHit[] hits = Physics.SphereCastAll(position, sphereSize, direction, range, layerMask); //Physics.RaycastAll(position, direction, range);
        return hits;
    }

    /// <inheritdoc/>
    public RaycastHit[] ShootSingleConfiguration(Vector3 position, Vector3 direction, int range)
    {
        int layerMask = 1 << 11;
        layerMask = ~layerMask;
        RaycastHit raycastHit;
        Physics.SphereCast(position, sphereSize, direction, out raycastHit, range, layerMask);
        RaycastHit[] hits = { raycastHit };
        return hits;
    }

    
}

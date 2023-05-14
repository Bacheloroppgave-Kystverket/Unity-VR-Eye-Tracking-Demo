using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Is a single raycaster.
/// </summary>
public class SingleRayCaster : RayCasterObject
{
    [SerializeField]
    private GameObject castingObject;

    ///<inheritdoc/>
    public override Vector3 FindDirection() {
        return transform.forward;
    }

    ///<inheritdoc/>
    public override Vector3 FindPosition() {
        return castingObject.transform.position;
    }


}

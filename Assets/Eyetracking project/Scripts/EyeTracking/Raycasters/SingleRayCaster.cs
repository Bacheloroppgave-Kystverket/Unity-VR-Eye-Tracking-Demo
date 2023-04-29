using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleRayCaster : RayCasterObject
{
    [SerializeField]
    private GameObject castingObject;

    public override Vector3 FindDirection() {
        return transform.forward;
    }

    ///<inheritdoc/>
    public override Vector3 FindPosition() {
        return castingObject.transform.position;
    }


}

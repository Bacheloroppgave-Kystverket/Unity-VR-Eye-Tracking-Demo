using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EyeCaster : RayCasterObject
{
    [SerializeField]
    private GameObject eyeOne;

    [SerializeField]
    private GameObject eyeTwo;

    protected override Vector3 FindDirection() {
        return (eyeOne.transform.forward + eyeTwo.transform.forward) / 2;
    }

    ///<inheritdoc/>
    protected override Vector3 FindPosition() {
        return (eyeOne.transform.position + eyeTwo.transform.position) / 2;
    }
}

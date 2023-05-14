using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates the object that its set on.
/// </summary>
public class RotateController : MonoBehaviour
{
    [SerializeField, Tooltip("Hello")]
    private float degrees;


    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, degrees));
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Represents a caster that looks at the result of a two component vecotr.
/// </summary>
public class EyeCaster : RayCasterObject
{
    [Space(10),Header("Raycaster objects")]
    [SerializeField, Tooltip("The first raycaster object.")]
    private GameObject eyeOne;

    [SerializeField, Tooltip("The second raycaster object.")]
    private GameObject eyeTwo;

    void Start()
    {
        base.Start();
        CheckField("Eye one", eyeOne);
        CheckField("Eye two", eyeTwo);
    }

    /// <inheritdoc/>
    protected override Vector3 FindDirection() {
        return (eyeOne.transform.forward + eyeTwo.transform.forward) / 2;
    }

    ///<inheritdoc/>
    protected override Vector3 FindPosition() {
        return (eyeOne.transform.position + eyeTwo.transform.position) / 2;
    }

    /// <summary>
    /// Checks if the defined field is set in the editor.
    /// </summary>
    /// <param name="error">the type of error like "type of object"</param>
    /// <param name="fieldToCheck">The field to check</param>
    private bool CheckField(string error, object fieldToCheck)
    {
        bool valid = fieldToCheck == null;
        if (valid)
        {
            Debug.Log("<color=red>Error:</color>" + error + " must be set.", gameObject);
        }
        return valid;
    }
}

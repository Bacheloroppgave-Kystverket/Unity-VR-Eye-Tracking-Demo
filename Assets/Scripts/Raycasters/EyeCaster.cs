using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Represents a caster that looks at the result of a two component vecotr.
/// </summary>
 
public class EyeCaster : RayCasterObject
{
    [Space(10), Header("Raycaster objects")]
    [SerializeField, Tooltip("The first raycaster object.")]
    private OVREyeGaze eyeOne;

    [SerializeField, Tooltip("The second raycaster object.")]
    private OVREyeGaze eyeTwo;

    void Start()
    {
        base.Start();
        CheckField("Eye one", eyeOne);
        CheckField("Eye two", eyeTwo);
    }

    /// <summary>
    /// Activates the eyes if the user sets the headset as active.
    /// </summary>
    /// <param name="active">true if the OVREyegaze should be active. False otherwise</param>
    public void SetOVREyeGaze(bool active) {
        eyeOne.enabled = active;
        eyeTwo.enabled = active;
    }

    /// <summary>
    /// Sets the confidence threshold of the eyes.
    /// </summary>
    /// <param name="confidenceThreshold">the new confidence threshold</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the confidence threshold </exception>
    public void SetConfidenceThreshold(float confidenceThreshold) {
        if (confidenceThreshold < 0 || confidenceThreshold > 1) {
            throw new IllegalArgumentException("The confidence threshold must be between 0 and 1");
        }
        eyeOne.ConfidenceThreshold = confidenceThreshold;
        eyeTwo.ConfidenceThreshold = confidenceThreshold;
    }

    /// <inheritdoc/>
    public override Vector3 FindDirection() {
        return (eyeOne.transform.forward + eyeTwo.transform.forward) / 2;
    }

    ///<inheritdoc/>
    public override Vector3 FindPosition() {
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

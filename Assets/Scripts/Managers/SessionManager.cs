using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Represents a session that a user is doing each time they put on the headset.
/// </summary>
public class SessionManager : MonoBehaviour
{
    [SerializeField]
    private DateTime currentDate = DateTime.Now;

    [SerializeField]
    private string userID;

    [SerializeField]
    private float sessionTime;

    [SerializeField]
    private string locationId;

    [SerializeField]
    private RayCasterObject rayCasterObject;

    [Space(10), Header("Managers")]
    [SerializeField]
    private ReferencePositionManager referencePositionManager;

    [SerializeField]
    private TrackableObjectsManager trackableObjectsManager;

    // Start is called before the first frame update
    void Start() {

        CheckStringField("User ID", userID);
        CheckField("Reference position manager", referencePositionManager);
        CheckField("Trackable Objects Manager", trackableObjectsManager);
        CheckField("Raycaster object", rayCasterObject);
    }

    public void StartEyeTracking() {
        referencePositionManager.StartEyeTracking();
        rayCasterObject.StartTracking();
    }

    /// <summary>
    /// Checks if a string field is valid.
    /// </summary>
    /// <param name="error">the error</param>
    /// <param name="stringToCheck">the string to check</param>
    private void CheckStringField(string error,string stringToCheck) {
        if (!CheckField(error, stringToCheck) && userID.Trim() == "") {
            Debug.Log("<color=red>Error:</color>" + error + " cannot be empty for " + gameObject.name, gameObject);
        }
    }

    /// <summary>
    /// Checks if the defined field is set in the editor.
    /// </summary>
    /// <param name="error">the type of error like "type of object"</param>
    /// <param name="fieldToCheck">The field to check</param>
    private bool CheckField(string error, object fieldToCheck) {
        bool valid = fieldToCheck == null;
        if (valid) {
            Debug.Log("<color=red>Error:</color>" + error + " must be set.", gameObject);
        }
        return valid;
    }

    // Update is called once per frame
    void Update()
    {
        sessionTime += Time.deltaTime;
    }

    /// <summary>
    /// Gets the date and time.
    /// </summary>
    /// <returns>the date time</returns>
    public DateTime GetDateTime() => currentDate;
}

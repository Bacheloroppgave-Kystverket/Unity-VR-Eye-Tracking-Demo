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
    public List<ReferencePosition> referencePositions = new List<ReferencePosition>();

    [SerializeField]
    private List<TrackableObject> trackableObjects = new List<TrackableObject>();

    [SerializeField]
    private ReferencePosition currentPosition;

    [SerializeField]
    private GameObject playerObject;

    private int position = 0;

    /// <summary>
    /// Gets the current position.
    /// </summary>
    /// <returns>the current position</returns>
    public ReferencePosition GetCurrentReferencePosition() => currentPosition;

    // Start is called before the first frame update
    void Start(){
        
        trackableObjects = new List<TrackableObject>();
        /*
        referencePositions = new Dictionary<string, ReferencePosition>();
        GameObject[] referenceObjects = GameObject.FindGameObjectsWithTag("ReferencePosition");
        foreach (GameObject rfObject in referenceObjects) {
            ReferencePosition referencePosition = rfObject.GetComponent<ReferencePosition>();
            referencePositions.Add(referencePosition.GetLocationId(), referencePosition);
        }*/
        GameObject[] trackableObjs = GameObject.FindGameObjectsWithTag("TrackableObject");
        foreach (GameObject trackObject in trackableObjs) {
            trackableObjects.Add(trackObject.GetComponent<TrackableObject>());
        }
        CheckField("Player", playerObject);
        CheckStringField("User ID", userID);
        CheckIfListIsValid("Reference positions", referencePositions.Any());
        CheckIfListIsValid("Trackable objects", trackableObjects.Any());
    }

    /// <summary>
    /// Checks if the list has any elements.
    /// </summary>
    /// <param name="error">the error to display</param>
    /// <param name="hasAny">true if the list has any. False otherwise</param>
    private void CheckIfListIsValid(string error, bool hasAny) {
        if (!hasAny) {
            Debug.Log("<color=red>Error:</color>" + error + " cannot be emtpy.", gameObject);
        }
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
        referencePositions[position].AddTime();
        sessionTime += Time.deltaTime;
    }

    public void NextPosition() {
        position = (position + 1) % referencePositions.Count;
        currentPosition = referencePositions[position];
        playerObject.gameObject.transform.position = currentPosition.gameObject.transform.position;
    }

    /// <summary>
    /// Gets the date and time.
    /// </summary>
    /// <returns>the date time</returns>
    public DateTime GetDateTime() => currentDate;
}

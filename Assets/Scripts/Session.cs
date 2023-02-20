using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

/// <summary>
/// Represents a session that is done by a user. 
/// Should be placed on an object that represents the main location that has all the reference positions.
/// </summary>
public class Session : MonoBehaviour{

    [Header("Session configuration")]
    [SerializeField, Tooltip("The id of the session")]
    private string sessionID;

    [SerializeField, Tooltip("The date of the session")]
    private DateTime currentDate = DateTime.Now;

    [SerializeField, Tooltip("The id of the user")]
    private string userID;

    [SerializeField, Tooltip("The ray caster object")]
    private RayCasterObject rayCasterObject;

    [SerializeField, Tooltip("The list of all the trackable objects that are close to this user.")]
    private List<TrackableObject> closeTrackableObjects = new List<TrackableObject>();

    [SerializeField, Tooltip("The configuration for the objects")]
    private List<FeedbackConfiguration> feedbackConfigurations = new List<FeedbackConfiguration>();

    [SerializeField, Tooltip("The reference positions of this session.")]
    private List<ReferencePosition> referencePositions = new List<ReferencePosition>();

    [Space(10),Header("Other")]
    [SerializeField, Tooltip("The list of all objects that are far away and has been observed.")]
    private List<TrackableObject> otherTrackableObjects = new List<TrackableObject>();

    [SerializeField]
    private List<Feedback> feedbackLog = new List<Feedback>();

    /// <summary>
    /// Gets the raycaster object.
    /// </summary>
    /// <returns>the raycaster object</returns>
    public RayCasterObject GetRayCasterObject() => rayCasterObject;

    private void Start(){
        closeTrackableObjects = GetComponentsInChildren<TrackableObject>().ToList();
        referencePositions = GetComponentsInChildren<ReferencePosition>().ToList();
        CheckIfListIsValid("Feedback configurations", feedbackConfigurations.Any());
        CheckIfListIsValid("Close trackable objects", closeTrackableObjects.Any());
        CheckIfListIsValid("Reference positions", referencePositions.Any());
        CheckField("Ray caster object", rayCasterObject);
    }

    /// <summary>
    /// Gets the reference positions.
    /// </summary>
    /// <returns>the reference positions</returns>
    public List<ReferencePosition> GetReferencePositions() => referencePositions;

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

    /// <summary>
    /// Checks if the list has any elements.
    /// </summary>
    /// <param name="error">the error to display</param>
    /// <param name="hasAny">true if the list has any. False otherwise</param>
    private void CheckIfListIsValid(string error, bool hasAny)
    {
        if (!hasAny)
        {
            Debug.Log("<color=red>Error:</color>" + error + " cannot be emtpy", gameObject);
        }
    }

    /// <summary>
    /// Gets the date and time.
    /// </summary>
    /// <returns>the date time</returns>
    public DateTime GetDateTime() => currentDate;


    /// <summary>
    /// Adds the other objects to the other objects collection if they are not in the close trackable objects list.
    /// </summary>
    /// <param name="otherObjects">the list with the other objects</param>
    public void AddOtherObjects(List<TrackableObject> otherObjects) {
        foreach (TrackableObject otherObject in otherObjects) {
            if (!closeTrackableObjects.Contains(otherObject) && !otherObjects.Contains(otherObject)){
                otherObjects.Add(otherObject);
            }
        }
    }

    /// <summary>
    /// Adds feedback to the log.
    /// </summary>
    /// <param name="feedback">the feedback to add</param>
    public void AddFeedback(Feedback feedback) {
        if (!feedbackLog.Contains(feedback)) {
            feedbackLog.Add(feedback);
        }
    }

    /// <summary>
    /// Gets all the trackable objects.
    /// </summary>
    /// <returns>the trackable objects that are close</returns>
    public List<TrackableObject> GetCloseTrackableObjects() => closeTrackableObjects;
}

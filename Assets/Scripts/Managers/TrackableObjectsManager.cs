using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manages all the trackable objects in the simulation.
/// </summary>
public class TrackableObjectsManager : MonoBehaviour{

    [SerializeField, Tooltip("This list will populate itself with all the objects once the session starts")]
    private List<TrackableObjectController> trackableObjects = new List<TrackableObjectController>();

    [SerializeField, Tooltip("The point of interest controllers")]
    private List<PointOfInterestController> pointOfInterestControllers = new List<PointOfInterestController>();

    [Space(10), Header("Managers")]
    [SerializeField, Tooltip("The session manager")]
    private SessionManager sessionManager;

    [SerializeField, Tooltip("The reference position manager")]
    private ReferencePositionManager referencePositionManager;

    [SerializeField, Tooltip("The point placer")]
    private PointPlacerController pointPlacer;

    
    private void Awake(){
        trackableObjects = GameObject.FindObjectsOfType<TrackableObjectController>().ToList();
        CheckIfListIsValid("Trackable objects", trackableObjects.Any());
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
            Debug.Log("<color=red>Error:</color>" + error + " cannot be emtpy.", gameObject);
        }
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

    /// <summary>
    /// Updates all the trackable objects to the new position.
    /// </summary>
    /// <param name="referencePosition">the new reference position</param>
    public void UpdatePositionOnAllTrackableObjects(ReferencePosition referencePosition) {
        trackableObjects.ForEach(trackableObject => trackableObject.SetPosition(referencePosition));
    }

    /// <summary>
    /// Calculates the average fixation time per object.
    /// </summary>
    public void CalculateAverageFixationTimePerObjectForPosition()
    {
        trackableObjects.ForEach(trackableObject => trackableObject.CalculateCurrentAverageFixationDuration(referencePositionManager.GetCurrentReferencePosition().GetLocationName()));
    }

    public void ShowAllPointsOfInterest() {
        pointPlacer.ShowPointsOfInterest();
    }
}

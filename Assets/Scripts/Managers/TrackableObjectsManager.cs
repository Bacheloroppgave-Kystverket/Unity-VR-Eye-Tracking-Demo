using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manages all the trackable objects in the simulation.
/// </summary>
public class TrackableObjectsManager : MonoBehaviour
{
    [SerializeField, Tooltip("This list will populate itself with all the objects once the session starts")]
    private List<TrackableObject> trackableObjects = new List<TrackableObject>();

    [Space(10), Header("Managers")]
    [SerializeField]
    private ReferencePositionManager referencePositionManager;

    // Start is called before the first frame update
    void Start()
    {
        trackableObjects = new List<TrackableObject>();
        GameObject[] trackableObjs = GameObject.FindGameObjectsWithTag(typeof(TrackableObject).Name);
        MonoBehaviour.print(trackableObjs.Count());
        MonoBehaviour.print(typeof(TrackableObject).Name);
        foreach (GameObject trackObject in trackableObjs)
        {
            trackableObjects.Add(trackObject.GetComponent<TrackableObject>());
        }
        CheckIfListIsValid("Trackable objects", trackableObjects.Any());
        CheckField("Reference position manager", referencePositionManager);
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
    /// <param name="locationID">the new location id</param>
    public void UpdatePositionOnAllTrackableObjects(string locationID) {
        trackableObjects.ForEach(trackableObject => trackableObject.SetPosition(locationID));
    }

    /// <summary>
    /// Calculates the average fixation time per object.
    /// </summary>
    public void CalculateAverageFixationTimePerObjectForPosition()
    {
        trackableObjects.ForEach(trackableObject => trackableObject.CalculateCurrentAverageFixationDuration(referencePositionManager.GetCurrentReferencePosition().GetLocationId()));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

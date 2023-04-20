using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Session
{
    [Header("Session configuration")]
    [SerializeField, Tooltip("The id of the session")]
    private string sessionID;

    [SerializeField, Tooltip("The date of the session")]
    private DateTime currentDate = DateTime.Now.Date;

    [SerializeField, Tooltip("The user that is doing this session.")]
    private User user;
    
    [SerializeField, Tooltip("List of all the trackable objects and for how long they have been watched.")]
    private List<TrackableRecord> trackableRecords = new List<TrackableRecord>();

    [SerializeField, Tooltip("The list of all the positions and the time spent there.")]
    private List<PositionRecord> positionRecords = new List<PositionRecord>();

    [SerializeField, Tooltip("The simulation setup.")]
    private SimulationSetup simulationSetup;

    public void ClearLists() { 
        trackableRecords.Clear();
        positionRecords.Clear();
    }

    /// <summary>
    /// Adds the trackable object
    /// </summary>
    /// <param name="trackableObjectsController">the trackable objects</param>
    public void AddTrackableObject(TrackableObjectController trackableObjectsController, ViewDistance viewDistance) {
        //Todo: Here the data from the DB should be loaded.
        if (trackableObjectsController != null && viewDistance == ViewDistance.CLOSE && trackableRecords.Find(record => record.GetTrackableObject() == trackableObjectsController.GetTrackableObject()) == null){
            trackableRecords.Add(trackableObjectsController.GetTrackableRecord());
        }
    }

    /// <summary>
    /// Adds an list of reference positions to this sessison.
    /// </summary>
    /// <param name="referencePosition">the positions</param>
    public void AddReferencePosition(ReferencePositionController referencePositionController)
    {
        if (referencePositionController != null && positionRecords.Find(record => record.GetReferencePosition() == referencePositionController.GetReferencePosition()) == null)
        {
            positionRecords.Add(referencePositionController.GetPositionRecord());
        }
    }

    /// <summary>
    /// Sets the simulation settup.
    /// </summary>
    /// <param name="simulationSetup">the new simulation setup.</param>
    public void SetSimulationSetup(SimulationSetup simulationSetup) {
        CheckIfObjectIsNull(simulationSetup, "simulation setup");
        this.simulationSetup = simulationSetup;
    }

    

    /// <summary>
    /// Gets the date and time.
    /// </summary>
    /// <returns>the date time</returns>
    public DateTime GetDateTime() => currentDate;

    /// <summary>
    /// Gets the trackable objects.
    /// </summary>
    /// <returns></returns>
    public List<TrackableRecord> GetTrackableRecords() => trackableRecords;


    /// <summary>
    /// Adds adaptiveFeedback to the log.
    /// </summary>
    /// <param name="referencePosition">the reference position</param>
    /// <exception cref="InvalidOperationException">gets thrown if the reference position is not a part of this session</exception>
    public PositionRecord GetReferenceRecording(ReferencePosition referencePosition)
    {
        CheckIfObjectIsNull(referencePosition, "reference position");
        PositionRecord record = positionRecords.Find(record => record.GetReferencePosition() == referencePosition);
        if (record == null) {
            throw new InvalidOperationException("There is no reference position like that in this session");
        }
        return record;
    }

    /// <summary>
    /// Checks if the object is null or not. Throws an exception if the object is null.
    /// </summary>
    /// <param name="objecToCheck">the object to check</param>
    /// <param name="error">the error to be in the string.</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the object to check is null.</exception>
    private void CheckIfObjectIsNull(object objecToCheck, string error)
    {
        if (objecToCheck == null)
        {
            throw new IllegalArgumentException("The " + error + " cannot be null.");
        }
    }
}

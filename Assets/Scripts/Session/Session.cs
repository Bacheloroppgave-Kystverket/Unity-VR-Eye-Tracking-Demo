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
    private DateTime currentDate = DateTime.Now;

    [SerializeField, Tooltip("The user that is doing this session.")]
    private User user;
    
    [SerializeField, Tooltip("List of all the trackable objects and for how long they have been watched.")]
    private List<TrackableRecord> trackableRecords = new List<TrackableRecord>();

    [SerializeField, Tooltip("The list of all the positions and the time spent there.")]
    private List<PositionRecord> positionRecords = new List<PositionRecord>();

    /// <summary>
    /// Adds all the trackable objects to this session.
    /// </summary>
    /// <param name="trackableObjects">the trackable objects</param>
    public void AddTrackableObjects(List<TrackableObject> trackableObjects, ViewDistance viewDistance) {
        if (trackableObjects != null){
            trackableObjects.ForEach(trackableObject => {
                if (!trackableRecords.Any(record => record.GetTrackableObject() == trackableObject)) {
                    TrackableRecordBuilder trackableRecordBuilder = new TrackableRecordBuilder(trackableObject);
                    trackableRecordBuilder = viewDistance == ViewDistance.CLOSE ? trackableRecordBuilder.SetAsCloseObject() : trackableRecordBuilder.SetAsFarObject();
                    this.trackableRecords.Add(trackableRecordBuilder.build());
                }
            });
        }
    }

    /// <summary>
    /// Adds an list of reference positions to this sessison.
    /// </summary>
    /// <param name="referencePositions">the positions</param>
    public void AddReferencePositions(List<ReferencePosition> referencePositions) {
        if(referencePositions != null)
        {
            referencePositions.ForEach(referencePosition =>
            {
                if (!referencePositions.Contains(referencePosition))
                {
                    this.positionRecords.Add(new PositionRecord(referencePosition));
                }
            });
        }
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

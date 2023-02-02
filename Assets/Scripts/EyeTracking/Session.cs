using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a session that a user is doing each time they put on the headset.
/// </summary>
public class Session : MonoBehaviour
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
    private Dictionary<string, ReferencePosition> referencePositions;

    [SerializeField]
    private List<TrackableObject> trackableObjects;

    // Start is called before the first frame update
    void Start(){
        referencePositions = new Dictionary<string, ReferencePosition>();
        trackableObjects = new List<TrackableObject>();
        GameObject[] referenceObjects = GameObject.FindGameObjectsWithTag("ReferencePosition");
        foreach (GameObject rfObject in referenceObjects) {
            ReferencePosition referencePosition = rfObject.GetComponent<ReferencePosition>();
            referencePositions.Add(referencePosition.GetLocationId(), referencePosition);
        }
        GameObject[] trackableObjs = GameObject.FindGameObjectsWithTag("ReferencePosition");
        foreach (GameObject trackObject in trackableObjs) {
            trackableObjects.Add(trackObject.GetComponent<TrackableObject>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        referencePositions[locationId].AddTime();
        sessionTime += Time.deltaTime;
    }

    /// <summary>
    /// Gets the date and time.
    /// </summary>
    /// <returns>the date time</returns>
    public DateTime GetDateTime() => currentDate;
}

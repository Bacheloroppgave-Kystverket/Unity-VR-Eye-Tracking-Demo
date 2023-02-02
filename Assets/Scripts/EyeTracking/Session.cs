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
    public List<ReferencePosition> referencePositions = new List<ReferencePosition>();

    [SerializeField]
    private List<TrackableObject> trackableObjects = new List<TrackableObject>();

    [SerializeField]
    private ReferencePosition currentPosition;

    [SerializeField]
    private GameObject player;

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
        player.gameObject.transform.position = currentPosition.gameObject.transform.position;
    }

    /// <summary>
    /// Gets the date and time.
    /// </summary>
    /// <returns>the date time</returns>
    public DateTime GetDateTime() => currentDate;
}

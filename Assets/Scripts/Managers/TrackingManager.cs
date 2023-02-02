using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingManager : MonoBehaviour
{

    [SerializeField]
    private List<TrackableObject> trackableObjects;

    [SerializeField]
    private float time;

    [SerializeField]
    private Session session;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    /// <summary>
    /// Calculates the average fixation time per object.
    /// </summary>
    public void CalculateAverageFixationTimePerObject() {
        trackableObjects.ForEach(trackableObject => trackableObject.CalculateCurrentAverageFixationDuration(session.GetCurrentReferencePosition().GetLocationId()));
    }
}

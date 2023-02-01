using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingManager : MonoBehaviour
{

    [SerializeField]
    private List<TrackableObject> trackableObjects;

    [SerializeField]
    private float time;

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

    public void CalculateAverageFixationTimePerObject() {
        trackableObjects.ForEach(trackableObject => trackableObject.CalculateAverageFixationTime());
    }
}

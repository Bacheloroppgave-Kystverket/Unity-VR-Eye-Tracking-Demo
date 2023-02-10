using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a trackable object.
/// </summary>
public class TrackableObject : MonoBehaviour, Observable<TrackableObserver>
{
    [SerializeField]
    private string nameOfObject;

    [SerializeField]
    private bool changeColor = true;

    [SerializeField]
    private GazeData currentGaze = null;

    private Dictionary<string, GazeData> gazeMap = new Dictionary<string, GazeData>();

    [SerializeField]
    private List<TrackableObserver> observers = new List<TrackableObserver>();

    [SerializeField]
    private TrackableTypes typeOfObject = TrackableTypes.UNDEFINED;


    // Start is called before the first frame update
    void Start()
    {
        nameOfObject = gameObject.name;
        currentGaze = null;
        gameObject.tag = "TrackableObject";
        gazeMap = new Dictionary<string, GazeData>();
        if (typeOfObject == TrackableTypes.UNDEFINED) {
            Debug.Log("<color=red>Error:</color>" + "Type of object must be defined for " + gameObject.name, gameObject);
        }
        
    }

    // Update is called once per frame
    void Update(){
        if (currentGaze != null) {
            currentGaze.AddTime();
            UpdateObserversFixationDuration();
        }
    }

    

    /// <summary>
    /// Gets the current gaze data.
    /// </summary>
    /// <returns>the current gaze data</returns>
    public GazeData GetCurrentGazeData() {
        return currentGaze;
    }


    /// <summary>
    /// Sets the new location of the object.
    /// </summary>
    /// <param name="locationID">the id of the location</param>
    public void SetBeingWatched(string locationID) {
        UpdateCurrentGazeData(locationID);
    }

    public void SetNotWatched() {
        currentGaze = null;
    }

    
    /// <summary>
    /// Updates the current gaze data to whats being watched.
    /// </summary>
    /// <param name="currentLocationId">the current locations id</param>
    private void UpdateCurrentGazeData(string currentLocationId) {
        if (currentLocationId != null && currentLocationId != "" && gazeMap.ContainsKey(currentLocationId)) {
            currentGaze = gazeMap[currentLocationId];
            currentGaze.IncrementFixation();
            UpdateObserversFixations();
           
        }else if (currentLocationId != null && currentLocationId != "") {
            currentGaze = new GazeData(currentLocationId);
            gazeMap.Add(currentLocationId, currentGaze);
            currentGaze.IncrementFixation();
            UpdateObserversFixations();
        } else {
            currentGaze = null;
        }

    }

    /// <summary>
    /// Checks if this object is being watched.
    /// </summary>
    /// <returns>true if the object is being watched</returns>
    public bool IsWatched() {
        return currentGaze != null;
    }

    public void FixedUpdate()
    {
        if (changeColor) {
            if (currentGaze != null)
            {
                gameObject.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
            }
        }
    }

    /// <summary>
    /// Calculates the average fixation duration of the current gaze location.
    /// </summary>
    /// <returns>the avereage fixation duration</returns>
    public float CalculateCurrentAverageFixationDuration(string locationID) {
        float averageFixationDuration = currentGaze.GetFixationDuration() / currentGaze.GetFixations();
        UpdateObserversAverageFixationDuration(averageFixationDuration);
        return averageFixationDuration;
    }

    /// </inheritdoc>
    public void AddObserver(TrackableObserver observer)
    {
        observers.Add(observer);
    }

    /// </inheritdoc>
    public void RemoveObserver(TrackableObserver observer)
    {
        observers.Remove(observer);
    }

    /// <summary>
    /// Updates the fixations.
    /// </summary>
    private void UpdateObserversFixations()
    {
        observers.ForEach(observer => observer.UpdateFixations(currentGaze.GetFixations()));
    }

    /// <summary>
    /// Updates the fixation durations.
    /// </summary>
    private void UpdateObserversFixationDuration(){
        observers.ForEach(observer => observer.UpdateFixationDuration(currentGaze.GetFixationDuration()));
    }

    /// <summary>
    /// Updates the average fixation duration.
    /// </summary>
    /// <param name="averageFixation">the average fixation</param>
    private void UpdateObserversAverageFixationDuration(float averageFixation)
    {
        observers.ForEach(observer => observer.UpdateAverageFixationDuration(averageFixation));
    }
}

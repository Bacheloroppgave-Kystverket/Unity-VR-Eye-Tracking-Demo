using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Represents a trackable object.
/// </summary>
public class TrackableObjectController : MonoBehaviour, Observable<TrackableObserver>
{
    [SerializeField, Tooltip("The trackable object")]
    private TrackableObject trackableObject;

    [SerializeField, Tooltip("Set to true if the object is supposed to change color")]
    private bool changeColor = true;

    [Space(10), Header("Debug fields")]
    [SerializeField, Tooltip("The current object that is being watched.")]
    private GazeData currentGaze = null;

    [SerializeField, Tooltip("Is set to true when its being watched")]
    private bool beingWatched;    

    [SerializeField, Tooltip("The observers")]
    private List<TrackableObserver> observers = new List<TrackableObserver>();

    // Start is called before the first frame update
    void Awake()
    {
        trackableObject.SetGameObjectName(gameObject.name);
        currentGaze = null;
        gameObject.tag = typeof(TrackableObjectController).Name;
        gameObject.layer = (int)PrefixLayer.Eyetracking;
        if (trackableObject.GetTrackableType() == TrackableType.UNDEFINED) {
            Debug.Log("<color=red>Error:</color>" + "Type of object must be defined for " + gameObject.name, gameObject);
        }
        CheckField("Object to track", gameObject);
        
    }

    /// <summary>
    /// Checks if the defined field is set in the editor.
    /// </summary>
    /// <param name="error">the type of error like "type of object"</param>
    /// <param name="fieldToCheck">The field to check</param>
    private bool CheckField(string error, object fieldToCheck)
    {
        bool valid = fieldToCheck == null;
        if (valid){
            Debug.Log("<color=red>Error:</color>" + error + " must be set.", gameObject);
        }
        return valid;
    }

    // Update is called once per frame
    void Update() {
        if (beingWatched) {
            currentGaze.AddTime();
            UpdateObserversFixationDuration();
        }
    }

    /// <summary>
    /// Gets the trackable object type
    /// </summary>
    /// <returns>the trackable object type</returns>
    public TrackableType GetTrackableType() {
        return trackableObject.GetTrackableType();
    }



    /// <summary>
    /// Gets the current gaze data.
    /// </summary>
    /// <returns>the current gaze data</returns>
    public GazeData GetCurrentGazeData() {
        return currentGaze;
    }

    /// <summary>
    /// Gets the name of the object
    /// </summary>
    /// <returns>the name of the object</returns>
    public string GetNameOfObject() { 
        return trackableObject.GetNameOfObject();
    }

    /// <summary>
    /// Gets the gaze data that has that location id.
    /// </summary>
    /// <param name="locationID">the location id</param>
    /// <returns>the gaze data that matches that location id. Is null if location does not exsist</returns>
    public GazeData GetGazeDataForPosition(string locationID) {
        return trackableObject.GetGazeDataForLocation(locationID);
    }

    /// <summary>
    /// Sets the new position that this trackable object should watch.
    /// </summary>
    /// <param name="locationID">the new location ID</param>
    public void SetPosition(string locationID) {
        if (locationID != null && locationID != "")
        {
            currentGaze = trackableObject.GetGazeDataForLocation(locationID);

            if (beingWatched && currentGaze != null) {
                currentGaze.IncrementFixation();
            }
            UpdateObserversFixationDuration();
            UpdateObserversFixations();
        }
        else {
            beingWatched = false;
            currentGaze = null;
        }
    }

    /// <summary>
    /// Sets the new location of the object.
    /// </summary>
    public void SetBeingWatched() {
        if (!beingWatched) {
            beingWatched = true;
            currentGaze.IncrementFixation();
            UpdateObserversFixations();
            if (changeColor)
            {
                gameObject.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
            }
        }
    }

    /// <summary>
    /// Sets the object to not be watched.
    /// </summary>
    public void SetNotWatched() {
        beingWatched = false;
        if (changeColor) {
            gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
        }
    }

    /// <summary>
    /// Checks if this object is being watched.
    /// </summary>
    /// <returns>true if the object is being watched</returns>
    public bool IsWatched() {
        return beingWatched;
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

    /// <summary>
    /// Gets the trackable object.
    /// </summary>
    /// <returns>the trackable object</returns>
    public TrackableObject GetTrackableObject() => trackableObject;

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

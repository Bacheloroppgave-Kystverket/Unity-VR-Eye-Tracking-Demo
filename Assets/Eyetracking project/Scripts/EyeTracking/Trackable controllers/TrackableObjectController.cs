using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Represents a trackable object.
/// </summary>
public class TrackableObjectController : MonoBehaviour, Observable<TrackableObserver>, Trackable
{
    [Header("Configure object")]
    [SerializeField, Tooltip("Set to true if the object is supposed to change color")]
    private bool changeColor = false;

    [Header("Gameobject data")]
    [SerializeField, Tooltip("The trackable object")]
    private TrackableObject trackableObject;

    [SerializeField, Tooltip("The trackable record")]
    private TrackableRecord trackableRecord;

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
        this.trackableRecord = new TrackableRecordBuilder(trackableObject).build();
        currentGaze = null;
        gameObject.tag = typeof(TrackableObjectController).Name;
        gameObject.layer = (int)PrefixLayer.Eyetracking;
        if (trackableObject.GetTrackableType() == TrackableType.UNDEFINED) {
            Debug.Log("<color=red>Error:</color>" + "Type of object must be defined for " + gameObject.name, gameObject);
        }
        CheckField("Object to track", gameObject);
        CheckField("Trackable object name", trackableObject.GetNameOfObject());
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
    public GazeData GetGazeDataForPosition(ReferencePosition referencePosition) {
        CheckIfObjectIsNull(referencePosition, "reference position");
        return trackableRecord.GetGazeDataForPosition(referencePosition);
    }

    /// <summary>
    /// Sets the new position that this trackable object should watch.
    /// </summary>
    /// <param name="referencePosition">the reference position</param>
    public void SetPosition(ReferencePosition referencePosition) {
        CheckIfObjectIsNull(referencePosition, "reference position");
        currentGaze = trackableRecord.GetGazeDataForPosition(referencePosition);
        if (currentGaze != null)
        {
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
    private void SetBeingWatched() {
        if (!beingWatched) {
            beingWatched = true;
            currentGaze.IncrementFixation();
            UpdateObserversFixations();
            if (changeColor)
            {
                Renderer renderer = gameObject.GetComponent<Renderer>();
                if (renderer != null) { 
                    renderer.material.color = new Color(0, 255, 0);
                }
                
            }
        }
    }

    /// <summary>
    /// Sets the object to not be watched.
    /// </summary>
    private void SetNotWatched() {
        beingWatched = false;
        if (changeColor) {
            Renderer renderer = gameObject.GetComponent<Renderer>();
            if (renderer != null) { 
                renderer.material.color = new Color(0, 0, 0);
            }
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

    /// <summary>
    /// Gets the trackable record.
    /// </summary>
    /// <returns>the trackable record</returns>
    public TrackableRecord GetTrackableRecord() => trackableRecord;

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

    /// <summary>
    /// Adds the trackable object to the register.
    /// </summary>
    /// <param name="simulationSetupManager">the simulation setup manager</param>
    private void AddTrackableToSimulationSetup(SimulationSetupManager simulationSetupManager)
    {
        CheckIfObjectIsNull(simulationSetupManager, "simulation setup manager");
        simulationSetupManager.AddTrackableObject(this.GetComponent<TrackableObjectController>());
    }

    /// <summary>
    /// Checks if the string is null or empty. Throws exceptions if one of these conditions are true.
    /// </summary>
    /// <param name="stringToCheck">the string to check</param>
    /// <param name="error">the error of the string</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the string to check is empty or null.</exception>
    private void CheckIfStringIsValid(string stringToCheck, string error)
    {
        CheckIfObjectIsNull(stringToCheck, error);
        if (stringToCheck.Trim().Length == 0)
        {
            throw new IllegalArgumentException("The" + error + " cannot be empty.");
        }
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

    /// <summary>
    /// Checks if the defined field is set in the editor.
    /// </summary>
    /// <param name="error">the type of error like "type of object"</param>
    /// <param name="fieldToCheck">The field to check</param>
    private bool CheckField(string error, object fieldToCheck)
    {
        bool valid = fieldToCheck == null;
        if (fieldToCheck is string)
        {
            string value = (string)fieldToCheck;
            valid = value.Length == 0;
        }
        if (valid)
        {
            Debug.Log("<color=red>Error:</color>" + error + " must be set.", gameObject);
        }
        return valid;
    }

    ///<inheritdoc/>
    public void OnGazeEnter()
    {
        SetBeingWatched();
    }

    ///<inheritdoc/>
    public void OnGazeExit()
    {
        SetNotWatched();
    }

    


}

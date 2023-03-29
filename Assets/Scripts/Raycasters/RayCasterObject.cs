using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// Represents a object that can cast rays. 
/// </summary>
public abstract class RayCasterObject : MonoBehaviour, Observable<RaycasterObserver>
{
    [Header("Configure raycaster")]
    [SerializeField, Tooltip("True if the object should be 'casting' rays")]
    private bool casting = false;

    [SerializeField, Tooltip("True if you are going to observe mutliple objects")]
    private bool shootMutliple = false;

    [SerializeField, Tooltip("The frequency to gather data for the raycaster. Quest pro has a rate of 30 hz in the forums.")]
    private int frequency = 30;

    [SerializeField, Range(1, 100), Tooltip("The range that the ray should be shot")]
    private int range = 50;

    [SerializeField, Tooltip("The raycaster configuration of this raycater object")]
    private RaycasterConfiguration raycasterConfiguration = new NormalRaycastConfig();

    [Space(5), Header("Debugging lists")]
    [SerializeField]
    private List<TrackableObjectController> currentObjectsWatched;

    [SerializeField, Tooltip("The last objects that was looked at.")]
    private List<TrackableObjectController> lastObjects;

    [SerializeField, Tooltip("The raycast observers")]
    private List<RaycasterObserver> raycasterObservers = new List<RaycasterObserver>();

    protected void Start() {
        currentObjectsWatched = new List<TrackableObjectController>();
    }

    /// <summary>
    /// Sets if the raygun shoud cast.
    /// </summary>
    /// <param name="isCasting">true if it should cast</param>
    public void SetIsCasting(bool isCasting) {
        this.casting = isCasting;
    }

    /// <summary>
    /// Checks if this object is casting.
    /// </summary>
    /// <returns>true if the object is casting. False otherwise.</returns>
    public bool IsCasting()
    {
        return casting;
    }

    /// <summary>
    /// Gets the frequency.
    /// </summary>
    /// <returns>the frequency</returns>
    public int GetFrequency() => frequency;

    /// <summary>
    /// Gets the position to shoot the ray from.
    /// </summary>
    /// <returns>the position</returns>
    public abstract Vector3 FindPosition();

    public abstract Vector3 FindDirection();

    /// <summary>
    /// Starts the eye tracking in the application. 
    /// </summary>
    /// <returns>the time to wait</returns>
    private IEnumerator StartEyeTracking() {
        RaycastHit[] raycastHits;
        Vector3 direction;
        Vector3 position;
        while (casting) {
            float timeToWait = 1f / frequency;
            //Makes ray
            
            direction = FindDirection();
            position = FindPosition();
            raycastHits = shootMutliple ? ShootMultipleObjects(position, direction) : ShootSingleObject(position, direction);
            bool hitSolid = false;
            if (raycastHits.Any()) {
                IEnumerator<RaycastHit> it = raycastHits.Reverse().GetEnumerator();
                while (it.MoveNext() && !hitSolid) {
                    hitSolid = WatchObject(it.Current);
                }
                UnwatchObjects(currentObjectsWatched);
            }
            else
            {
                UnwatchObjects();
            }
            currentObjectsWatched.Clear();
            UpdateObservers(raycastHits);
            yield return new WaitForSeconds(timeToWait);
        }
        MonoBehaviour.print("Eyetracking has stopped.");
    }

    /// <summary>
    /// Watches the object that was hit if its not null.
    /// </summary>
    /// <param name="raycastHit">the raycast hit</param>
    /// <returns>true if the object is solid. False if the object is not solid</returns>
    private bool WatchObject(RaycastHit raycastHit) {
        bool isSolid = false;
        TrackableObjectController trackObject = raycastHit.collider.gameObject.GetComponent<TrackableObjectController>();
        if (trackObject != null) {
            ObserveObject(trackObject);
            isSolid = TrackableTypeMethods.IsTrackableSolid(trackObject.GetTrackableObject().GetTrackableType());

        }
        return isSolid;
    }

    /// <summary>
    /// Shoots multiple objects in a line.
    /// </summary>
    /// <param name="position">the starting position</param>
    /// <param name="direction">the direction</param>
    /// <returns>gets the raycast hits</returns>
    private RaycastHit[] ShootMultipleObjects(Vector3 position, Vector3 direction) {
        //Shoots ray
        return raycasterConfiguration.ShootMultipleObjectsConfiguration(position, direction, range);
    }

    /// <summary>
    /// Shoots a single object with the raycast.
    /// </summary>
    /// <param name="position">the starting position</param>
    /// <param name="direction">the direction</param>
    /// <returns></returns>
    private RaycastHit[] ShootSingleObject(Vector3 position, Vector3 direction) {
        //Shoots ray
        return raycasterConfiguration.ShootSingleConfiguration(position, direction, range);
    }

    /// <summary>
    /// Unwatches an object based on if its in the list or not.
    /// </summary>
    /// <param name="watchedObjects">a list with the newly watched objects</param>
    private void UnwatchObjects(List<TrackableObjectController> watchedObjects = null) {
        if (lastObjects.Any()) {
            List<TrackableObjectController> objectsToRemove = new List<TrackableObjectController>();
            if (watchedObjects != null && watchedObjects.Count > 0) {
                lastObjects.ForEach(trackObject => {
                    if (!watchedObjects.Exists(watchObject => trackObject.GetInstanceID() == watchObject.GetInstanceID())) {
                        objectsToRemove.Add(trackObject);
                    }
                });
            } else {
                objectsToRemove.AddRange(lastObjects);
            }
            objectsToRemove.ForEach(trackedObject => {
                trackedObject.SetNotWatched();
            });

            objectsToRemove.ForEach(trackObject => lastObjects.Remove(trackObject));
        }
    }

    /// <summary>
    /// Checks if the object is watched in the list already.
    /// </summary>
    /// <param name="objectToFind">the object to check against.</param>
    /// <returns>true if the object is watched. False otherwise</returns>
    private bool CheckIfObjectIsWatched(TrackableObjectController objectToFind) {
        return lastObjects.Exists(trackableObject => GameObject.ReferenceEquals(trackableObject, objectToFind));
    }

    /// <summary>
    /// Observes an object.
    /// </summary>
    /// <param name="trackObject">the trackable object</param>
    private void ObserveObject(TrackableObjectController trackObject) {

        if (!CheckIfObjectIsWatched(trackObject)) {
            trackObject.SetBeingWatched();
            lastObjects.Add(trackObject);
        }
        currentObjectsWatched.Add(trackObject);
    }

    

    /// <inheritdoc/>
    public void StartTracking() {
        if (!casting) {
            casting = true;
            MonoBehaviour.print("Starting eye tracking");
            StartCoroutine(StartEyeTracking());
        }
    }

    /// <inheritdoc/>
    public void StopEyeTracking()
    {
        casting = false;
        UnwatchObjects();
    }

    /// <summary>
    /// Gets the current objects watched.
    /// </summary>
    /// <returns>A list with all the current objects.</returns>
    public List<TrackableObjectController> GetCurrentObjectsWatched() {
        return currentObjectsWatched;
    }

    /// <summary>
    /// Gets the last objects watched.
    /// </summary>
    /// <returns>the last objects</returns>
    public List<TrackableObjectController> GetLastObjects() {
        return lastObjects;
    }

    public void SetProperties(int frequency, float sphereSize, int range) {
        CheckIfNumberIsAboveZero(frequency, "frequency");
        CheckIfNumberIsAboveZeroAndUnderN(sphereSize, "sphere size", 0.5f);
        CheckIfNumberIsAboveZeroAndUnderN(range, "range", 100);
    }

    /// <summary>
    /// Checks if the number is above zero and under N.
    /// </summary>
    /// <param name="number">the number to check</param>
    /// <param name="error">the value as a prefix</param>
    /// <param name="maxValue">the max amount</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the number is negative or above the max limit.</exception>
    private void CheckIfNumberIsAboveZeroAndUnderN(float number, string error, float maxValue) {
        CheckIfNumberIsAboveZero(number, error);
        if (number > maxValue) {
            throw new IllegalArgumentException("Expected the " + error + " to be larger than " + maxValue + ".");
        }
    }

    /// <summary>
    /// Checks if a number is above zero.
    /// </summary>
    /// <param name="number">the number to check.</param>
    /// <param name="error">the error prefix.</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the number is negative.</exception>
    private void CheckIfNumberIsAboveZero(float number, string error) {
        if (number < 0) {
            throw new IllegalArgumentException("The " + error + " must be above zero.");
        }
    }

    /// <inheritdoc/>
    public void AddObserver(RaycasterObserver observer)
    {
        CheckIfObjectIsNull(observer, "observer");
        if (!raycasterObservers.Contains(observer)) {
            raycasterObservers.Add(observer);
        }
        else {
            throw new InvalidOperationException("The observer cannot be added since its already an observer.");
        }
    }

    /// <summary>
    /// Updates the observers with the new raycast hits.
    /// </summary>
    /// <param name="raycastHits">the raycast hits</param>
    public void UpdateObservers(RaycastHit[] raycastHits) {
        raycasterObservers.ForEach(observer => observer.ObservedObjects(raycastHits));
    }

    /// <inheritdoc/>
    public void RemoveObserver(RaycasterObserver observer)
    {
        CheckIfObjectIsNull(observer, "observer");
        if (raycasterObservers.Contains(observer))
        {
            raycasterObservers.Remove(observer);
        }
        else {
            throw new InvalidOperationException("The observer is not in the list of observers");
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
}

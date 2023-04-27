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

    [SerializeField, Tooltip("The max distance of the position to change vectors.")]
    private float maxDistance = 0.03f;

    [SerializeField, Tooltip("The raycaster configuration of this raycater object")]
    private RaycasterConfiguration raycasterConfiguration = new SphereCastConfig();

    [Space(5), Header("Debugging lists")]
    [SerializeField]
    private List<GameObject> currentObjectsWatched;

    [SerializeField, Tooltip("The last objects that was looked at.")]
    private List<GameObject> lastObjects;

    [SerializeField]
    private List<GameObject> debugHits = new List<GameObject>();

    [SerializeField, Tooltip("The raycast observers")]
    private List<RaycasterObserver> raycasterObservers = new List<RaycasterObserver>();

    [SerializeField, Tooltip("The old area that the eyes where looking at")]
    private Vector3 oldArea;

    protected void Start() {
        currentObjectsWatched = new List<GameObject>();
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
        List<RaycastHit> newRaycasts = new List<RaycastHit>();
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
                IEnumerator<RaycastHit> it = raycastHits.ToList().GetEnumerator();
                while (it.MoveNext() && !hitSolid) {
                    RaycastHit hit = it.Current;
                    if (hit.collider != null) {
                        hitSolid = WatchObject(hit);
                        newRaycasts.Add(hit);
                    }
                }
                Vector3 newPos = raycastHits.First().point;
                UnwatchObjects(currentObjectsWatched);
                if (oldArea.Equals(Vector3.negativeInfinity)) {
                    oldArea = newPos;
                }
                else {
                    float distance = Vector3.Distance(raycastHits.First().point, oldArea);
                    if (distance > maxDistance)
                    {
                        oldArea = newPos;
                    }
                }
            }
            else
            {
                oldArea = Vector3.negativeInfinity;
                UnwatchObjects();
            }
            currentObjectsWatched.Clear();
            

            UpdateObservers(newRaycasts.ToArray());
            newRaycasts.Clear();
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

        if (raycastHit.collider != null) {
            GameObject currentGameObject = raycastHit.collider.gameObject;
            if (currentGameObject != null)
            {
                TrackableObjectController trackObject = raycastHit.collider.gameObject.GetComponent<TrackableObjectController>();
                ObserveObject(currentGameObject);
                if (trackObject != null)
                {
                    isSolid = TrackableTypeMethods.IsTrackableSolid(trackObject.GetTrackableObject().GetTrackableType());
                }
                else
                {
                    isSolid = true;
                }
            }
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
        RaycastHit[] hits = raycasterConfiguration.ShootMultipleObjectsConfiguration(position, direction, range);
        return SortRaycastHits(position, hits);
    }

    /// <summary>
    /// Sorts the raycast hits based on their distance to the raycasting object.
    /// </summary>
    /// <param name="startPos">the start pos.</param>
    /// <param name="hits">the hits</param>
    /// <returns>the sorted raycast hits.</returns>
    public RaycastHit[] SortRaycastHits(Vector3 startPos, RaycastHit[] hits)
    {
        SortedList<float, RaycastHit> sortedList = new SortedList<float, RaycastHit>();
        if (hits.Length > 0)
        {
            foreach (RaycastHit raycastHit in hits)
            {
                Vector3 postion = raycastHit.point;
                sortedList.Add(Vector3.Distance(startPos, postion), raycastHit);
            }
        }
        return sortedList.Values.ToArray();
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
    private void UnwatchObjects(List<GameObject> watchedObjects = null) {
        if (lastObjects.Any()) {
            List<GameObject> objectsToRemove = new List<GameObject>();
            if (watchedObjects != null && watchedObjects.Count > 0) {
                lastObjects.ForEach(currentGameObject => {
                    if (!watchedObjects.Exists(watchObject => currentGameObject.GetInstanceID() == watchObject.GetInstanceID())) {
                        objectsToRemove.Add(currentGameObject);
                    }
                });
            } else {
                objectsToRemove.AddRange(lastObjects);
            }
            objectsToRemove.ForEach(trackedObject => {
                trackedObject.BroadcastMessage("OnGazeExit", SendMessageOptions.DontRequireReceiver);
            });

            objectsToRemove.ForEach(trackObject => lastObjects.Remove(trackObject));
        }
    }

    /// <summary>
    /// Checks if the object is watched in the list already.
    /// </summary>
    /// <param name="objectToFind">the object to check against.</param>
    /// <returns>true if the object is watched. False otherwise</returns>
    private bool CheckIfObjectIsWatched(GameObject gameObject) {
        return lastObjects.Exists(trackableObject => trackableObject.GetInstanceID() == gameObject.GetInstanceID());
    }

    /// <summary>
    /// Observes an object.
    /// </summary>
    /// <param name="currentGameObject">the trackable object</param>
    private void ObserveObject(GameObject currentGameObject) {

        if (!CheckIfObjectIsWatched(currentGameObject)) {
            currentGameObject.BroadcastMessage("OnGazeEnter", SendMessageOptions.DontRequireReceiver);
            lastObjects.Add(currentGameObject);
        }
        currentObjectsWatched.Add(currentGameObject);
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
    public List<GameObject> GetCurrentObjectsWatched() {
        return currentObjectsWatched;
    }

    /// <summary>
    /// Gets the last objects watched.
    /// </summary>
    /// <returns>the last objects</returns>
    public List<GameObject> GetLastObjects() {
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
        raycasterObservers.ForEach(observer => observer.ObservedObjects(raycastHits, oldArea));
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

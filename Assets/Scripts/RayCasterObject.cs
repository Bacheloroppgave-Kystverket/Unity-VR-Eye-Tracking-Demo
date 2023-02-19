using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// Represents a object that can cast rays. 
/// </summary>
public abstract class RayCasterObject : MonoBehaviour
{
    [Header("Configure raycaster")]
    [SerializeField, Tooltip("True if the object should be 'casting' rays")]
    private bool casting = false;

    [SerializeField, Tooltip("True if you are going to observe mutliple objects")]
    private bool shootMutliple = false;

    [SerializeField, Tooltip("The frequency to gather data for the raycaster. Quest pro has a rate of 30 hz in the forums.")]
    private int frequency = 30;

    [SerializeField, Min(0.01f), Tooltip("The size of the sphere that we shoot to determine what you look at.")]
    private float sphereSize = 0.03f;

    [SerializeField, Range(1,100), Tooltip("The range that the ray should be shot")]
    private int range = 50;

    [SerializeField, Tooltip("The object to visualize where the user looks")]
    private GameObject hitSpot;

    [Space(5),Header("Managers")]
    [SerializeField, Tooltip("The session object")]
    private ReferencePositionManager referencePositionManager;

    [Space(5),Header("Debugging lists")]
    [SerializeField]
    private List<TrackableObject> currentObjectsWatched;

    [SerializeField, Tooltip("The last objects that was looked at.")]
    private List<TrackableObject> lastObjects;

    protected void Start() {
        currentObjectsWatched = new List<TrackableObject>();
        CheckField("Hitspot", hitSpot);
        CheckField("Reference position manager", referencePositionManager);
        
    }


    public void StartTracking() {
        StartCoroutine(StartEyeTracking());
    }

    /// <summary>
    /// Checks if the defined field is set in the editor.
    /// </summary>
    /// <param name="error">the type of error like "type of object"</param>
    /// <param name="fieldToCheck">The field to check</param>
    private void CheckField(string error, object fieldToCheck) {
        if (fieldToCheck == null) {
            Debug.Log("<color=red>Error:</color>" + error + " must be set.", gameObject);
        }
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
    /// Gets the position to shoot the ray from.
    /// </summary>
    /// <returns>the position</returns>
    protected abstract Vector3 FindPosition();

    protected abstract Vector3 FindDirection();

    /// <summary>
    /// Starts the eye tracking in the application. 
    /// </summary>
    /// <returns>the time to wait</returns>
    private IEnumerator StartEyeTracking() {
        while(true)
        {
            if (casting) {
                //Makes ray
                RaycastHit[] raycastHits;
                Vector3 direction = FindDirection();
                Vector3 position = FindPosition();
                raycastHits = shootMutliple ? ShootMultipleObjects(position, direction) : ShootSingleObject(position, direction);

                if (raycastHits.Any())
                {
                    foreach (RaycastHit raycastHit in raycastHits)
                    {
                        WatchObject(raycastHit);
                    }
                    UnwatchObjects(currentObjectsWatched);
                    VisualizeHitpoint(raycastHits.First(), position, direction);
                }
                else
                {
                    UnwatchObjects();
                }
                currentObjectsWatched.Clear();
            }
            MonoBehaviour.print("Watching");
            
            yield return new WaitForSeconds(1 / frequency);
        }
        MonoBehaviour.print("Eyetracking has stopped.");
    }

    /// <summary>
    /// Watches the object that was hit if its not null.
    /// </summary>
    /// <param name="raycastHit">the raycast hit</param>
    private void WatchObject(RaycastHit raycastHit) {
        TrackableObject trackObject = raycastHit.collider.gameObject.GetComponent<TrackableObject>();
        if (trackObject != null) {
            ObserveObject(trackObject);
        }
    }

    /// <summary>
    /// Shoots multiple objects in a line.
    /// </summary>
    /// <param name="position">the starting position</param>
    /// <param name="direction">the direction</param>
    /// <returns></returns>
    private RaycastHit[] ShootMultipleObjects(Vector3 position, Vector3 direction) {
        //Shoots ray
        RaycastHit[] hits = Physics.SphereCastAll(position, sphereSize, direction, range);//Physics.RaycastAll(position, direction, range);
        return hits;
    }

    /// <summary>
    /// Shoots a single object with the raycast.
    /// </summary>
    /// <param name="position">the starting position</param>
    /// <param name="direction">the direction</param>
    /// <returns></returns>
    private RaycastHit[] ShootSingleObject(Vector3 position, Vector3 direction) {
        //Shoots ray
        RaycastHit raycastHit;
        Physics.Raycast(position, direction, out raycastHit, range);
        RaycastHit[] hits = { raycastHit };
        return hits;
    }

    /// <summary>
    /// Unwatches an object based on if its in the list or not.
    /// </summary>
    /// <param name="watchedObjects">a list with the newly watched objects</param>
    private void UnwatchObjects(List<TrackableObject> watchedObjects = null) {
        if (lastObjects.Any()) {
            List<TrackableObject> objectsToRemove = new List<TrackableObject>();
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
    private bool CheckIfObjectIsWatched(TrackableObject objectToFind) {
        return lastObjects.Exists(trackableObject => GameObject.ReferenceEquals(trackableObject, objectToFind));
    }

    /// <summary>
    /// Observes an object.
    /// </summary>
    /// <param name="trackObject">the trackable object</param>
    private void ObserveObject(TrackableObject trackObject) {
        
        if (!CheckIfObjectIsWatched(trackObject)) {
            trackObject.SetBeingWatched();
            lastObjects.Add(trackObject);
        }
        currentObjectsWatched.Add(trackObject);
    }

    /// <summary>
    /// Visualizes the hitpoint in space.
    /// </summary>
    /// <param name="raycastHit">the first hit</param>
    /// <param name="position">the starting position</param>
    /// <param name="direction">the direction</param>
    private void VisualizeHitpoint(RaycastHit raycastHit, Vector3 position, Vector3 direction) {
        hitSpot.transform.position = raycastHit.point;
        Debug.DrawRay(position, direction * raycastHit.distance);
    }
}

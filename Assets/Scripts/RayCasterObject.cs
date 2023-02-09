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
    [SerializeField, Tooltip("True if the object should be 'casting' rays")]
    private bool casting = false;

    [SerializeField, Tooltip("The last objects that was looked at.")]
    private List<TrackableObject> lastObjects;

    [SerializeField, Min(0.01f), Tooltip("The size of the sphere that we shoot to determine what you look at.")]
    private float sphereSize = 0.03f;

    [SerializeField, Tooltip("The session object")]
    private Session session;

    [SerializeField, Tooltip("The object to visualize where the user looks")]
    private GameObject hitSpot;

    [SerializeField]
    private List<TrackableObject> currentObjectsWatched;

    [SerializeField, Range(0, 100), Tooltip("The range of the ray")]
    private int range = 50;

    [SerializeField, Tooltip("True if you are going to observe mutliple objects")]
    private bool shootMutliple = false;

    private void Start() {
        currentObjectsWatched = new List<TrackableObject>();
    }

    public void ToggleIsCasting() {
        casting = !casting;
    }

    /// <summary>
    /// Sets if the raygun shoud cast.
    /// </summary>
    /// <param name="isCasting">true if it should cast</param>
    public void SetIsCasting(bool isCasting) {
        this.casting = isCasting;
    }

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

    private void FixedUpdate(){
        if (casting){
            //Makes ray
            RaycastHit[] raycastHits;
            Vector3 direction = FindDirection();
            Vector3 position = FindPosition();
            raycastHits = shootMutliple ? ShootMultipleObjects(position, direction) : ShootSingleObject(position, direction);

            if (raycastHits.Any())
            {
                foreach (RaycastHit raycastHit in raycastHits) {
                    WatchObject(raycastHit);
                    
                }
                UnwatchObjects(currentObjectsWatched);
                VisualizeHitpoint(raycastHits.Last(), position, direction);
            }
            else {
                UnwatchObjects();
            }
        }
        else {
            UnwatchObjects();
        }
        currentObjectsWatched.Clear();
    }

    private void WatchObject(RaycastHit raycastHit) {
        TrackableObject trackObject = raycastHit.collider.gameObject.GetComponent<TrackableObject>();
        if (trackObject != null) {
            ObserveObject(trackObject);
            
        }
    }

    private RaycastHit[] ShootMultipleObjects(Vector3 position, Vector3 direction) {
        //Shoots ray
        RaycastHit[] hits = Physics.RaycastAll(position, direction, range);//Physics.RaycastAll(position, direction, range);
        return hits;
    }

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
            trackObject.SetBeingWatched(session.GetCurrentReferencePosition().GetLocationId());
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

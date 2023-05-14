using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// The controller that visualizes the diffrent point recordings.
/// </summary>
[RequireComponent(typeof(EyetrackingPlayer))]
public class VisualizeHitpointController : MonoBehaviour, RaycasterObserver
{

    [SerializeField, Tooltip("The object to visualize where the user looks")]
    private HitpointController hitSpot;

    [SerializeField, Tooltip("Set to true if the hitpoint should be visualized.")]
    private bool visualizeHitpoint;

    [SerializeField, Tooltip("Set to true if the line to the hitpoint should be visualized.")]
    private bool visualizeLine;

    [SerializeField, Tooltip("The hitspot prefab.")]
    private HitpointController hitspotPrefab;

    [SerializeField, Tooltip("The raycaster object.")]
    private RayCasterObject raycaster;

    [SerializeField, Tooltip("The projector")]
    private GameObject projector;

    [SerializeField, Tooltip("")]
    private Vector3 oldPos;

    ///<inheritdoc/>
    private void Start()
    {
        if (visualizeHitpoint && hitSpot == null) {
            HitpointController newHitspot = Instantiate(hitspotPrefab);
            hitspotPrefab.transform.position = Vector3.zero;
            this.hitSpot = newHitspot;
            newHitspot.tag = "hitspot";
        }
        this.raycaster = GetComponent<EyetrackingPlayer>().GetRaycaster();
        raycaster.AddObserver(this);
    }

    ///<inheritdoc/>
    public void ObservedObjects(RaycastHit[] raycastHits, Vector3 lookPosition)
    {
        CheckIfObjectIsNull(raycastHits, "raycast hits");
        Vector3 hitPoint = raycastHits != null && raycastHits.Length > 0 ? raycastHits[0].point : Vector3.zero;
        VisualizeHitpointAndDrawLine(gameObject.transform.position, raycaster.FindDirection(), lookPosition, hitPoint);
    }

    /// <summary>
    /// Visualizes the hitpoint in space.
    /// </summary>
    /// <param name="position">the starting position</param>
    /// <param name="direction">the direction</param>
    /// <param name="lookPosition">the look position right now</param>
    /// <param name="hitPoint">the vector where the first object was hit</param>
    private void VisualizeHitpointAndDrawLine(Vector3 position, Vector3 direction, Vector3 lookPosition, Vector3 hitPoint)
    {
        if (visualizeHitpoint) {
            if (!lookPosition.Equals(Vector3.negativeInfinity))
            {
                hitSpot.transform.position = lookPosition;
                hitSpot.SetHitpointPosition(hitPoint);
                hitSpot.SetHitpointActive(true);
                Transform projector = hitSpot.GetProjector().transform;
                projector.position = raycaster.FindPosition();
                projector.LookAt(hitSpot.transform);
            }
            else { 
                hitSpot.SetHitpointActive(false); 
            }
            
        }
        if (visualizeLine) {
            if (!lookPosition.Equals(Vector3.negativeInfinity)) {
                Debug.DrawRay(position, direction * Vector3.Distance(position, lookPosition));
            }
            else {
                Debug.DrawRay(Vector3.zero, Vector3.zero);
            }
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
    private void CheckField(string error, object fieldToCheck)
    {
        if (fieldToCheck == null)
        {
            Debug.Log("<color=red>Error:</color>" + error + " must be set.", gameObject);
        }
    }


}

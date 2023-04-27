using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(EyetrackingPlayer))]
public class VisualizeHitpointController : MonoBehaviour, RaycasterObserver
{

    [SerializeField, Tooltip("The object to visualize where the user looks")]
    private GameObject hitSpot;

    [SerializeField, Tooltip("Set to true if the hitpoint should be visualized.")]
    private bool visualizeHitpoint;

    [SerializeField, Tooltip("Set to true if the line to the hitpoint should be visualized.")]
    private bool visualizeLine;

    [SerializeField, Tooltip("The hitspot prefab.")]
    private GameObject hitspotPrefab;

    [SerializeField, Tooltip("The raycaster object.")]
    private RayCasterObject raycaster;

    [SerializeField, Tooltip("")]
    private Vector3 oldPos;

    ///<inheritdoc/>
    private void Start()
    {
        if (visualizeHitpoint && hitSpot == null) {
            GameObject newHitspot = Instantiate(hitspotPrefab);
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
        VisualizeHitpointAndDrawLine(gameObject.transform.position, raycaster.FindDirection(), lookPosition);
    }

    /// <summary>
    /// Visualizes the hitpoint in space.
    /// </summary>
    /// <param name="position">the starting position</param>
    /// <param name="direction">the direction</param>
    /// <param name="lookPosition">the look position right now</param>
    private void VisualizeHitpointAndDrawLine(Vector3 position, Vector3 direction, Vector3 lookPosition)
    {
        if (visualizeHitpoint) {
            if (!lookPosition.Equals(Vector3.negativeInfinity))
            {
                Vector3 hitPos = lookPosition;
                hitSpot.transform.position = hitPos;
                hitSpot.SetActive(true);
            }
            else { 
                hitSpot.SetActive(false); 
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

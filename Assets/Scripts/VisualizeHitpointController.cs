using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(RayCasterObject))]
public class VisualizeHitpointController : MonoBehaviour, RaycasterObserver
{

    [SerializeField, Tooltip("The object to visualize where the user looks")]
    private GameObject hitSpot;

    [SerializeField, Tooltip("Set to true if the hitpoint should be visualized.")]
    private bool visualizeHitpoint;

    [SerializeField, Tooltip("Set to true if the line to the hitpoint should be visualized.")]
    private bool visualizeLine;

    [SerializeField, Tooltip("The raycaster object.")]
    private RayCasterObject raycaster;

    ///<inheritdoc/>
    private void Start()
    {
        CheckField("Hitspot", hitSpot);
        this.raycaster = GetComponent<RayCasterObject>();
        raycaster.AddObserver(this);
    }

    ///<inheritdoc/>
    public void ObservedObjects(RaycastHit[] raycastHits)
    {
        CheckIfObjectIsNull(raycastHits, "raycast hits");
        VisualizeHitpointAndDrawLine(raycastHits, raycaster.FindPosition(), raycaster.FindDirection());
    }

    /// <summary>
    /// Visualizes the hitpoint in space.
    /// </summary>
    /// <param name="raycastHit">the first hit</param>
    /// <param name="position">the starting position</param>
    /// <param name="direction">the direction</param>
    private void VisualizeHitpointAndDrawLine(RaycastHit[] raycastHit, Vector3 position, Vector3 direction)
    {
        Vector3 hitPos = raycastHit.Last().point;
        hitSpot.transform.position = hitPos;

        Debug.DrawRay(position, direction * raycastHit.First().distance);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterestController : MonoBehaviour
{
    [SerializeField, Tooltip("The point of interest that this object represents.")]
    private PointOfInterest pointOfInterest;

    [SerializeField, Tooltip("The default material to show the normal data.")]
    private Material defaultMaterial;

    [SerializeField, Tooltip("The heatmap material that is transparent")]
    private Material heatMapMaterial;

    /// <summary>
    /// Sets the position of the intrest point. Also sets is as a child of that ibhect and
    /// </summary>
    /// <param name="pointOfInterest"></param>
    /// <param name="trackableObjectController"></param>
    public void SetPointOfInterest(PointOfInterest pointOfInterest) {
        CheckIfObjectIsNull(pointOfInterest, "point of interest");
        this.pointOfInterest = pointOfInterest;
        transform.SetParent(pointOfInterest.GetParentTransform());
        transform.localPosition = pointOfInterest.GetLocalPosition();
    }

    /// <summary>
    /// Shows the point of interest with its default material
    /// </summary>
    public void ShowPointOfInterest() { 
        gameObject.SetActive(true);
        GetComponent<MeshRenderer>().material = defaultMaterial;
    }

    /// <summary>
    /// Shows the point of interest as a heatmap.
    /// </summary>
    public void ShowPointOfInterestAsHeatmap() { 
        gameObject.SetActive(true);
        GetComponent<MeshRenderer>().material = heatMapMaterial;
    }

    /// <summary>
    /// Hides the point of interest.
    /// </summary>
    public void HidePointOfInterest() { 
        gameObject.SetActive(false);
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

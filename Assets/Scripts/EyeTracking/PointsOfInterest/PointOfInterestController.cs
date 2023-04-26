using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.VFX;

public class PointOfInterestController : MonoBehaviour
{
    [Header("List of values")]
    [SerializeField, Tooltip("The point of interest that this object represents.")]
    private List<RecordedPoint> points = new List<RecordedPoint>();

    /// <summary>
    /// Sets the position of the intrest point. Also sets is as a child of that ibhect and
    /// </summary>
    /// <param name="pointOfInterest"></param>
    /// <param name="trackableObjectController"></param>
    public void SetPointOfInterest(PointOfInterest pointOfInterest) {
        CheckIfObjectIsNull(pointOfInterest, "point of interest");
        points.Add(pointOfInterest);
        Transform parentTransform = pointOfInterest.GetParentTransform();
        transform.SetParent(parentTransform);
        transform.localPosition = pointOfInterest.GetLocalPosition();
        transform.localScale = parentTransform.InverseTransformVector(new Vector3(0.03f,0.03f,0.03f));
        transform.localRotation = Quaternion.Euler(0,0,0);

    }

    /// <summary>
    /// Shows the point of interest with its default material
    /// </summary>
    public void ShowPointOfInterest() { 
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Adds a recorded point to this point of interest controller.
    /// </summary>
    /// <param name="recordedPoint">the recorded point</param>
    public void AddRecordedPoint(RecordedPoint recordedPoint) {
        CheckIfObjectIsNull(recordedPoint, "recorded point");
        this.points.Add(recordedPoint);
    }

    /// <summary>q
    /// Shows the point of interest as a heatmap.
    /// </summary>
    public void ShowPointOfInterestAsHeatmap() { 
        gameObject.SetActive(true);
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

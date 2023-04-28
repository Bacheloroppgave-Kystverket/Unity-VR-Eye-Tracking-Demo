using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.VFX;

public class PointOfInterestController : MonoBehaviour
{
    [SerializeField, Tooltip("The point of interest that this object represents.")]
    private PointOfInterest pointOfInterest;

    [SerializeField, Tooltip("The order id of this point")]
    private int orderId;

    [SerializeField, Tooltip("The text of the point of interest")]
    private TextMeshPro textOfPoint;

    /// <summary>
    /// Sets the position of the intrest point. Also sets is as a child of that ibhect and
    /// </summary>
    /// <param name="pointOfInterest">the point of interest</param>
    /// <param name="orderId">the order id</param>
    /// <param name="showText">true if the text of the point should show.</param>
    public void SetPointOfInterest(PointOfInterest pointOfInterest, int orderId, bool showText) {
        CheckIfObjectIsNull(pointOfInterest, "point of interest");
        this.pointOfInterest = pointOfInterest;
        this.orderId = orderId;
        PointRecording pointRecording = pointOfInterest.GetPointRecordingWithId(orderId);
        this.textOfPoint.text = pointRecording.GetOrderId()  + "\n" + pointRecording.GetTime().ToString() + "s";
        this.textOfPoint.gameObject.SetActive(showText);
        Transform parentTransform = pointRecording.GetParentTransform();
        transform.position = parentTransform.transform.TransformPoint(pointOfInterest.GetPointRecordingWithId(orderId).GetLocalPosition());
        transform.LookAt(GameObject.FindWithTag("Player").transform);
        //transform.localScale = parentTransform.InverseTransformVector(new Vector3(0.03f,0.03f,0.03f));
    }

    /// <summary>
    /// Shows the point of interest with its default material
    /// </summary>
    public void ShowPointOfInterest() { 
        gameObject.SetActive(true);
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

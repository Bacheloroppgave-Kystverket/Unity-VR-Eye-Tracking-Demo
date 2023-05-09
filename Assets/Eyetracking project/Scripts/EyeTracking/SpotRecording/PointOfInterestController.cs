using TMPro;
using UnityEngine;

public class PointOfInterestController : MonoBehaviour
{
    [SerializeField, Tooltip("The point of interest that this object represents.")]
    private PointOfInterestContainer pointOfInterest;

    [SerializeField, Tooltip("The order id of this point")]
    private int orderId;

    [SerializeField, Tooltip("The text of the point of interest")]
    private TextMeshPro textOfPoint;

    /// <summary>
    /// Sets the position of the intrest point. Also sets is as a child of that ibhect and
    /// </summary>
    /// <param name="pointOfInterestContainer">the point of interest</param>
    /// <param name="orderId">the order id</param>
    /// <param name="showText">true if the text of the point should show.</param>
    /// <param name="player">the player</param>
    public void SetPointOfInterest(PointOfInterestContainer pointOfInterestContainer, int orderId, bool showText, EyetrackingPlayer player) {
        CheckIfObjectIsNull(pointOfInterestContainer, "point of interest");
        this.pointOfInterest = pointOfInterestContainer;
        this.orderId = orderId;
        PointRecording pointRecording = pointOfInterestContainer.GetRecord();
        this.textOfPoint.text = pointRecording.GetOrderId()  + "\n" + pointRecording.GetTime().ToString() + "s";
        this.textOfPoint.gameObject.SetActive(showText);
        Transform parentTransform = pointOfInterestContainer.GetParentTransform();
        transform.position = parentTransform.transform.TransformPoint(pointOfInterestContainer.GetRecord().GetLocalPosition());
        transform.LookAt(player.GetRaycaster().transform);
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

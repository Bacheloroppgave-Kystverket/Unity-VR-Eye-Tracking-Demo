using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.VFX;

public class PointOfInterestController : MonoBehaviour
{
    [SerializeField, Tooltip("The point of interest that this object represents.")]
    private RecordedPoint pointOfInterest;

    [SerializeField, Tooltip("The default material to show the normal data.")]
    private Material defaultMaterial;

    [SerializeField, Tooltip("The heatmap material that is transparent")]
    private Material heatMapMaterial;

    [SerializeField, Tooltip("The visual effect")]
    private VisualEffect visualEffect;

    /// <summary>
    /// Sets the position of the intrest point. Also sets is as a child of that ibhect and
    /// </summary>
    /// <param name="pointOfInterest"></param>
    /// <param name="trackableObjectController"></param>
    public void SetPointOfInterest(PointOfInterest pointOfInterest) {
        CheckIfObjectIsNull(pointOfInterest, "point of interest");
        this.pointOfInterest = pointOfInterest;
        Transform parentTransform = pointOfInterest.GetParentTransform();
        transform.SetParent(parentTransform);
        transform.localPosition = pointOfInterest.GetLocalPosition();
        transform.localScale = parentTransform.InverseTransformVector(new Vector3(1f,1f,1f));
        transform.localRotation = Quaternion.Euler(0,0,0);

    }

    /// <summary>
    /// Shows the point of interest with its default material
    /// </summary>
    public void ShowPointOfInterest() { 
        gameObject.SetActive(true);
        visualEffect.Reinit();
        visualEffect.SetBool("Heatmap", false);
        visualEffect.Play();
        GetComponent<MeshRenderer>().material = defaultMaterial;
    }

    /// <summary>q
    /// Shows the point of interest as a heatmap.
    /// </summary>
    public void ShowPointOfInterestAsHeatmap() { 
        gameObject.SetActive(true);
        visualEffect.Reinit();
        visualEffect.SetBool("Heatmap", true);
        visualEffect.Play();
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

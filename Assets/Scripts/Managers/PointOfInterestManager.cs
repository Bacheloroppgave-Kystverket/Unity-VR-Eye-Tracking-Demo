using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterestManager : MonoBehaviour
{

    [SerializeField, Tooltip("The default point placer")]
    private PointPlacerController pointPlacer;

    private void Start()
    {
        CheckField("point placer", pointPlacer);
    }


    public void ShowInterestPoints() {
        pointPlacer.ShowPointsOfInterestAsDefault();
    }

    /// <summary>
    /// Shows the interest points as a heatmap with transparent color.
    /// </summary>
    public void ShowInterestPointsAsHeatmap() {
        pointPlacer.ShowPointsOfInterestAsHeatmap();
    }

    /// <summary>
    /// Hides the intrest points.
    /// </summary>
    public void HideInterestPoints(){
        pointPlacer.HidePointsOfInterest();
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

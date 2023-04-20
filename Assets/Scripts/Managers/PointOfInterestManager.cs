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
        VisualDotDeployer[] visualDotDeployers = GameObject.FindObjectsOfType<VisualDotDeployer>();
        foreach (VisualDotDeployer visualDotDeployer in visualDotDeployers)
        {
            visualDotDeployer.RemoveVisualDots();
            visualDotDeployer.ShowAllPointsOfInterest();
        }
    }

    /// <summary>
    /// Shows the interest points as a heatmap with transparent color.
    /// </summary>
    public void ShowInterestPointsAsHeatmap() {
        VisualDotDeployer[] visualDotDeployers = GameObject.FindObjectsOfType<VisualDotDeployer>();
        foreach (VisualDotDeployer visualDotDeployer in visualDotDeployers)
        {
            visualDotDeployer.RemoveVisualDots();
            visualDotDeployer.ShowAllHeatmapPoints();
        }
    }

    /// <summary>
    /// Hides the intrest points.
    /// </summary>
    public void HideInterestPoints(){
        VisualDotDeployer[] visualDotDeployers = GameObject.FindObjectsOfType<VisualDotDeployer>();
        foreach (VisualDotDeployer visualDotDeployer in visualDotDeployers)
        {
            visualDotDeployer.RemoveVisualDots();
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

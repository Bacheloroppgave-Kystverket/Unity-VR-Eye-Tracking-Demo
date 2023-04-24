using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterestManager : MonoBehaviour
{

    [SerializeField, Tooltip("The default point placer")]
    private PointPlacerController pointPlacer;

    [SerializeField, Tooltip("The points of intertest collection controller")]
    private PointOfInterestCollectionController pointOfInterestCollectionController;

    [SerializeField, Tooltip("The point start to show.")]
    private int pointStart = 0;

    private void Start()
    {
        CheckField("point placer", pointPlacer);
        pointOfInterestCollectionController = GameObject.FindGameObjectWithTag("Player").GetComponent<PointOfInterestCollectionController>();
        CheckField("point of interest collection controller", pointOfInterestCollectionController);
    }


    public void ShowInterestPoints() {
        pointOfInterestCollectionController.ShowPointsOfInterest();
        pointOfInterestCollectionController.UpdateOrderOfPointsOfInterest(pointStart);
    }

    /// <summary>
    /// Shows the interest points as a heatmap with transparent color.
    /// </summary>
    public void ShowInterestPointsAsHeatmap() {
        pointOfInterestCollectionController.ShowHeatmapPoints();
    }

    /// <summary>
    /// Hides the intrest points.
    /// </summary>
    public void HideInterestPoints(){
        pointOfInterestCollectionController.HideHeatmapPoints();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointOfInterestManager : MonoBehaviour
{

    [Header("Configuration of point manager")]
    [SerializeField, Tooltip("The default point placer")]
    private PointPlacerController pointPlacer;

    [SerializeField, Tooltip("The points of intertest collection controller")]
    private PointOfInterestCollectionController pointOfInterestCollectionController;

    [SerializeField, Tooltip("The point start to show.")]
    private int pointStart = 0;

    [SerializeField, Tooltip("Set to true if the line for gazeplots should be shown.")]
    private bool showLine = true;

    [SerializeField, Tooltip("Set to true if the line text for gazeplots should be shown.")]
    private bool showLineText = true;

    [SerializeField, Tooltip("Set to true if the point text should be shown.")]
    private bool showPointText = true;

    [SerializeField, Tooltip("Set to true if the point cloud or heatmap should have solid color.")]
    private bool showPointsAsSolid = false;

    [SerializeField, Tooltip("All the toggles of showPointLine, showPointLineText, showPointText and showPointsAsSolid in that order.")]
    private List<Toggle> toggles = new List<Toggle>();

    private bool showHeatmap;

    private bool showPoints;

    private void Start()
    {
        CheckField("point placer", pointPlacer);
        pointOfInterestCollectionController = GameObject.FindGameObjectWithTag("Player").GetComponent<PointOfInterestCollectionController>();
        CheckField("point of interest collection controller", pointOfInterestCollectionController);
        
    }

    private void Awake()
    {
        showLine = !showLine;
        ToggleLine();
        showLineText = !showLineText;
        ToggleLineText();
        showPointText = !showPointText;
        TogglePointText();
        showPointsAsSolid = !showPointsAsSolid;
        TogglePointCouldMode();
        toggles[0].enabled = showLine;
        toggles[1].enabled = showLineText;
        toggles[2].enabled = showPointText;
        toggles[2].enabled = showPointsAsSolid;
    }

 

    public void TogglePointCouldMode() { 
        pointOfInterestCollectionController.SetShowPointsAsSolid(showPointsAsSolid);
        showPointsAsSolid = !showPointsAsSolid;
    }

    /// <summary>
    /// Toggles the heatmap and if it should be on or not.
    /// </summary>
    public void ToggleHeatmap() {
        showHeatmap = !showHeatmap;
        if (showHeatmap)
        {
            pointOfInterestCollectionController.ShowHeatmapPoints();
        }
        else {
            pointOfInterestCollectionController.HideHeatmapPoints();
        }
    }

    /// <summary>
    /// Toggles the points or gazeplot
    /// </summary>
    public void TogglePoints()
    {
        showPoints = !showPoints;
        if (showPoints)
        {
            pointOfInterestCollectionController.ShowPointsOfInterest();
            pointOfInterestCollectionController.UpdateOrderOfPointsOfInterest(pointStart);
        }
        else {
            pointOfInterestCollectionController.HidePointsOfInterest();
        }  
    }

    /// <summary>
    /// Toggles the line of the gazeplots.
    /// </summary>
    public void ToggleLine() { 
        showLine = !showLine;
        pointOfInterestCollectionController.GetLineController().SetShowLine(showLine);
    }

    /// <summary>
    /// Toggles the text of the points.
    /// </summary>
    public void TogglePointText() {
        showPointText = !showPointText;
        pointOfInterestCollectionController.SetShowPointText(showPointText);
    }

    /// <summary>
    /// Toggles the text of the line.
    /// </summary>
    public void ToggleLineText() {
        showLineText = !showLineText;
        pointOfInterestCollectionController.GetLineController().SetShowLineText(showLineText);
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

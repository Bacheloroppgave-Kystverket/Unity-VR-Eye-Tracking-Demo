using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PointOfInterestManager : MonoBehaviour
{

    [Header("Configuration of point manager")]
    [SerializeField, Tooltip("The default point placer")]
    private PointPlacerController pointPlacer;

    [SerializeField, Tooltip("The points of intertest collection controller")]
    private RecordedPointsController pointOfInterestCollectionController;

    [SerializeField, Tooltip("The points of interest displayer")]
    private DisplayPointsOfInterestController displayPointsOfInterest;

    [SerializeField, Tooltip("The point start to show.")]
    private int pointStart = 0;

    [SerializeField, Tooltip("The ending of the gaze")]
    private int pointEnd = 0;

    [SerializeField, Tooltip("Set to true if the line for gazeplots should be shown.")]
    private bool showLine = true;

    [SerializeField, Tooltip("Set to true if the point text should be shown.")]
    private bool showPointText = true;

    [SerializeField, Tooltip("Set to true if the point cloud or heatmap should have solid color.")]
    private bool showPointsAsSolid = false;

    [SerializeField, Tooltip("All the toggles of showPointLine and showPointText")]
    private List<Toggle> toggles = new List<Toggle>();

    private bool showHeatmap;

    private bool showPoints;

    private void Start()
    {
        CheckField("point placer", pointPlacer);
        pointOfInterestCollectionController = GameObject.FindGameObjectWithTag("Player").GetComponent<RecordedPointsController>();
        CheckField("point of interest collection controller", pointOfInterestCollectionController);
        CheckField("display points of interest", displayPointsOfInterest);
    }

    private void Awake()
    {
        showLine = !showLine;
        ToggleLine();
        showPointText = !showPointText;
        TogglePointText();
        toggles[0].enabled = showLine;
        toggles[1].enabled = showPointText;
    }

    public void SetStartAndEnd(int pointStart, int pointEnd) {
        if (pointStart > pointEnd) {
            throw new IllegalArgumentException("The start position cannot be larger than the ending");
        }
        this.pointStart = pointStart;
        this.pointEnd = pointEnd;
        displayPointsOfInterest.UpdateOrderOfPointsOfInterest(pointStart, pointEnd, showLine, showPointText);
    }

 

    public void TogglePointCouldMode() { 
        showPointsAsSolid = !showPointsAsSolid;
    }

    /// <summary>
    /// Toggles the heatmap and if it should be on or not.
    /// </summary>
    public void ToggleHeatmap() {
        showHeatmap = !showHeatmap;
        pointOfInterestCollectionController.DeployHeatmapPoints();
        List<VisualDotDeployerController> visualDotDeployers = GameObject.FindObjectsOfType<VisualDotDeployerController>().ToList();
        if (showHeatmap)
        {
            visualDotDeployers.ForEach(deployer => deployer.ShowAllHeatmapPoints(showPointsAsSolid));
        }
        else {
            visualDotDeployers.ForEach(deployer => deployer.RemoveVisualDots());
        }
    }

    /// <summary>
    /// Gets the amount of points of interest.
    /// </summary>
    /// <returns>the amount</returns>
    public int GetAmountOfPointsOfInterest() => pointOfInterestCollectionController.GetPointRecordings().Count;

    /// <summary>
    /// Toggles the points or gazeplot
    /// </summary>
    public void TogglePoints()
    {
        showPoints = !showPoints;
        if (showPoints)
        {
            displayPointsOfInterest.UpdateOrderOfPointsOfInterest(pointStart, pointEnd, showLine, showPointText);
        }
        else {
            displayPointsOfInterest.HidePointsOfInterest();
        }  
    }

    /// <summary>
    /// Toggles the line of the gazeplots.
    /// </summary>
    public void ToggleLine() { 
        showLine = !showLine;
    }

    /// <summary>
    /// Toggles the text of the points.
    /// </summary>
    public void TogglePointText() {
        showPointText = !showPointText;
        
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

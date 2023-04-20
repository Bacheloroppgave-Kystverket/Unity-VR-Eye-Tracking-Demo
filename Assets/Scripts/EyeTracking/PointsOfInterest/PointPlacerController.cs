using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EyeCaster))]
public class PointPlacerController : MonoBehaviour, RaycasterObserver
{

    [SerializeField, Tooltip("The raycast hits of this object for the heatmap")]
    private List<RecordedPoint> heatmapList = new List<RecordedPoint>();

    [SerializeField, Tooltip("The points of interest")]
    private List<PointOfInterest> pointOfInterests = new List<PointOfInterest>();

    [SerializeField, Tooltip("The point of interest controllers")]
    private List<PointOfInterestController> pointOfInterestControllers = new List<PointOfInterestController>();

    [SerializeField, Tooltip("The current point"), Min(1)]
    private int currentPoint = 1;

    [SerializeField, Tooltip("The amount of samples per second"), Min(1)]
    private int heatmapFrequency;

    [SerializeField, Tooltip("The frequency of the tracker"), Min(1)]
    private int frequency;

    [SerializeField, Tooltip("The prefab that the points of interest should have")]
    private GameObject pointPrefab;

    [SerializeField, Tooltip("The frequency of the heatmap."), Min(1)]
    private int pointOfInterestFrequency;

    [SerializeField, Tooltip("The current order of the points of interest"), Min(1)]
    private int orderID;


    private void Start()
    {
        RayCasterObject rayCasterObject = gameObject.GetComponent<RayCasterObject>();
        this.frequency = rayCasterObject.GetFrequency();
        rayCasterObject.AddObserver(this);
    }

    /// <summary>
    /// Adds an interest point to the list.
    /// </summary>
    /// <param name="raycastHit">the raycast hit</param>
    private void AddHeatmapPoint(RaycastHit raycastHit) {
        heatmapList.Add(new RecordedPoint(raycastHit));
    }

    ///<inheritdoc/>
    public void ObservedObjects(RaycastHit[] raycastHits) {
        CheckIfObjectIsNull(raycastHits, "raycast hits");
        RaycastHit raycastHit = raycastHits.Last();
        if (currentPoint % (frequency / heatmapFrequency) == 0 && raycastHits.Length > 0)
        {
            AddHeatmapPoint(raycastHit);
        }
        if (currentPoint % (frequency / pointOfInterestFrequency) == 0 && raycastHits.Length > 0) {
            pointOfInterests.Add(new PointOfInterest(orderID, raycastHit));
            orderID++;
        }
        currentPoint += 1;
    }

    /// <summary>
    /// Shows all the points of interest in the worldspace.
    /// </summary>
    public void ShowPointsOfInterestAsDefault() {
        AddNewestPointsOfInterest();
        pointOfInterestControllers.ForEach(pointController => pointController.ShowPointOfInterest());
    }

    /// <summary>
    /// Shows the points of interest with a transparent membrane. 
    /// </summary>
    public void ShowPointsOfInterestAsHeatmap()
    {
        AddNewestPointsOfInterest();
        pointOfInterestControllers.ForEach(pointController => pointController.ShowPointOfInterestAsHeatmap());
    }
    
    /// <summary>
    /// Hides the points of interest.
    /// </summary>
    public void HidePointsOfInterest() {
        pointOfInterestControllers.ForEach(pointController => pointController.HidePointOfInterest());
    }

    /// <summary>
    /// Adds the newest points of interest. The old ones are not added again.
    /// </summary>
    public void AddNewestPointsOfInterest(){
        int nextPos = pointOfInterestControllers.Count();
        int maxAmount = pointOfInterests.Count();
        while (nextPos < maxAmount)
        {
            PointOfInterest pointOfInterest = pointOfInterests[nextPos];
            InstansiatePoint(pointOfInterest);
            nextPos += 1;
        }
    }

    /// <summary>
    /// Instansiates the point of interest and sets the location.
    /// </summary>
    /// <param name="pointOfInterest">the point of interest to instansiate</param>
    private void InstansiatePoint(PointOfInterest pointOfInterest) {
        PointOfInterestController pointOfInterestController = Instantiate(pointPrefab).GetComponent<PointOfInterestController>();
        pointOfInterestController.SetPointOfInterest(pointOfInterest);
        pointOfInterestControllers.Add(pointOfInterestController);
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

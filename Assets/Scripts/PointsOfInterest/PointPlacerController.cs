using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EyeCaster))]
public class PointPlacerController : MonoBehaviour, RaycasterObserver
{

    [SerializeField, Tooltip("The raycast hits of this object")]
    private List<PointOfInterest> pointsOfInterest = new List<PointOfInterest>();

    [SerializeField, Tooltip("The point of interest controllers")]
    private List<PointOfInterestController> pointOfInterestControllers= new List<PointOfInterestController>();

    [SerializeField, Tooltip("The current point")]
    private int currentPoint = 1;

    [SerializeField, Tooltip("The amount of samples per ")]
    private int sampleSize;

    [SerializeField, Tooltip("The frequency of the tracker")]
    private int frequency;

    [SerializeField, Tooltip("The prefab that the points of interest should have")]
    private GameObject pointPrefab;


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
    private void AddInterestPoint(RaycastHit raycastHit) {
        pointsOfInterest.Add(new PointOfInterest(currentPoint / (frequency/sampleSize), raycastHit));
    }

    ///<inheritdoc/>
    public void ObservedObjects(RaycastHit[] raycastHits){
        CheckIfObjectIsNull(raycastHits, "raycast hits");
        if (currentPoint % (frequency/sampleSize) == 0 && raycastHits.Length > 0)
        {
            AddInterestPoint(raycastHits.Last());
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
    public void ShowPointsOfInterestAsHeatmap() {
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
    public void AddNewestPointsOfInterest()
    {
        int nextPos = pointOfInterestControllers.Count();
        int maxAmount = pointsOfInterest.Count();
        while (nextPos < maxAmount)
        {
            PointOfInterest pointOfInterest = pointsOfInterest[nextPos];
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

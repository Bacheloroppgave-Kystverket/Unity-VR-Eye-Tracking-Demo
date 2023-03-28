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
        pointsOfInterest.Add(new PointOfInterest(currentPoint / frequency, raycastHit));
    }

    ///<inheritdoc/>
    public void ObservedObjects(RaycastHit[] raycastHits){
        if (currentPoint % frequency == 0 && raycastHits.Length > 0)
        {
            AddInterestPoint(raycastHits.Last());
        }
        currentPoint += 1;
    }

    public void ShowPointsOfInterest() {
        int nextPos =  pointOfInterestControllers.Count();
        int maxAmount = pointsOfInterest.Count();
        while (nextPos < maxAmount) {
            PointOfInterest pointOfInterest = pointsOfInterest[nextPos];
            InstansiatePoint(pointOfInterest);
            nextPos += 1;
        }
        pointOfInterestControllers.ForEach(pointController => pointController.gameObject.SetActive(true));
    }

    /// <summary>
    /// Instansiates the point of interest and sets the location.
    /// </summary>
    /// <param name="pointOfInterest">the point of interest to instansiate</param>
    private void InstansiatePoint(PointOfInterest pointOfInterest) {
        PointOfInterestController pointOfInterestController = Instantiate(gameObject).GetComponent<PointOfInterestController>();
        pointOfInterestController.SetPointOfInterest(pointOfInterest);
        pointOfInterestControllers.Add(pointOfInterestController);
    }
}

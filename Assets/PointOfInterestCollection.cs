using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PointOfInterestCollectionController : MonoBehaviour
{
    [SerializeField]
    private List<PointOfInterest> pointOfInterests = new List<PointOfInterest>();

    [SerializeField]
    private List<RecordedPoint> recordedPoints = new List<RecordedPoint>();

    [SerializeField, Tooltip("The prefab that the points of interest should have")]
    private GameObject pointPrefab;

    [SerializeField, Tooltip("The visual effect prefab")]
    private GameObject visualEffectPrefab;

    [SerializeField]
    private List<PointOfInterestController> pointOfInterestControllers = new List<PointOfInterestController>();

    [SerializeField]
    private List<VisualDotDeployer> visualDotDeployers = new List<VisualDotDeployer>();

    [SerializeField, Tooltip("The current heatpoint that has been synced.")]
    private int currentHeatPoint = 0;

    [SerializeField, Tooltip("The amount of diffrent point controllers that can be moved around.")]
    private int amountOfPointControllers;

    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// Adds an interest point to the list.
    /// </summary>
    /// <param name="raycastHit">the raycast hit</param>
    public void AddHeatmapPoint(RecordedPoint recordedPoint)
    {
        recordedPoints.Add(recordedPoint);
    }

    public void ShowPointsOfInterest() { 
    
    }

    public void ShowHeatmapPoints() {
        SyncHeatmap();
        visualDotDeployers.ForEach(visualDotDeployer => visualDotDeployer.ShowAllHeatmapPoints());
    }

    public void HideHeatmapPoints() {
        visualDotDeployers.ForEach(visualDotDeployer => visualDotDeployer.RemoveVisualDots());
    }

    /// <summary>
    /// Syncs the heatmaps values.
    /// </summary>
    private void SyncHeatmap() {
        for (int i = currentHeatPoint; i < recordedPoints.Count; i++) {
            RecordedPoint recordedPoint = recordedPoints[i];
            Transform parentTransform = recordedPoint.GetParentTransform();
            VisualDotDeployer visualDotDeployer = parentTransform.GetComponent<VisualDotDeployer>();
            if (visualDotDeployer == null)
            {
                visualDotDeployer = parentTransform.AddComponent<VisualDotDeployer>();
                visualDotDeployer.SetupVisualDot(visualEffectPrefab);
                visualDotDeployers.Add(visualDotDeployer);
            }
            visualDotDeployer.AddHeatmapPoint(recordedPoint);
            
        }
        currentHeatPoint = recordedPoints.Count;
    }

    /// <summary>
    /// Adds the newest points of interest. The old ones are not added again.
    /// </summary>
    public void AddNewestPointsOfInterest()
    {
        /*
         int nextPos = pointOfInterestControllers.Count();
        int maxAmount = pointOfInterests.Count();
        while (nextPos < maxAmount)
        {
            PointOfInterest pointOfInterest = pointOfInterests[nextPos];
            InstansiatePoint(pointOfInterest);
            nextPos += 1;
        }*/
    }

    /// <summary>
    /// Instansiates the point of interest and sets the location.
    /// </summary>
    /// <param name="pointOfInterest">the point of interest to instansiate</param>
    private void InstansiatePoint(PointOfInterest pointOfInterest)
    {
        PointOfInterestController pointOfInterestController = Instantiate(pointPrefab).GetComponent<PointOfInterestController>();
        pointOfInterestController.SetPointOfInterest(pointOfInterest);
        pointOfInterestControllers.Add(pointOfInterestController);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="raycastHit"></param>
    public void AddPointOfInterest(PointOfInterest pointOfInterest)
    {
        pointOfInterests.Add(pointOfInterest);
        
    }
    /*
     VisualDotDeployer visualDotDeployer = raycastHit.collider.GetComponent<VisualDotDeployer>();
        if (visualDotDeployer == null)
        {
            visualDotDeployer = raycastHit.collider.AddComponent<VisualDotDeployer>();
            visualDotDeployer.SetupVisualDot();
        }
        visualDotDeployer.AddPointOfInterest(pointOfInterest);
    */
}

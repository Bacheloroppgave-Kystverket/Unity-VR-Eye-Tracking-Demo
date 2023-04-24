using System.Collections.Generic;
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

    [SerializeField]
    private int startPos = 0;

    [SerializeField, Tooltip("The amount of diffrent point controllers that can be moved around."), Range(10, 50)]
    private int amountOfPointControllers = 10;

    [SerializeField]
    private LineController lineController;

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
        UpdateAmountOfPoints();
        List<Transform> transforms = new List<Transform>();
        pointOfInterestControllers.ForEach(controller => {
            controller.gameObject.SetActive(true);
            transforms.Add(controller.transform);
        });
        
    }

    private void UpdateAmountOfPoints() {
        List<Transform> transforms = new List<Transform>();
        for (int i = pointOfInterestControllers.Count; i < amountOfPointControllers; i++) {
            PointOfInterestController pointOfInterestController = Instantiate(pointPrefab).GetComponent<PointOfInterestController>();
            pointOfInterestControllers.Add(pointOfInterestController);
            transforms.Add(pointOfInterestController.transform);
        }
        lineController.SetTransforms(transforms);
    }

    public void UpdateOrderOfPointsOfInterest(int startValue) {
        CheckIfStartNumberIsValid(startValue);
        this.startPos = startValue;
        int index = startPos;
        IEnumerator<PointOfInterestController> it = pointOfInterestControllers.GetEnumerator();
        while (index < pointOfInterests.Count && it.MoveNext()) { 
            PointOfInterestController controller = it.Current;
            controller.SetPointOfInterest(pointOfInterests[index]);
            index++;
        }
        lineController.UpdateLines();
    }

    private void CheckIfStartNumberIsValid(int value) {
        if (value < 0 && value > pointOfInterests.Count) {
            throw new IllegalArgumentException("The start value must be larger than zero and lower than " + pointOfInterests.Count);
        }
    }



    public void ShowHeatmapPoints() {
        SyncHeatmap();
        visualDotDeployers.ForEach(visualDotDeployer => visualDotDeployer.ShowAllHeatmapPoints());
    }

    public void HideHeatmapPoints() {
        visualDotDeployers.ForEach(visualDotDeployer => visualDotDeployer.RemoveVisualDots());
    }

    public void HidePointsOfInterest() {
        pointOfInterestControllers.ForEach(pointController => pointController.HidePointOfInterest());
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

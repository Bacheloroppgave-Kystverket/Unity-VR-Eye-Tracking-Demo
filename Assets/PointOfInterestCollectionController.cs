using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.Jobs;
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

    [SerializeField, Tooltip("The line controller")]
    private LineController lineController;

    private bool showPointText;

    private bool showPointsAsSolid;

    /// <summary>
    /// Sets the show points as solid property.
    /// </summary>
    /// <param name="value">true if the point cloud should be solid. False otherwise.</param>
    public void SetShowPointsAsSolid(bool value) {
        this.showPointsAsSolid = value;
    }

    /// <summary>
    /// Sets the show point text property.
    /// </summary>
    /// <param name="showPointText">true if the gaze points should show their text. False otherwise.</param>
    public void SetShowPointText(bool showPointText) {
        this.showPointText = showPointText;
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
        pointOfInterestControllers.ForEach(controller => {
            controller.gameObject.SetActive(true);
        });
        lineController.DrawLine();
    }

    private void UpdateAmountOfPoints() {
        lineController.ClearLineList();
        for (int i = pointOfInterestControllers.Count; i < amountOfPointControllers; i++) {
            PointOfInterestController pointOfInterestController = Instantiate(pointPrefab).GetComponent<PointOfInterestController>();
            pointOfInterestControllers.Add(pointOfInterestController);
            lineController.AddTransform(pointOfInterestController.transform);
        }
        lineController.DrawLine();
    }

    /// <summary>
    /// Gets the line controller.
    /// </summary>
    /// <returns>the line controller</returns>
    public LineController GetLineController() => lineController;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="startValue"></param>
    public void UpdateOrderOfPointsOfInterest(int startValue) {
        CheckIfStartNumberIsValid(startValue);
        this.startPos = startValue;
        int index = startPos;
        lineController.ClearLineList();
        IEnumerator<PointOfInterestController> it = pointOfInterestControllers.GetEnumerator();
        while (index < pointOfInterests.Count && it.MoveNext()) { 
            PointOfInterestController controller = it.Current;
            controller.SetPointOfInterest(pointOfInterests[index]);
            index++;
            lineController.AddTransform(controller.transform);
        }
        lineController.DrawLine();
    }

    private void CheckIfStartNumberIsValid(int value) {
        if (value < 0 && value > pointOfInterests.Count) {
            throw new IllegalArgumentException("The start value must be larger than zero and lower than " + pointOfInterests.Count);
        }
    }

    /// <summary>
    /// Shows the heatmap points.
    /// </summary>
    public void ShowHeatmapPoints() {
        SyncHeatmap();
        visualDotDeployers.ForEach(visualDotDeployer => visualDotDeployer.ShowAllHeatmapPoints(showPointsAsSolid));
    }

    /// <summary>
    /// Hides the heatmap points.
    /// </summary>
    public void HideHeatmapPoints() {
        visualDotDeployers.ForEach(visualDotDeployer => visualDotDeployer.RemoveVisualDots());
    }

    /// <summary>
    /// Hides the points of interests.
    /// </summary>
    public void HidePointsOfInterest() {
        pointOfInterestControllers.ForEach(pointController => pointController.HidePointOfInterest());
        lineController.HideLine();
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

    /// <summary>
    /// Adds an order id to the last point of interest.
    /// </summary>
    /// <param name="orderId">the new order id</param>
    public bool AddOrderIdToLastPoint(int orderId) {
        bool valid = this.pointOfInterests.Count > 0;
        if (valid) {
            pointOfInterests.Last().AddOrderId(orderId);
        }
        return valid;
    }

    /// <summary>
    /// Gets the last point of interest. Returns null if the last point is not made.
    /// </summary>
    /// <returns>the point of interest or null</returns>
    public PointOfInterest GetLastPointOfInterest() { 
        return pointOfInterests.Count > 0 ? pointOfInterests.Last() : null;
    }
}

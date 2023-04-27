using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PointOfInterestCollectionController : MonoBehaviour
{
    [SerializeField]
    private List<PointRecording> pointRecordings = new List<PointRecording>();


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
        int stopValue = startValue + 10;
        SortedList<int, PointOfInterest> sortedPointsOfInterest = new SortedList<int, PointOfInterest>();
        List<PointOfInterest> pointOfInterests = SortPointsOfInterestJob.SortPoints(pointRecordings);
        lineController.ClearLineList();
        IEnumerator<PointOfInterestController> it = pointOfInterestControllers.GetEnumerator();
        while (index < pointOfInterests.Count && it.MoveNext()) { 
            PointOfInterestController controller = it.Current;
            PointOfInterest pointOfInterest = pointOfInterests[index];
            List<int> matchingIds = pointOfInterest.CheckHowManyMatches(startValue, stopValue);
            if (matchingIds.Count > 0) {
                index += matchingIds.Count;
                matchingIds.ForEach(id => sortedPointsOfInterest.Add(id, pointOfInterest));
            }
            lineController.AddTransform(controller.transform);
        }

        IEnumerator<int> orderIdIt = sortedPointsOfInterest.Keys.GetEnumerator();
        IEnumerator<PointOfInterestController> controllerIt = pointOfInterestControllers.GetEnumerator();
        while (orderIdIt.MoveNext() && controllerIt.MoveNext()) {
            controllerIt.Current.SetPointOfInterest(sortedPointsOfInterest[orderIdIt.Current], orderIdIt.Current);
        }
        lineController.DrawLine();
    }

    private void CheckIfStartNumberIsValid(int value) {
        if (value < 0 && value > pointRecordings.Count) {
            throw new IllegalArgumentException("The start value must be larger than zero and lower than " + pointRecordings.Count);
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
    private void InstansiatePoint()
    {
        PointOfInterestController pointOfInterestController = Instantiate(pointPrefab).GetComponent<PointOfInterestController>();
        pointOfInterestControllers.Add(pointOfInterestController);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="raycastHit"></param>
    public void AddPointOfInterest(PointRecording pointRecording)
    {
        pointRecordings.Add(pointRecording);
        
    }

    /// <summary>
    /// Adds an order id to the last point of interest.
    /// </summary>
    /// <param name="orderId">the new order id</param>
    public bool IncrementLastPointRecording() {
        bool valid = this.pointRecordings.Count > 0;
        if (valid) {
            pointRecordings.Last().IncrementAmountOfTimes();
        }
        return valid;
    }

    /// <summary>
    /// Gets the last point recording. Returns null if the last point is not made.
    /// </summary>
    /// <returns>the point recording or null</returns>
    public PointRecording GetLastPointRecording() { 
        return pointRecordings.Count > 0 ? pointRecordings.Last() : null;
    }
}

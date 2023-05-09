using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(RecordedPointsController))]
public class DisplayPointsOfInterestController : MonoBehaviour
{
    [SerializeField]
    private List<PointOfInterestController> pointOfInterestControllers = new List<PointOfInterestController>();

    [SerializeField, Tooltip("The start time")]
    private int startPos = 0;

    [SerializeField, Tooltip("The end time")]
    private int endPos = 0;

    [SerializeField, Tooltip("The prefab that the points of interest should have")]
    private GameObject pointPrefab;

    [SerializeField, Tooltip("The line controller")]
    private LineController lineController;

    [SerializeField, Tooltip("The amount of diffrent point controllers that can be moved around."), Range(10, 50)]
    private int amountOfPointControllers = 10;

    private RecordedPointsController recordedPointsController;

    private void Start()
    {
        recordedPointsController = GetComponent<RecordedPointsController>();
    }

    private void UpdateAmountOfPoints()
    {
        for (int i = pointOfInterestControllers.Count; i < amountOfPointControllers; i++)
        {
            PointOfInterestController pointOfInterestController = Instantiate(pointPrefab).GetComponent<PointOfInterestController>();
            pointOfInterestControllers.Add(pointOfInterestController);
        }
    }

    /// <summary>
    /// Updates the order of points of interest. 
    /// </summary>
    /// <param name="startTime"></param>
    /// <param name="stopTime"></param>
    /// <param name="showLine"></param>
    /// <param name="showPointText"></param>
    public void UpdateOrderOfPointsOfInterest(int startTime, int stopTime, bool showLine, bool showPointText)
    {
        UpdateAmountOfPoints();
        HidePointsOfInterest();
        lineController.SetShowLine(showLine);
        CheckIfStartNumberIsValid(startTime);
        this.startPos = startTime;
        this.endPos = stopTime;
        List<PointOfInterestContainer> pointOfInterests = recordedPointsController.GetPointRecordings(); //SortPoints(recordedPointsController.GetPointRecordings());
        lineController.ClearLineList();

        float totalTime = 0;
        IEnumerator<PointOfInterestContainer> pointOfInterestContainerIt = pointOfInterests.GetEnumerator();
        IEnumerator<PointOfInterestController> controllerIt = pointOfInterestControllers.GetEnumerator();
        while (pointOfInterestContainerIt.MoveNext() && totalTime <= stopTime)
        {
            PointOfInterestContainer pointOfInterestContainer = pointOfInterestContainerIt.Current;
            totalTime += pointOfInterestContainer.GetRecord().GetTime();
            if (totalTime >= startTime && controllerIt.MoveNext()) {
                PointOfInterestController pointOfInterestController = controllerIt.Current;
                pointOfInterestController.SetPointOfInterest(pointOfInterestContainer, pointOfInterestContainer.GetRecord().GetOrderId(), showPointText, GetComponent<EyetrackingPlayer>());
                pointOfInterestController.gameObject.SetActive(true);
                lineController.AddTransform(pointOfInterestController.transform);
            }
            
        }
        lineController.DrawLine();
    }

    /// <summary>
    /// Updates the look direction
    /// </summary>
    public IEnumerator UpdateLookDirection() {
        yield return new WaitForSeconds(0.5f);
        foreach (PointOfInterestController pointOfInterestController in pointOfInterestControllers) {
            if (pointOfInterestController.enabled) {
                pointOfInterestController.transform.LookAt(GetComponent<EyetrackingPlayer>().GetRaycaster().transform);
            }
        }
    }

    private void CheckIfStartNumberIsValid(int value)
    {
        if (value < 0 && value > recordedPointsController.GetPointRecordings().Count)
        {
            throw new IllegalArgumentException("The start value must be larger than zero and lower than " + recordedPointsController.GetPointRecordings().Count);
        }
    }

    /// <summary>
    /// Hides the points of interests.
    /// </summary>
    public void HidePointsOfInterest()
    {
        pointOfInterestControllers.ForEach(pointController => pointController.HidePointOfInterest());
        lineController.HideLine();
    }

    /// <summary>
    /// Gets the line controller.
    /// </summary>
    /// <returns>the line controller</returns>
    public LineController GetLineController() => lineController;
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(RecordedPointsController))]
public class DisplayPointsOfInterest : MonoBehaviour
{
    [SerializeField]
    private List<PointOfInterestController> pointOfInterestControllers = new List<PointOfInterestController>();

    [SerializeField]
    private int startPos = 0;

    [SerializeField, Tooltip("The prefab that the points of interest should have")]
    private GameObject pointPrefab;

    [SerializeField, Tooltip("The line controller")]
    private LineController lineController;

    [SerializeField, Tooltip("The amount of diffrent point controllers that can be moved around."), Range(10, 50)]
    private int amountOfPointControllers = 10;

    private bool showPointText;

    private bool showPointsAsSolid;

    private RecordedPointsController recordedPointsController;

    private void Start()
    {
        recordedPointsController = GetComponent<RecordedPointsController>();
    }

    /// <summary>
    /// Sets the show points as solid property.
    /// </summary>
    /// <param name="value">true if the point cloud should be solid. False otherwise.</param>
    public void SetShowPointsAsSolid(bool value)
    {
        this.showPointsAsSolid = value;
    }

    /// <summary>
    /// Sets the show point text property.
    /// </summary>
    /// <param name="showPointText">true if the gaze points should show their text. False otherwise.</param>
    public void SetShowPointText(bool showPointText)
    {
        this.showPointText = showPointText;
    }

    public void ShowPointsOfInterest()
    {
        UpdateAmountOfPoints();
        pointOfInterestControllers.ForEach(controller => {
            controller.gameObject.SetActive(true);
        });
        lineController.DrawLine();
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
    /// 
    /// </summary>
    /// <param name="startValue"></param>
    public void UpdateOrderOfPointsOfInterest(int startValue)
    {
        UpdateAmountOfPoints();
        CheckIfStartNumberIsValid(startValue);
        this.startPos = startValue;
        int index = 0;
        int stopValue = startValue + 10;
        SortedList<int, PointOfInterest> sortedPointsOfInterest = new SortedList<int, PointOfInterest>();
        List<PointOfInterest> pointOfInterests = new List<PointOfInterest>(); //SortPoints(recordedPointsController.GetPointRecordings());
        recordedPointsController.GetPointRecordings().ForEach(point => pointOfInterests.Add(new PointOfInterest(point)));
        lineController.ClearLineList();
    
        IEnumerator<PointOfInterest> pointOfInterestIt = pointOfInterests.GetEnumerator();
        IEnumerator<PointOfInterestController> controllerIt = pointOfInterestControllers.GetEnumerator();
        while (pointOfInterestIt.MoveNext() && controllerIt.MoveNext())
        {
            PointOfInterestController pointOfInterestController = controllerIt.Current;
            pointOfInterestController.SetPointOfInterest(pointOfInterestIt.Current, pointOfInterestIt.Current.GetLatestRecording().GetOrderId(), showPointText);
            pointOfInterestController.gameObject.SetActive(true);
            lineController.AddTransform(pointOfInterestController.transform);
        }
        lineController.DrawLine();
    }

    public List<PointOfInterest> SortPoints(List<PointRecording> newPoints)
    {
        List<PointOfInterest> newAddedPoints = new List<PointOfInterest>();
        for (int x = 0; x < newPoints.Count; x++)
        {
            PointRecording pointRecording = newPoints[x];
            newAddedPoints.Add(new PointOfInterest(pointRecording));
           
            for (int i = x; i < newPoints.Count; i++)
            {
                PointRecording pointRecordingToCompare = newPoints[i];
                if (!pointRecordingToCompare.IsSorted() && pointRecording.GetOrderId() != pointRecordingToCompare.GetOrderId())
                {
                    if (pointRecording.GetParentTransform().GetInstanceID() == pointRecordingToCompare.GetParentTransform().GetInstanceID())
                    {
                        float distance = Vector3.Distance(pointRecording.GetLocalPosition(), pointRecordingToCompare.GetLocalPosition());
                        if (distance > 0 && distance < 0.015f || distance == 0)
                        {
                            
                            newAddedPoints.Last().AddPointRecording(pointRecordingToCompare);
                        }
                    }
                }
            }
        }
        Debug.Log("Sorted " + newAddedPoints.Count);
        Debug.Log("Aactual size " + newPoints.Count);
        return newAddedPoints;
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

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(EyetrackingPlayer))]
public class PointPlacerController : MonoBehaviour, RaycasterObserver
{
    [Header("Configure object")]
    [SerializeField, Tooltip("The point of interest collection controller")]
    private RecordedPointsController pointOfInterestCollectionController;

    [Header("Frequencies of diffrent parameters")]
    [SerializeField, Tooltip("The frequency of the tracker"), Min(1)]
    private int frequency = 1;

    [SerializeField, Tooltip("The amount of samples per second"), Min(1)]
    private int heatmapFrequency = 1;

    [SerializeField, Tooltip("The frequency of the heatmap."), Min(1)]
    private int pointOfInterestFrequency = 1;

    [Header("Other fields")]
    [SerializeField, Tooltip("The current order of the points of interest"), Min(1)]
    private int orderID;

    [SerializeField, Tooltip("The current point"), Min(1)]
    private int currentPoint = 1;

    private void Start()
    {
        RayCasterObject rayCasterObject = GetComponent<EyetrackingPlayer>().GetRaycaster();
        this.frequency = rayCasterObject.GetFrequency();
        rayCasterObject.AddObserver(this);
        pointOfInterestCollectionController = GetComponent<RecordedPointsController>();
    }

    /// <summary>
    /// Adds an interest point to the list.
    /// </summary>
    /// <param name="raycastHit">the raycast hit</param>
    private void AddHeatmapPoint(RaycastHit raycastHit) {
        RecordedPoint recordedPoint = new RecordedPoint(raycastHit);
        pointOfInterestCollectionController.AddHeatmapPoint(recordedPoint);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="raycastHit"></param>
    private void AddPointRecording(RaycastHit raycastHit) {
        PointRecording pointRecording = new PointRecording(orderID, raycastHit);
        pointOfInterestCollectionController.AddPointOfInterest(pointRecording);
        orderID++;
    }

    /// <summary>
    /// Increments the point of interest that was added lately.
    /// </summary>
    private void IncrementPointOfInterest() {
        pointOfInterestCollectionController.IncrementLastPointRecording();
    }

    ///<inheritdoc/>
    public void ObservedObjects(RaycastHit[] raycastHits, Vector3 lookPosition) {
        CheckIfObjectIsNull(raycastHits, "raycast hits");
        if (raycastHits != null && raycastHits.Length > 0) {
            RaycastHit raycastHit = raycastHits.Last();
            bool addPointOfInterest = currentPoint % (frequency / pointOfInterestFrequency) == 0 && raycastHits.Length > 0;
            if (addPointOfInterest && lookPosition != Vector3.negativeInfinity)
            {
                PointRecording pointRecording = pointOfInterestCollectionController.GetLastPointRecording();
                
                if (pointRecording != null && Vector3.Distance(pointRecording.GetWorldPosition(), lookPosition) < 0.06f)
                {
                    IncrementPointOfInterest();
                }
                else
                {
                    AddPointRecording(raycastHit);
                }
            }
            if (currentPoint % (frequency / heatmapFrequency) == 0 && raycastHits.Length > 0 && lookPosition != null)
            {
                AddHeatmapPoint(raycastHit);
            }
        }
        currentPoint += 1;
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

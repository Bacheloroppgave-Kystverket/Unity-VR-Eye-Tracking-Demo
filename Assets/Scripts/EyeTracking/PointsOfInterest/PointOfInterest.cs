using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PointOfInterest
{
    [SerializeField, Tooltip("The point of interest.")]
    private List<PointRecording> pointRecordings = new List<PointRecording>();

    /// <summary>
    /// Represents a point of interest.
    /// </summary>
    /// <param name="pointOfInterestOrder">the order of the point of interest.</param>
    /// <param name="hit">the raycast hit</param>
    public PointOfInterest(PointRecording pointRecording)
    {
        CheckIfObjectIsNull(pointRecording, "point recording");
        this.pointRecordings = new List<PointRecording>();
        this.pointRecordings.Add(pointRecording);
    }

    /// <summary>
    /// Gets the point recording with the matching id.
    /// </summary>
    /// <param name="orderId">the order id</param>
    /// <returns>the point recording</returns>
    public PointRecording GetPointRecordingWithId(int orderId) {
        return pointRecordings.Find(point => point.GetOrderId() == orderId);
    }

    /// <summary>
    /// Gets all the order ids that are within that range in this point.
    /// </summary>
    /// <param name="startValue">the start value. Is inclusive</param>
    /// <param name="stopValue">the stop value. Is inclusive</param>
    /// <returns>a list with the matching ids</returns>
    public List<int> CheckHowManyMatches(int startValue, int stopValue)
    {
        List<int> matchingOrderIds = new List<int>();
        pointRecordings.ForEach(pointRecording =>
        {
            if (pointRecording.GetOrderId() >= startValue && pointRecording.GetOrderId() <= stopValue)
            {
                matchingOrderIds.Add(pointRecording.GetOrderId());
            }
        });
        return matchingOrderIds;
    }


    /// <summary>
    /// Adds an order pointRecording.
    /// </summary>
    /// <param name="pointRecording">the new order point recording</param>
    public void AddPointRecording(PointRecording pointRecording) {
        CheckIfObjectIsNull(pointRecording, "point recording");
        this.pointRecordings.Add(pointRecording);
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

    public PointRecording GetLatestRecording() => pointRecordings.Last();

    
}

using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class SortPointsOfInterestJob
{
    public static List<PointOfInterest> pointOfInterests;

    public static int result;

    public static bool isDone;

    public static List<PointState> pointStates = new List<PointState>();


    public static void MakePointStates() {
        pointStates.Clear();
        pointOfInterests.ForEach(point => pointStates.Add(new PointState(point)));
    }

    public static List<PointOfInterest> SortPoints(List<PointOfInterest> newPoints) {
        pointOfInterests = newPoints;
        MakePointStates();
        List<PointOfInterest> newAddedPoints = new List<PointOfInterest>();
        for (int x = 0; x < pointStates.Count; x++)
        {
            PointState pointState = pointStates[x];
            PointOfInterest pointOfInterest = pointState.GetPointOfInterest();
            if (!pointState.IsSorted())
            {
                newAddedPoints.Add(pointOfInterest);
                for (int i = x; i < pointOfInterests.Count; i++)
                {
                    PointState pointStateToCompare = pointStates[i];
                    PointOfInterest pointOfInterestToCompare = pointStateToCompare.GetPointOfInterest();
                    if (!pointStateToCompare.IsSorted())
                    {

                        if (pointOfInterest.GetPointOfInterestOrder() != pointOfInterestToCompare.GetPointOfInterestOrder())
                        {
                            Transform firstTransform = pointOfInterest.GetParentTransform();
                            Transform lastTransform = pointOfInterestToCompare.GetParentTransform();
                            if (firstTransform.GetInstanceID() == lastTransform.GetInstanceID())
                            {
                                float distance = Vector3.Distance(pointOfInterest.GetLocalPosition(), pointOfInterestToCompare.GetLocalPosition());
                                if (distance > 0 && distance < 0.015f || distance == 0)
                                {
                                    pointStateToCompare.SetSorted();
                                    result += 1;
                                    pointOfInterestToCompare.GetAllOrderIds().ForEach(id => pointOfInterest.AddOrderId(id));
                                }
                            }
                        }
                    }
                }
                pointState.SetSorted();
            }
        }
        result = newAddedPoints.Count;
        isDone = true;
        return newAddedPoints;
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

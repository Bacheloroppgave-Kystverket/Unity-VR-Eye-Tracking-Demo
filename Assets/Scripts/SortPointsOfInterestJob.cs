using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Jobs;
using UnityEngine;

public class SortPointsOfInterestJob
{
    public static List<PointOfInterest> pointOfInterests;

    public static int result;

    public static bool isDone;



    public static List<PointOfInterest> SortPoints(List<PointRecording> newPoints) {
        List<PointOfInterest> newAddedPoints = new List<PointOfInterest>();
        for (int x = 0; x < newPoints.Count; x++)
        {
            PointRecording pointRecording = newPoints[x];
            pointRecording.SetSorted();
            if (!pointRecording.IsSorted())
            {
                newAddedPoints.Add(new PointOfInterest(pointRecording));
                for (int i = x; i < pointOfInterests.Count; i++)
                {
                    PointRecording pointRecordingToCompare = newPoints[i];
                    if (!pointRecordingToCompare.IsSorted())
                    {

                        if (pointRecording.GetOrderId() != pointRecordingToCompare.GetOrderId())
                        {
                            Transform firstTransform = pointRecording.GetParentTransform();
                            Transform lastTransform = pointRecordingToCompare.GetParentTransform();
                            if (firstTransform.GetInstanceID() == lastTransform.GetInstanceID())
                            {
                                float distance = Vector3.Distance(pointRecording.GetLocalPosition(), pointRecordingToCompare.GetLocalPosition());
                                if (distance > 0 && distance < 0.015f || distance == 0)
                                {
                                    pointRecordingToCompare.SetSorted();
                                    result += 1;
                                    newAddedPoints.Last().AddPointRecording(pointRecordingToCompare);
                               
                                }
                            }
                        }
                    }
                }
            }
        }
        result = newAddedPoints.Count;
        Debug.Log("there are " + result);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PointState
{
    private PointOfInterest pointOfInterest;

    private bool sorted;


    /// <summary>
    /// Makes an instance of the Point state class.
    /// </summary>
    /// <param name="pointOfInterest">the point of interest</param>
    public PointState(PointOfInterest pointOfInterest)
    {
        this.pointOfInterest = pointOfInterest;
        this.sorted = false;
    }

    /// <summary>
    /// Checks if the point is sorted;
    /// </summary>
    /// <returns>True if the point is sorted. False otherwise</returns>
    public bool IsSorted() => sorted;

    /// <summary>
    /// Gets the point of interest.
    /// </summary>
    /// <returns>the point of interest</returns>
    public PointOfInterest GetPointOfInterest() => pointOfInterest;

    /// <summary>
    /// Sets the sorted variable to true.
    /// </summary>
    public void SetSorted(){
        this.sorted = true;
}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that builds the trakcalbe record that you need.
/// </summary>
public class TrackableRecordBuilder
{

    private TrackableObject trackableObject;

    private ViewDistance viewDistance = ViewDistance.FAR;

    /// <summary>
    /// Makes an instanc eof the TrackableRecordBuilder.
    /// </summary>
    /// <param name="trackableObject">the trackable object</param>
    public TrackableRecordBuilder(TrackableObject trackableObject) {
        checkIfObjectIsNull(trackableObject, "trackable object");
        this.trackableObject = trackableObject;
    }

    /// <summary>
    /// Sets the object as close.
    /// </summary>
    /// <returns>this builder</returns>
    public TrackableRecordBuilder SetAsCloseObject() {
        this.viewDistance = ViewDistance.CLOSE;
        return this;
    }

    /// <summary>
    /// Sets the view distance to far.
    /// </summary>
    /// <returns>this builder</returns>
    public TrackableRecordBuilder SetAsFarObject() {
        this.viewDistance = ViewDistance.FAR;
        return this;
    }

    /// <summary>
    /// Builds the trackable record.
    /// </summary>
    /// <returns>the trackable record</returns>
    public TrackableRecord build() {
        return new TrackableRecord(this);
    }

    /// <summary>
    /// Gets the view distance.
    /// </summary>
    /// <returns>the view distance</returns>
    public ViewDistance GetViewDistance() {
        return viewDistance;
    }

    /// <summary>
    /// Gets the trackable object.
    /// </summary>
    /// <returns>the trackable object</returns>
    public TrackableObject GetTrackableObject() => trackableObject;

    /// <summary>
    /// Checks if the object is null or not. Throws an exception if the object is null.
    /// </summary>
    /// <param name="objecToCheck">the object to check</param>
    /// <param name="error">the error to be in the string.</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the object to check is null.</exception>
    private void checkIfObjectIsNull(object objecToCheck, string error)
    {
        if (objecToCheck == null)
        {
            throw new IllegalArgumentException("The " + error + " cannot be null.");
        }
    }
}

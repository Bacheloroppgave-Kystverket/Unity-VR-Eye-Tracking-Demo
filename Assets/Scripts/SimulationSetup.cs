using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SimulationSetup {

    [SerializeField, Tooltip("The simulation setup id")]
    private long simulationSetupId;

    [SerializeField, Tooltip("The name of the setup")]
    private string nameOfSetup;

    [SerializeField, Tooltip("The trackable objects")]
    private List<TrackableObject> closeTrackableObjects = new List<TrackableObject>();

    [SerializeField, Tooltip("The reference positions")]
    private List<ReferencePosition> referencePositionList = new List<ReferencePosition>();

    /// <summary>
    /// Sets the name of the setup.
    /// </summary>
    /// <param name="nameOfSetup">the setup name</param>
    public void SetNameOfSetup(string nameOfSetup) {
        CheckIfStringIsValid(nameOfSetup, "name of setup");
        this.nameOfSetup = nameOfSetup;
    }

    /// <summary>
    /// Sets the trackable objects for the setup.
    /// </summary>
    /// <param name="trackableObjects">the trackable objects</param>
    public void SetTrackableObjects(List<TrackableObject> trackableObjects) {
        CheckIfObjectIsNull(trackableObjects, "trackable objects");
        this.closeTrackableObjects = trackableObjects;
    }

    /// <summary>
    /// Sets the reference postions.
    /// </summary>
    /// <param name="referencePositions">the reference positions</param>
    public void SetReferencePositions(List<ReferencePosition> referencePositions) {
        CheckIfObjectIsNull(referencePositions, "reference positions");
        this.referencePositionList = referencePositions;
    }

    /// <summary>
    /// Updates this simulation setup to match the input simulation setup.
    /// </summary>
    /// <param name="simulationSetup">the new simulation setup</param>
    public void UpdateSimulationSetup(SimulationSetup simulationSetup) {
        CheckIfObjectIsNull(simulationSetup, "simulation setup");
        this.simulationSetupId = simulationSetup.GetSimulationSetupId();
        foreach(TrackableObject trackableObject in simulationSetup.GetTrackableObjects()) {
            TrackableObject matchObject = this.closeTrackableObjects.Find(trackable => trackable.GetNameOfObject() == trackableObject.GetNameOfObject());
            matchObject.SetTrackableObjectId(trackableObject.GetTrackableObjectId());
        }
        foreach (ReferencePosition referencePosition in simulationSetup.GetReferencePositions()) {
            ReferencePosition referencePostionMatch = this.GetReferencePositions().Find(pos => pos.GetLocationName() == referencePosition.GetLocationName());
            referencePostionMatch.SetLocationId(referencePosition.GetLocationId());
            referencePostionMatch.SetPositionConfiguration(referencePosition.GetPositionConfiguration());
        }
    }

    /// <summary>
    /// Gets all the close trackable objects.
    /// </summary>
    /// <returns>the close trackable objects</returns>
    public List<TrackableObject> GetTrackableObjects() {
        return closeTrackableObjects;
    }

    /// <summary>
    /// Gets the reference positions.
    /// </summary>
    /// <returns>the reference positions</returns>
    public List<ReferencePosition> GetReferencePositions() {
        return referencePositionList;
    }

    /// <summary>
    /// Gets the simulation setup id.
    /// </summary>
    /// <returns>the simulation setup id</returns>
    public long GetSimulationSetupId() {
        return simulationSetupId;
    }

    /// <summary>
    /// Checks if the string is null or empty. Throws exceptions if one of these conditions are true.
    /// </summary>
    /// <param name="stringToCheck">the string to check</param>
    /// <param name="error">the error of the string</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the string to check is empty or null.</exception>
    private void CheckIfStringIsValid(string stringToCheck, string error)
    {
        CheckIfObjectIsNull(stringToCheck, error);
        if (stringToCheck.Trim().Length == 0)
        {
            throw new IllegalArgumentException("The" + error + " cannot be empty.");
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a class that can identify the trackable objects in the project during edit.
/// </summary>
[ExecuteAlways]
public class TrackableObjectIdentifier : MonoBehaviour
{
    /// <summary>
    /// Adds the trackable object to the register.
    /// </summary>
    /// <param name="simulationSetupManager">the simulation setup manager</param>
    private void AddTrackableToSimulationSetup(SimulationSetupManager simulationSetupManager)
    {
        CheckIfObjectIsNull(simulationSetupManager, "simulation setup manager");
        simulationSetupManager.AddTrackableObject(this.GetComponent<TrackableObjectController>());
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

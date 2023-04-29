using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an identifier for the reference position.
/// </summary>
[ExecuteInEditMode]
[RequireComponent(typeof(ReferencePositionController))]
public class ReferencePositionIdentifier : MonoBehaviour
{
    /// <summary>
    /// Adds the reference position to the simulation setup manager.
    /// </summary>
    /// <param name="simulationSetupManager">the simulation setup manager</param>
    public void AddPositionToSimulationSetup(SimulationSetupManager simulationSetupManager)
    {
        CheckIfObjectIsNull(simulationSetupManager, "Simulation setup");
        simulationSetupManager.AddReferencePosition(this.GetComponent<ReferencePositionController>());
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

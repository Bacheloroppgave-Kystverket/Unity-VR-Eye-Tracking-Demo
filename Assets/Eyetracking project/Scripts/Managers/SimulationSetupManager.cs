using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the simulation setup and collects all the positons and trackable objects for this simulation setup.
/// </summary>
public class SimulationSetupManager : MonoBehaviour
{
    [SerializeField, Tooltip("Current simulation setup controller")]
    private SimulationSetupController simulationSetup;

    [SerializeField, Tooltip("Set to true if the scene should update on load")]
    private bool updateSceneOnLoad;

    [SerializeField, Tooltip("The reference position manager.")]
    private ReferencePositionManager referencePositionManager;


    public void Start()
    {
        UpdateCurrentPositionsForSession();
        UpdateCurrentTrackableObjectsForSession();
    }

    
    /// <summary>
    /// Updates the current trackable objects for this session and gets them to add themselves.
    /// </summary>
    public void UpdateCurrentTrackableObjectsForSession() {
        simulationSetup.BroadcastMessage("AddTrackableToSimulationSetup", this, SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// Updates the current positions for the session and gets them to add themselves.
    /// </summary>
    public void UpdateCurrentPositionsForSession() {
        simulationSetup.BroadcastMessage("AddPositionToSimulationSetup", this, SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// Adds a reference positon controller.
    /// </summary>
    /// <param name="referencePositionController">the controller</param>
    public void AddReferencePosition(ReferencePositionController referencePositionController) {
        simulationSetup.AddRefernecePosition(referencePositionController);
    }

    /// <summary>
    /// Adds the trackable object to the session object.
    /// </summary>
    /// <param name="trackableObjectController">the controller</param>
    public void AddTrackableObject(TrackableObjectController trackableObjectController) {
        simulationSetup.AddTrackableObject(trackableObjectController);
    }
}

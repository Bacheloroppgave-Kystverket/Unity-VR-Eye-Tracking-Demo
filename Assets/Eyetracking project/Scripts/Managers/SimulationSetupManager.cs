using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    

    public void UpdateCurrentTrackableObjectsForSession() {
        simulationSetup.BroadcastMessage("AddTrackableToSimulationSetup", this, SendMessageOptions.DontRequireReceiver);
    }

    public void UpdateCurrentPositionsForSession() {
        simulationSetup.BroadcastMessage("AddPositionToSimulationSetup", this, SendMessageOptions.DontRequireReceiver);
    }

    public void AddReferencePosition(ReferencePositionController referencePositionController) {
        simulationSetup.AddRefernecePosition(referencePositionController);
    }

    /// <summary>
    /// Adds the trackable object to the session object.
    /// </summary>
    /// <param name="trackableObjectController"></param>
    public void AddTrackableObject(TrackableObjectController trackableObjectController) {
        simulationSetup.AddTrackableObject(trackableObjectController);
    }
}

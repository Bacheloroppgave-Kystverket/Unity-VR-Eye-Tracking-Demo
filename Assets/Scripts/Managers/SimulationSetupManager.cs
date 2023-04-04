using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SimulationSetupManager : MonoBehaviour
{
    [SerializeField, Tooltip("Current session controller")]
    private SessionController currentSessionController;

    [SerializeField, Tooltip("Set to true if the scene should update on load")]
    private bool updateSceneOnLoad;

    [SerializeField, Tooltip("The trackable objects")]
    private List<TrackableObjectController> trackableObjects;

    [SerializeField, Tooltip("The reference positions")]
    private List<ReferencePositionController> referencePositionControllers;


    public void Start()
    {
        if (updateSceneOnLoad)
        {
            UpdateCurrentTrackableObjectsForSession();
        }
    }

    public void UpdateCurrentTrackableObjectsForSession() {
        this.trackableObjects = currentSessionController.GetCloseTrackableObjects();
        currentSessionController.BroadcastMessage("AddTrackableToSimulationSetup", this, SendMessageOptions.DontRequireReceiver);
    }

    public void UpdateCurrentPositionsForSession() {
        this.referencePositionControllers = currentSessionController.GetReferencePositions();
        currentSessionController.BroadcastMessage("AddPositionToSimulationSetup", this, SendMessageOptions.DontRequireReceiver);
    }

    public void AddReferencePosition(ReferencePositionController referencePositionController) {
        currentSessionController.AddRefernecePosition(referencePositionController);
    }

    /// <summary>
    /// Adds the trackable object to the session object.
    /// </summary>
    /// <param name="trackableObjectController"></param>
    public void AddTrackableObject(TrackableObjectController trackableObjectController) {
        currentSessionController.AddTrackableObejct(trackableObjectController);
    }
}

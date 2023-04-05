
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Represents a sessionController that is done by a user. 
/// Should be placed on an object that represents the main location that has all the reference positions.
/// </summary>
[RequireComponent(typeof(SimulationSetupController))]
public class SessionController : MonoBehaviour {

    [SerializeField, Tooltip("The session")]
    private Session session;

    [SerializeField, Tooltip("The ray caster object")]
    private RayCasterObject rayCasterObject;

    [Space(10), Header("Other")]
    [SerializeField, Tooltip("The list of all objects that are far away and has been observed.")]
    private List<TrackableObjectController> otherTrackableObjects = new List<TrackableObjectController>();

    [SerializeField, Tooltip("The networking that sends the session")]
    private ServerRequest<Session> sendData;

    

    /// <summary>
    /// Gets the raycaster object.
    /// </summary>
    /// <returns>the raycaster object</returns>
    public RayCasterObject GetRayCasterObject() => rayCasterObject;

    private void Start() {
        sendData.SetData(session);
        //closeTrackableObjects = GetComponentsInChildren<TrackableObjectController>().ToList();
        //referencePositions = GetComponentsInChildren<ReferencePosition>().ToList();
        
        CheckField("Ray caster object", rayCasterObject);
        //SetSessionData();
    }

    public void SetSessionData() {
        List<ReferencePosition> references = new List<ReferencePosition>();
        SimulationSetupController simulationSetupController = GetComponent<SimulationSetupController>();
        session.AddReferencePositions(simulationSetupController.GetReferencePositions());
        session.SetSimulationSetup(simulationSetupController.GetSimulationSetup());
        AddAllTrackableObjectsToSession(simulationSetupController.GetCloseTrackableObjects(), ViewDistance.CLOSE);
        AddAllTrackableObjectsToSession(otherTrackableObjects, ViewDistance.FAR);
    }

    /// <summary>
    /// Adds all teh trackable objects to the sessionController.
    /// </summary>
    /// <param name="objectsToAdd">the list of objects to add</param>
    /// <param name="viewDistance">the distance to these objects.</param>
    private void AddAllTrackableObjectsToSession(List<TrackableObjectController> objectsToAdd, ViewDistance viewDistance) {
        List<TrackableObject> trackables = new List<TrackableObject>();
        objectsToAdd.ForEach(trackableObjectController => {
            TrackableObject trackableObject = trackableObjectController.GetTrackableObject();
            trackables.Add(trackableObject);
        });
        session.AddTrackableObjects(objectsToAdd, viewDistance);
    }

    

    public void SendSessionAndSimulationSetup() {
        StartCoroutine(SendSimulationSetupAndThenSession());
    }

    /// <summary>
    /// Sends the simulation setup and then the session.
    /// </summary>
    /// <returns>the enumerator</returns>
    private IEnumerator SendSimulationSetupAndThenSession() {
        GetComponent<SimulationSetupController>().SendSimulationSetup();
        yield return new WaitForSeconds(10);
        SendSession();
    }

    /// <summary>
    /// Sends the sessionController to the backend.
    /// </summary>
    public void SendSession() {
        sendData.SetData(session);
        StartCoroutine(sendData.SendCurrentData());
    }

    

    

    

    /// <summary>
    /// Checks if the defined field is set in the editor.
    /// </summary>
    /// <param name="error">the type of error like "type of object"</param>
    /// <param name="fieldToCheck">the field to check</param>
    private bool CheckField(string error, object fieldToCheck)
    {
        bool valid = fieldToCheck == null;
        if (valid)
        {
            Debug.Log("<color=red>Error:</color>" + error + " must be set.", gameObject);
        }
        return valid;
    }

    


    /// <summary>
    /// Adds the other objects to the other objects collection if they are not in the close trackable objects list.
    /// </summary>
    /// <param name="otherObjects">the list with the other objects</param>
    public void AddOtherObjects(List<TrackableObjectController> otherObjects) {
        List<TrackableObjectController> closeObjects = GetComponent<SimulationSetupController>().GetCloseTrackableObjects();
        foreach (TrackableObjectController otherObject in otherObjects) {
            if (!closeObjects.Contains(otherObject) && !otherObjects.Contains(otherObject)) {
                otherObjects.Add(otherObject);
            }
        }
    }

    /// <summary>
    /// Adds a adaptiveFeedback to the sessionController.
    /// </summary>
    /// <param name="adaptiveFeedback">the adaptiveFeedback to add</param>
    /// <param name="referencePosition">the reference position</param>
    public PositionRecord GetPositionRecord(ReferencePosition referencePosition) {
        return session.GetReferenceRecording(referencePosition);
    }

    /// <summary>
    /// Gets the simulation setup component.
    /// </summary>
    /// <returns>the simulation setup controller</returns>
    public SimulationSetupController GetSimulationSetupController()
    {
        return GetComponent<SimulationSetupController>();
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

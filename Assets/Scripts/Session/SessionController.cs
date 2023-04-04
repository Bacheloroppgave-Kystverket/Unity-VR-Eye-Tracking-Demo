
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Represents a sessionController that is done by a user. 
/// Should be placed on an object that represents the main location that has all the reference positions.
/// </summary>
[ExecuteAlways]
public class SessionController : MonoBehaviour {

    [SerializeField, Tooltip("The session")]
    private Session session;

    [SerializeField, Tooltip("The ray caster object")]
    private RayCasterObject rayCasterObject;

    [SerializeField, Tooltip("The reference positions of this session.")]
    private List<ReferencePositionController> referencePositions = new List<ReferencePositionController>();

    [Space(10), Header("Other")]
    [SerializeField, Tooltip("The list of all objects that are far away and has been observed.")]
    private List<TrackableObjectController> otherTrackableObjects = new List<TrackableObjectController>();

    [SerializeField, Tooltip("The list of all the trackable objects that are close to this user.")]
    private List<TrackableObjectController> closeTrackableObjects = new List<TrackableObjectController>();

    [SerializeField, Tooltip("The simulation setup")]
    private SimulationSetup simulationSetup;

    [SerializeField, Tooltip("The networking that sends the session")]
    private ServerRequest<Session> sendData;

    [SerializeField, Tooltip("The simulation setup sender")]
    private ServerRequest<SimulationSetup> simulationSetupSend;

    /// <summary>
    /// Gets the raycaster object.
    /// </summary>
    /// <returns>the raycaster object</returns>
    public RayCasterObject GetRayCasterObject() => rayCasterObject;

    private void Start() {
        sendData.SetData(session);
        //closeTrackableObjects = GetComponentsInChildren<TrackableObjectController>().ToList();
        //referencePositions = GetComponentsInChildren<ReferencePosition>().ToList();
        CheckIfListIsValid("Close trackable objects", closeTrackableObjects.Any());
        CheckIfListIsValid("Reference positions", referencePositions.Any());
        CheckField("Ray caster object", rayCasterObject);
        SetSessionData();
    }

    public void SetSessionData() {
        List<ReferencePosition> references = new List<ReferencePosition>();
        referencePositions.ForEach(positionController => references.Add(positionController.GetReferencePosition()));
        session.AddReferencePositions(referencePositions);
        simulationSetup.SetReferencePositions(references);
        session.SetSimulationSetup(simulationSetup);
        AddAllTrackableObjectsToSession(closeTrackableObjects, ViewDistance.CLOSE);
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
        if (viewDistance == ViewDistance.CLOSE) {
            simulationSetup.SetTrackableObjects(trackables);
        }
        session.AddTrackableObjects(objectsToAdd, viewDistance);
    }

    public void AddTrackableObejct(TrackableObjectController trackableObjectController) {
        CheckIfObjectIsNull(trackableObjectController, "trackable object");
        if (!closeTrackableObjects.Contains(trackableObjectController)) {
            closeTrackableObjects.Add(trackableObjectController);
        }
    }

    public void AddRefernecePosition(ReferencePositionController referencePositionController) {
        CheckIfObjectIsNull(referencePositionController, "refernece position controller");
        if (!referencePositions.Contains(referencePositionController)) { 
            referencePositions.Add(referencePositionController);
        }
    }

    public void SendSessionAndSimulationSetup() {
        StartCoroutine(SendSessioonAndThenSimulationSetup());
    }

    private IEnumerator SendSessioonAndThenSimulationSetup() {
        SendSimulationSetup();
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

    public void SendSimulationSetup() {
        simulationSetup.SetNameOfSetup(gameObject.name);

        MonoBehaviour.print("oig");
        simulationSetupSend.SetData(simulationSetup);
        StartCoroutine(SendAndUpdateSimulaitonSetup());
    }

    private IEnumerator SendAndUpdateSimulaitonSetup() {
        StartCoroutine(simulationSetupSend.SendCurrentData());
        yield return new WaitForSeconds(5);
        StartCoroutine(simulationSetupSend.SendGetRequest());
    }

    /// <summary>
    /// Gets the reference positions.
    /// </summary>
    /// <returns>the reference positions</returns>
    public List<ReferencePositionController> GetReferencePositions() => referencePositions;

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
    /// Checks if the list has any elements.
    /// </summary>
    /// <param name="error">the error to display</param>
    /// <param name="hasAny">true if the list has any. False otherwise</param>
    private void CheckIfListIsValid(string error, bool hasAny)
    {
        if (!hasAny)
        {
            Debug.Log("<color=red>Error:</color>" + error + " cannot be emtpy", gameObject);
        }
    }


    /// <summary>
    /// Adds the other objects to the other objects collection if they are not in the close trackable objects list.
    /// </summary>
    /// <param name="otherObjects">the list with the other objects</param>
    public void AddOtherObjects(List<TrackableObjectController> otherObjects) {
        foreach (TrackableObjectController otherObject in otherObjects) {
            if (!closeTrackableObjects.Contains(otherObject) && !otherObjects.Contains(otherObject)) {
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
    /// Gets all the trackable objects.
    /// </summary>
    /// <returns>the trackable objects that are close</returns>
    public List<TrackableObjectController> GetCloseTrackableObjects() => closeTrackableObjects;

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

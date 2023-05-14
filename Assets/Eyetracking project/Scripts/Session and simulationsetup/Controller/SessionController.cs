
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Represents a sessionController that is done by a user. 
/// Should be placed on an object that represents the main location that has all the reference positions.
/// </summary>
[RequireComponent(typeof(SimulationSetupController), typeof(AuthenticationController))]
public class SessionController : MonoBehaviour {

    [SerializeField, Tooltip("The session")]
    private Session session;

    [SerializeField, Tooltip("The player")]
    private EyetrackingPlayer player;

    [Space(10), Header("Other")]
    [SerializeField, Tooltip("The list of all objects that are far away and has been observed.")]
    private List<TrackableObjectController> otherTrackableObjects = new List<TrackableObjectController>();

    [SerializeField, Tooltip("The networking that sends the session")]
    private ServerRequest<Session> sendData;

    /// <summary>
    /// Gets the raycaster object.
    /// </summary>
    /// <returns>the raycaster object</returns>
    public RayCasterObject GetRayCasterObject() => player.GetRaycaster();

    private void Start() {
        sendData.SetData(session);
        session.SetSimulationSetup(GetComponent<SimulationSetupController>().GetSimulationSetup());
        CheckField("Player", player);
    }

    private void OnApplicationQuit()
    {
        session.ClearLists();

    }

    /// <summary>
    /// Sets the path and port of this request.
    /// </summary>
    /// <param name="path">the path</param>
    /// <param name="port">the port</param>
    public void SetPathAndPort(string path, int port)
    {
        this.sendData.SetPathAndPort(path, port);
    }

    /// <summary>
    /// Adds the trackable object to the session.
    /// </summary>
    /// <param name="trackableObjectController">the trackable object controller</param>
    /// <param name="viewDistance">the distance to these objects.</param>
    public void AddTrackableObject(TrackableObjectController trackableObjectController, ViewDistance viewDistance) {
        session.AddTrackableObject(trackableObjectController, viewDistance);
    }

    public void AddReferencePosition(ReferencePositionController referencePositionController) {
        session.AddReferencePosition(referencePositionController);
    }

    /// <summary>
    /// Sends the sessionController to the backend.
    /// </summary>
    public void SendSession() {
        sendData.SetData(session);
        StartCoroutine(sendData.SendCurrentData(GetComponent<AuthenticationController>().GetToken()));
    }

    /// <summary>
    /// Gets the session
    /// </summary>
    /// <returns>the session</returns>
    public Session GetSession() => session;

    

    

    

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

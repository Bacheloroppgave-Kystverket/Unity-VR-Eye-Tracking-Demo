using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Represents a simulation setup controller.
/// </summary>
[RequireComponent(typeof(SessionController))]
public class SimulationSetupController : MonoBehaviour
{
    [SerializeField, Tooltip("The simulation setup")]
    private SimulationSetup simulationSetup;

    [SerializeField, Tooltip("The reference positions of this session.")]   
    private List<ReferencePositionController> referencePositions = new List<ReferencePositionController>();

    [SerializeField, Tooltip("The list of all the trackable objects that are close to this user.")]
    private List<TrackableObjectController> closeTrackableObjects = new List<TrackableObjectController>();

    [SerializeField, Tooltip("The simulation setup sender")]
    private ServerRequest<SimulationSetup> simulationSetupSend;

    private void Start()
    {
        closeTrackableObjects.Clear();
        referencePositions.Clear();
        simulationSetup.ClearLists();
    }

    /// <summary>
    /// Gets the simulation setup.
    /// </summary>
    /// <returns>the simulation setup.</returns>
    public SimulationSetup GetSimulationSetup() {
        return simulationSetup;
    }

    /// <summary>
    /// Adds a trackable object to the controller and serialized data.
    /// </summary>
    /// <param name="trackableObjectController">the trackable object to add</param>
    public void AddTrackableObject(TrackableObjectController trackableObjectController)
    {
        CheckIfObjectIsNull(trackableObjectController, "trackable object");
        if (!closeTrackableObjects.Contains(trackableObjectController))
        {
            closeTrackableObjects.Add(trackableObjectController);
            simulationSetup.AddTrackableObject(trackableObjectController, ViewDistance.CLOSE);
        }
        GetComponent<SessionController>().AddTrackableObject(trackableObjectController, ViewDistance.CLOSE);
    }

    public void AddRefernecePosition(ReferencePositionController referencePositionController)
    {
        CheckIfObjectIsNull(referencePositionController, "refernece position controller");
        if (!referencePositions.Contains(referencePositionController))
        {
            referencePositions.Add(referencePositionController);
            simulationSetup.AddReferencePosition(referencePositionController);
        }
        GetComponent<SessionController>().AddReferencePosition(referencePositionController);
    }

    /// <summary>
    /// Sends the simulation setup to the backend.
    /// </summary>
    public void SendSimulationSetup()
    {
        simulationSetup.SetNameOfSetup(gameObject.name);
        simulationSetupSend.SetData(simulationSetup);
        StartCoroutine( SendAndUpdateSimulaitonSetup());
    }

    /// <summary>
    /// Sends the simulation setup to the backend and updates the values of it.
    /// </summary>
    /// <returns>the enumerator</returns>
    private IEnumerator SendAndUpdateSimulaitonSetup()
    {
        simulationSetupSend.SetPathVariable(simulationSetup.GetNameOfSetup());
        yield return simulationSetupSend.SendGetRequest();
        if (!simulationSetupSend.GetSuccessful()) {
            simulationSetupSend.SetPost();
            yield return simulationSetupSend.SendCurrentData();
            yield return new WaitForSeconds(1);
            
            yield return simulationSetupSend.SendGetRequest();
        }
        GetComponent<SessionController>().SendSession();
        
        
    }


    /// <summary>
    /// Gets all the trackable objects.
    /// </summary>
    /// <returns>the trackable objects that are close</returns>
    public List<TrackableObjectController> GetCloseTrackableObjects() => closeTrackableObjects;

    /// <summary>
    /// Gets the reference positions.
    /// </summary>
    /// <returns>the reference positions</returns>
    public List<ReferencePositionController> GetReferencePositions() => referencePositions;

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

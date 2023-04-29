using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReferencePositionManager : MonoBehaviour
{

    [SerializeField, Tooltip("Set to true if eyetracking is active. False otherwise.")]
    private bool currentlyTracking;

    [SerializeField, Tooltip("The trackable objects manager.")]
    private TrackableObjectsManager trackableObjectsManager;

    [SerializeField, Tooltip("The session manager")]
    private SessionManager sessionManager;

    [SerializeField, Tooltip("The current reference position")]
    private ReferencePositionController currentReferencePositon;

    [SerializeField, Tooltip("The amount of seconds to wait when changing position")]
    private float secondsToWait = 2f;

    // Start is called before the first frame update
    void Start(){
        CheckField("Trackable Object Manager", trackableObjectsManager);
        
        if (currentReferencePositon == null) {
            SetPosition(sessionManager.GetSessionController().GetSimulationSetupController().GetReferencePositions().First());
        }
        trackableObjectsManager.UpdatePositionOnAllTrackableObjects(GetCurrentReferencePosition().GetReferencePosition());
    }

    /// <summary>
    /// Checks if the defined field is set in the editor.
    /// </summary>
    /// <param name="error">the type of error like "type of object"</param>
    /// <param name="fieldToCheck">The field to check</param>
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
            Debug.Log("<color=red>Error:</color>" + error + " cannot be emtpy.", gameObject);
        }
    }

    /// <summary>
    /// Gets the currernt reference position.
    /// </summary>
    /// <returns>the current reference position</returns>
    public ReferencePositionController GetCurrentReferencePosition() {
        return currentReferencePositon;
    }


    public IEnumerator<ReferencePositionController> GetEnumeratorForReferencePositions() {
        return sessionManager.GetSessionController().GetSimulationSetupController().GetReferencePositions().GetEnumerator();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyTracking) {
            GetCurrentReferencePosition().AddTime();
        }
    }

    /// <summary>
    /// Goes to the next reference position.
    /// </summary>
    /// <param name="referencePositionController">the reference position controller</param>
    public void SetPosition(ReferencePositionController referencePositionController){
        CheckIfObjectIsNull(referencePositionController, "reference position controller");
        if (currentlyTracking) {
            sessionManager.PauseEyeTrackingForNSeconds(secondsToWait);
        }
        if (currentReferencePositon != null) {
            Collider collider = currentReferencePositon.gameObject.GetComponent<Collider>();
            if (collider != null) {
                collider.enabled = true;
            }
        }
        this.currentReferencePositon = referencePositionController;
        Collider currentCollider = currentReferencePositon.GetComponent<Collider>();
        if (currentCollider != null) { 
            currentCollider.enabled = false;
        }
        
        trackableObjectsManager.UpdatePositionOnAllTrackableObjects(GetCurrentReferencePosition().GetReferencePosition());
        
    }

    /// <inheritdoc/>
    public void StartEyeTracking()
    {
        currentlyTracking = true;
    }

    /// <inheritdoc/>
    public void StopEyeTracking(){
        currentlyTracking = false;
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

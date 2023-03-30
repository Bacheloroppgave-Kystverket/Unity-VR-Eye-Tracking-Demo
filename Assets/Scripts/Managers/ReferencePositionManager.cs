using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReferencePositionManager : MonoBehaviour
{
    [SerializeField, Tooltip("The player object")]
    private GameObject playerObject;

    [SerializeField, Tooltip("Set to true if eyetracking is active. False otherwise.")]
    private bool currentlyTracking;

    [SerializeField, Tooltip("The trackable objects manager.")]
    private TrackableObjectsManager trackableObjectsManager;

    [SerializeField, Tooltip("The session manager")]
    private SessionManager sessionManager;

    [SerializeField, Tooltip("Position")]
    private int position;

    [SerializeField, Tooltip("The amount of seconds to wait when changing position")]
    private float secondsToWait = 2f;

    // Start is called before the first frame update
    void Start()
    {
        CheckField("Player", playerObject);
        CheckField("Trackable Object Manager", trackableObjectsManager);
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
        return sessionManager.GetSession().GetReferencePositions()[position];
    }


    public IEnumerator<ReferencePositionController> GetEnumeratorForReferencePositions() {
        return sessionManager.GetSession().GetReferencePositions().GetEnumerator();
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
    public void NextPosition(){
        sessionManager.PauseEyeTrackingForNSeconds(secondsToWait);
        position = (position + 1) % sessionManager.GetSession().GetReferencePositions().Count;
        playerObject.gameObject.transform.position = GetCurrentReferencePosition().gameObject.transform.position;
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
}

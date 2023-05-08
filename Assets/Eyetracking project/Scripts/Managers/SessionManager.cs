using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Scripting;

/// <summary>
/// Represents a sessionController that a user is doing each time they put on the headset.
/// </summary>
public class SessionManager : MonoBehaviour
{
    [SerializeField, Tooltip("The session that this is.")]
    private SessionController sessionController;

    [SerializeField, Tooltip("The main raycaster object")]
    private RayCasterObject rayCasterObject;

    [SerializeField]
    private ReferencePositionManager referencePositionManager;

    [SerializeField]
    private FeedbackManager feedbackManager;

    private float pauseDuration;

    private bool isPaused;

    [SerializeField]
    private bool eyetracking = false;

    // Start is called before the first frame update
    void Start() {
        CheckField("Session", sessionController);
        CheckField("Raycaster object", rayCasterObject);
    }

    public void SendData() {
        StopEyeTracking();
        sessionController.SendSession();
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

    public void PauseEyeTrackingForNSeconds(float duration) {
        if (!isPaused && duration > 0) {
            isPaused = true;
            pauseDuration = duration;
            StartCoroutine(PauseEyeTracking());
        }
    }

    /// <summary>
    /// Pauses the eyetracking for a set duration in the class.
    /// </summary>
    /// <returns></returns>
    private IEnumerator PauseEyeTracking() {
        StopEyeTracking();
        yield return new WaitForSeconds(pauseDuration);
        StartEyeTracking();
        isPaused = false;
    }

    public void ToggleEyeTracking() {
        if (eyetracking)
        {
            StopEyeTracking();
        }
        else {
            StartEyeTracking();
        }
    }

    /// <summary>
    /// Starts the eye tracking.
    /// </summary>
    public void StartEyeTracking() {
        eyetracking = !eyetracking;
        StartCoroutine(StartTracking());
    }

    public IEnumerator StartTracking() {
        yield return new WaitForSeconds(1);

        if (eyetracking) {
            referencePositionManager.StartEyeTracking();
            sessionController.GetRayCasterObject().StartTracking();
            feedbackManager.StartEyetracking();
        }
    }

    /// <summary>
    /// Stops the eye tracking.
    /// </summary>
    public void StopEyeTracking() {
        eyetracking = !eyetracking;
        referencePositionManager.StopEyeTracking();
        sessionController.GetRayCasterObject().StopEyeTracking();
        feedbackManager.StopEyeTracking();

    }

    /// <summary>
    /// Gets the sessionController.
    /// </summary>
    /// <returns>the current sessionController</returns>
    public SessionController GetSessionController() => sessionController;

    /// <summary>
    /// Checks if a string field is valid.
    /// </summary>
    /// <param name="error">the error</param>
    /// <param name="stringToCheck">the string to check</param>
    private void CheckStringField(string error,string stringToCheck) {
        if (!CheckField(error, stringToCheck) && stringToCheck.Trim() == "") {
            Debug.Log("<color=red>Error:</color>" + error + " cannot be empty for " + gameObject.name, gameObject);
        }
    }

    /// <summary>
    /// Checks if the defined field is set in the editor.
    /// </summary>
    /// <param name="error">the type of error like "type of object"</param>
    /// <param name="fieldToCheck">The field to check</param>
    private bool CheckField(string error, object fieldToCheck) {
        bool valid = fieldToCheck == null;
        if (valid) {
            Debug.Log("<color=red>Error:</color>" + error + " must be set.", gameObject);
        }
        return valid;
    }


}

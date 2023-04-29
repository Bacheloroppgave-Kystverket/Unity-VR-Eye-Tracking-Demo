using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuController : MonoBehaviour
{

    [SerializeField, Tooltip("The session manager")]
    private SessionManager sessionManager;

    [SerializeField, Tooltip("True if the eyetracking is on. False otherwise")]
    private bool eyetrackingActive = false;

    [SerializeField, Tooltip("True if the eyetracking should be stopped. False otherwise")]
    private bool stopEyetracking = false;

    /// <summary>
    /// Toggles if the eyetracking is active from the corrensponding toggle button.
    /// </summary>
    public void ToggleEyetrackingActive() {
        eyetrackingActive = !eyetrackingActive;
    }

    /// <summary>
    /// Disables the eyetracking.
    /// </summary>
    public void DisableEyeTracking() {
        if (stopEyetracking && eyetrackingActive) {
            sessionManager.StopEyeTracking();
        }
    }

    /// <summary>
    /// Starts the eyetracking.
    /// </summary>
    public void StartEyeTracking() {
        if (stopEyetracking && eyetrackingActive) {
            sessionManager.StartEyeTracking();
        }
    }
}

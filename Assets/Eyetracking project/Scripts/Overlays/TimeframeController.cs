using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A controller that controls the time frame of the points of interest.
/// </summary>
public class TimeframeController : MonoBehaviour
{
    [SerializeField, Tooltip("The minimum increment controller")]
    private IncrementController minIncrementController;

    [SerializeField, Tooltip("The max increment controller")]
    private IncrementController maxIncrementController;

    [SerializeField, Tooltip("The session manager")]
    private SessionManager sessionManager;

    [SerializeField, Tooltip("The points of interest manager")]
    private PointOfInterestManager pointOfInterestManager;

    [SerializeField, Tooltip("The default position")]
    private Transform defaultPosition;

    private bool showOverlay = false;

    /// <summary>
    /// Toggles the visibility of this tablet.
    /// </summary>
    public void ToggleVisibility() { 
        showOverlay = !showOverlay;
        if (showOverlay) {
            ShowTimeframeController();
            float maxValue = pointOfInterestManager.GetAmountOfPointsOfInterest();
        }
        else {
            HideTimeframeController();
        }
    }

    /// <summary>
    /// Shows the time frame controller.
    /// </summary>
    private void ShowTimeframeController() { 
        gameObject.SetActive(true);
        float value = sessionManager.GetSessionController().GetSession().GetTotalTime();
        maxIncrementController.SetMaxValue(value);
        minIncrementController.SetMaxValue(value);
        transform.position = defaultPosition.position;
        MonoBehaviour.print(defaultPosition.childCount + defaultPosition.gameObject.name);
        transform.rotation = Quaternion.Euler(135, 0, 0);
    }

    /// <summary>
    /// Hides the time frame controller.
    /// </summary>
    private void HideTimeframeController() {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Updates the start and stop values.
    /// </summary>
    public void UpdateStartAndStopValues() {
        this.pointOfInterestManager.SetStartAndEnd(minIncrementController.GetCurrentValue(), maxIncrementController.GetCurrentValue());
    }
}

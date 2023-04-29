using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void ToggleVisibility() { 
        showOverlay = !showOverlay;
        if (showOverlay) {
            ShowTimeframeController();
        }
        else {
            HideTimeframeController();
        }
    }

    private void ShowTimeframeController() { 
        gameObject.SetActive(true);
        float value = sessionManager.GetSessionController().GetSession().GetTotalTime();
        maxIncrementController.SetMaxValue(value);
        minIncrementController.SetMaxValue(value);
        transform.position = defaultPosition.position;
        transform.rotation = Quaternion.Euler(135, 0, 0);
    }

    private void HideTimeframeController() {
        gameObject.SetActive(false);
    }

    public void UpdateStartAndStopValues() {
        this.pointOfInterestManager.SetStartAndEnd(minIncrementController.GetCurrentValue(), maxIncrementController.GetCurrentValue());
    }
}

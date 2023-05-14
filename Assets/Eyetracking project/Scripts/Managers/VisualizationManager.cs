using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds diffrent visaulization methods for the gaze vecotor and its hitpoints.
/// </summary>
public class VisualizationManager : MonoBehaviour
{
    private bool activeHitspotRaw;

    /// <summary>
    /// Toggles the showOverlay state for the hitspot.
    /// </summary>
    public void ToggleHitspot() {
        GameObject hitspot = GetHitspot();
        if (hitspot != null) {
            hitspot.SetActive(!hitspot.activeSelf);
        }
    }

    /// <summary>
    /// Toggles if the raw position should be shown.
    /// </summary>
    public void ToggleRawPosition() {
        GameObject hitspot = GetHitspot();
        if (hitspot != null)
        {
            activeHitspotRaw = !activeHitspotRaw;
            hitspot.GetComponent<HitpointController>().SetRawHitpointActive(activeHitspotRaw);
        }
    }

    /// <summary>
    /// Finds the hitspot point.
    /// </summary>
    /// <returns>the hitspot.</returns>
    private GameObject GetHitspot()
    {
        return GameObject.FindGameObjectWithTag("hitspot");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizationManager : MonoBehaviour
{

    /// <summary>
    /// Toggles the active state for the hitspot.
    /// </summary>
    public void ToggleHitspot() {
        GameObject hitspot = GameObject.FindGameObjectWithTag("hitspot");
        if (hitspot != null) {
            hitspot.SetActive(!hitspot.activeSelf);
        }
    }
}

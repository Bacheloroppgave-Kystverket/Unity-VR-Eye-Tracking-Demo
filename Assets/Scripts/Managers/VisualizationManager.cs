using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizationManager : MonoBehaviour
{

    /// <summary>
    /// Toggles the active state for the hitspot.
    /// </summary>
    public void ToggleHitspot() {
        GameObject hitspot = GetHitspot();
        if (hitspot != null) {
            hitspot.SetActive(!hitspot.activeSelf);
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

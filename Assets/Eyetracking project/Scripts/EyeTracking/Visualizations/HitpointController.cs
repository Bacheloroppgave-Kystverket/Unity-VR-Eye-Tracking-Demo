using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitpointController : MonoBehaviour
{
    [SerializeField, Tooltip("The hitpoints gameobject")]
    private GameObject hitpoint;

    [SerializeField, Tooltip("The projector")]
    private GameObject projector;

    /// <summary>
    /// Sets the position of the hitpoint.
    /// </summary>
    /// <param name="position">the position</param>
    public void SetHitpointPosition(Vector3 position) {
        if (hitpoint.gameObject.activeSelf) {
            this.hitpoint.transform.position = position;
        }
    }

    /// <summary>
    /// Sets the raw hitpoint property.
    /// </summary>
    /// <param name="active">true if the raw hitpoint should be viusalized. False otherwise.</param>
    public void SetRawHitpointActive(bool active) {
        hitpoint.gameObject.SetActive(active);
    }

    /// <summary>
    /// Gets the projector
    /// </summary>
    /// <returns>the projector</returns>
    public GameObject GetProjector() {
        return projector;
    }

    /// <summary>
    /// Changes if the hitspot is active.
    /// </summary>
    /// <param name="active">true if the hitspot should show. False otherwise.</param>
    public void SetHitpointActive(bool active) { 
        gameObject.SetActive(active);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyetrackingPlayer : MonoBehaviour{

    [SerializeField, Tooltip("The eyes of the player")]
    private RayCasterObject eyes;

    [SerializeField, Tooltip("The eye gun.")]
    private RayCasterObject eyeGun;

    [SerializeField, Tooltip("Set to true if the gun is the 'tracker'. False otherwise")]
    private bool useGun;

    /// <summary>
    /// Sets the use gun to a new value.
    /// </summary>
    /// <param name="useGun">the use gun</param>
    public void SetUseGun(bool useGun) {
        this.useGun = useGun;
    }

    /// <summary>
    /// Gets the eyetracker that is showOverlay for this player.
    /// </summary>
    /// <returns>the raycaster object</returns>
    public RayCasterObject GetRaycaster() => useGun ? eyeGun : eyes;
}

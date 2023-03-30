using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PlayerEyetrackingConfig : MonoBehaviour {
    [SerializeField, Tooltip("The eyes of the player. Must contain one or two child objects with OVReyeGaze")]
    private EyeCaster eyeCaster;

    [Space(5), Header("Eyes")]
    [SerializeField, Tooltip("Set to true if the oculus eye tracking should be enabled.")]
    private bool oculusEyeTracking;

    [SerializeField, Range(0, 1), Tooltip("The threshold that the eyetracker should have to give out data.")]
    private float confidenceThreshold;

    [Space(5),SerializeField, Header("The range of the eyes."), Range(0, 100)]
    private int range;

    [SerializeField, Tooltip("The size of the spehere that is used in eyetracking.")]
    private float sphereSize;

    [SerializeField, Tooltip("The ferquency of the tracking.")]
    private int frequency;


    [Space(5), Header("Debugging lists")]
    [SerializeField, Tooltip("The current objects that are being watched.")]
    private List<TrackableObjectController> currentTrackableObjects;

    [SerializeField, Tooltip("The last objects that where viewed.")]
    private List<TrackableObjectController> lastObjects;

    private void Start() {
        CheckField("Eyecaster", eyeCaster);
        UpdatePlayer();
        this.currentTrackableObjects = eyeCaster.GetCurrentObjectsWatched();
        this.lastObjects = eyeCaster.GetLastObjects();
    }

    public void UpdatePlayer() {
        UpdateEyes();
        UpdateEyeCaster();
    }

    /// <summary>
    /// Updates the eyes and their parameters.
    /// </summary>
    private void UpdateEyes() {
        eyeCaster.SetConfidenceThreshold(confidenceThreshold);
        eyeCaster.SetOVREyeGaze(oculusEyeTracking);
    }

    public void UpdateEyeCaster() { 
        
    }

    /// <summary>
    /// Checks if the defined field is set in the editor.
    /// </summary>
    /// <param name="error">the type of error like "type of object"</param>
    /// <param name="fieldToCheck">The field to check</param>
    private void CheckField(string error, object fieldToCheck) {
        if (fieldToCheck == null) {
            Debug.Log("<color=red>Error:</color>" + error + " must be set.", gameObject);
        }
    }


}

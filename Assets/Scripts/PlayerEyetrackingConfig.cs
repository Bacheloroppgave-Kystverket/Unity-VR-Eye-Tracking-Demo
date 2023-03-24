using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PlayerEyetrackingConfig : MonoBehaviour
{

    [SerializeField, Tooltip("The eyes of the player. Must contain one or two child objects with OVReyeGaze")]
    private EyeCaster eyeCaster;

    [SerializeField, Tooltip("Set to true if the oculus eye tracking should be enabled.")]
    private bool oculusEyeTracking;

    [SerializeField, Range(0,1), Tooltip("The threshold that the eyetracker should have to give out data.")]
    private float confidenceThreshold;

    public void UpdatePlayer() {
        eyeCaster.SetOVREyeGaze(oculusEyeTracking);
        eyeCaster.SetConfidenceThreshold(confidenceThreshold);
    }


    
}

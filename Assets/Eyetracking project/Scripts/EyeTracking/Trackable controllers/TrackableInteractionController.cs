using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Represents a component that has actions with the 
/// </summary>
public class TrackableInteractionController : MonoBehaviour, Trackable {

    [SerializeField, Tooltip("Events that are triggered when the gaze enters the collider")]
    private UnityEvent onGazeEnter;

    [SerializeField, Tooltip("Events that are triggered when the gaze is exited")]
    private UnityEvent onGazeExit;

    ///<inheritdoc/>
    public void OnGazeEnter() {
        this.onGazeEnter.Invoke();
    }

    ///<inheritdoc/>
    public void OnGazeExit() {
        this.onGazeExit.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Represents a hold object task.
/// </summary>
[RequireComponent(typeof(XRGrabInteractable))]
public class HoldObjectTaskController : TimedTaskController{

    
    private void Start()
    {
        XRGrabInteractable xRGrabInteractable = GetComponent<XRGrabInteractable>();
        xRGrabInteractable.selectEntered.AddListener((pog) => Grabbed());
        xRGrabInteractable.selectExited.AddListener((pog) => Dropped());
    }

    /// <summary>
    /// Gets run when the object is grabbed.
    /// </summary>
    public void Grabbed(){
        if (!GetTask().IsComplete()) {
            if (TaskManager.GetTaskManager().GetCurrentTask() == GetTask().GetTaskOrder() && GetTask().IsForceTaskOrder())
            {
                StartCoroutine(StartTimer());
            }
            else if (!GetTask().IsForceTaskOrder())
            {
                StartCoroutine(StartTimer());
            }
        }
    }

    /// <summary>
    /// Gets run when the object is dropped.
    /// </summary>
    public void Dropped() {
        StopTimer();
    }
}

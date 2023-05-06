using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

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
        StartCoroutine(StartTimer());
    }

    /// <summary>
    /// Gets run when the object is dropped.
    /// </summary>
    public void Dropped() {
        StopTimer();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class TouchObjectTaskController : TaskController
{
    [SerializeField, Tooltip("The touch task.")]
    private SimpleTask task;

    private void Start()
    {
        XRGrabInteractable xRGrabInteractable = GetComponent<XRGrabInteractable>();
        xRGrabInteractable.selectEntered.AddListener((pog) => Grabbed());
    }

    public void Grabbed() {
        if (!task.IsComplete()) {
            task.SetDone();
            CompleteTask();
        }
    }

    protected override Task GetTask()
    {
        return task;
    }
}

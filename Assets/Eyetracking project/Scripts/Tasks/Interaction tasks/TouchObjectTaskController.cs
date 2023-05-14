using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Represents a task that only needs a touch to be completed.
/// </summary>
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

    /// <summary>
    /// Sets the task of this controller.
    /// </summary>
    /// <param name="newTask">the new task</param>
    public void SetTask(SimpleTask newTask) {
        CheckIfObjectIsNull(newTask, "new task");
        this.task = newTask;
    }

    /// <summary>
    /// Finishes this task if its not completed.
    /// </summary>
    public void Grabbed() {
        if (!task.IsComplete()) {
            if (task.IsForceTaskOrder() && TaskManager.GetTaskManager().GetCurrentTask() == task.GetTaskOrder())
            {
                task.SetDone();
                CompleteTask();
            }
            else if (!GetTask().IsForceTaskOrder()) {
                task.SetDone();
                CompleteTask();
            }
        }
    }

    ///<inheritdoc/>
    public override Task GetTask()
    {
        return task;
    }

    /// <summary>
    /// Checks if the object is null or not. Throws an exception if the object is null.
    /// </summary>
    /// <param name="objecToCheck">the object to check</param>
    /// <param name="error">the error to be in the string.</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the object to check is null.</exception>
    private void CheckIfObjectIsNull(object objecToCheck, string error)
    {
        if (objecToCheck == null)
        {
            throw new IllegalArgumentException("The " + error + " cannot be null.");
        }
    }
}

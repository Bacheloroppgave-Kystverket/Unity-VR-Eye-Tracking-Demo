using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the general structure of a task.
/// </summary>
public abstract class Task
{
    [SerializeField, Tooltip("The title of the task")]
    private string taskTitle;

    [SerializeField, Tooltip("The tasks order")]
    private int taskOrder;

    [SerializeField, Tooltip("True if the taskorder is forced. False otherwise")]
    private bool forceTaskOrder;

    /// <summary>
    /// Gets if the task is complete.
    /// </summary>
    /// <returns></returns>
    public abstract bool IsComplete();

    /// <summary>
    /// Resets the task to its origianl state.
    /// </summary>
    public abstract void ResetTask();

    /// <summary>
    /// Gets the task title.
    /// </summary>
    /// <returns>the task title</returns>
    public string GetTaskTitle() => forceTaskOrder ? taskOrder + " " + taskTitle : taskTitle;

    /// <summary>
    /// Gets the task order.
    /// </summary>
    /// <returns>the order</returns>
    public int GetTaskOrder() => taskOrder;

    /// <summary>
    /// Gets if the task order is forced.
    /// </summary>
    /// <returns>true if the task order is forced</returns>
    public bool IsForceTaskOrder() => forceTaskOrder;

    /// <summary>
    /// Sets the task order.
    /// </summary>
    /// <param name="order">the order of the task.</param>
    /// <param name="forcedOrder">true if the order is forced. False otherwise</param>
    public void SetTaskOrderAndForcedOrder(int order, bool forcedOrder) { 
        this.forceTaskOrder = forcedOrder;
        this.taskOrder = order;
    }
}

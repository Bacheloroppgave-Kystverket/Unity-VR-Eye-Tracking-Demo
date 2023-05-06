using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task
{
    [SerializeField, Tooltip("The title of the task")]
    private string taskTitle;

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
    public string GetTaskTitle() => taskTitle;
}

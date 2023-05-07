using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookGazeController : TaskController, Trackable
{
    [SerializeField, Tooltip("The simple task")]
    private SimpleTask simpleTask;

    ///<inheritdoc/>
    public void OnGazeEnter()
    {
        if (!simpleTask.IsComplete())
        {
            if (simpleTask.IsForceTaskOrder() && TaskManager.GetTaskManager().GetCurrentTask() == simpleTask.GetTaskOrder())
            {
                simpleTask.SetDone();
                CompleteTask();
            }
            else if (!GetTask().IsForceTaskOrder())
            {
                simpleTask.SetDone();
                CompleteTask();
            }
        }
        
    }

    ///<inheritdoc/>
    public void OnGazeExit()
    {

    }

    /// <summary>
    /// Sets the simple task.
    /// </summary>
    /// <param name="task">the task</param>
    public void SetSimpleTask(SimpleTask task)
    {
        CheckIfObjectIsNull(task, "task");
        this.simpleTask = task;
    }

    ///<inheritdoc/>
    public override Task GetTask()
    {
        return simpleTask;
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

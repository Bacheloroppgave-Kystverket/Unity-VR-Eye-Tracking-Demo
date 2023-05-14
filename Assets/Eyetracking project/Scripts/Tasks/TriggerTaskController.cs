using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A task that is done when the action is tiggered.
/// </summary>
public class TriggerTaskController : TaskController
{
    [SerializeField, Tooltip("The task")]
    private SimpleTask simpleTask;

    /// <summary>
    /// Does the task so that the conditions are done.
    /// </summary>
    public void DoTask() {
        if (!simpleTask.IsComplete()) {
            if (simpleTask.IsForceTaskOrder() && TaskManager.GetTaskManager().GetCurrentTask() == simpleTask.GetTaskOrder() || !simpleTask.IsForceTaskOrder())
            {
                simpleTask.SetDone();
                CompleteTask();
            }
            
        }
    }

    ///<inheritdoc/>
    public override Task GetTask()
    {
        return simpleTask;
    }

    
}
